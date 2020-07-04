using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace MyPhotoshop
{
    public struct Pixel
    {
        public Pixel(double r, double g, double b)
        {
            this.r = r;
            this.g = g;
            this.b = b;
        }

        private double r;
        private double g;
        private double b;

        private double Check(double value)
        {
            if (value > 1 || value < 0) throw new ArgumentException();
            return value;
        }
        public double R { get { return r; } set { r = Check(value); } }
        public double G { get { return g; } set { g = Check(value); } }
        public double B { get { return b; } set { b = Check(value); } }

        public static Pixel operator *(Pixel pixel, double n)
        {
            return new Pixel(pixel.R * n, pixel.G * n, pixel.B * n);
        }
    }
}


