namespace VehicleParkSystem.VehicleClasses
{
    using System.Text;

    public class Motorbike : Vehicle
    {
        public Motorbike(string licensePlate, string owner, int reservedHours)
            : base(licensePlate, owner, reservedHours)
        {
        }

        public override string ToString()
        {
            var output = new StringBuilder();

            output.Append("Motorbike ");
            output.Append(base.ToString());

            return output.ToString();
        }
    }
}
