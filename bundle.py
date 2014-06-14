import os
t=[x for x in os.listdir(os.getcwd()) if x.endswith((".dll",".exe"))]
t.remove("GrowthCurveLibrary.dll")
t.append("GrowthCurveLibrary.dll")
cmd="mkbundle --static  --deps -o curvefitter "+" ".join(t)
#above didn't work, order seems to matter
cmd="mkbundle --deps -o curvefitter --static CurveFitterMonoGUI.exe  ShoOptimizer.dll Microsoft.Solver.Foundation.dll MatrixInterf.dll ShoArray.dll alglibnet2.dll MatrixArrayPlot.dll ZedGraph.dll GrowthCurveLibrary.dll "
print cmd
os.system(cmd)


