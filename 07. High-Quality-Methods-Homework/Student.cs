using System;

namespace Methods
{
    class Student
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string OtherInfo { get; set; }

        public bool IsOlderThan(Student other)
        {
            DateTime firstDate =
                DateTime.ParseExact(this.OtherInfo.Substring(this.OtherInfo.Length - 10), "dd.MM.yyyy",
                System.Globalization.CultureInfo.InvariantCulture);
            DateTime secondDate =
                DateTime.ParseExact(other.OtherInfo.Substring(other.OtherInfo.Length - 10), "dd.MM.yyyy",
                System.Globalization.CultureInfo.InvariantCulture);
            return firstDate < secondDate;
        }
    }
}
