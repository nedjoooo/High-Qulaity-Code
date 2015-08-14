namespace VehicleParkSystem.VehicleClasses
{
    using System.Text;
    using VehicleParkSystem.Contracts;

    public abstract class Vehicle : IVehicle
    {
        public Vehicle(string licensePlate, string owner, int reservedHours)
        {
            this.LicensePlate = licensePlate;
            this.Owner = owner;
            this.ReservedHours = reservedHours;
        }

        public string LicensePlate { get; private set; }

        public string Owner { get; private set; }

        public int ReservedHours { get; private set; }

        public decimal RegularRate { get; set; }

        public decimal OvertimeRate { get; set; }

        public override string ToString()
        {
            var output = new StringBuilder();

            output.AppendFormat("[{0}], owned by {1}", this.LicensePlate, this.Owner);

            return output.ToString();
        }
    }
}
