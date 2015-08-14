namespace Theatre.Tests
{
    using System;
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Theatre.Contracts;
    using Theatre.Exceptions;

    [TestClass]
    public class UnitTestsPerformances
    {
        [TestMethod]
        public void TestAddSinglePerformanceShouldListPerformancesCorrectly()
        {
            IPerformanceDatabase performanceDb = new PerformanceDatabase();
            performanceDb.AddTheatre("Theatre Sofia");

            performanceDb.AddPerformance("Theatre Sofia", "Hamlet", new DateTime(2015, 02, 23, 19, 00, 00), new TimeSpan(1, 45, 0), 12.50M);
            string actualPerformances = string.Join(", ", performanceDb.ListAllPerformances());

            string expectedPerformances = "Performance(Theatre: Theatre Sofia; Title: Hamlet; StartDateTime: 23.02.2015 19:00, Duration: 01:45, Price: 12.50)";
            Assert.AreEqual(expectedPerformances, actualPerformances);
        }

        [TestMethod]
        public void TestAddSeveralPerformancesShouldListPerformancesCorrectly()
        {
            IPerformanceDatabase performanceDb = new PerformanceDatabase();
            performanceDb.AddTheatre("Theatre Sofia");
            performanceDb.AddTheatre("Theatre 199");

            performanceDb.AddPerformance("Theatre Sofia", "Hamlet", new DateTime(2015, 02, 23, 19, 00, 00), new TimeSpan(1, 45, 0), 12.50M);
            performanceDb.AddPerformance("Theatre Sofia", "Bella Donna", new DateTime(2015, 02, 24, 19, 30, 00), new TimeSpan(1, 20, 0), 10M);
            performanceDb.AddPerformance("Theatre Sofia", "Dance Show", new DateTime(2015, 02, 24, 18, 00, 00), new TimeSpan(1, 20, 0), 8.00M);
            performanceDb.AddPerformance("Theatre 199", "Bella Donna", new DateTime(2015, 02, 24, 19, 30, 00), new TimeSpan(1, 20, 0), 10M);

            string actualPerformances = string.Join(", ", performanceDb.ListAllPerformances());

            string expectedPerformances = "Performance(Theatre: Theatre 199; Title: Bella Donna; StartDateTime: 24.02.2015 19:30, Duration: 01:20, Price: 10.00), Performance(Theatre: Theatre Sofia; Title: Hamlet; StartDateTime: 23.02.2015 19:00, Duration: 01:45, Price: 12.50), Performance(Theatre: Theatre Sofia; Title: Dance Show; StartDateTime: 24.02.2015 18:00, Duration: 01:20, Price: 8.00), Performance(Theatre: Theatre Sofia; Title: Bella Donna; StartDateTime: 24.02.2015 19:30, Duration: 01:20, Price: 10.00)";
            Assert.AreEqual(expectedPerformances, actualPerformances);
        }

        [TestMethod]
        [ExpectedException(typeof(TheatreNotFoundException))]
        public void TestAddPerformanceNonExistingTheatreShouldThrowException()
        {
            IPerformanceDatabase performanceDb = new PerformanceDatabase();

            performanceDb.AddPerformance("Theatre Sofia", "Hamlet", new DateTime(2015, 02, 23, 19, 00, 00), new TimeSpan(1, 45, 0), 12.50M);
        }

        [TestMethod]
        [ExpectedException(typeof(TimeDurationOverlapException))]
        public void TestAddOverlappingPerformancesShouldThrowException()
        {
            IPerformanceDatabase performanceDb = new PerformanceDatabase();
            performanceDb.AddTheatre("Theatre Sofia");
            performanceDb.AddPerformance("Theatre Sofia", "Dances", new DateTime(2015, 2, 2, 19, 00, 00), new TimeSpan(1, 40, 0), 12.50M);
            performanceDb.AddPerformance("Theatre Sofia", "Hamlet", new DateTime(2015, 2, 2, 20, 00, 00), new TimeSpan(1, 30, 0), 14.50M);
        }

        [TestMethod]
        [ExpectedException(typeof(TimeDurationOverlapException))]
        public void TestAddOverlappingPerformancesFirstEndSecondStartShouldThrowException()
        {
            IPerformanceDatabase performanceDb = new PerformanceDatabase();
            performanceDb.AddTheatre("Theatre Sofia");
            performanceDb.AddPerformance("Theatre Sofia", "Hamlet", new DateTime(2015, 2, 2, 19, 00, 00), new TimeSpan(1, 30, 0), 12.50M);
            performanceDb.AddPerformance("Theatre Sofia", "Hamlet", new DateTime(2015, 2, 2, 20, 30, 00), new TimeSpan(1, 00, 0), 12.50M);
        }

        [TestMethod]
        [ExpectedException(typeof(TimeDurationOverlapException))]
        public void TestAddOverlappingPerformancesFirstStartSecondEndShouldThrowException()
        {
            IPerformanceDatabase performanceDb = new PerformanceDatabase();
            performanceDb.AddTheatre("Theatre Sofia");
            performanceDb.AddPerformance("Theatre Sofia", "Hamlet", new DateTime(2015, 2, 2, 20, 30, 00), new TimeSpan(1, 00, 0), 12.50M);
            performanceDb.AddPerformance("Theatre Sofia", "Hamlet", new DateTime(2015, 2, 2, 19, 00, 00), new TimeSpan(1, 30, 0), 12.50M);
        }

        [TestMethod]
        [ExpectedException(typeof(TimeDurationOverlapException))]
        public void TestAddOverlappingPerformancesFirstAllDayShouldThrowException()
        {
            IPerformanceDatabase performanceDb = new PerformanceDatabase();
            performanceDb.AddTheatre("Theatre Sofia");
            performanceDb.AddPerformance("Theatre Sofia", "All Day Event", new DateTime(2015, 2, 2, 9, 00, 00), new TimeSpan(14, 00, 0), 12.50M);
            performanceDb.AddPerformance("Theatre Sofia", "Hamlet", new DateTime(2015, 2, 2, 19, 00, 00), new TimeSpan(1, 30, 0), 12.50M);
        }

        [TestMethod]
        [ExpectedException(typeof(TimeDurationOverlapException))]
        public void TestAddOverlappingPerformancesSecondAllDayShouldThrowException()
        {
            IPerformanceDatabase performanceDb = new PerformanceDatabase();
            performanceDb.AddTheatre("Theatre Sofia");
            performanceDb.AddPerformance("Theatre Sofia", "Hamlet", new DateTime(2015, 2, 2, 19, 00, 00), new TimeSpan(1, 30, 0), 12.50M);
            performanceDb.AddPerformance("Theatre Sofia", "All Day Event", new DateTime(2015, 2, 2, 9, 00, 00), new TimeSpan(14, 00, 0), 12.50M);
        }

        [TestMethod]
        public void TestPrintEmptyPerformances()
        {
            IPerformanceDatabase performanceDb = new PerformanceDatabase();
            var performances = performanceDb.ListAllPerformances().ToList();
            Assert.AreEqual(0, performances.Count());
        }

        [TestMethod]
        public void TestListPerformancesByTheatre()
        {
            IPerformanceDatabase performanceDb = new PerformanceDatabase();

            performanceDb.AddTheatre("Theatre Sofia");
            performanceDb.AddPerformance("Theatre Sofia", "Bella Donna", new DateTime(2015, 02, 24, 19, 30, 00), new TimeSpan(1, 20, 0), 10M);
            performanceDb.AddPerformance("Theatre Sofia", "Dance Show", new DateTime(2015, 02, 24, 18, 00, 00), new TimeSpan(1, 20, 0), 8.00M);
            performanceDb.AddPerformance("Theatre Sofia", "Hamlet", new DateTime(2015, 02, 23, 19, 00, 00), new TimeSpan(1, 45, 0), 12.50M);

            performanceDb.AddTheatre("Theatre 199");
            performanceDb.AddPerformance("Theatre 199", "Bella Donna", new DateTime(2015, 02, 24, 19, 30, 00), new TimeSpan(1, 20, 0), 10M);

            var actualPerformancesSofia = string.Join(
                ", ", performanceDb.ListPerformances("Theatre Sofia"));
            var expectedPerformancesSofia = "Performance(Theatre: Theatre Sofia; Title: Hamlet; StartDateTime: 23.02.2015 19:00, Duration: 01:45, Price: 12.50), Performance(Theatre: Theatre Sofia; Title: Dance Show; StartDateTime: 24.02.2015 18:00, Duration: 01:20, Price: 8.00), Performance(Theatre: Theatre Sofia; Title: Bella Donna; StartDateTime: 24.02.2015 19:30, Duration: 01:20, Price: 10.00)";
            Assert.AreEqual(expectedPerformancesSofia, actualPerformancesSofia);

            var actualPerformances199 = string.Join(", ", performanceDb.ListPerformances("Theatre 199"));
            var expectedPerformances199 = "Performance(Theatre: Theatre 199; Title: Bella Donna; StartDateTime: 24.02.2015 19:30, Duration: 01:20, Price: 10.00)";
            Assert.AreEqual(expectedPerformances199, actualPerformances199);
        }

        [TestMethod]
        [ExpectedException(typeof(TheatreNotFoundException))]
        public void TestListPerformancesInvalidTheatreShouldThrowException()
        {
            IPerformanceDatabase performanceDb = new PerformanceDatabase();
            performanceDb.ListPerformances("Theatre Sofia");
        }
    }
}
