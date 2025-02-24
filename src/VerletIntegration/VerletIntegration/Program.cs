using System;

namespace VerletIntegration
{
    public static class Program
    {
        public static void Main()
        {
            double dt = 0.016;
            Particle particle = new Particle(new Vector2D(0, 0), new Vector2D(10, 10), 1.0, dt);
            for (int step = 0; step < 10; step++)
            {
                // Apply a constant downward gravity force.
                particle.ApplyForce(new Vector2D(0, -9.81));
                VerletIntegrator.Integrate(particle, dt);
                Console.WriteLine("Step " + step + ": Position=(" + particle.CurrentPosition.X + ", " + particle.CurrentPosition.Y + ")");
            }
        }
    }
}