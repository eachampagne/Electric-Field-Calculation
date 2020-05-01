using System;
using VenatorNameSpace;

//This should have a cartesian property, not just a method! (why?)
//Should this extend the regular vector class??
namespace VenatorNameSpace
{
    public class SphereVector
    {
        public double r, theta, phi;
        public SphereVector(double r, double theta, double phi)
        {
            this.r = r;
            this.theta = theta;
            this.phi = phi;
        }
        public Vector toCartesian()
        {
            double x, y, z;
            x = this.r * Math.Cos(this.phi) * Math.Sin(this.theta);
            y = this.r * Math.Sin(this.phi) * Math.Sin(this.theta);
            z = this.r * Math.Cos(this.theta);

            Vector cartesian = new Vector(x, y, z);
            return cartesian;
        }
        public SphereVector normalize()
        {
            return new SphereVector(1, this.theta, this.phi);
        }
        public void trace()
        {
            Console.WriteLine("<" + this.r + ", " + this.theta + ", " + this.phi + ">");
        }
        public static SphereVector operator *(double scalar, SphereVector vector)
        {
            return new SphereVector(vector.r * scalar, vector.theta, vector.phi);
        }
    }
}
