using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;

namespace Day04
{
    class GuardStats
    {
        int Id { get; set; }
        int MinutesAsleep { get; set; }
        SortedDictionary<DateTime, string> Records { get; set; }

        public GuardStats(KeyValuePair<int, SortedDictionary<DateTime, string>> guardRecords)
        {
            Id = guardRecords.Key;
            Records = guardRecords.Value;
        }

        public int CalculateMinutesAsleep()
        {
            return Records.Count;
        }
    }
}
