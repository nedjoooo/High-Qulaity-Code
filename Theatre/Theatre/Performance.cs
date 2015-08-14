namespace Theatre
{
    using System;

    public class Performance : IComparable<Performance>
    {
        public Performance(string theatre, string performance, DateTime dateAndTime, TimeSpan duration, decimal price)
        {
            this.Theatre = theatre;
            this.ThisPerformance = performance;
            this.DateAndTime = dateAndTime;
            this.Duration = duration; 
            this.Price = price;
        }

        public string Theatre { get; private set; }

        public string ThisPerformance { get; private set; }

        public DateTime DateAndTime { get; private set; }

        public TimeSpan Duration { get; private set; }

        public decimal Price { get; private set; }

        int IComparable<Performance>.CompareTo(Performance comparedPerformance)
        {
            int comparer = this.DateAndTime.CompareTo(comparedPerformance.DateAndTime);

            return comparer;
        }

        public override string ToString()
        {
            string result = string.Format(
                "({0}; {1}; {2}, {3}, {4})",
                this.Theatre,
                this.ThisPerformance,
                this.DateAndTime.ToString("dd.MM.yyyy HH:mm"),
                this.Duration.ToString("hh':'mm"),
                this.Price.ToString("f2"));

            return result;
        }
    }
}
