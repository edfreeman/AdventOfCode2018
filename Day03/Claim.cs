using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;

namespace Day03
{
    public class Claim
    {
        int Id { get; set; }
        int X { get; set; }
        int Y { get; set; }
        int Width { get; set; }
        int Height { get; set; }
        //public IEnumerable<(int X, int Y)> Coords => Enumerable
        //        .Range(0, Width)
        //        .SelectMany(i => Enumerable
        //            .Range(0, Height)
        //            .Select(j => (X: i + X, Y: j + Y)));
        public IEnumerable<(int X, int Y)> Coords => 
                from i in Enumerable.Range(0, Width)
                from j in Enumerable.Range(0, Height)
                select (X: i + X, Y: j + Y);                //using LINQ query syntax.

        public Claim(string claim)
        {
            var claimParts = claim.Replace("#", String.Empty).Replace(" ", String.Empty).Split(new[] { '@', ',', ':', 'x' });

            Id =        int.Parse(claimParts[0]);
            X =         int.Parse(claimParts[1]);
            Y =         int.Parse(claimParts[2]);
            Width =     int.Parse(claimParts[3]);
            Height =    int.Parse(claimParts[4]);
        }        

        public void ApplyToFabric(Dictionary<(int X, int Y), (int ct, List<int> ids)> fabric)
        {            
            foreach (var point in Coords)
            {
                if (fabric.TryGetValue(point, out (int ct, List<int> ids) claimsCoveringPoint))
                {
                    claimsCoveringPoint.ct++;
                    claimsCoveringPoint.ids.Add(Id);
                    fabric[point] = claimsCoveringPoint;
                    //fabric[point] = (claimsCoveringPoint.count + 1, claimsCoveringPoint.ids);//claimsCoveringPoint.count + 1;
                }
                else
                {
                    fabric.Add(point, (1, new List<int>() { Id }));
                }
            }
        }

        //public IImmutableDictionary<(int X, int Y), int> ApplyToFabric(IImmutableDictionary<(int X, int Y), int> fabric)
        //{
        //    var points = Enumerable.Range(0, RectangleSize.Width)
        //        .SelectMany(x => Enumerable.Range(0, RectangleSize.Height).Select(y => (X: x + RectangleOriginCoordinates.X, Y: y + RectangleOriginCoordinates.Y)));

        //    return points.Aggregate(
        //        fabric,
        //        (f, point) => f.TryGetValue(point, out int currentClaimCount) ? f.SetItem(point, currentClaimCount + 1) : f.Add(point, 1)
        //        );
        //}
    }
}
