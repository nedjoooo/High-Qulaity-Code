namespace OfficeTicket
{
    using System;
    using System.Globalization;
    using OfficeTicket.Tickets;   

    public abstract class Ticket : IComparable<Ticket>
    {
        public Ticket(TicketType type, string from = null, string to = null, DateTime dateTime = default(DateTime), decimal price = 0M)
        {
            this.Type = type;
            this.From = from;
            this.To = to;
            this.DateAndTime = dateTime;
            this.Price = price;
        }

        public TicketType Type { get; private set; }

        public string From { get; private set; }

        public string To { get; private set; }

        public DateTime DateAndTime { get; set; }

        public decimal Price { get; set; }

        public string FromToKey
        {
            get
            {
                return CreateFromToKey(this.From, this.To);
            }
        }        

        public abstract string UniqueKey { get; }

        public static string CreateFromToKey(string from, string to)
        {
            return from + "; " + to;
        }

        public int CompareTo(Ticket otherTicket)
        {
            int comparer = this.DateAndTime.CompareTo(otherTicket.DateAndTime);

            if (comparer == 0)
            {
                comparer = this.Type.CompareTo(otherTicket.Type);
            }

            if (comparer == 0)
            {
                comparer = this.Price.CompareTo(otherTicket.Price);
            }

            return comparer;
        }

        public override string ToString()
        {
            string input = "["
                + this.DateAndTime.ToString("dd.MM.yyyy HH:mm")
                + "|" + this.Type.ToString().ToLower()
                + "|"
                + String.Format("{0:f2}", this.Price)
                + "]";

            return input;
        }
    }
}
