using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.IO;

namespace Day04
{
    class Program
    {
        static void Main(string[] args)
        {
            string[] testInput = File.ReadAllLines(@"C:\_Projects\Training\AdventOfCode\2018\Day04\testInput.txt");
            string[] rawInput = File.ReadAllLines(@"C:\_Projects\Training\AdventOfCode\2018\Day04\input.txt");

            SortedDictionary<DateTime, string> recordsInOrder = OrganizeRecords(testInput);
            Dictionary<int, SortedDictionary<DateTime, string>> guardsSeparated = SeparateGuards(recordsInOrder);

            var allGuardStats = new List<GuardStats>();

            foreach(var guard in guardsSeparated)
            {
                allGuardStats.Add(new GuardStats(guard));
            }
        }
        
        static SortedDictionary<DateTime, string> OrganizeRecords(string[] records)
        {
            var organizedRecords = new SortedDictionary<DateTime, string>();

            foreach(var record in records)
            {
                var recordParts = record.TrimStart('[').Replace("Guard ","").Split("] ");
                organizedRecords.Add(DateTime.Parse(recordParts[0]), recordParts[1]);
            }

            return organizedRecords;
        }

        static Dictionary<int, SortedDictionary<DateTime, string>> SeparateGuards(SortedDictionary<DateTime, string> records)
        {
            var guardsSeparated = new Dictionary<int, SortedDictionary<DateTime, string>>();

            var recordsAsList = records.ToList();

            int recordsToNextId = 0;            

            for (int i = 0; i < recordsAsList.Count; i += recordsToNextId)
            {
                int currentGuardId = int.Parse(recordsAsList[i].Value.Substring(1,recordsAsList[i].Value.IndexOf(' ')));
                int recordsForCurrentIdPeriod = 0;
                bool idChanged = false;

                while (!idChanged)
                {
                    var currentRecord = recordsAsList[i + recordsForCurrentIdPeriod];

                    if (guardsSeparated.ContainsKey(currentGuardId))
                    {
                        guardsSeparated[currentGuardId].Add(currentRecord.Key, currentRecord.Value);
                    }

                    else
                    {
                        var recordsForId = new SortedDictionary<DateTime, string>
                        {
                            { currentRecord.Key, currentRecord.Value }
                        };

                        guardsSeparated.Add(currentGuardId, recordsForId);
                    }

                    if(i + recordsForCurrentIdPeriod + 1 < recordsAsList.Count)
                    {
                        recordsForCurrentIdPeriod++;
                    }
                    else
                    {
                        break;
                    }

                    if (recordsAsList[i + recordsForCurrentIdPeriod].Value.Contains('#'))
                    {
                        idChanged = true;
                        recordsToNextId = recordsForCurrentIdPeriod;
                        recordsForCurrentIdPeriod = 0;
                    }                    
                }
            }
            
            return guardsSeparated;
        }
    }
}
