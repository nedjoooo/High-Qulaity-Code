namespace VehicleParkSystem
{
    using System;
    using System.Collections.Generic;
    using System.Web.Script.Serialization;
    using VehicleParkSystem.Contracts;
    using VehicleParkSystem.VehicleClasses;

    public class Execution
    {
        private VehiclePark vehiclePark;

        public class Command : ICommand
        {
            public Command(string str)
            {
                this.CommandName = str.Substring(0, str.IndexOf(' '));
                this.CommandParameters = new JavaScriptSerializer()
                    .Deserialize<Dictionary<string, string>>(str.Substring(str.IndexOf(' ') + 1));
            }

            public string CommandName { get; set; }

            public IDictionary<string, string> CommandParameters { get; set; }            
        }       

        public string ExecuteCommand(ICommand command)
        {
            if (command.CommandName != "SetupPark" && this.vehiclePark == null)
            {
                return "The vehicle park has not been set up";
            }

            switch (command.CommandName)
            {
                case "SetupPark":
                    {
                        this.vehiclePark = new VehiclePark(int.Parse(command.CommandParameters["sectors"]), int.Parse(command.CommandParameters["placesPerSector"]));
                        return "Vehicle park created";
                    }

                case "Park":
                    {
                        DateTime time = DateTime.Parse(command.CommandParameters["time"], null, System.Globalization.DateTimeStyles.RoundtripKind);
                        switch (command.CommandParameters["type"])
                        {
                            case "car":
                                {
                                    return this.vehiclePark.InsertCar(
                                        new Car(command.CommandParameters["licensePlate"],
                                            command.CommandParameters["owner"],
                                            int.Parse(command.CommandParameters["hours"])),
                                            int.Parse(command.CommandParameters["sector"]), 
                                            int.Parse(command.CommandParameters["place"]),
                                            time);
                                }

                            case "motorbike":
                                {
                                    return this.vehiclePark.InsertMotorbike(
                                        new Motorbike(command.CommandParameters["licensePlate"],
                                            command.CommandParameters["owner"],
                                            int.Parse(command.CommandParameters["hours"])),
                                            int.Parse(command.CommandParameters["sector"]), 
                                            int.Parse(command.CommandParameters["place"]),
                                            time);
                                }

                            case "truck":
                                {
                                    return this.vehiclePark.InsertTruck(
                                        new Truck(command.CommandParameters["licensePlate"],
                                            command.CommandParameters["owner"],
                                            int.Parse(command.CommandParameters["hours"])),
                                            int.Parse(command.CommandParameters["sector"]),
                                            int.Parse(command.CommandParameters["place"]),
                                            time);
                                }
                        }

                        break;
                    }

                case "Exit":
                    {
                        DateTime time = DateTime.Parse(command.CommandParameters["time"], null, System.Globalization.DateTimeStyles.RoundtripKind);
                        string output = this.vehiclePark.ExitVehicle(command.CommandParameters["licensePlate"], time, decimal.Parse(command.CommandParameters["paid"]));

                        return output;
                    }

                case "Status":
                    {
                        return this.vehiclePark.GetStatus();
                    }

                case "FindVehicle":
                    {
                        string result = this.vehiclePark.FindVehicle(command.CommandParameters["licensePlate"]);

                        return result;
                    }

                case "VehiclesByOwner":
                    {
                        return this.vehiclePark.FindVehiclesByOwner(command.CommandParameters["owner"]);
                    }

                default:
                    {
                        throw new IndexOutOfRangeException("Invalid command.");
                    }
            }

            return string.Empty;
        }
    }
}