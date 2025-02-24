using System;

namespace BootstrapAggregating
{
    public interface IPredictor
    {
        double Predict(double[] input);
    }
}