namespace OfficeTicket
{
    using System;

    public class OfficeTicketMain
    {
        public static void Main()
        {
            TicketRepository ticketRepository = new TicketRepository();

            while (true)
            {
                string commandLine = Console.ReadLine();

                if (commandLine == null)
                {
                    break;
                }

                if(commandLine != string.Empty)
                {
                    commandLine = commandLine.Trim();
                    string commandResult = ticketRepository.ExecuteCommand(commandLine);
                    Console.WriteLine(commandResult);
                }              
            }
        }
    }
}






