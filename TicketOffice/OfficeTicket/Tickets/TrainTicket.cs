namespace OfficeTicket.Tickets
{
    using System;

    class TrainTicket : Ticket
    {
        public TrainTicket(string from, string to, DateTime dateTime, decimal price = 0M, decimal studentPrice = 0M)
            :base(TicketType.Train, from, to, dateTime, price)
        {
            this.StudentPrice = studentPrice;
        }

        public decimal StudentPrice { get; private set; }

        public override string ToString()
        {
            return base.ToString();
        }

        public override string UniqueKey
        {
            get
            {
                return this.Type.ToString()
                    + ";;" + this.From
                    + ";" + this.To
                    + ";"
                    + this.DateAndTime.ToString("dd.MM.yyyy HH:mm")
                    + ";";
            }
        }
    }
}
