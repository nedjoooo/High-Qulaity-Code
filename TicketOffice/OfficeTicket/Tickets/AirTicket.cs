namespace OfficeTicket.Tickets
{
    using System;

    public class AirTicket : Ticket
    {
        public AirTicket(string flightNumber, string from, string to, string airlineCompany, DateTime dateTime, decimal price)
            : base(TicketType.Flight, from, to, dateTime, price)
        {
            this.FlightNumber = flightNumber;
            this.AirlineCompany = airlineCompany;
        }

        public AirTicket(string flightNumber) : base(TicketType.Flight)
        {
            this.FlightNumber = flightNumber;
        }

        public string FlightNumber { get; private set; }

        public string AirlineCompany { get; private set; }

        public override string UniqueKey
        {
            get
            {
                return this.Type.ToString() + ";;" + this.FlightNumber;
            }
        }

        public override string ToString()
        {
            return base.ToString();
        }       
    }
}
