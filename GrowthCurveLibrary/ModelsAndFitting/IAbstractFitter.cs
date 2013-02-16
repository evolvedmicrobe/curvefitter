using System;
namespace GrowthCurveLibrary
{
    /// <summary>
    /// A generic interface, in the hopes of eventually cleaning the thing up with a more OO programming method
    /// </summary>
    interface IAbstractFitter
    {
        /// <summary>
        /// Should this be fit by default?  Not fitting can save automatic loadings
        /// </summary>
        bool FitByDefault { get; } 
        /// <summary>
        /// Was the fit successful? Converged, etc.
        /// </summary>
        bool SuccessfulFit { get; }
        double AbsError { get; }
        double calculateAbsError();
        double calculateResidualSumofSquares();
        double[] Parameters { get; }
        double[] PredictedValues { get; }
        double R2 { get; }
        double[] Residuals { get; }
        double RMSE { get; }
        double[] X { get; }
        double[] Y { get; }
        string Comment { get; }
    }
}
