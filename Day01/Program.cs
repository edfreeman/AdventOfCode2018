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

            Console.WriteLine(CalculateFrequency(myListAsInts));

            //Part 2

            Debug.Assert(CalculateFrequencyPart2(new int[] { 1, -1 }) == 0);
            Debug.Assert(CalculateFrequencyPart2(new int[] { 3, 3, 4, -2, -4 }) == 10);
            Debug.Assert(CalculateFrequencyPart2(new int[] { -6, 3, 8, 5, -6 }) == 5);
            Debug.Assert(CalculateFrequencyPart2(new int[] { 7, 7, -2, -7, -4 }) == 14);

            Console.WriteLine(CalculateFrequencyPart2(myListAsInts));
        }

        static int CalculateFrequency(IEnumerable<int> values)
        {
            return values.Sum();
        }

        static int? CalculateFrequencyPart2(IEnumerable<int> values)
        {
            var runningFrequency = new List<int>() {0};
            bool valueReachedTwice = false;

            int i = 0;
            int currentSum = 0;
            int listCount = values.Count();

            while (!valueReachedTwice)
            {
                currentSum += values.ElementAt(i % listCount);
                runningFrequency.Add(currentSum);

                if (runningFrequency.Count(x => x == currentSum) > 1)
                {
                    valueReachedTwice = true;
                    return runningFrequency.Last();
                }
                
                i++;
            }

            return null;
        }
    }
}
