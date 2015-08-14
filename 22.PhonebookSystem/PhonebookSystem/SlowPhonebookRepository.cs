namespace PhonebookSystem
{
    using PhonebookSystem.Contracts;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class SlowPhonebookRepository : IPhonebookRepository
    {
        private readonly List<PhonebookEntry> entries = new List<PhonebookEntry>();

        public bool AddPhone(string name, IEnumerable<string> phoneNumbers)
        {
            var matchedEntry = 
                from e in this.entries 
                where e.Name.ToLowerInvariant() == name.ToLowerInvariant() 
                select e;

            //matchedEntry = this.entries
            //    .Where(entry => string.Equals(entry.Name, name, StringComparison.InvariantCultureIgnoreCase))
            //    .ToList();

            bool isNewEntry;

            if (matchedEntry.Count() == 0)
            {
                PhonebookEntry entry = new PhonebookEntry(name);

                AddPhoneToEntry(phoneNumbers, entry);

                this.entries.Add(entry);
                isNewEntry = true;
            }
            else if (matchedEntry.Count() == 1)
            {
                PhonebookEntry existingEntry = matchedEntry.First();
                var newPhoneNumbers = phoneNumbers
                    .Where(phoneNumber => !existingEntry.PhoneNumbers.Contains(phoneNumber))
                    .ToList();

                AddPhoneToEntry(newPhoneNumbers, existingEntry);

                isNewEntry = false;
            }
            else
            {
                throw new InvalidOperationException("Duplicate name.");
            }

            return isNewEntry;
        }

        public int ChangePhone(string oldPhoneNumber, string newPhoneNumber)
        {
            var matchedEntries = this.entries
                .Where(entry => entry.PhoneNumbers.Contains(oldPhoneNumber))
                .ToList();

            //matchedEntries = 
            //    from e in this.entries
            //    where e.PhoneNumbers.Contains(oldPhoneNumber)
            //    select e;
            
            foreach (var entry in matchedEntries)
            {
                entry.PhoneNumbers.Remove(oldPhoneNumber); 
                entry.PhoneNumbers.Add(newPhoneNumber);
            }

            return matchedEntries.Count;
        }

        public IList<PhonebookEntry> ListEntries(int startIndex, int count)
        {
            if (startIndex < 0 || startIndex + count > this.entries.Count)
            {
                throw new ArgumentOutOfRangeException("Invalid start index or count.");
            }

            //objListOrder.Sort((x, y) => x.OrderDate.CompareTo(y.OrderDate));
            this.entries.Sort((x, y) => x.Name.CompareTo(y.Name));

            var listedEntries = new PhonebookEntry[count];

            for (int i = startIndex; i <= startIndex + count - 1; i++)
            {
                var entry = this.entries[i];
                listedEntries[i - startIndex] = entry;
            }

            return listedEntries;
        }

        private static void AddPhoneToEntry(IEnumerable<string> phoneNumbers, PhonebookEntry entry)
        {
            foreach (var phoneNumber in phoneNumbers)
            {
                entry.PhoneNumbers.Add(phoneNumber);
            }
        }
    }
}
