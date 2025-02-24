namespace LaxWendroffMethod
{
    public static class LaxWendroffSolver
    {
        public static double[] Step(double[] u, double c, double dt, double dx)
        {
            int n = u.Length;
            double[] newU = new double[n];
            double r = c * dt / dx;
            for (int j = 0; j < n; j++)
            {
                int jPrev = (j - 1 + n) % n;
                int jNext = (j + 1) % n;
                newU[j] = u[j] - 0.5 * r * (u[jNext] - u[jPrev]) + 0.5 * r * r * (u[jNext] - 2 * u[j] + u[jPrev]);
            }

            return newU;
        }
    }
}