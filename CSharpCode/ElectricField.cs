using System;
using VenatorNameSpace;

namespace VenatorNameSpace
{
    public class Program
    {
        public static void Main()
        {
            Vector integralSum = new Vector(0, 0, 0);
            double TAU = 2 * Math.PI;
            double PI = Math.PI;
            int n = 1000;
            double R = 1.0;
            double theta, phi;

            double theta1 = 1 * PI / 180; //*
            double theta2 = PI;
            double phi1 = 0.0;
            double phi2 = TAU;

            double dtheta = (theta2 - theta1) / n;
            double dphi = (phi2 - phi1) / n;
            Console.WriteLine(dtheta + " " + dphi);
            Console.WriteLine(dtheta * 180 / PI  + " " + dphi * 180 / PI);

            for (double z = 0; z < 5.01; z += 0.01)
            {
                integralSum = new Vector(0, 0, 0);
                Vector testPoint = new Vector(0, 0, z);
                for (int i = 0; i < n; i++) //theta
                {
                    theta = theta1 + (i + 0.5) * dtheta;
                    for (int j = 0; j < n; j++) //phi
                    {
                        phi = phi1 + (j + 0.5) * dphi;

                        SphereVector currentVector = new SphereVector(R, theta, phi);
                        Vector shellVector = currentVector.toCartesian() - testPoint;
                        SphereVector toShell = shellVector.toSpherical();
                        //currentVector.trace();
                        SphereVector currentField = new SphereVector(0, 0, 0);
                        currentField = Math.Sin(theta) * dtheta * dphi * Math.Pow(toShell.r, -2) * toShell.normalize();
                        //currentField.trace();
                        integralSum = integralSum + currentField.toCartesian();

                        /**
                        SphereVector currentSphere = new SphereVector(R, theta, phi);
                        Vector currentVector = currentSphere.toCartesian();
                        Vector currentField = ????
                        **/
                        
                        //integralSum.trace();
                    }
                }
                Console.WriteLine("Z = " + z);
                integralSum.trace();
            }
            //integralSum.trace();
            //Console.WriteLine(4 * PI);
        }
    }
}
