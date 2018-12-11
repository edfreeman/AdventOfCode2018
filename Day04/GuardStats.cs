using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;

namespace Day04
{
    class GuardStats
    {
        public int Id { get; set; }
        SortedDictionary<DateTime, string> Records { get; set; }

        public GuardStats(KeyValuePair<int, SortedDictionary<DateTime, string>> guardRecords)
        {
            Id = guardRecords.Key;
            Records = guardRecords.Value;
        }

        public List<Dictionary<string, List<DateTime>>> GenerateShifts()
        {
            var shifts = new List<Dictionary<string, List<DateTime>>>();

            var recordsAsList = Records.ToList();

            for (int i = 0; i < Records.Count; i++)
            {
                var currentRecord = recordsAsList[i];

                if (currentRecord.Value.Contains('#'))
                {
                    shifts.Add(new Dictionary<string, List<DateTime>>() { { "Shift start", new List<DateTime>() { currentRecord.Key } } });
                }

                if (currentRecord.Value.Equals("falls asleep"))
                {
                    if(!shifts.Last().ContainsKey("Falls asleep"))
                    {
                        shifts.Last().Add("Falls asleep", new List<DateTime>() { currentRecord.Key });
                    }
                    else
                    {
                        shifts.Last()["Falls asleep"].Add(currentRecord.Key);
                    }
                }
                
                if (currentRecord.Value.Equals("wakes up"))
                {
                    if (!shifts.Last().ContainsKey("Wakes up"))
                    {
                        shifts.Last().Add("Wakes up", new List<DateTime>() { currentRecord.Key });
                    }
                    else
                    {
                        shifts.Last()["Wakes up"].Add(currentRecord.Key);
                    }
                }                
            }
            return shifts;
        }

        public int CalculateMinutesAsleep()
        {
            List<Dictionary<string, List<DateTime>>> shifts = GenerateShifts();

            var minutesAsleep = 0;

            if(!shifts.Any(shift => shift.Keys.Contains("Falls asleep")))
            {
                return 0;
            }

            foreach(var shift in shifts)
            {
                DateTime shiftStart = shift["Shift start"][0];
                int numberOfSnoozes = shift["Falls asleep"].Count;

                for (int i = 0; i < numberOfSnoozes; i++)
                {
                    TimeSpan snoozePeriod = shift["Wakes up"][i].Subtract(shift["Falls asleep"][i]);
                    
                    minutesAsleep += (int)snoozePeriod.TotalMinutes;
                }
            }

            return minutesAsleep;
        }

        public (int? minute, int? frequency) FindFavouriteMinute()
        {
            (int?, int?) favouriteMinuteAndFrequency = (null, null);
            List<Dictionary<string, List<DateTime>>> shifts = GenerateShifts();

            if (!shifts.Any(shift => shift.Keys.Contains("Falls asleep")))
            {
                return (null, null);
            }

            var minuteCounts = new Dictionary<int, int>();

            for (int i = 0; i < 60; i++)
            {
                minuteCounts.Add(i, 0);
            }

            foreach(var shift in shifts)
            {
                int numberOfSnoozes = shift["Falls asleep"].Count;

                for (int i = 0; i < numberOfSnoozes; i++)
                {
                    int startMinute = shift["Falls asleep"][i].Minute;
                    TimeSpan snoozePeriod = shift["Wakes up"][i].Subtract(shift["Falls asleep"][i]);
                    int lengthOfSnooze = (int)snoozePeriod.TotalMinutes;

                    for (int j = 0; j < lengthOfSnooze; j++)
                    {
                        minuteCounts[j + startMinute]++;
                    }
                }
            }

            var favouriteMinute = minuteCounts.MaxBy(minute => minute.Value).First();

            favouriteMinuteAndFrequency = (favouriteMinute.Key, favouriteMinute.Value);

            return favouriteMinuteAndFrequency;
        }
    }
}
