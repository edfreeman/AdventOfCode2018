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

        static List<(int X, int Y)> FindUnbounded(List<(int X, int Y)> coords)
        {
            var unboundedCoords = new List<(int X, int Y)>();

            foreach(var coord1 in coords)
            {
                foreach(var coord2 in coords)
                {
                    if (coord2.Equals(coord1)) { continue; }

                    int currentXCoord = coord2.X
                }
            }





            return unboundedCoords;
        }

        static bool CheckUp((int X, int Y) primaryCoord, (int X, int Y) comparisonCoord)
        {
            bool primaryCoordBounded = false;



            return primaryCoordBounded;
        }
    }
}
