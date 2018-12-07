using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;
using System.Text;
namespace Day03
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] rawInput = File.ReadAllLines(@"C:\_Projects\Training\AdventOfCode\2018\Day03\input.txt");

            var fabric = new Dictionary<(int X, int Y), int>();

            foreach(string line in rawInput)
            {
                new Claim(line).ApplyToFabric(fabric);
            }

            Console.WriteLine(fabric.Count(cell => cell.Value > 1));
        }
    }
}
