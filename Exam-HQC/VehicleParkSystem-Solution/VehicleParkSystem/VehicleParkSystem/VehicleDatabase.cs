namespace VehicleParkSystem
{
    using System;
    using System.Collections.Generic;
    using VehicleParkSystem.Contracts;
    using Wintellect.PowerCollections;

    public class VehicleDatabase
    {
        public VehicleDatabase(int numberOfSectors)
        {
            this.VehiclesPark = new Dictionary<IVehicle, string>();
            this.Park = new Dictionary<string, IVehicle>();
            this.VehiclesRegistrationNumber = new Dictionary<string, IVehicle>();
            this.DateAndTime = new Dictionary<IVehicle, DateTime>();
            this.VehicleOwner = new MultiDictionary<string, IVehicle>(false);
            this.VehiclesCount = new int[numberOfSectors];
        }

        #region Hard Stuff! My boss wrote that

        public Dictionary<IVehicle, string> VehiclesPark { get; set; }

        public Dictionary<string, IVehicle> Park { get; set; }

        public Dictionary<string, IVehicle> VehiclesRegistrationNumber { get; set; }

        public Dictionary<IVehicle, DateTime> DateAndTime { get; set; }

        public MultiDictionary<string, IVehicle> VehicleOwner { get; set; }

        public int[] VehiclesCount { get; set; }

        #endregion
    }
}
