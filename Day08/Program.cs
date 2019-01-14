using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;

namespace Day08
{
    class Program
    {
        static void Main(string[] args)
        {
            var testInput1 = new List<int> { 1, 3, 0, 3, 10, 11, 12, 1, 1, 2 };
            var testInput2 = new List<int> { 2, 3, 0, 3, 10, 11, 12, 1, 1, 0, 1, 99, 2, 1, 1, 2 };
            var actualInput = File.ReadAllText(@"C:\_Projects\Training\AdventOfCode\2018\Day08\input.txt");
            var actualInputParsed = ParseInput(actualInput);


            //Part 1
            Debug.Assert(CalculateMetadataSum(testInput1) == 37);
            Debug.Assert(CalculateMetadataSum(testInput2) == 138);

            Console.WriteLine(CalculateMetadataSum(actualInputParsed));

        }

        static int CalculateMetadataSum(List<int> input)
        {
            var listOfPairs = new List<(int numberOfSubtrees, int numberOfMetadataEntries)>();

            bool remainingNodes = true;
            int sum = 0;
            int i = 0;
            //{ 2, 3, 0, 3, 10, 11, 12, 1, 1, 0, 1, 99, 2, 1, 1, 2 }

            while (remainingNodes)
            {
                if (input[i] != 0)
                {
                    listOfPairs.Add((input[i], input[i + 1]));
                    i += 2;
                    continue;
                }

                else
                {
                    var (numberOfSubtrees, numberOfMetadataEntries) = listOfPairs[listOfPairs.Count - 1];
                    listOfPairs[listOfPairs.Count - 1] = (numberOfSubtrees - 1, numberOfMetadataEntries); //why doesn't numberOfSubtrees-- work?

                    int step = input[i + 1];
                    i += 2;

                    for (int j = i; j < i + step; j++)
                    {
                        sum += input[j];
                    }


                    i += step;
                }

                //{ 1, 3, 0, 3, 10, 11, 12, 1, 1, 2 }
                while (listOfPairs.Last().numberOfSubtrees == 0)
                {
                    int x = listOfPairs.Last().numberOfMetadataEntries;
                    for (int k = i; k < i + x; k++)
                    {
                        sum += input[k];
                    }
                    listOfPairs.Remove(listOfPairs.Last());

                    if (listOfPairs.Count > 0)
                    {
                        var (numberOfSubtrees, numberOfMetadataEntries) = listOfPairs[listOfPairs.Count - 1];
                        listOfPairs[listOfPairs.Count - 1] = (numberOfSubtrees - 1, numberOfMetadataEntries);

                        i += x;
                    }
                    else
                    {
                        remainingNodes = false;
                        break;
                    }
                }                
            }
            
            return sum;
        }

        static List<(int id, int parentId, int numberOfSubtrees, int numberOfMetadataEntries,List<int> metadataEntries)> CreateTree(List<int> input)
        {
            var tree = new List<(int id, int parentId, int numberOfSubtrees, int numberOfMetadataEntries, List<int> metadataEntries)>();
            var remainingSubtrees = new List<int>();

            bool endOfTree = false;
            int i = 0;
            int j = 0;
            var parentIds = new List<int>() { { -1 } };

            while (!endOfTree)
            {
                //{ 2, 3, 0, 3, 10, 11, 12, 1, 1, 0, 1, 99, 2, 1, 1, 2 }
                if (input[i] != 0)
                {
                    var metadata = new List<int>();
                    tree.Add((j, parentIds.Last(), input[i], input[i + 1], metadata));
                    if(remainingSubtrees.Count > 0)
                    {
                        remainingSubtrees[remainingSubtrees.Count - 1]--;// = remainingSubtrees[remainingSubtrees.Count - 1] - 1;
                    }
                    remainingSubtrees.Add(input[i]);
                    parentIds.Add(j);
                    i += 2;
                    j++;
                }
                else
                {
                    var metadata = new List<int>();

                    for (int k = 0; k < input[i+1]; k++)
                    {
                        metadata.Add(i + k + 2);
                    }

                    tree.Add((j, parentIds.Last(), input[i], input[i + 1], metadata));
                    parentIds.Add(j);
                    i += input[i+1] + 2;
                    j++;

                    if 
                }
            }

            return tree;
        }

        static List<int> ParseInput(string input)
        {
            string[] parts = input.Split(' ');
            var output = new List<int>();

            foreach(var part in parts)
            {
                output.Add(int.Parse(part));
            }

            return output;
        }
    }
}
