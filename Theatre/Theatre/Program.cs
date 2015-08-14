namespace Theatre
{
    using System;
    using System.Text;
    using System.Threading;
    using System.Collections.Generic;
    using System.Globalization;
    using System.Linq;
    using Theatre.Contracts;

    public static class ThatreMain
    {
        public static IPerformanceDatabase universal = new PerformanceDatabase();
        public static void Main()
        {
            Thread.CurrentThread.CurrentCulture = CultureInfo.InvariantCulture;

            while (true)
            {
                string commandLine = Console.ReadLine();

                if (commandLine == null)
                {
                    break;
                }

                if(commandLine != string.Empty)
                {
                    string result = ExecuteCommand(commandLine);
                    Console.WriteLine(result);
                }                
            }
        }

        public static string ExecuteCommand(string commandLine)
        {
            string[] commandAllParams = commandLine.Split( new[] { '(', ',', ')' }, StringSplitOptions.RemoveEmptyEntries);
            string command = commandAllParams[0];           
            string[] commandParams = commandAllParams
                .Skip(1)
                .Select(p => p.Trim())
                .ToArray();
            string commandResult;

            try
            {
                switch (command)
                {
                    case "AddTheatre":
                        {
                            commandResult = ExecuteTheatreCommands.ExecuteAddTheatreCommand(commandParams);
                            break;
                        }

                    case "PrintAllTheatres":
                        {
                            commandResult = ExecuteTheatreCommands.ExecutePrintAllTheatresCommand(); break;
                        }

                    case "AddPerformance":
                        {
                            string theatreName;
                            string performanceTitle;
                            DateTime startDateTime;
                            TimeSpan duration;
                            decimal price;

                            GetPerformanceParams(
                                commandParams, 
                                out theatreName,
                                out performanceTitle,
                                out startDateTime, 
                                out duration, 
                                out price);

                            ThatreMain.universal.AddPerformance(theatreName, performanceTitle, startDateTime, duration, price);
                            commandResult = "Performance added"; break;
                        }

                    case "PrintAllPerformances":
                        {
                            commandResult = ExecutePrintAllPerformancesCommand();
                            break;
                        }

                    case "PrintPerformances":
                        {
                            commandResult = PrintPerformances(commandParams);
                            break;
                        }

                    default:
                        {
                            commandResult = "Invalid command!";
                            break;
                        }
                }
            }

            catch (Exception ex)
            {
                commandResult = "Error: " + ex.Message;
            }

            return commandResult;
        }

        private static string PrintPerformances(string[] commandParams)
        {
            string commandResult;
            string theatre = commandParams[0];

            var performances = universal.ListPerformances(theatre)
                .Select(p =>
                {
                    string result1 = p.DateAndTime.ToString("dd.MM.yyyy HH:mm");
                    return string.Format("({0}, {1})", p.ThisPerformance, result1);
                })
                .ToList();

            if (performances.Any())
            {
                commandResult = string.Join(", ", performances);
            }
            else
            {
                commandResult = "No performances";
            }

            return commandResult;
        }

        private static void GetPerformanceParams(string[] commandParams, out string theatreName, out string performanceTitle, out DateTime startDateTime, out TimeSpan duration, out decimal price)
        {
            theatreName = commandParams[0];
            performanceTitle = commandParams[1];
            DateTime result = DateTime.ParseExact(commandParams[2], "dd.MM.yyyy HH:mm", CultureInfo.InvariantCulture);
            startDateTime = result;
            TimeSpan result2 = TimeSpan.Parse(commandParams[3]);
            duration = result2;
            decimal result3 = decimal.Parse(commandParams[4], NumberStyles.Float);
            price = result3;
        }

        private static string ExecutePrintAllPerformancesCommand()
        {
            var performances = ThatreMain.universal.ListAllPerformances().ToList();
            var result = String.Empty;

            if (performances.Any())
            {
                for (int i = 0; i < performances.Count; i++)
                {
                    var sb = new StringBuilder();
                    sb.Append(result);

                    if (i > 0)
                    {
                        sb.Append(", ");
                    }

                    string result1 = performances[i].DateAndTime.ToString("dd.MM.yyyy HH:mm");
                    sb.AppendFormat("({0}, {1}, {2})", performances[i].ThisPerformance, performances[i].Theatre, result1);
                    result = sb + "";
                }
                return result;
            }

            return "No performances";
        }
    }
}