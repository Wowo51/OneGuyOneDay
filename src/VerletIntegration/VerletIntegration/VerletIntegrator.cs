using System;

namespace VerletIntegration
{
    public static class VerletIntegrator
    {
        public static void Integrate(Particle particle, double dt)
        {
            Vector2D newPosition = particle.CurrentPosition * 2 - particle.PreviousPosition + particle.Acceleration * (dt * dt);
            particle.PreviousPosition = particle.CurrentPosition;
            particle.CurrentPosition = newPosition;
            particle.Acceleration = new Vector2D(0, 0);
        }
    }
}