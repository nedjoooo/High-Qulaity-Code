namespace Theatre.Tests
{
    using System.Linq;

    using Microsoft.VisualStudio.TestTools.UnitTesting;

    using Theatre.Contracts;

    [TestClass]
    public class TheatreTests
    {
        private IPerformanceDatabase performanceDb;

        [TestInitialize]
        public void InitializeTest()
        {
            performanceDb = new PerformanceDatabase();
        }

        [TestMethod]
        public void Test_AddTheatre_ShouldAddTheatreCorrectly()
        {
            performanceDb.AddTheatre("Ivan Vazov National Theatre");

            var expextedTheatres = new[] { "Ivan Vazov National Theatre" };
            var actualTheatres = performanceDb.ListTheatres().ToList();

            CollectionAssert.AreEqual(expextedTheatres, actualTheatres);
        }
    }
}
