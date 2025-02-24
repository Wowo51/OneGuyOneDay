namespace BensonAlgorithm.Models
{
    public class LinearVectorOptimizationProblem
    {
        public double[, ] A { get; set; }
        public double[] b { get; set; }
        public double[, ] C { get; set; }

        public LinearVectorOptimizationProblem(double[, ] a, double[] b, double[, ] c)
        {
            this.A = a;
            this.b = b;
            this.C = c;
        }
    }
}