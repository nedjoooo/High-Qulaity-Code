namespace Theatre
{
    using System;
    using System.Linq;
    using System.Collections.Generic;
    using Theatre.Contracts;

    public class PerformanceDatabase : IPerformanceDatabase
    {
        private readonly SortedDictionary<string, SortedSet<Performance>> sortedDictionaryStringSortedSetPerformance;
        //private readonly SortedSet<Performance> sortedSetPerformances;

        public PerformanceDatabase()
        {
            this.sortedDictionaryStringSortedSetPerformance = new SortedDictionary<string, SortedSet<Performance>>();
            //this.sortedSetPerformances = new SortedSet<Performance>();
        }

        public void AddTheatre(string theatre)
        {
            if (this.sortedDictionaryStringSortedSetPerformance.ContainsKey(theatre))
            {
                throw new InvalidOperationException("Duplicate theatre");
            }

            this.sortedDictionaryStringSortedSetPerformance[theatre] = new SortedSet<Performance>();
        }

        public IEnumerable<string> ListTheatres()
        {
            var theatres = this.sortedDictionaryStringSortedSetPerformance.Keys;
            return theatres;
        }

        public void AddPerformance(
            string theatre,
            string performance, 
            DateTime performanceStartDateAndTime,
            TimeSpan duration,
            decimal price)
        {
            if (!this.sortedDictionaryStringSortedSetPerformance.ContainsKey(theatre))
            {
                throw new ArgumentException("Theatre does not exist");
            }

            var performances = this.sortedDictionaryStringSortedSetPerformance[theatre];
            var performanceEndDateAndTime = performanceStartDateAndTime + duration;

            if (CheckValidPerformanceDateAndTime(performances, performanceStartDateAndTime, performanceEndDateAndTime))
            {
                throw new InvalidOperationException("Time/duration overlap");
            }

            var currentPerformance = new Performance(theatre, performance, performanceStartDateAndTime, duration, price);
            performances.Add(currentPerformance);
            //sortedSetPerformances.Add(currentPerformance);
        }

        public IEnumerable<Performance> ListAllPerformances()
        {
            var theatres = this.sortedDictionaryStringSortedSetPerformance.Keys;
            var listTheatres = new List<Performance>();

            foreach (var theatre in theatres)
            {
                var performances = this.sortedDictionaryStringSortedSetPerformance[theatre];
                listTheatres.AddRange(performances);
            }

            return listTheatres;
        }

        IEnumerable<Performance> IPerformanceDatabase.ListPerformances(string theatreName)
        {
            if (!this.sortedDictionaryStringSortedSetPerformance.ContainsKey(theatreName))
            {
                throw new InvalidOperationException("Theatre does not exist");
            }

            var performancesOfTheatre = this.sortedDictionaryStringSortedSetPerformance[theatreName];
            return performancesOfTheatre;
        }

        private static bool CheckValidPerformanceDateAndTime(
            IEnumerable<Performance> performances,
            DateTime performanceStartDateAndTime, 
            DateTime performanceEndDateAndTime)
        {
            foreach (var performance in performances)
            {
                var currentPerformanceStartDateAndTime = performance.DateAndTime;
                var currentPerformanceEndDateOfTime = performance.DateAndTime + performance.Duration;
                var checkPerformanceDateAndTime = (
                    currentPerformanceStartDateAndTime <= performanceStartDateAndTime && performanceStartDateAndTime <= currentPerformanceEndDateOfTime)
                    || (currentPerformanceStartDateAndTime <= performanceEndDateAndTime && performanceEndDateAndTime <= currentPerformanceEndDateOfTime)
                    || (performanceStartDateAndTime <= currentPerformanceStartDateAndTime && currentPerformanceStartDateAndTime <= performanceEndDateAndTime)
                    || (performanceStartDateAndTime <= currentPerformanceEndDateOfTime && currentPerformanceEndDateOfTime <= performanceEndDateAndTime);

                if (checkPerformanceDateAndTime)
                {
                    return true;
                }
            }

            return false;
        }
    }
}
