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
            var actualInput = File.ReadAllText(@"C:\_Projects\Training\AdventOfCode2018\Day08\input.txt");
            var actualInputParsed = ParseInput(actualInput);

            //Part 1

            //var input1Tree = CreateTree(testInput1);
            var input2Tree = CreateTree(testInput2);
            var actualInputTree = CreateTree(actualInputParsed);

            //Debug.Assert(CalculateMetadataSum(input1Tree) == 37);
            Debug.Assert(CalculateMetadataSum(input2Tree) == 138);

            Console.WriteLine(CalculateMetadataSum(actualInputTree));

        }

        static int CalculateMetadataSum(List<(int id, int parentId, int numberOfSubtrees, int numberOfMetadataEntries, List<int> metadataEntries)> tree)
        {
            int sum = tree.SelectMany(o => o.metadataEntries).Sum();
            
            return sum;
        }

        static List<(int id, int parentId, int numberOfSubtrees, int numberOfMetadataEntries, List<int> metadataEntries)> CreateTree(List<int> input)
        {
            var tree = new List<(int id, int parentId, int numberOfSubtrees, int numberOfMetadataEntries, List<int> metadataEntries)>();
            var remainingSubtrees = new Dictionary<int,int>();
            int endIndex = input.Count - input[1];

            bool endOfTree = false;
            int positionIndex = 0;
            int idIndex = 1;
            var parentIds = new List<int>();

            while (!endOfTree)
            {
                if (idIndex == 1)
                {
                    tree.Add((idIndex, 0, input[positionIndex], input[positionIndex + 1], input.TakeLast(input[1]).ToList()));
                    remainingSubtrees.Add(idIndex, input[0]);
                    parentIds.Add(1);
                    positionIndex = 2;
                    idIndex++;
                }
                //{ 1, 3, 0, 3, 10, 11, 12, 1, 1, 2 }
                //{ 2, 3, 0, 3, 10, 11, 12, 1, 1, 0, 1, 99, 2, 1, 1, 2 }
                if (input[positionIndex] != 0)
                {
                    var metadata = new List<int>();
                    tree.Add((idIndex, remainingSubtrees.Last().Key, input[positionIndex], input[positionIndex + 1], metadata));
                    
                    remainingSubtrees.Add(idIndex, input[positionIndex]);
                    parentIds.Add(idIndex);
                    positionIndex += 2;
                    idIndex++;
                }
                else
                {
                    var metadata = new List<int>();

                    for (int metadataCounter = 0; metadataCounter < input[positionIndex+1]; metadataCounter++)
                    {
                        metadata.Add(input[positionIndex + metadataCounter + 2]);
                    }

                    tree.Add((idIndex, remainingSubtrees.Last().Key, input[positionIndex], input[positionIndex + 1], metadata));
                    positionIndex += input[positionIndex + 1] + 2;
                    idIndex++;
                    int parent = remainingSubtrees.Last().Key;
                    remainingSubtrees[parent]--;

                    //{ 1, 3, 0, 3, 10, 11, 12, 1, 1, 2 }
                    //{ 2, 3, 0, 3, 10, 11, 12, 1, 1, 0, 1, 99, 2, 1, 1, 2 }

                    while (remainingSubtrees[parent] == 0)
                    {
                        remainingSubtrees.Remove(parent);

                        var treeForParent = tree.First(node => node.id == parent);

                        treeForParent.metadataEntries.AddRange(input.GetRange(positionIndex, treeForParent.numberOfMetadataEntries));

                        positionIndex += treeForParent.numberOfMetadataEntries;

                        if (remainingSubtrees.Count == 0 || positionIndex == endIndex)
                        {
                            endOfTree = true;
                            break;
                        }
                        else
                        {
                            remainingSubtrees[treeForParent.parentId]--;

                            parent = treeForParent.parentId;
                        }                        
                    }
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
