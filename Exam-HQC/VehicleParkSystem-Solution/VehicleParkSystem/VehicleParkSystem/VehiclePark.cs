namespace VehicleParkSystem
{
    using System;
    using System.Collections.Generic;
    using System.Linq;   
    using System.Text;
    using VehicleParkSystem.Contracts;
    using VehicleParkSystem.VehicleClasses;
    using Wintellect.PowerCollections;

    public class VehiclePark : IVehiclePark
    {
        private ParkSize parkSize;
        private VehicleDatabase vehicleDatabase;

        public VehiclePark(int numberOfSectors, int placesPerSector)
        {
            this.parkSize = new ParkSize(numberOfSectors, placesPerSector);
            this.vehicleDatabase = new VehicleDatabase(numberOfSectors);
        }

        public string InsertCar(Car car, int sector, int place, DateTime time)
        {
            if (sector > this.parkSize.NumberOfSectors)
            {
                return string.Format("There is no sector {0} in the park", sector);
            }

            if (place > this.parkSize.PlacesPerSector)
            {
                return string.Format("There is no place {0} in sector {1}", place, sector);
            }

            if (this.vehicleDatabase.Park.ContainsKey(string.Format("({0},{1})", sector, place)))
            {
                return string.Format("The place ({0},{1}) is occupied", sector, place);
            }

            if (this.vehicleDatabase.VehiclesRegistrationNumber.ContainsKey(car.LicensePlate))
            {
                return string.Format("There is already a vehicle with license plate {0} in the park", car.LicensePlate);
            }

            this.vehicleDatabase.VehiclesPark[car] = string.Format("({0},{1})", sector, place);
            this.vehicleDatabase.Park[string.Format("({0},{1})", sector, place)] = car;
            this.vehicleDatabase.VehiclesRegistrationNumber[car.LicensePlate] = car;
            this.vehicleDatabase.DateAndTime[car] = time;
            this.vehicleDatabase.VehicleOwner[car.Owner].Add(car);
            this.vehicleDatabase.VehiclesCount[sector - 1]++;

            return string.Format("{0} parked successfully at place ({1},{2})", car.GetType().Name, sector, place);
        }

        public string InsertMotorbike(Motorbike motorbike, int sector, int place, DateTime time)
        {
            if (sector > this.parkSize.NumberOfSectors)
            {
                return string.Format("There is no sector {0} in the park", sector);
            }

            if (place > this.parkSize.PlacesPerSector)
            {
                return string.Format("There is no place {0} in sector {1}", place, sector);
            }

            if (this.vehicleDatabase.Park.ContainsKey(string.Format("({0},{1})", sector, place)))
            {
                return string.Format("The place ({0},{1}) is occupied", sector, place);
            }

            if (this.vehicleDatabase.VehiclesRegistrationNumber.ContainsKey(motorbike.LicensePlate))
            {
                return string.Format("There is already a vehicle with license plate {0} in the park", motorbike.LicensePlate);
            }

            this.vehicleDatabase.VehiclesPark[motorbike] = string.Format("({0},{1})", sector, place);
            this.vehicleDatabase.Park[string.Format("({0},{1})", sector, place)] = motorbike;
            this.vehicleDatabase.VehiclesRegistrationNumber[motorbike.LicensePlate] = motorbike;
            this.vehicleDatabase.DateAndTime[motorbike] = time;
            this.vehicleDatabase.VehicleOwner[motorbike.Owner].Add(motorbike);
            this.vehicleDatabase.VehiclesCount[sector - 1]++;

            return string.Format("{0} parked successfully at place ({1},{2})", motorbike.GetType().Name, sector, place);
        }

        public string InsertTruck(Truck truck, int sector, int place, DateTime time)
        {
            if (sector > this.parkSize.NumberOfSectors)
            {
                return string.Format("There is no sector {0} in the park", sector);
            }

            if (place > this.parkSize.PlacesPerSector)
            {
                return string.Format("There is no place {0} in sector {1}", place, sector);
            }

            if (this.vehicleDatabase.Park.ContainsKey(string.Format("({0},{1})", sector, place)))
            {
                return string.Format("The place ({0},{1}) is occupied", sector, place);
            }

            if (this.vehicleDatabase.VehiclesRegistrationNumber.ContainsKey(truck.LicensePlate))
            {
                return string.Format("There is already a vehicle with license plate {0} in the park", truck.LicensePlate);
            }

            this.vehicleDatabase.VehiclesPark[truck] = string.Format("({0},{1})", sector, place);
            this.vehicleDatabase.Park[string.Format("({0},{1})", sector, place)] = truck;
            this.vehicleDatabase.VehiclesRegistrationNumber[truck.LicensePlate] = truck;
            this.vehicleDatabase.DateAndTime[truck] = time;
            this.vehicleDatabase.VehicleOwner[truck.Owner].Add(truck);
            this.vehicleDatabase.VehiclesCount[sector - 1]++;

            return string.Format("{0} parked successfully at place ({1},{2})", truck.GetType().Name, sector, place);
        }

        public string ExitVehicle(string l_pl, DateTime endTime, decimal money)
        {
            var vehicle = this.vehicleDatabase.VehiclesRegistrationNumber.ContainsKey(l_pl) ? this.vehicleDatabase.VehiclesRegistrationNumber[l_pl] : null;

            if (vehicle == null)
            {
                return string.Format("There is no vehicle with license plate {0} in the park", l_pl);
            }

            var startTime = this.vehicleDatabase.DateAndTime[vehicle];
            int totalTime = (int)Math.Round((endTime - startTime).TotalHours);
            var ticket = new StringBuilder();
            ticket.AppendLine(
                new string('*', 20)).AppendFormat("{0}", vehicle.ToString()).AppendLine().AppendFormat("at place {0}",
                    this.vehicleDatabase.VehiclesPark[vehicle])
                        .AppendLine()
                        .AppendFormat("Rate: ${0:F2}", vehicle.ReservedHours * vehicle.RegularRate)
                        .AppendLine()
                        .AppendFormat("Overtime rate: ${0:F2}", totalTime > vehicle.ReservedHours ? (totalTime - vehicle.ReservedHours) * vehicle.OvertimeRate : 0)
                        .AppendLine()
                        .AppendLine(new string('-', 20))
                        .AppendFormat("Total: ${0:F2}", vehicle.ReservedHours * vehicle.RegularRate + (totalTime > vehicle.ReservedHours ? (totalTime - vehicle.ReservedHours) * vehicle.OvertimeRate : 0))
                        .AppendLine()
                        .AppendFormat("Paid: ${0:F2}", money)
                        .AppendLine()
                        .AppendFormat("Change: ${0:F2}", money - ((vehicle.ReservedHours * vehicle.RegularRate) + (totalTime > vehicle.ReservedHours ? (totalTime - vehicle.ReservedHours) * vehicle.OvertimeRate : 0)))
                        .AppendLine()
                        .Append(new string('*', 20));

            int sector = int.Parse(this.vehicleDatabase.VehiclesPark[vehicle].Split(new[] { "(", ",", ")" }, StringSplitOptions.RemoveEmptyEntries)[0]);

            this.vehicleDatabase.Park.Remove(this.vehicleDatabase.VehiclesPark[vehicle]);
            this.vehicleDatabase.VehiclesPark.Remove(vehicle);
            this.vehicleDatabase.VehiclesRegistrationNumber.Remove(vehicle.LicensePlate);
            this.vehicleDatabase.DateAndTime.Remove(vehicle);
            this.vehicleDatabase.VehicleOwner.Remove(vehicle.Owner, vehicle);
            this.vehicleDatabase.VehiclesCount[sector - 1]--;
            
            return ticket.ToString();
        }

        public string GetStatus()
        {
            var places = this.vehicleDatabase.VehiclesCount
                .Select((countVehicles, sector) => string.Format(
                    "Sector {0}: {1} / {2} ({3}% full)",
                    sector + 1,
                    countVehicles,
                    this.parkSize.PlacesPerSector,
                    Math.Round((double)countVehicles / this.parkSize.PlacesPerSector * 100)));

            return string.Join(Environment.NewLine, places);
        }

        public string FindVehicle(string l_pl)
        {
            var vehicle = this.vehicleDatabase.VehiclesRegistrationNumber.ContainsKey(l_pl) ? this.vehicleDatabase.VehiclesRegistrationNumber[l_pl] : null;

            if (vehicle == null)
            {
                return string.Format("There is no vehicle with license plate {0} in the park", l_pl);
            }
                
            return this.Output(new[] { vehicle });
        }

        public string FindVehiclesByOwner(string owner)
        {
            if (!this.vehicleDatabase.Park.Values.Where(v => v.Owner == owner).Any())
            {
                return string.Format("No vehicles by {0}", owner);
            }
            else
            {
                var foundVehicles = this.vehicleDatabase.Park.Values.ToList();
                var result = foundVehicles;

                foreach (var vehicle in foundVehicles)
                {
                    result = result.Where(v => v.Owner == owner).ToList();
                }

                return string.Join(Environment.NewLine, this.Output(result));
            }
        }

        private string Output(IEnumerable<IVehicle> vehicles)
        {
            var output = string.Join(
                Environment.NewLine, 
                vehicles.Select(
                    vehicle => string.Format(
                        "{0}{1}Parked at {2}", 
                        vehicle.ToString(),
                        Environment.NewLine,
                        this.vehicleDatabase.VehiclesPark[vehicle])));

            return output;
        }
    }
}