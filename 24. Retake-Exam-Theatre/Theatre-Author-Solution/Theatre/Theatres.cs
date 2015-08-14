namespace Theatre
{
    using System;
    using System.Globalization;
    using System.Linq;
    using System.Threading;

    using Theatre.Contracts;

    public static class Theatres
    {
        private static IPerformanceDatabase performanceDb = new PerformanceDatabase();

        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            while (true)
            {
                string commandLine = Console.ReadLine();
                if (commandLine == null)
                {
                    // The end of the input is reached
                    return;
                }

                if (commandLine != string.Empty)
                {
                    string command = ExtractCommand(commandLine);
                    string[] parameters = ExtractCommandParameters(commandLine);
                    ExecuteCommand(command, parameters);
                }
            }
        }

        private static string ExtractCommand(string commandLine)
        {
            string[] commandParts = commandLine.Split('(');
            return commandParts[0];
        }

        private static string[] ExtractCommandParameters(string commandLine)
        {
            string[] commandParts = commandLine.Split(
                new[] { '(', ',', ')' }, StringSplitOptions.RemoveEmptyEntries);
            string[] parameters = commandParts.Skip(1).Select(p => p.Trim()).ToArray();
            return parameters;
        }

        private static void ExecuteCommand(string command, string[] parameters)
        {
            string cmdResult;
            try
            {
                switch (command)
                {
                    case "AddTheatre":
                        cmdResult = ExecuteAddTheatreCommand(parameters);
                        break;
                    case "PrintAllTheatres":
                        cmdResult = ExecutePrintAllTheatresCommand();
                        break;
                    case "AddPerformance":
                        cmdResult = ExecuteAddPerformanceCommand(parameters);
                        break;
                    case "PrintAllPerformances":
                        cmdResult = ExecutePrintAllPerformancesCommand();
                        break;
                    case "PrintPerformances":
                        cmdResult = ExecutePrintPerformancesCommand(parameters);
                        break;
                    default:
                        cmdResult = Constants.InvalidCommandMsg;
                        break;
                }
            }
            catch (Exception ex)
            {
                cmdResult = Constants.ErrorPrefixMsg + ex.Message;
            }

            Console.WriteLine(cmdResult);
        }

        private static string ExecuteAddTheatreCommand(string[] parameters)
        {
            string theatreName = parameters[0];
            performanceDb.AddTheatre(theatreName);
            return Constants.TheatreAddedMsg;
        }

        private static string ExecutePrintAllTheatresCommand()
        {
            var theatres = performanceDb.ListTheatres().ToList();
            if (theatres.Any())
            {
                return string.Join(", ", theatres);
            }

            return Constants.NoTheatresMsg;
        }

        private static string ExecuteAddPerformanceCommand(string[] parameters)
        {
            string theatreName = parameters[0];
            string performanceTitle = parameters[1];
            DateTime startDateTime = ParseDateTime(parameters[2]);
            TimeSpan duration = ParseDuration(parameters[3]);
            decimal price = ParseNumber(parameters[4]);
            performanceDb.AddPerformance(theatreName, performanceTitle, startDateTime, duration, price);
            return Constants.PerformanceAddedMsg;
        }

        private static string ExecutePrintAllPerformancesCommand()
        {
            var performances = performanceDb.ListAllPerformances()
                .Select(p => string.Format(
                    "({0}, {1}, {2})", p.Title, p.Theatre, FormatDateTime(p.StartDateTime)))
                .ToList();
            if (performances.Any())
            {
                return string.Join(", ", performances);
            }

            return Constants.NoPerformancesMsg;
        }

        private static string ExecutePrintPerformancesCommand(string[] parameters)
        {
            string theatre = parameters[0];
            var performances = performanceDb.ListPerformances(theatre)
                .Select(p => string.Format(
                    "({0}, {1})", p.Title, FormatDateTime(p.StartDateTime)))
                .ToList();
            if (performances.Any())
            {
                return string.Join(", ", performances);
            }

            return Constants.NoPerformancesMsg;
        }

        private static DateTime ParseDateTime(string dateTimeStr)
        {
            DateTime result = DateTime.ParseExact(
                dateTimeStr, Constants.DateTimeFormat, CultureInfo.InvariantCulture);
            return result;
        }
        
        private static string FormatDateTime(DateTime dateTime)
        {
            string result = dateTime.ToString(Constants.DateTimeFormat);
            return result;
        }

        private static TimeSpan ParseDuration(string durationStr)
        {
            TimeSpan result = TimeSpan.Parse(durationStr);
            return result;
        }

        private static decimal ParseNumber(string numberStr)
        {
            decimal result = decimal.Parse(numberStr, NumberStyles.Float);
            return result;
        }
    }
}
