namespace PhonebookSystem
{
    using System;
    using System.Collections.Generic;

    public class PhonebookEntry : IComparable<PhonebookEntry>
    {
        private string name;

        public PhonebookEntry(string name)
        {
            this.Name = name;
            this.PhoneNumbers = new SortedSet<string>();
        }

        public string Name { get; private set; }

        public SortedSet<string> PhoneNumbers { get; private set; }

        public override string ToString()
        {
            var joinedPhoneNumbers = string.Join(", ", this.PhoneNumbers);

            var entry = string.Format("[{0}: {1}]", this.Name, joinedPhoneNumbers);

            return entry;
        }

        public int CompareTo(PhonebookEntry other)
        {
            return string.Compare(this.name, other.name, StringComparison.InvariantCultureIgnoreCase);
        }
    }
}
