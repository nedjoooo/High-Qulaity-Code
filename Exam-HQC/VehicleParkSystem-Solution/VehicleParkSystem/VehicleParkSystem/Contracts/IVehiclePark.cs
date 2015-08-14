namespace VehicleParkSystem.Contracts
{
    using System;
    using VehicleParkSystem.VehicleClasses;

    public interface IVehiclePark
    {
        /// <summary>
        /// Insert a new vehicle of type car to an existing Vehicle Park.
        /// </summary>
        /// <param name="car">The type car of the vehicle</param>
        /// <param name="sector">The sector of the Vehicle Park</param>
        /// <param name="placeNumber">The Place number of the car</param>
        /// <param name="startTime">The start time of downtime</param>
        /// <returns>Return a string "Car parked successfully at definitely place"</returns>
        string InsertCar(Car car, int sector, int placeNumber, DateTime startTime);

        /// <summary>
        /// Insert a new vehicle of type motorbike to an existing Vehicle Park.
        /// </summary>
        /// <param name="motorbike">The type motorbike of the vehicle</param>
        /// <param name="sector">The sector of the Vehicle Park</param>
        /// <param name="placeNumber">The Place number of the car</param>
        /// <param name="startTime">The start time of downtime</param>
        /// <returns>Return a string "Motorbike parked successfully at definitely place"</returns>
        string InsertMotorbike(Motorbike motorbike, int sector, int placeNumber, DateTime startTime);
        
        /// <summary>
        /// Insert a new vehicle of type truck to an existing Vehicle Park.
        /// </summary>
        /// <param name="truck">The type truck of the vehicle</param>
        /// <param name="sector">The sector of the Vehicle Park</param>
        /// <param name="placeNumber">The Place number of the car</param>
        /// <param name="startTime">The start time of downtime</param>
        /// <returns>Return a string "Truck parked successfully at definitely place"</returns>
        string InsertTruck(Truck truck, int sector, int placeNumber, DateTime startTime);
        
        /// <summary>
        /// Exit vechicle in Vehicle Park and remove them of Vehicle database
        /// </summary>
        /// <param name="licensePlate">License plate in vehicle</param>
        /// <param name="endTime">The end time of downtime</param>
        /// <param name="amountPaid">Amount of paid in time</param>
        /// <returns>Return object in the string format</returns>
        string ExitVehicle(string licensePlate, DateTime endTime, decimal amountPaid);
        
        /// <summary>
        /// Returns fill status of Vehicle Park
        /// </summary>
        /// <returns></returns>
        string GetStatus();
        
        /// <summary>
        /// Find return existing vehicle from Vehicle Park
        /// </summary>
        /// <param name="licensePlate"></param>
        /// <returns></returns>
        string FindVehicle(string licensePlate);
        
        /// <summary>
        /// Find and return existing vehicle by owner from Vehicle Park
        /// </summary>
        /// <param name="owner"></param>
        /// <returns></returns>
        string FindVehiclesByOwner(string owner);
    }
}
