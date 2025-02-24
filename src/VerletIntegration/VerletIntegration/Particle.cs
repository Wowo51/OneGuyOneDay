using System;

namespace VerletIntegration
{
    public class Particle
    {
        public Vector2D CurrentPosition { get; set; }
        public Vector2D PreviousPosition { get; set; }
        public Vector2D Acceleration { get; set; }
        public double Mass { get; set; }

        public Particle(Vector2D initialPosition, Vector2D initialVelocity, double mass, double dt)
        {
            CurrentPosition = initialPosition;
            PreviousPosition = initialPosition - initialVelocity * dt;
            Acceleration = new Vector2D(0, 0);
            Mass = mass;
        }

        public void ApplyForce(Vector2D force)
        {
            if (Mass != 0)
            {
                Acceleration = Acceleration + force * (1.0 / Mass);
            }
        }
    }
}