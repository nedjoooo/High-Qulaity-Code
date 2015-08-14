namespace VehicleParkSystem
{
    using System;

    public class ParkSize
    {
        private int numberOfSectors;
        private int placesPerSector;

        public ParkSize(int numberOfSectors, int placesPerSector)
        {   
            this.NumberOfSectors = numberOfSectors;
            this.PlacesPerSector = placesPerSector;
        }

        public int NumberOfSectors
        {
            get
            {
                return this.numberOfSectors;
            }

            set
            {
                if (value <= 0)
                {
                    throw new DivideByZeroException("The number of sectors must be positive.");
                }

                this.numberOfSectors = value;
            }
        }

        public int PlacesPerSector
        {
            get
            {
                return this.placesPerSector;
            }

            set
            {
                if (value <= 0)
                {
                    throw new DivideByZeroException("The number of places per sector must be positive.");
                }

                this.placesPerSector = value;
            }
        }
    }
}
