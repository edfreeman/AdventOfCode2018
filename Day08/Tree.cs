using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Day08
{
    public static class Tree
    {
        public List<Node> Create(List<int> input)
        {
            var tree = new List<Node>();
            var remainingSubtrees = new Dictionary<int, int>();
            int endIndex = input.Count - input[1];

            bool endOfTree = false;
            int positionIndex = 0;
            int idIndex = 1;
            var parentIds = new List<int>();

            while (!endOfTree)
            {
                if (idIndex == 1)
                {
                    tree.Add
                        (
                            new Node
                            {
                                Id = idIndex,
                                ParentId = 0,
                                NumberOfSubtrees = input[positionIndex],
                                NumberOfMetadataEntries = input[positionIndex + 1],
                                MetadataEntries = input.TakeLast(input[1]).ToList()
                            }
                        );

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

                    tree.Add
                        (
                            new Node
                            {
                                Id = idIndex,
                                ParentId = remainingSubtrees.Last().Key,
                                NumberOfSubtrees = input[positionIndex],
                                NumberOfMetadataEntries = input[positionIndex + 1],
                                MetadataEntries = metadata
                            }
                        );

                    remainingSubtrees.Add(idIndex, input[positionIndex]);
                    parentIds.Add(idIndex);
                    positionIndex += 2;
                    idIndex++;
                }
                else
                {
                    var metadata = new List<int>();

                    for (int metadataCounter = 0; metadataCounter < input[positionIndex + 1]; metadataCounter++)
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
    }
}
