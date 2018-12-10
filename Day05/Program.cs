using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;

namespace Day05
{
    class Program
    {
        static void Main(string[] args)
        {
            Debug.Assert(CalculateRemainingUnits("aabAAB") == 6);
            Debug.Assert(CalculateRemainingUnits("aA") == 0);
            Debug.Assert(CalculateRemainingUnits("aAb") == 1);
            Debug.Assert(CalculateRemainingUnits("abBA") == 0);
            Debug.Assert(CalculateRemainingUnits("abAB") == 4);
            Debug.Assert(CalculateRemainingUnits("aabAAB") == 6);
            Debug.Assert(CalculateRemainingUnits("dabAcCaCBAcCcaDA") == 10);

            string myInput = File.ReadAllText(@"C:\_Projects\Training\AdventOfCode\2018\Day05\input.txt").Trim();

            Console.WriteLine(CalculateRemainingUnits(myInput));

            var myString = "aacccAAACCC";
            Console.WriteLine(myString.Replace("a", "", true, System.Globalization.CultureInfo.InvariantCulture));


        }

        static string ApplyReactions(string input)
        {
            bool noMoreReactions = false;
            string output = input;
            int i = 0;

            while (!noMoreReactions)
            {
                if (Math.Abs(output[i] - output[i + 1]) == 32)
                {
                    output = output.Remove(i, 2);
                    i = 0;
                }
                else
                {
                    i++;
                }
                if(i == output.Length - 1 || i == output.Length)
                {
                    noMoreReactions = true;
                }
            }

            return output;

        }

        static int CalculateRemainingUnits(string input)
        {
            var length = ApplyReactions(input).Length;

            return length;
        }

        //static int OptimalCharacterRemovalRemainingUnitsLength(string input)
        //{
        //    var distinctCharacters = input.ToUpperInvariant().Distinct().ToList();

        //    distinctCharacters.Remove();

        //    foreach(distinct)
        //}
    }
}
