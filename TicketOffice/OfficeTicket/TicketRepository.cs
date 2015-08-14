namespace OfficeTicket
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;    
    using OfficeTicket.Contracts;
    using OfficeTicket.Tickets;
    using OfficeTicket.Utilities; 
    using Wintellect.PowerCollections;   
       
    public class TicketRepository : ITicketRepository
    {
        private Dictionary<string, Ticket> ticketCatalog = new Dictionary<string, Ticket>();

        private MultiDictionary<string, Ticket> ticketCatalogByFromToKey = new MultiDictionary<string, Ticket>(true);

        private OrderedMultiDictionary<DateTime, Ticket> ticketCatalogByDateAndTimeKey = new OrderedMultiDictionary<DateTime, Ticket>(true);

        private int airTicketsCount = 0;
        private int busTicketsCount = 0;
        private int trainTicketsCount = 0;

        public static string ReadTickets(ICollection<Ticket> tickets)
        {
            List<Ticket> sortedTickets = new List<Ticket>(tickets);

            sortedTickets.Sort();

            StringBuilder result = new StringBuilder();

            for (int i = 0; i < sortedTickets.Count; i++)
            {
                Ticket ticket = sortedTickets[i];
                result.Append(ticket.ToString()).Append(' ');
            }

            return result.ToString();
        }

        public string AddTicket(Ticket ticket)
        {
            string key = ticket.UniqueKey;

            if (this.ticketCatalog.ContainsKey(key))
            {
                return "Duplicated " + ticket.Type.ToString().ToLower();
            }

            this.ticketCatalog.Add(key, ticket);
            string fromToKey = ticket.FromToKey;
            this.ticketCatalogByFromToKey.Add(fromToKey, ticket);
            this.ticketCatalogByDateAndTimeKey.Add(ticket.DateAndTime, ticket);

            return ticket.Type + " created";
        }

        public string DeleteTicket(Ticket ticket)
        {
            string key = ticket.UniqueKey;

            if (this.ticketCatalog.ContainsKey(key))
            {
                ticket = this.ticketCatalog[key];
                this.ticketCatalog.Remove(key);
                string fromToKey = ticket.FromToKey;
                this.ticketCatalogByFromToKey.Remove(fromToKey, ticket);
                this.ticketCatalogByDateAndTimeKey.Remove(ticket.DateAndTime, ticket);

                return ticket.Type + " deleted";
            }

            return ticket.Type + " does not exist";
        }

        public string AddAirTicket(string flightNumber, string from, string to, string airlineCompany, DateTime dateTime, decimal ticketPrice)
        {
            AirTicket ticket = new AirTicket(flightNumber, from, to, airlineCompany, dateTime, ticketPrice);
            string result = this.AddTicket(ticket);

            if (result.Contains("created"))
            {
                this.airTicketsCount++;
            }

            return result;
        }

        public string DeleteAirTicket(string flightNumber)
        {
            AirTicket ticket = new AirTicket(flightNumber);
            string result = this.DeleteTicket(ticket);

            if (result.Contains("deleted"))
            {
                this.airTicketsCount--;
            }

            return result;
        }

        public string AddTrainTicket(string from, string to, DateTime dateTime, decimal ticketPrice, decimal studentTicketPrice)
        {
            TrainTicket ticket = new TrainTicket(from, to, dateTime, ticketPrice, studentTicketPrice);
            string result = this.AddTicket(ticket);

            if (result.Contains("created"))
            {
                this.trainTicketsCount++;
            }

            return result;
        }

        public string DeleteTrainTicket(string from, string to, DateTime dateTime)
        {
            TrainTicket ticket = new TrainTicket(from, to, dateTime);
            string result = this.DeleteTicket(ticket);

            if (result.Contains("deleted"))
            {
                this.trainTicketsCount--;
            }

            return result;
        }

        public string AddBusTicket(string from, string to, string busCompany, DateTime dateTime, decimal price)
        {
            BusTicket ticket = new BusTicket(from, to, busCompany, dateTime, price);
            string key = ticket.UniqueKey;
            string result;

            if (this.ticketCatalog.ContainsKey(key))
            {
                result = "Duplicated " + ticket.Type.ToString().ToLower();
            }
            else
            {
                this.ticketCatalog.Add(key, ticket);
                string fromToKey = ticket.FromToKey;
                this.ticketCatalogByFromToKey.Add(fromToKey, ticket);
                this.ticketCatalogByDateAndTimeKey.Add(ticket.DateAndTime, ticket);
                result = ticket.Type + " created";
            }

            if (result.Contains("created"))
            {
                this.busTicketsCount++;
            }

            return result;
        }

        public string DeleteBusTicket(string from, string to, string busCompany, DateTime dateTime)
        {
            BusTicket ticket = new BusTicket(from, to, busCompany, dateTime);
            string result = this.DeleteTicket(ticket);

            if (result.Contains("deleted"))
            {
                this.busTicketsCount--;
            }

            return result;
        }
       
        public string FindTickets(string from, string to)
        {
            string fromToKey = Ticket.CreateFromToKey(from, to);

            if (this.ticketCatalogByFromToKey.ContainsKey(fromToKey))
            {
                //List<Ticket> ticketsFound = new List<Ticket>();
                //foreach (var t in this.ticketCatalogByFromToKey.Values)
                //{
                //    if (t.FromToKey == fromToKey)
                //    {
                //        ticketsFound.Add(t);
                //    }
                //}

                string ticketsAsString = ReadTickets(this.ticketCatalogByFromToKey[fromToKey]);

                return ticketsAsString;
            }
            else
            {
                return "No matches";
            }
        }

        public string FindTicketsInRange(DateTime startDateTime, DateTime endDateTime)
        {
            string ticketsAsString = this.FindTicketsInInterval(startDateTime, endDateTime);

            return ticketsAsString;
        }

        public string FindTicketsInInterval2(DateTime startDateTime, DateTime endDateTime)
        {
            var ticketsFound = this.ticketCatalogByDateAndTimeKey.Values
                .Where(t => t.DateAndTime >= startDateTime)
                .Where(t => t.DateAndTime <= endDateTime).ToList();

            if (ticketsFound.Count > 0)
            {
                string ticketsAsString = ReadTickets(ticketsFound);

                return ticketsAsString;
            }
            else
            {
                return "No matches";
            }
        }

        public string FindTicketsInInterval(DateTime startDateTime, DateTime endDateTime)
        {
            var ticketsFound = this.ticketCatalogByDateAndTimeKey.Range(startDateTime, true, endDateTime, true).Values;

            if (ticketsFound.Count > 0)
            {
                string ticketsAsString = ReadTickets(ticketsFound);
                return ticketsAsString;
            }
            else
            {
                return "No matches";
            }
        }

        public string ExecuteCommand(string line)
        {
            int firstSpaceIndex = line.IndexOf(' ');

            if (firstSpaceIndex == -1)
            {
                return "Invalid command!";
            }

            string command = line.Substring(0, firstSpaceIndex);

            TicketObject ticketObject = this.GetParametersToObject(command, line, firstSpaceIndex);

            string output = "Invalid command!";

            switch (command)
            {
                case "CreateFlight":
                    output = this.AddAirTicket(
                        ticketObject.FlightNumber,
                        ticketObject.From, 
                        ticketObject.To,
                        ticketObject.CompanyName, 
                        ticketObject.DateAndTime, 
                        ticketObject.Price);

                    break;

                case "DeleteFlight":
                    output = this.DeleteAirTicket(ticketObject.FlightNumber);

                    break;

                case "CreateTrain":
                    output = this.AddTrainTicket(
                        ticketObject.From, 
                        ticketObject.To,
                        ticketObject.DateAndTime, 
                        ticketObject.Price, 
                        ticketObject.StudentPrice);

                    break;

                case "DeleteTrain":
                    output = this.DeleteTrainTicket(ticketObject.From, ticketObject.To, ticketObject.DateAndTime);

                    break;

                case "CreateBus":
                    output = this.AddBusTicket(
                        ticketObject.From,
                        ticketObject.To,
                        ticketObject.CompanyName,
                        ticketObject.DateAndTime,
                        ticketObject.Price);

                    break;

                case "DeleteBus":
                    output = this.DeleteBusTicket(
                        ticketObject.From,
                        ticketObject.To,
                        ticketObject.CompanyName,
                        ticketObject.DateAndTime);

                    break;

                case "FindTickets":
                    output = this.FindTickets(ticketObject.From, ticketObject.To);

                    break;

                case "FindByDates":
                    output = this.FindTicketsInRange(ticketObject.StartFindDateTime, ticketObject.EndFindDateTime);

                    break;
            }

            return output;
        }

        public int GetTicketsCount(TicketType type)
        {
            if (type == TicketType.Flight)
            {
                return this.airTicketsCount;
            }

            if (type == TicketType.Bus)
            {
                return this.busTicketsCount;
            }

            return this.trainTicketsCount;
        }

        private TicketObject GetParametersToObject(string command, string commandLine, int firstSpaceIndex)
        {
            TicketObject ticketObject = new TicketObject();

            string allParameters = commandLine.Substring(firstSpaceIndex + 1);
            string[] parameters = allParameters.Split(new char[] { '|' }, StringSplitOptions.RemoveEmptyEntries);

            for (int i = 0; i < parameters.Length; i++)
            {
                parameters[i] = parameters[i].Trim();
            }

            switch (command)
            {
                case "CreateFlight":
                    {
                        ticketObject.FlightNumber = parameters[0];
                        ticketObject.From = parameters[1];
                        ticketObject.To = parameters[2];
                        ticketObject.CompanyName = parameters[3];
                        ticketObject.DateAndTime = Utils.ParseDateTime(parameters[4]);
                        ticketObject.Price = decimal.Parse(parameters[5]);

                        break;
                    }

                case "DeleteFlight":
                    {
                        ticketObject.FlightNumber = parameters[0];

                        break;
                    }

                case "CreateTrain":
                    {
                        ticketObject.From = parameters[0];
                        ticketObject.To = parameters[1];
                        ticketObject.DateAndTime = Utils.ParseDateTime(parameters[2]);
                        ticketObject.Price = decimal.Parse(parameters[3]);
                        ticketObject.StudentPrice = decimal.Parse(parameters[4]);

                        break;
                    }

                case "DeleteTrain":
                    {
                        ticketObject.From = parameters[0];
                        ticketObject.To = parameters[1];
                        ticketObject.DateAndTime = Utils.ParseDateTime(parameters[2]);

                        break;
                    }

                case "CreateBus":
                    {
                        ticketObject.From = parameters[0];
                        ticketObject.To = parameters[1];
                        ticketObject.CompanyName = parameters[2];
                        ticketObject.DateAndTime = Utils.ParseDateTime(parameters[3]);
                        ticketObject.Price = decimal.Parse(parameters[4]);

                        break;
                    }

                case "DeleteBus":
                    {
                        ticketObject.From = parameters[0];
                        ticketObject.To = parameters[1];
                        ticketObject.CompanyName = parameters[2];
                        ticketObject.DateAndTime = Utils.ParseDateTime(parameters[3]);

                        break;
                    }

                case "FindTickets":
                    {
                        ticketObject.From = parameters[0];
                        ticketObject.To = parameters[1];

                        break;
                    }

                case "FindByDates":
                    {
                        ticketObject.StartFindDateTime = Utils.ParseDateTime(parameters[0]);
                        ticketObject.EndFindDateTime = Utils.ParseDateTime(parameters[1]);

                        break;
                    }                 
            }

            return ticketObject;
        }
    }
}