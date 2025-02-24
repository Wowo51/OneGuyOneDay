using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using VerletIntegration;

namespace VerletIntegrationTest
{
    [TestClass]
    public class VerletIntegrationTests
    {
        private const double Tolerance = 1e-6;
        [TestMethod]
        public void Test_Vector2D_Arithmetic()
        {
            Vector2D vectorA = new Vector2D(1.0, 2.0);
            Vector2D vectorB = new Vector2D(3.0, 4.0);
            Vector2D sum = vectorA + vectorB;
            Assert.AreEqual(4.0, sum.X, Tolerance);
            Assert.AreEqual(6.0, sum.Y, Tolerance);
            Vector2D diff = vectorB - vectorA;
            Assert.AreEqual(2.0, diff.X, Tolerance);
            Assert.AreEqual(2.0, diff.Y, Tolerance);
            Vector2D product1 = vectorA * 2.0;
            Assert.AreEqual(2.0, product1.X, Tolerance);
            Assert.AreEqual(4.0, product1.Y, Tolerance);
            Vector2D product2 = 2.0 * vectorA;
            Assert.AreEqual(2.0, product2.X, Tolerance);
            Assert.AreEqual(4.0, product2.Y, Tolerance);
        }

        [TestMethod]
        public void Test_ApplyForce_WithNonZeroMass()
        {
            double dt = 1.0;
            Particle particle = new Particle(new Vector2D(0.0, 0.0), new Vector2D(0.0, 0.0), 2.0, dt);
            Vector2D force = new Vector2D(4.0, 6.0);
            particle.ApplyForce(force);
            // Expected acceleration = force / mass = (2.0, 3.0)
            Assert.AreEqual(2.0, particle.Acceleration.X, Tolerance);
            Assert.AreEqual(3.0, particle.Acceleration.Y, Tolerance);
        }

        [TestMethod]
        public void Test_ApplyForce_WithZeroMass()
        {
            double dt = 1.0;
            Particle particle = new Particle(new Vector2D(0.0, 0.0), new Vector2D(0.0, 0.0), 0.0, dt);
            Vector2D force = new Vector2D(4.0, 6.0);
            particle.ApplyForce(force);
            // Expect acceleration remains unchanged (0,0) when mass is zero.
            Assert.AreEqual(0.0, particle.Acceleration.X, Tolerance);
            Assert.AreEqual(0.0, particle.Acceleration.Y, Tolerance);
        }

        [TestMethod]
        public void Test_Integrate_ZeroAcceleration()
        {
            double dt = 1.0;
            // initialVelocity = (1,1) so Previous = initial - velocity * dt = (0,0) - (1,1) = (-1,-1)
            Particle particle = new Particle(new Vector2D(0.0, 0.0), new Vector2D(1.0, 1.0), 1.0, dt);
            // No force applied: acceleration remains (0,0).
            VerletIntegrator.Integrate(particle, dt);
            // newPosition = 2*(0,0) - (-1,-1) = (1,1)
            Assert.AreEqual(1.0, particle.CurrentPosition.X, Tolerance);
            Assert.AreEqual(1.0, particle.CurrentPosition.Y, Tolerance);
            // Second integration step with zero acceleration.
            VerletIntegrator.Integrate(particle, dt);
            // Now, previous = (0,0) and current = (1,1), so newPosition = 2*(1,1) - (0,0) = (2,2)
            Assert.AreEqual(2.0, particle.CurrentPosition.X, Tolerance);
            Assert.AreEqual(2.0, particle.CurrentPosition.Y, Tolerance);
        }

        [TestMethod]
        public void Test_Integrate_SingleStep_WithForce()
        {
            double dt = 1.0;
            // Starting at rest: initial velocity = (0,0) thus Previous = (0,0)
            Particle particle = new Particle(new Vector2D(0.0, 0.0), new Vector2D(0.0, 0.0), 1.0, dt);
            Vector2D gravity = new Vector2D(0.0, -9.81);
            particle.ApplyForce(gravity);
            VerletIntegrator.Integrate(particle, dt);
            // newPosition = 2*(0,0) - (0,0) + gravity*(dt*dt) = gravity = (0, -9.81)
            Assert.AreEqual(0.0, particle.CurrentPosition.X, Tolerance);
            Assert.AreEqual(-9.81, particle.CurrentPosition.Y, Tolerance);
            // Acceleration should be reset after integration.
            Assert.AreEqual(0.0, particle.Acceleration.X, Tolerance);
            Assert.AreEqual(0.0, particle.Acceleration.Y, Tolerance);
        }

        [TestMethod]
        public void Test_Integrate_MultipleSteps_WithConstantForce()
        {
            double dt = 1.0;
            Particle particle = new Particle(new Vector2D(0.0, 0.0), new Vector2D(0.0, 0.0), 1.0, dt);
            Vector2D constantForce = new Vector2D(0.0, -1.0);
            // Step 1
            particle.ApplyForce(constantForce);
            VerletIntegrator.Integrate(particle, dt);
            // Expected: newPosition = (0, -1)
            Assert.AreEqual(0.0, particle.CurrentPosition.X, Tolerance);
            Assert.AreEqual(-1.0, particle.CurrentPosition.Y, Tolerance);
            // Step 2
            particle.ApplyForce(constantForce);
            VerletIntegrator.Integrate(particle, dt);
            // Expected: newPosition = 2*(0,-1) - (0,0) + (0,-1) = (0, -3)
            Assert.AreEqual(0.0, particle.CurrentPosition.X, Tolerance);
            Assert.AreEqual(-3.0, particle.CurrentPosition.Y, Tolerance);
            // Step 3
            particle.ApplyForce(constantForce);
            VerletIntegrator.Integrate(particle, dt);
            // With previous = (0,-1) and current = (0,-3):
            // newPosition = 2*(0,-3) - (0,-1) + (0,-1) = (0, -6)
            Assert.AreEqual(0.0, particle.CurrentPosition.X, Tolerance);
            Assert.AreEqual(-6.0, particle.CurrentPosition.Y, Tolerance);
        }
    }
}