using System;
using VenatorNameSpace;

namespace VenatorNameSpace
{

    public class Vector
    {
        double xCoor, yCoor, zCoor;

        public Vector(double x, double y, double z)
        {
            this.xCoor = x;
            this.yCoor = y;
            this.zCoor = z;
        }
        public SphereVector toSpherical()
        {
            //When I fix this part, REMEMBER TO USE THE CORRECT COORDINATES!
            double r, theta, phi;
            theta = 0;
            phi = 0;

            double PI = Math.PI;

            r = this.mag();
            if (this.zCoor > 0) //Has issues with certain octants and with negative z values
            {
                theta = Math.Acos((this.zCoor / r));
            } else if (this.zCoor < 0)
            {
                theta = Math.PI - Math.Acos((-this.zCoor / r));
            } else if (this.zCoor == 0)
            {
                theta = Math.PI / 2;
            }
            if (this.xCoor > 0) //Quadrants I and IV
            {
                if(this.yCoor > 0) //Quadrant I
                {
                    phi = Math.Atan((this.yCoor / this.xCoor));
                } else if (this.yCoor < 0) //Quadrant IV 
                {
                    phi = 3 * PI * 0.5 + Math.Atan((this.xCoor / -this.yCoor));
                } else if (this.yCoor == 0) //Positive x-axis
                {
                    phi = 0;
                }
            } else if (this.xCoor < 0) //Quadrants II and III
            {
                if(this.yCoor > 0) //Quadrant II
                {
                    phi = PI * 0.5 + Math.Atan((-this.xCoor / this.yCoor));
                } else if (this.yCoor < 0) //Quadrant III
                {
                    phi = PI + Math.Atan((this.yCoor / this.xCoor));
                } else if (this.yCoor == 0) //Negative x-axis
                {
                    phi = PI;
                }
            } else if (this.xCoor == 0) //Y axis
            {
                if(this.yCoor > 0) //Positive y-axis
                {
                    phi = PI / 2;
                } else if (this.yCoor < 0) //Negative y-axis
                {
                    phi = 3 * PI / 2;
                } else if (this.yCoor == 0) //On the z-axis
                {
                    phi = 0; //At this point it doesn't really matter
                }
            }
            SphereVector spherical = new SphereVector(r, theta, phi);
            return spherical;
        }
        public Vector normalize()
        {
            double mag = this.mag();
            return new Vector(this.xCoor / mag, this.yCoor / mag, this.zCoor / mag);
        }
        public void trace()
        {
            Console.WriteLine("<" + this.xCoor + ", " + this.yCoor + ", " + this.zCoor + ">");
        }
        public static double dot(Vector vector1, Vector vector2)
        {
            double dotProd = 0;

            dotProd += vector1.xCoor * vector2.xCoor;
            dotProd += vector1.yCoor * vector2.yCoor;
            dotProd += vector1.zCoor * vector2.zCoor;

            return dotProd;
        }
        public static Vector cross(Vector vector1, Vector vector2)
        {
            double newX, newY, newZ;

            newX = vector1.yCoor * vector2.zCoor - vector1.zCoor * vector2.yCoor;
            newY = -(vector1.xCoor * vector2.zCoor - vector1.zCoor * vector2.xCoor);
            newZ = vector1.xCoor * vector2.yCoor - vector1.yCoor * vector2.xCoor;

            Vector outputVect = new Vector(newX,newY,newZ);

            return outputVect;
        }
        public double mag()
        {
            double magnitude = (Math.Pow(this.xCoor*this.xCoor + this.yCoor*this.yCoor + this.zCoor*this.zCoor,(0.5)));
            return magnitude;
        }

        //Need to override operators for vector addition/subtraction (done) and scalar multiplication/division (how do I deal with order?)
        public static Vector operator +(Vector vector1, Vector vector2)
        {
            return new Vector(vector1.xCoor + vector2.xCoor, vector1.yCoor + vector2.yCoor, vector1.zCoor + vector2.zCoor);
        }
        public static Vector operator -(Vector vector1, Vector vector2)
        {
            return new Vector(vector1.xCoor - vector2.xCoor, vector1.yCoor - vector2.yCoor, vector1.zCoor - vector2.zCoor);
        }
        public static Vector operator *(double scalar, Vector vector)
        {
            return new Vector(vector.xCoor * scalar, vector.yCoor * scalar, vector.zCoor * scalar);
        }
    }

}