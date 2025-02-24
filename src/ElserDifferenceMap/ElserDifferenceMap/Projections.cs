namespace ElserDifferenceMap
{
    public interface IProjection
    {
        double[] Project(double[] vector);
    }

    public class IdentityProjection : IProjection
    {
        public double[] Project(double[] vector)
        {
            if (vector == null)
            {
                return new double[0];
            }

            double[] result = new double[vector.Length];
            for (int i = 0; i < vector.Length; i++)
            {
                result[i] = vector[i];
            }

            return result;
        }
    }
}