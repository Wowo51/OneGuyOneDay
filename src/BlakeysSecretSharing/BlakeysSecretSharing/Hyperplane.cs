using System;

namespace BlakeysSecretSharing
{
    public class Hyperplane
    {
        public double[] Coefficients { get; set; }
        public double Constant { get; set; }

        public Hyperplane(double[] coefficients, double constant)
        {
            this.Coefficients = coefficients;
            this.Constant = constant;
        }
    }
}