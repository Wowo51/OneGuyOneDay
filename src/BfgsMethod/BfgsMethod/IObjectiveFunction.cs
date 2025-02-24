namespace BfgsMethod
{
    public interface IObjectiveFunction
    {
        double Value(double[] point);
        double[] Gradient(double[] point);
    }
}