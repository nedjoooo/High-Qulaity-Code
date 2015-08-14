namespace vp_himineu.Contracts
{
    using System;

    // TODO: Documente esta contrato
    public interface IVehiclePark
    {
        // TODO: Documentar esse método
        string InsertCar(Carro car, int sector, int placeNumber, DateTime startTime);
        // TODO: Documentar esse método
        string InsertMotorbike(Moto motorbike, int sector, int placeNumber, DateTime startTime);
        // TODO: Documentar esse método
        string InsertTruck(Caminhão truck, int sector, int placeNumber, DateTime startTime);
        // TODO: Documentar esse método
        string ExitVehicle(string licensePlate, DateTime endTime, decimal amountPaid);
        // TODO: Documentar esse método
        string GetStatus();
        // TODO: Documentar esse método
        string FindVehicle(string licensePlate);
        // TODO: Documentar esse método
        string FindVehiclesByOwner(string owner);
    }

}
