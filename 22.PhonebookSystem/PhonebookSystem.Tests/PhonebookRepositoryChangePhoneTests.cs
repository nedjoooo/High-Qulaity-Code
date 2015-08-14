namespace PhonebookSystem.Tests
{
    using Microsoft.VisualStudio.TestTools.UnitTesting;
    using PhonebookSystem.Contracts;

    [TestClass]
    public class PhonebookRepositoryChangePhoneTests
    {
        private IPhonebookRepository phonebook;

        [TestInitialize]
        public void InitilizePhonebookRepository()
        {
            this.phonebook = new PhonebookRepository();
        }

        [TestMethod]
        public void ShouldChangeOneEntry()
        {

        }

        [TestMethod]
        public void ShouldChangeNoEntries()
        {

        }

        [TestMethod]
        public void ShouldChangeNEntries()
        {

        }
    }
}
