import matplotlib.pyplot as plt
import numpy as np
import numpy.ma as M
import math

#Initiate plot
fig = plt.figure()

#Create a list of z values
z = np.linspace(0,5,501)

#Opens a text file containing the output of ElectricField.py, reformatted to have only the z-values
#I could have appended all the graphing stuff to the end of the other program, but that would require recalculating the field every
#time I wanted to regraph
f = open("PythonZOutputReal.txt", 'r');
zOutput = []

#Collect calculated field values into a list
for i in f:
    zOutput.append(float(i))

f.close()

k = 1
R = 1
Q = 1
PI = math.pi
degCos = math.cos(PI/180)
EPSILON_NAUGHT = 8.854187817*pow(10,-12) #F/m

#Formula I derived, with adjustments so the "units" match
eField = -2*PI*((R+z)/(z*z*pow((z*z+R*R+2*z*R),0.5))-(R-z*degCos)/(z*z*pow((z*z+R*R-2*z*R*degCos),0.5)))

#Actually graph stuff
plt.plot(z,eField,'r',label="Analytical Field")
plt.scatter(z,zOutput,10,label="Computational Field")

#Labels and such
plt.title('Electric Field of a Charged Shell with a Hole')
plt.legend()
plt.xlabel('Positions along Z-axis (R)')
plt.ylabel('Z-Component of the Electric Field (Q/(A4πε(0)))')

#Actually display stuff
plt.show()