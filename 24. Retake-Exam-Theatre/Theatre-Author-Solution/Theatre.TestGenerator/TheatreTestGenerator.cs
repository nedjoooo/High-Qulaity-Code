using System;
using System.Linq;

class TheatreTestGenerator
{
    const int TheatresCount = 5;
    const int PerformancesCount = 300;
    const int PrintPerformancesCount = 10;
    const int PrintAllPerformancesCount = 3;
    const int PrintAllTheatresCount = 1;

    private static Random random = new Random();

    static void Main()
    {
        var theatreNames = 
            Enumerable.Range(1, TheatresCount)
            .Select(i => "T" + i).ToList();
        foreach (var theatre in theatreNames)
        {
            Console.WriteLine("AddTheatre({0})", theatre);
        }

        var performanceNames = 
            Enumerable.Range(1, PerformancesCount)
            .Select(i => "P" + i).ToList();
        foreach (var performance in performanceNames)
        {
            string theatre = theatreNames[random.Next(theatreNames.Count)];
            DateTime dateTime =
                new DateTime(2015, 1, 1)
                .AddDays(random.Next(PerformancesCount / 10))
                .AddHours(random.Next(96) / 4.0);
            TimeSpan duration = new TimeSpan(random.Next(3), random.Next(12) * 5, 0);
            decimal price = (decimal)random.Next(1, 200) / 10;
            Console.WriteLine(
                "AddPerformance({0}, {1}, {2:dd.MM.yyyy HH:mm}, {3:hh':'mm}, {4})", 
                theatre,
                performance,
                dateTime,
                duration,
                price);
        }

        for (int i = 0; i < PrintPerformancesCount; i++)
        {
            string theatre = theatreNames[random.Next(theatreNames.Count)];
            Console.WriteLine("PrintPerformances({0})", theatre);
        }

        for (int i = 0; i < PrintAllPerformancesCount; i++)
        {
            Console.WriteLine("PrintAllPerformances()");
        }

        for (int i = 0; i < PrintAllTheatresCount; i++)
        {
            Console.WriteLine("PrintAllTheatres()");
        }
    }
}
