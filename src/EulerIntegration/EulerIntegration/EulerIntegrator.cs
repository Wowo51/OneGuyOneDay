using System;

namespace EulerIntegration
{
    public static class EulerIntegrator
    {
        public static double Integrate(Func<double, double, double> derivative, double t0, double y0, double stepSize, int steps)
        {
            double t = t0;
            double y = y0;
            for (int i = 0; i < steps; i++)
            {
                double dy = derivative(t, y);
                y += dy * stepSize;
                t += stepSize;
            }

            return y;
        }
    }
}