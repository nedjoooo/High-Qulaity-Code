namespace Theatre
{
    using System;
    using System.Collections.Generic;

    using Theatre.Contracts;
    using Theatre.Exceptions;

    public class PerformanceDatabase : IPerformanceDatabase
    {
        private readonly SortedDictionary<string, SortedSet<Performance>> performancesByTheatre =
            new SortedDictionary<string, SortedSet<Performance>>();

        public void AddTheatre(string theatreName)
        {
            if (this.performancesByTheatre.ContainsKey(theatreName))
            {
                throw new DuplicateTheatreException(Constants.DuplicateTheatreMsg);
            }

            this.performancesByTheatre[theatreName] = new SortedSet<Performance>();
        }

        public IEnumerable<string> ListTheatres()
        {
            var theatres = this.performancesByTheatre.Keys;
            return theatres;
        }

        public void AddPerformance(
            string theatreName, string performanceTitle, DateTime startDateTime, TimeSpan duration, decimal price)
        {
            if (!this.performancesByTheatre.ContainsKey(theatreName))
            {
                throw new TheatreNotFoundException(Constants.TheatreDoesNotExistMsg);
            }

            var performances = this.performancesByTheatre[theatreName];
            var endDateTime = startDateTime + duration;
            var hasOverlapping = CheckForOverlapping(performances, startDateTime, endDateTime);
            if (hasOverlapping)
            {
                throw new TimeDurationOverlapException(Constants.TimeDurationOverlapMsg);
            }

            var performance = new Performance(theatreName, performanceTitle, startDateTime, duration, price);
            performances.Add(performance);
        }

        public IEnumerable<Performance> ListAllPerformances()
        {
            var theatres = this.performancesByTheatre.Keys;
            var allPerformances = new List<Performance>();
            foreach (var theatre in theatres)
            {
                var performances = this.performancesByTheatre[theatre];
                allPerformances.AddRange(performances);
            }

            return allPerformances;
        }

        public IEnumerable<Performance> ListPerformances(string theatreName)
        {
            if (!this.performancesByTheatre.ContainsKey(theatreName))
            {
                throw new TheatreNotFoundException(Constants.TheatreDoesNotExistMsg);
            }

            var performances = this.performancesByTheatre[theatreName];
            return performances;
        }

        private static bool CheckForOverlapping(
            IEnumerable<Performance> performances, DateTime startDateTime, DateTime endDateTime)
        {
            foreach (var p in performances)
            {
                var existingStart = p.StartDateTime;
                var existingEnd = p.StartDateTime + p.Duration;
                if (CheckForOverlapping(existingStart, existingEnd, startDateTime, endDateTime))
                {
                    return true;
                }
            }

            return false;
        }

        private static bool CheckForOverlapping(
            DateTime start1, DateTime end1, DateTime start2, DateTime end2)
        {
            var opervapping =
                (start1 <= start2 && start2 <= end1) ||
                (start1 <= end2 && end2 <= end1) ||
                (start2 <= start1 && start1 <= end2) ||
                (start2 <= end1 && end1 <= end2);
            return opervapping;
        }
    }
}
