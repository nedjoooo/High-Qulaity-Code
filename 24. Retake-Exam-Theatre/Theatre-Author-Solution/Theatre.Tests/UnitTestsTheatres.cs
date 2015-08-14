namespace Theatre.Tests
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Theatre.Contracts;
    using Theatre.Exceptions;

    [TestClass]
    public class UnitTestsTheatres
    {
        [TestMethod]
        public void TestAddTheatreShouldListTheatresCorrectly()
        {
            IPerformanceDatabase performanceDb = new PerformanceDatabase();
            performanceDb.AddTheatre("Theatre Sofia");
            
            var expectedTheatres = new[] { "Theatre Sofia" };
            var actualTheatres = performanceDb.ListTheatres().ToList();

            CollectionAssert.AreEqual(expectedTheatres, actualTheatres);
        }

        [TestMethod]
        public void TestAddSeveralTheatresShouldListTheatresCorrectly()
        {
            IPerformanceDatabase performanceDb = new PerformanceDatabase();
            performanceDb.AddTheatre("Theatre 199");
            performanceDb.AddTheatre("Theatre Sofia");
            performanceDb.AddTheatre("Art Theatre");

            var expectedTheatres = new[] { "Art Theatre", "Theatre 199", "Theatre Sofia" };
            var actualTheatres = performanceDb.ListTheatres().ToList();

            CollectionAssert.AreEqual(expectedTheatres, actualTheatres);
        }

        [TestMethod]
        public void TestAddSeveralTheatresWithDuplicatesShouldListTheatresCorrectly()
        {
            IPerformanceDatabase performanceDb = new PerformanceDatabase();
            performanceDb.AddTheatre("Theatre 199");
            performanceDb.AddTheatre("Theatre Sofia");
            try
            {
                performanceDb.AddTheatre("Theatre 199");
            }
            catch (DuplicateTheatreException)
            {
                // Do nothing -> this exception is expected
            }

            var expectedTheatres = new[] { "Theatre 199", "Theatre Sofia" };
            var actualTheatres = performanceDb.ListTheatres().ToList();

            CollectionAssert.AreEqual(expectedTheatres, actualTheatres);
        }

        [TestMethod]
        [ExpectedException(typeof(DuplicateTheatreException))]
        public void TestAddDuplicateTheatresShouldThrowException()
        {
            IPerformanceDatabase performanceDb = new PerformanceDatabase();
            performanceDb.AddTheatre("Theatre 199");
            performanceDb.AddTheatre("Theatre 199");
        }

        [TestMethod]
        public void TestNoTheatresShouldReturnEmptyList()
        {
            IPerformanceDatabase performanceDb = new PerformanceDatabase();

            var actualTheatres = performanceDb.ListTheatres().ToList();

            Assert.AreEqual(0, actualTheatres.Count());
        }
    }
}
