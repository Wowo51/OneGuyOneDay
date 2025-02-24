namespace NestedSamplingAlgorithm
{
    public interface ILikelihoodEvaluator
    {
        double Evaluate(double[] parameters);
    }
}