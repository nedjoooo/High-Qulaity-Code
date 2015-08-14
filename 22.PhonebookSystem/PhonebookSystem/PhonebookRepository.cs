
namespace PhonebookSystem
{
    using PhonebookSystem.Contracts;
    using System;
    using System.Collections.Generic;
    using Wintellect.PowerCollections;
    using System.Linq;

    public class PhonebookRepository : IPhonebookRepository
    {
        private readonly OrderedSet<PhonebookEntry> sortedEntries = new OrderedSet<PhonebookEntry>();
        private readonly Dictionary<string, PhonebookEntry> entriesByName = new Dictionary<string, PhonebookEntry>();
        private readonly MultiDictionary<string, PhonebookEntry> entriesByPhoneNumber = new MultiDictionary<string, PhonebookEntry>(false);

        public bool AddPhone(string name, IEnumerable<string> phoneNumbers)
        {
            var nameLowercase = name.ToLowerInvariant();
            PhonebookEntry entry;
            bool isNewEntry = !this.entriesByName.TryGetValue(nameLowercase, out entry);

            if (isNewEntry)
            {
                entry = new PhonebookEntry(name);
                this.entriesByName.Add(nameLowercase, entry);
                this.sortedEntries.Add(entry);
            }

            foreach (var num in phoneNumbers)
            {
                this.entriesByPhoneNumber.Add(num, entry);
            }

            entry.PhoneNumbers.UnionWith(phoneNumbers);
            return isNewEntry;
        }

        public int ChangePhone(string oldPhoneNumber, string newPhoneNumber)
        {
            var matchedEntries = this.entriesByPhoneNumber[oldPhoneNumber].ToList(); 

            foreach (var entry in matchedEntries)
            {
                entry.PhoneNumbers.Remove(oldPhoneNumber);
                this.entriesByPhoneNumber.Remove(oldPhoneNumber, entry);

                entry.PhoneNumbers.Add(newPhoneNumber);
                this.entriesByPhoneNumber.Add(newPhoneNumber, entry);
            }

            return matchedEntries.Count;
        }

        public IList<PhonebookEntry> ListEntries(int startIndex, int count)
        {
            if (startIndex < 0 || startIndex >= this.entriesByName.Count)
            {
                throw new ArgumentOutOfRangeException("startIndex");
            }

            if (count > this.entriesByName.Count)
            {
                throw new ArgumentOutOfRangeException("count");
            }

            if (startIndex + count > this.entriesByName.Count)
            {
                throw new ArgumentOutOfRangeException("Invalid parameters");
            }

            var matchedEntries = new PhonebookEntry[count];

            for (int i = startIndex; i <= startIndex + count - 1; i++)
            {
                PhonebookEntry entry = this.sortedEntries[i];
                matchedEntries[i - startIndex] = entry;
            }

            return matchedEntries;
        }
    }
}
