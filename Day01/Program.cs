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
            Debug.Assert(CalculateFrequency(new int[] { 1, 1, 1 }) == 3);
            Debug.Assert(CalculateFrequency(new int[] { 1, 1, -2 }) == 0);
            Debug.Assert(CalculateFrequency(new int[] { -1, -2, -3}) == -6);

            string input = "C:/_Projects/Training/AdventOfCode/2018/AoC_2018/Day01/input.txt";
            string[] myList = File.ReadAllLines(input);

            var myListAsInts = myList.Select(x => int.Parse(x));
            
            Console.WriteLine(CalculateFrequency(myListAsInts));

        }

        static int CalculateFrequency(IEnumerable<int> values)
        {
            return values.Sum();
        }
    }
}
