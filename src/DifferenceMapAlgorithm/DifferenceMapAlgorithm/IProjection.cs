using System;

namespace DifferenceMapAlgorithm
{
    public interface IProjection
    {
        double[] Project(double[] input);
    }
}