namespace VehicleParkSystem.VehicleClasses
{
    using System.Text;

    public class Car : Vehicle
    {
        public Car(string licensePlate, string owner, int reservedHours)
            : base(licensePlate, owner, reservedHours)
        {
        }

        public override string ToString()
        {
            var output = new StringBuilder();

            output.Append("Car ");
            output.Append(base.ToString());

            return output.ToString();
        }
    }
}
