using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;

namespace Day01
{
    class Program
    {
        static void Main(string[] args)
        {
            //Part 1

            Debug.Assert(CalculateFrequency(new int[] { 1, 1, 1 }) == 3);
            Debug.Assert(CalculateFrequency(new int[] { 1, 1, -2 }) == 0);
            Debug.Assert(CalculateFrequency(new int[] { -1, -2, -3 }) == -6);

            string input = "C:/_Projects/Training/AdventOfCode/2018/Day01/input.txt";
            string[] myList = File.ReadAllLines(input);

            var myListAsInts = myList.Select(x => int.Parse(x));

            var myListAsInts2 = myListAsInts.ToList();

            Console.WriteLine(CalculateFrequency(myListAsInts));

            //Part 2

            Debug.Assert(CalculateFrequencyPart2(new int[] { 1, -1 }) == 0);
            Debug.Assert(CalculateFrequencyPart2(new int[] { 3, 3, 4, -2, -4 }) == 10);
            Debug.Assert(CalculateFrequencyPart2(new int[] { -6, 3, 8, 5, -6 }) == 5);
            Debug.Assert(CalculateFrequencyPart2(new int[] { 7, 7, -2, -7, -4 }) == 14);



            var stopwatch = new Stopwatch();

            stopwatch.Start();
            var answer = CalculateFrequencyPart2(myListAsInts2);
            stopwatch.Stop();

            Console.WriteLine("Day01 Part 2 answer: " + answer);

            Console.WriteLine($"Time taken: {stopwatch.Elapsed}");
        }

        static int CalculateFrequency(IEnumerable<int> values)
        {
            return values.Sum();
        }

        static int? CalculateFrequencyPart2(IList<int> values)
        {
            var runningFrequency = new List<int>() {0};
            bool valueReachedTwice = false;

            int i = 0;
            int currentSum = 0;
            int listCount = values.Count();

            while (!valueReachedTwice)
            {
                currentSum += values[(i % listCount)];

                if (runningFrequency.Contains(currentSum))
                {
                    valueReachedTwice = true;
                    return currentSum;
                }

                runningFrequency.Add(currentSum);
                i++;
            }

            return null;
        }
    }
}
