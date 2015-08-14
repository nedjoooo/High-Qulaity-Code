namespace Theatre
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    public static class ExecuteTheatreCommands
    {
        internal static string ExecuteAddTheatreCommand(string[] parameters)
        {
            string theatreName = parameters[0];
            ThatreMain.universal.AddTheatre(theatreName);
            return "Theatre added";
        }

        public static string ExecutePrintAllTheatresCommand()
        {
            var theatresCount = ThatreMain.universal.ListTheatres().Count();

            if (theatresCount > 0)
            {
                var resultTheatres = new LinkedList<string>();

                for (int i = 0; i < theatresCount; i++)
                {
                    ThatreMain.universal.ListTheatres()
                        .Skip(i)
                        .ToList()
                        .ForEach(t => resultTheatres.AddLast(t));

                    foreach (var t in ThatreMain.universal.ListTheatres().Skip(i + 1))
                    {
                        resultTheatres.Remove(t);
                    }

                }

                return String.Join(", ", resultTheatres);
            }

            return "No theatres";
        }
    }
}
