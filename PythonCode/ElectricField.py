class Vector:    
    def __init__(self, x, y, z):
        self.x = x
        self.y = y
        self.z = z

    def normalize(self):
        mag = self.mag()
        return Vector(self.x / mag, self.y / mag, self.z / mag)

    def trace(self):
        print("<" + str(self.x) + ", " + str(self.y) + ", " + str(self.z) + ">")

    def toSpherical(self):
        import math
        r = self.mag()
        theta = 0
        phi = 0
        if (self.z > 0):
            theta = math.acos((self.z / r))
        elif (self.z < 0):
            theta = math.pi - math.acos((-self.z / r))
        elif (self.z == 0):
            theta = math.pi / 2
        if (self.x > 0): #Quadrants I and IV
            if(self.y > 0): #Quadrant I
                phi = math.atan((self.y / self.x))
            elif (self.y < 0): #Quadrant IV
                phi = 3 * math.pi * 0.5 + math.atan((self.x / -self.y))
            elif (self.y == 0): #Positive x-axis
                phi = 0
        elif (self.x < 0): #Quadrants II and III
            if(self.y > 0): #Quadrant II
                phi = math.pi * 0.5 + math.atan((-self.x / self.y))
            elif (self.y < 0): #Quadrant III
                phi = math.pi + math.atan((self.y / self.x))
            elif (self.y == 0): #Negative x-axis
                phi = math.pi
        elif (self.x == 0): #Y axis
            if(self.y > 0): #Positive y-axis
                phi = math.pi / 2
            elif (self.y < 0): #Negative y-axis
                phi = 3 * math.pi / 2
            elif (self.y == 0): #On the z-axis
                phi = 0 #At this point it doesn't really matter, as long as it's defined
        spherical = SphereVector(r, theta, phi)
        return spherical

    def __add__(self,other):
        x = self.x + other.x
        y = self.y + other.y
        z = self.z + other.z
        return Vector(x,y,z)

    def __sub__(self,other):
        x = self.x - other.x
        y = self.y - other.y
        z = self.z - other.z
        return Vector(x,y,z)

    def __mul__(self,other):
        x = self.x * other
        y = self.y * other
        z = self.z * other
        return Vector(x,y,z)
    
    @staticmethod
    def dot(vect1, vect2):
        return vect1.x * vect2.x + vect1.y * vect2.y + vect1.z * vect2.z

    @staticmethod
    def cross(vect1, vect2):
        newX = vect1.y * vect2.z - vect1.z * vect2.y
        newY = -(vect1.x * vect2.z - vect1.z * vect2.x)
        newZ = vect1.x * vect2.y - vect1.y * vect2.x
        return Vector(newX,newY,newZ)

    def mag(self):
        magnitude = (pow(self.x**2 + self.y**2 + self.z**2,(0.5)))
        return magnitude

class SphereVector:
    def __init__(self, r, theta, phi):
        self.r = r
        self.theta = theta
        self.phi = phi

    def trace(self):
        print("<" + str(self.r) + ", " + str(self.theta) + ", " + str(self.phi) + ">")

    def toCartesian(self):
        import math
        x = self.r * math.cos(self.phi) * math.sin(self.theta)
        y = self.r * math.sin(self.phi) * math.sin(self.theta)
        z = self.r * math.cos(self.theta)
        return Vector(x, y, z)

    def normalize(self):
        return SphereVector(1, self.theta, self.phi)

    def __mul__(self,other):
        newR = self.r * other
        return SphereVector(newR,self.theta,self.phi)

    #Can I overload operators to add a multiplication function? Apparently, yes!
        
import math

integralSum = Vector(0, 0, 0)

#A few constants
TAU = 2 * math.pi
PI = math.pi
EPSILON_NAUGHT = 8.854187817*pow(10,-12) #F/m

n = 1000 #number of divisions
R = 1.0 #arbitrary length
Q = 1.0 #arbitrary charge

#Bounds of integration
theta1 = 1 * PI / 180
theta2 = PI
phi1 = 0.0
phi2 = TAU

dtheta = (theta2 - theta1) / n
dphi = (phi2 - phi1) / n

#Integrate for A
A = 0
for j in range(n): #theta
        theta = theta1 + (j + 0.5) * dtheta
        for k in range(n): #phi
            phi = phi1 + (k + 0.5) * dphi
            A += (math.sin(theta) * dtheta * dphi * pow(R,2))

SIGMA = Q / A #Surface charge density
CONSTANTS = SIGMA / (4 * PI * EPSILON_NAUGHT) #I decided this is a bad idea

#integrate the actual field
for i in range(501):
    z = i / 100
    integralSum = Vector(0, 0, 0)
    testPoint = Vector(0, 0, z)
    for j in range(n): #theta
        theta = theta1 + (j + 0.5) * dtheta
        for k in range(n): #phi
            phi = phi1 + (k + 0.5) * dphi
            currentVector = SphereVector(R, theta, phi) #coordinates of point on shell
            shellVector = currentVector.toCartesian() - testPoint #from z-axis to shell
            toShell = shellVector.toSpherical() #from z-axis to shell, in spherical coordinates
            currentField = SphereVector(0, 0, 0)
            currentField = toShell.normalize() * (1 * (math.sin(theta) * dtheta * dphi * pow(toShell.r, -2)))
            integralSum = integralSum + currentField.toCartesian()    
    #print(z)
    integralSum.trace()