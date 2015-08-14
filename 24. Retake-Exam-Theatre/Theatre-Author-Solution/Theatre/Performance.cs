namespace Theatre
{
    using System;

    public class Performance : IComparable<Performance>
    {
        public Performance(string theatre, string title, DateTime startDateTime, TimeSpan duration, decimal price)
        {
            this.Theatre = theatre;
            this.Title = title;
            this.StartDateTime = startDateTime;
            this.Duration = duration;
            this.Price = price;
        }

        public string Theatre { get; set; }

        public string Title { get; set; }

        public DateTime StartDateTime { get; set; }

        public TimeSpan Duration { get; set; }

        public decimal Price { get; set; }

        public int CompareTo(Performance otherPerformance)
        {
            int compareResult = this.StartDateTime.CompareTo(
                otherPerformance.StartDateTime);
            return compareResult;
        }

        public override string ToString()
        {
            string result = string.Format(
                "Performance(Theatre: {0}; Title: {1}; StartDateTime: {2}, Duration: {3}, Price: {4})",
                this.Theatre, 
                this.Title,
                this.StartDateTime.ToString(Constants.DateTimeFormat),
                this.Duration.ToString(Constants.DurationFormat),
                this.Price.ToString(Constants.PriceFormat));
            return result;
        }
    }
}
