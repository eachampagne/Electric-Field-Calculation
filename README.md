# Electric-Field-Calculation
Code from an E&amp;M I project

See "PHYS 331 Honors Computational Project.pdf" for project description, and "331HonorsWriteUp.docx" for my final paper I turned in. "Graphs.png" shows my computational results vs. the analytical solution.

For this project, I created two custom classes, a Cartesian vector class, and a spherical vector class. (I'm sure others have created comprable classes, but I didn't want to hunt down appropriate libraries when I could do it myself.) I originally wrote my code in C#, because I was working on a Unity project at the time, and hadn't learned C++ yet. 

After I got results, I realized that graphing anything in C# is rather complicated, so I rewrote my code in Python so I could take advantage of MatPlotLib. The corresponding Python classes are written in "ElectricField.py". "ElectricFieldGrapher.py" simply plots the data from the former program, which kept me from recalculating everything every time I ran the graphing part. (In retrospect, I could have run the Python grapher on the C# output, but I wouldn't have learned classes for Python...)

My integrater is a simple Riemann style "calculate tiny sections and add them all up." I used an excessive number of divisions- 1000 in phi and 1000 in theta (for a total of 1 million calculations for each data point), which is why my code takes so long to run (especially in Python). I considered lowering this, but then I wouldn't have had the (questionable) bragging rights of forcing my poor laptop to perform a million calculations.

If I did this project again, I would code in C++. I'd either use gnuplot to graph, since the graph itself is not complicated, or I'd write a python program to graph the data from the C++ code, stored in a text file. Python worked, but was much slower than C# or C++.
