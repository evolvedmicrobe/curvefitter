#Users

Users of the program can see documentation, tutorials and sample data at http://www.evolvedmicrobe.com/CurveFitter/.  Any issues can be reported on the issue tracker, and questions can be asked at the discussions tab.

#Developers

##Organization of the code

The code was originally written using the visual studio IDE and the source files contain solution folders for this program.  The code is logically divided into three different projects which reflect the different functions of the program.

* CurveFitterGUI – This is the code for the GUI that handles all user interactions.
* GrowthCurveLibrary – This is a library that contains growth curve fitting functions used by both the scripting environment and the GUI.  Code in here handles all of the data import/export as well as implementing the different model fits.  Data is organized into  GrowthCurve classes.  All the data in a GrowthCurve class is fit using classes that derive from the AbstractFitter which handles various common tasks such as computing residuals, etc.  The folder ModelsAndFitting contains all the models implemented.
* MatrixArrayPlot – This class plots Matrices as heatmaps, which are used by both the scripting environment and the GUI.
Dependencies

The code uses a number of libraries written by others.  They are:

Library

Function

| Library                           | Function                                         |
|-----------------------------------|--------------------------------------------------|
| [Sho: the .NET Playground for Data](https://www.microsoft.com/en-us/research/project/sho-the-net-playground-for-data/?from=http%3A%2F%2Fresearch.microsoft.com%2Fen-us%2Fprojects%2Fsho%2F) | This is the basis for the scripting environment. |
| [IronPython](http://ironpython.net/)                        | The python runtime used by Sho.                 |
| [ZedGraph](https://sourceforge.net/projects/zedgraph/)                          | Graphing component used in the GUI.              |
| [AlgLib](http://www.alglib.net/)                            | Implementation of several LM fitting routines.   |