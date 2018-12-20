using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Text;

namespace Day07
{
    class Program
    {
        static void Main(string[] args)
        {
            var testInput = File.ReadAllLines(@"C:\_Projects\Training\AdventOfCode\2018\Day07\testInput.txt");
            var actualInput = File.ReadAllLines(@"C:\_Projects\Training\AdventOfCode\2018\Day07\input.txt");
            
            //Part 1
            Debug.Assert(CalculateOrder(testInput) == "CABDFE");

            Console.WriteLine(CalculateOrder(actualInput));

            ////Part 2
            Debug.Assert(TimeToComplete(testInput, 2, 0) == 15);

            Console.WriteLine(TimeToComplete(actualInput, 5, 60));

            char x = (char)90;
            Console.WriteLine(x);

        }

        static int TimeToComplete(string[] input, int numberOfWorkers, int stepDurationOffset)
        {
            var workers = new List<Dictionary<int, char?>>();
            workers.Capacity = numberOfWorkers;

            var stepDurations = new Dictionary<char, int>();

            for (int i = 0; i < 26; i++)
            {
                stepDurations.Add((char)(65 + i), i + 61);
            }

            List<Queue<char>> myList = ParseInput(input);
            var availableSteps = new SortedDictionary<char, int>();

            var firstLetters = myList.Select(o => o.First()).ToHashSet();
            var lastLetters = myList.Select(o => o.Last()).ToHashSet();
            var numberOfDistinct = firstLetters.Union(lastLetters).Count();
            var firstStepCandidates = firstLetters.Where(letter => !lastLetters.Contains(letter)).ToList();
            var completedSteps = new List<char>();

            foreach (var step in firstStepCandidates)
            {
                availableSteps.Add(step, stepDurations[step]);
            }

            int ticker = 0;

            while (completedSteps.Count != numberOfDistinct)
            {
                foreach (var step in availableSteps)
                {
                    if (workers.Count(worker => !worker.ContainsKey(ticker)) > 0 && step.Value != 0)
                    {
                        workers.Where(worker => !worker.ContainsKey(ticker)).First().Add(ticker, step.Key);
                        availableSteps[step.Key]--;
                    }
                }

                foreach (var step in availableSteps)
                {
                    if (step.Value == 0)
                    {
                        completedSteps.Add(step.Key);
                        availableSteps.Remove(step.Key);
                    }
                }

                ticker++;
            }

            return ticker;
        }

        static string CalculateOrder(string[] input)
        {
            List<Queue<char>> myList = ParseInput(input);
            var availableSteps = new SortedSet<char>();
            var orderedSteps = new List<char>();
            
            var firstLetters = myList.Select(o => o.First()).ToHashSet();
            var lastLetters = myList.Select(o => o.Last()).ToHashSet();
            var numberOfDistinct = firstLetters.Union(lastLetters).Count();

            var firstStepCandidates = firstLetters.Where(letter => !lastLetters.Contains(letter)).ToList();
            char lastStep = lastLetters.Where(letter => !firstLetters.Contains(letter)).First();

            foreach (var step in firstStepCandidates)
            {
                availableSteps.Add(step);
            }

            for (int i = 0; i < numberOfDistinct - 1; i++)
            {
                char currentStep = availableSteps.First();
                orderedSteps.Add(currentStep);
                availableSteps.Remove(currentStep);

                foreach(var queue in myList)
                {
                    if (queue.First() == currentStep && queue.Count == 2)
                    {
                        if (myList.Where(x => x.Count == 2).Where(o => o.Last() == queue.Last()).Count() == 1)
                        {
                            availableSteps.Add(queue.Last());
                        }

                        queue.Dequeue();
                    }
                }
            }

            orderedSteps.Add(lastStep);

            var sbuilder = new StringBuilder();

            string answer = sbuilder.Append(orderedSteps.ToArray()).ToString();

            return answer;
        }

        static List<Queue<char>> ParseInput(string[] input)
        {
            var inputPairs = new List<Queue<char>>();

            foreach(var line in input)
            {
                var queue = new Queue<char>();
                queue.Enqueue(line[5]);
                queue.Enqueue(line[36]);
                inputPairs.Add(queue);
            }            

            return inputPairs;
        }
    }
}
