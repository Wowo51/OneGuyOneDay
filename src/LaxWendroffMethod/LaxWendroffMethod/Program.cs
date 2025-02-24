using System;

namespace LaxWendroffMethod
{
    public class Program
    {
        public static void Main()
        {
            int gridSize = 101;
            double[] u = new double[gridSize];
            double dx = 1.0 / (gridSize - 1);
            double c = 1.0;
            double dt = 0.005;
            int timeSteps = 200;
            // Initialize a sine wave as the initial condition.
            for (int i = 0; i < gridSize; i++)
            {
                double x = i * dx;
                u[i] = Math.Sin(2 * Math.PI * x);
            }

            // Perform time stepping using the Lax-Wendroff scheme with periodic boundaries.
            for (int n = 0; n < timeSteps; n++)
            {
                u = LaxWendroffSolver.Step(u, c, dt, dx);
            }

            // Output the final solution to the console.
            Console.WriteLine("Final solution:");
            for (int i = 0; i < gridSize; i++)
            {
                Console.WriteLine(u[i]);
            }
        }
    }
}