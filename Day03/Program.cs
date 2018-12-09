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
            string[] testInput = { "#1 @ 1,3: 4x4", "#2 @ 3,1: 4x4", "#3 @ 5,5: 2x2" };

            string[] rawInput = File.ReadAllLines(@"C:\_Projects\Training\AdventOfCode\2018\Day03\input.txt");

            var fabric = new Dictionary<(int X, int Y), (int ct, List<int> ids)>();
            int? loneId = null;
            var claims = new List<Claim>();

            foreach(string line in rawInput)
            {
                var claim = new Claim(line);
                claim.ApplyToFabric(fabric);
                claims.Add(claim);

                if(claims.Count() == rawInput.Count())
                {
                    foreach(var clm in claims)
                    {
                        int j = 0;

                        foreach (var coord in clm.Coords)
                        {
                            if (fabric[coord].ct != 1)
                            {
                                break;
                            }

                            j++;

                            if (j == clm.Coords.Length)
                            {
                                loneId = fabric[coord].ids.First();
                            }
                        }
                    }
                }                
            }
            Console.WriteLine("Answer to part 1: " + fabric.Count(cell => cell.Value.ct > 1));
            Console.WriteLine("Answer to part 2: " + loneId);
        }
    }
}
