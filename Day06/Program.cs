using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;

namespace Day06
{
    class Program
    {
        static void Main(string[] args)
        {
            var coords = ParseInput(@"C:\_Projects\Training\AdventOfCode\2018\Day06\input.txt");
            
            Console.WriteLine(FindLargestArea(coords));
            Console.WriteLine(FindSizeOfRegion(coords,10000));
        }

        static List<(int X, int Y)> ParseInput(string filePath)
        {
            var coords = new List<(int X, int Y)>();
            string[] input = File.ReadAllLines(filePath);

            foreach (var line in input)
            {
                var parts = line.Split(", ");
                coords.Add((int.Parse(parts[0]), int.Parse(parts[1])));
            }

            return coords;
        }

        static int FindSizeOfRegion(List<(int X, int Y)> coords, int distance)
        {
            int sizeOfRegion = 0;

            List<(int X, int Y)> unboundedCoords = FindUnbounded(coords);
            var dictionary = new Dictionary<(int X, int Y), int>();

            foreach (var coord in coords)
            {
                dictionary.Add(coord, 0);
            }

            int minX = unboundedCoords.Min(coord => coord.X);
            int maxX = unboundedCoords.Max(coord => coord.X);
            int minY = unboundedCoords.Min(coord => coord.Y);
            int maxY = unboundedCoords.Max(coord => coord.Y);

            IEnumerable<(int X, int Y)> spanningCoords =
                from i in Enumerable.Range(minX, maxX)
                from j in Enumerable.Range(minY, maxY)
                select (X: i, Y: j);

            foreach (var spanningCoord in spanningCoords)
            {
                var distancesFromSpanningCoord = new Dictionary<(int X, int Y), int>();

                foreach (var coord in coords)
                {
                    distancesFromSpanningCoord.Add(coord, Math.Abs(coord.X - spanningCoord.X) + Math.Abs(coord.Y - spanningCoord.Y));
                }

                if (distancesFromSpanningCoord.Values.Sum() < distance) { sizeOfRegion++; };
            }

            return sizeOfRegion;
        }

        static int FindLargestArea(List<(int X, int Y)> coords)
        {
            int largestArea = 0;
            List<(int X, int Y)> unboundedCoords = FindUnbounded(coords);
            var dictionary = new Dictionary<(int X, int Y), int>();

            foreach(var coord in coords)
            {
                dictionary.Add(coord, 0);
            }
            
            int minX = unboundedCoords.Min(coord => coord.X);
            int maxX = unboundedCoords.Max(coord => coord.X);
            int minY = unboundedCoords.Min(coord => coord.Y);
            int maxY = unboundedCoords.Max(coord => coord.Y);

            IEnumerable<(int X, int Y)> spanningCoords =
                from i in Enumerable.Range(minX, maxX)
                from j in Enumerable.Range(minY, maxY)
                select (X: i, Y: j);
            
            foreach(var spanningCoord in spanningCoords)
            {
                var distancesFromSpanningCoord = new Dictionary<(int X, int Y), int>();

                foreach(var coord in coords)
                {
                    distancesFromSpanningCoord.Add(coord, Math.Abs(coord.X - spanningCoord.X) + Math.Abs(coord.Y - spanningCoord.Y));
                }

                if(distancesFromSpanningCoord.MinBy(distance => distance.Value).Count() == 1)
                {
                    var key = distancesFromSpanningCoord.MinBy(distance => distance.Value).First().Key;

                    dictionary[key]++;
                }
            }

            largestArea = dictionary.Where(entry1 => !unboundedCoords.Contains(entry1.Key)).Max(entry2 => entry2.Value);

            return largestArea;
        }

        static List<(int X, int Y)> FindUnbounded(List<(int X, int Y)> coords)
        {
            var unboundedCoords = new List<(int X, int Y)>();

            foreach(var coord1 in coords)
            {
                bool isBoundedAbove = false;
                bool isBoundedBelow = false;
                bool isBoundedToRight = false;
                bool isBoundedToLeft = false;

                foreach (var coord2 in coords)
                {
                    if (coord2.Equals(coord1)) { continue; }

                    if (!isBoundedAbove)
                    {
                        if (CheckUp(coord1, coord2))
                        {
                            isBoundedAbove = true;
                        }
                    }
                    if (!isBoundedBelow)
                    {
                        if (CheckDown(coord1, coord2))
                        {
                            isBoundedBelow = true;
                        }
                    }                    
                    if (!isBoundedToRight)
                    {
                        if (CheckRight(coord1, coord2))
                        {
                            isBoundedToRight = true;
                        }
                    }
                    if (!isBoundedToLeft)
                    {
                        if (CheckLeft(coord1, coord2))
                        {
                            isBoundedToLeft = true;
                        }
                    }                                    
                }

                if (isBoundedAbove && isBoundedBelow && isBoundedToRight && isBoundedToLeft)
                {
                    continue;
                }
                else
                {
                    unboundedCoords.Add(coord1);
                }
            }

            return unboundedCoords;
        }

        static bool CheckUp((int X, int Y) primaryCoord, (int X, int Y) comparisonCoord)
        {
            bool primaryCoordBounded = false;

            int positiveGradientIntercept = primaryCoord.Y - primaryCoord.X;
            int negativeGradientIntercept = primaryCoord.Y + primaryCoord.X;

            if ((comparisonCoord.Y - comparisonCoord.X - positiveGradientIntercept >= 0) && (comparisonCoord.Y + comparisonCoord.X - negativeGradientIntercept >= 0))
            {
                primaryCoordBounded = true;
            }

            return primaryCoordBounded;
        }
        static bool CheckDown((int X, int Y) primaryCoord, (int X, int Y) comparisonCoord)
        {
            bool primaryCoordBounded = false;

            int positiveGradientIntercept = primaryCoord.Y - primaryCoord.X;
            int negativeGradientIntercept = primaryCoord.Y + primaryCoord.X;

            if ((comparisonCoord.Y - comparisonCoord.X - positiveGradientIntercept <= 0) && (comparisonCoord.Y + comparisonCoord.X - negativeGradientIntercept <= 0))
            {
                primaryCoordBounded = true;
            }

            return primaryCoordBounded;
        }
        static bool CheckRight((int X, int Y) primaryCoord, (int X, int Y) comparisonCoord)
        {
            bool primaryCoordBounded = false;

            int positiveGradientIntercept = primaryCoord.Y - primaryCoord.X;
            int negativeGradientIntercept = primaryCoord.Y + primaryCoord.X;

            if ((comparisonCoord.Y - comparisonCoord.X - positiveGradientIntercept <= 0) && (comparisonCoord.Y + comparisonCoord.X - negativeGradientIntercept >= 0))
            {
                primaryCoordBounded = true;
            }

            return primaryCoordBounded;
        }
        static bool CheckLeft((int X, int Y) primaryCoord, (int X, int Y) comparisonCoord)
        {
            bool primaryCoordBounded = false;

            int positiveGradientIntercept = primaryCoord.Y - primaryCoord.X;
            int negativeGradientIntercept = primaryCoord.Y + primaryCoord.X;

            if ((comparisonCoord.Y - comparisonCoord.X - positiveGradientIntercept >= 0) && (comparisonCoord.Y + comparisonCoord.X - negativeGradientIntercept <= 0))
            {
                primaryCoordBounded = true;
            }

            return primaryCoordBounded;
        }
    }
}
