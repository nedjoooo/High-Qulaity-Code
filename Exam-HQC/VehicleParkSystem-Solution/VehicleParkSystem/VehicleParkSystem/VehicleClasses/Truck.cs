namespace VehicleParkSystem.VehicleClasses
{
    using System.Text;

    public class Truck : Vehicle
    {
        public Truck(string licensePlate, string owner, int reservedHours)
            : base(licensePlate, owner, reservedHours)
        {
        }

        public override string ToString()
        {
            var output = new StringBuilder();

            output.Append("Truck ");
            output.Append(base.ToString());

            return output.ToString();
        }
    }
}
