namespace VehicleParkSystem
{
    using System;
    using VehicleParkSystem;    
    using VehicleParkSystem.Contracts;

    public class Engine : IEngine
    {
        private Execution ex;

        public Engine(Execution ex)
        {
            this.ex = ex;
        }

        public Engine() : this(new Execution())
        {
        }

        public void Run()
        {
            while (true)
            {
                string commandLine = Console.ReadLine();
                if (commandLine == null)
                {
                    break;
                }

                commandLine.Trim();
                if (!string.IsNullOrEmpty(commandLine))
                {
                    try
                    {
                        var command = new Execution.Command(commandLine);
                        string commandResult = this.ex.ExecuteCommand(command);
                        Console.WriteLine(commandResult);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex.Message);
                    }
                }
            }
        }
    }
}