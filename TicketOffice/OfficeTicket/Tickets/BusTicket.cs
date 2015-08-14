namespace OfficeTicket.Tickets
{
    using System;

    class BusTicket : Ticket
    {
        public BusTicket(string from, string to, string busCompany, DateTime dateTime, decimal price = 0M)
            :base(TicketType.Bus, from, to, dateTime, price)
        {
            this.busCompany = busCompany;
        }

        public string busCompany { get; private set; }

        public override string ToString()
        {
            return base.ToString();
        }

        public override string UniqueKey
        {
            get
            {
                return this.Type.ToString() + ";;" + this.From + ";" + this.To + ";" +
                    this.busCompany + this.DateAndTime.ToString("dd.MM.yyyy HH:mm") + ";";
            }
        }
    }
}
