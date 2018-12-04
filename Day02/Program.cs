using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Text;

namespace Day02
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] testInput = { "abcdef", "bababc", "abbcde", "abcccd", "aabcdd", "abcdee", "ababab" };
            Debug.Assert(CalculateChecksumPart1(testInput) == 12);

            string input = "C:/_Projects/Training/AdventOfCode/2018/Day02/input.txt";
            string[] myList = File.ReadAllLines(input);

            var myarray = new char[2];

            foreach (var item in myarray)
            {
                Console.WriteLine(item == '\0');
            }
            Console.WriteLine(CalculateChecksumPart1(myList));
        }

        static int CalculateChecksumPart1(string[] input)
        {
            int countOfIdsWithSameLetterAppearingExactlyTwice = 0;
            int countOfIdsWithSameLetterAppearingExactlyThrice = 0;

            foreach (var id in input)
            {
                var letters = id.ToCharArray();
                var distinctLetters = letters.Distinct();
                var dictionary = new Dictionary<char, int>();
                foreach(var distinctLetter in distinctLetters)
                {
                    dictionary.Add(distinctLetter, letters.Count(o => o.Equals(distinctLetter)));
                }
                if (dictionary.ContainsValue(2))
                {
                    countOfIdsWithSameLetterAppearingExactlyTwice++;
                }
                if (dictionary.ContainsValue(3))
                {
                    countOfIdsWithSameLetterAppearingExactlyThrice++;
                }
            }

            return countOfIdsWithSameLetterAppearingExactlyTwice * countOfIdsWithSameLetterAppearingExactlyThrice;
        }

        static string FindCommonLetters(string[] input)
        {
            int j = 0;

            int inputLength = input.Length;
            char[] commonLetters = new char[input.Length - 1];

            foreach(var id1 in input)
            {
                foreach(var id2 in input)
                {                    
                    if(id2 == id1) { continue; }

                    int mismatches = 0;

                    for (int i = 0; i < id1.Length; i++)
                    {
                        if (mismatches > 1)
                        {
                            break;
                        }
                        if (id2[i] != id1[i])
                        {
                            mismatches++;
                            continue;
                        }
                        else
                        {
                            commonLetters[j] = id2[i];
                            j++;
                        }
                    }
                }
            }




            return "";
        }
    }
}
