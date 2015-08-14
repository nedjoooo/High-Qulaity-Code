namespace PhonebookSystem.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PhonebookSystem.Contracts;

    [TestClass]
    public class PhonebookRepositoryAddPhonebookEntriesTests
    {
        private IPhonebookRepository phonebook;

        [TestInitialize]
        public void InitilizePhonebookRepository()
        {
            this.phonebook = new PhonebookRepository();
        }

        [TestMethod]
        public void ShouldAddSingleEntryWithSinglePhoneNumber()
        {
            var isNew = this.phonebook.AddPhone("Alex St.Zagora", new[] { "042 77 33 55" });

            Assert.IsTrue(isNew);
        }

        [TestMethod]
        public void ShouldAddMultipleEntryWithSinglePhoneNumber()
        {
            var isNew = this.phonebook.AddPhone("Alex St.Zagora", new[] { "042123123 77 33 55" });
            Assert.IsTrue(isNew, "Alex St.Zagora");

            var isSecondNew = this.phonebook.AddPhone("Aslex St.Zagora", new[] { "042123121233 77 33 55" });
            Assert.IsTrue(isSecondNew, "Aslex St.Zagora");
        }

        [TestMethod]
        public void ShouldAddSingleEntryForEqualNamesWithoutCasing()
        {
            var isNew = this.phonebook.AddPhone("Alex St.Zagora", new[] { "042123123 77 33 55" });
            Assert.IsTrue(isNew, "Alex St.Zagora");

            var isSecondNew = this.phonebook.AddPhone("ALEX St.ZaGora", new[] { "042123121233 77 33 55" });
            Assert.IsFalse(isSecondNew, "ALEX St.ZaGora");
        }

        [TestMethod]
        public void ShouldMergePhoneNumbers()
        {
            this.phonebook.AddPhone("Maria", new[] { "0888 888 888", "+359888 999 123" });
            this.phonebook.AddPhone("Maria", new[] { "0888 888 888", "+359888 666 123" });

            var entries = this.phonebook.ListEntries(0, 1);
            var entry = entries[0];

            Assert.AreEqual(3, entry.PhoneNumbers.Count);
        }
    }
}