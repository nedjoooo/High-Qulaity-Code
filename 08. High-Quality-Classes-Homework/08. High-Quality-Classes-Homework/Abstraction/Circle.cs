using System;

namespace Abstraction
{
    class Circle : Figure
    {
        private double radius;
        public double Radius
        {
            get
            { 
                return this.radius;
            }
            set
            {
                if(value <= 0)
                {
                    throw new ArgumentNullException("Radius can not be negative or zero!");
                }
                this.radius = value;
            }
        }

        public Circle(double radius)
        {
            this.Radius = radius;
        }

        public override double CalcPerimeter()
        {
            double perimeter = 2 * Math.PI * this.Radius;
            return perimeter;
        }

        public override double CalcSurface()
        {
            double surface = Math.PI * this.Radius * this.Radius;
            return surface;
        }
    }
}
