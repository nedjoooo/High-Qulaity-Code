namespace PhonebookSystem.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PhonebookSystem.Contracts;

    [TestClass]
    public class PhonebookRepositoryListTests
    {
        private IPhonebookRepository phonebook;

        [TestInitialize]
        public void InitilizePhonebookRepository()
        {
            this.phonebook = new PhonebookRepository();
        }

        [TestMethod]
        public void ShouldListPhoneNumbers()
        {
            this.phonebook.AddPhone("Maria", new[] { "0888 888 888", "+359888 999 123" });
            this.phonebook.AddPhone("Maria", new[] { "0888 888 888", "+359888 666 123" });

            var entries = this.phonebook.ListEntries(0, 1);

            Assert.AreEqual(1, entries.Count);
            Assert.AreEqual("Maria", entries[0].Name);
            Assert.AreEqual(3, entries[0].PhoneNumbers.Count);
        }
    }
}
