namespace OfficeTicket.Tickets
{
    using System;

    public class TicketObject
    {
        public TicketObject()
        {

        }

        public TicketType Type { get; set; }

        public string FlightNumber { get; set; }

        public string From { get; set; }

        public string To { get; set; }

        public string CompanyName { get; set; }

        public DateTime DateAndTime { get; set; }

        public decimal Price { get; set; }

        public decimal StudentPrice { get; set; }

        public DateTime StartFindDateTime { get; set; }

        public DateTime EndFindDateTime { get; set; }
    }
}
