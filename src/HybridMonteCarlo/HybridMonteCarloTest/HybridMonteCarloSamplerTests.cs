using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using HybridMonteCarlo;

namespace HybridMonteCarloTest
{
    [TestClass]
    public class HybridMonteCarloSamplerTests
    {
        [TestMethod]
        public void TestQuadraticPotential()
        {
            double[] position = new double[]
            {
                1.0,
                2.0,
                3.0
            };
            QuadraticPotential potential = new QuadraticPotential();
            double expectedPotential = 0.5 * (1.0 * 1.0 + 2.0 * 2.0 + 3.0 * 3.0);
            double actualPotential = potential.Potential(position);
            Assert.AreEqual(expectedPotential, actualPotential, 1e-6, "Potential is not computed correctly.");
            double[] gradient = potential.Gradient(position);
            Assert.AreEqual(position.Length, gradient.Length, "Gradient length mismatch.");
            for (int i = 0; i < position.Length; i++)
            {
                Assert.AreEqual(position[i], gradient[i], 1e-6, "Gradient element mismatch at index " + i);
            }
        }

        private class FakePotential : IPotentialEnergy
        {
            public double Potential(double[] position)
            {
                return 1.0;
            }

            public double[] Gradient(double[] position)
            {
                int dim = position.Length;
                double[] grad = new double[dim];
                for (int i = 0; i < dim; i++)
                {
                    grad[i] = 0.0;
                }

                return grad;
            }
        }

        private class FakeRandom : Random
        {
            private readonly Queue<double> _values;
            public FakeRandom(double[] values)
            {
                _values = new Queue<double>(values);
            }

            public override double NextDouble()
            {
                if (_values.Count > 0)
                {
                    return _values.Dequeue();
                }

                return 0.0;
            }
        }

        [TestMethod]
        public void TestSamplerReturnsSameDimension()
        {
            double[] initialState = new double[]
            {
                1.0,
                -1.0
            };
            QuadraticPotential potential = new QuadraticPotential();
            Random random = new Random(42);
            HybridMonteCarloSampler sampler = new HybridMonteCarloSampler(potential, 0.1, 10, random);
            double[] newState = sampler.Sample(initialState);
            Assert.IsNotNull(newState, "Sampler returned null state.");
            Assert.AreEqual(initialState.Length, newState.Length, "Sampler did not return state of same dimension.");
        }

        [TestMethod]
        public void TestSamplerWithFakeRandomAcceptance()
        {
            double[] initialState = new double[]
            {
                0.0
            };
            // For a 1D state, Sample makes 2 NextDouble calls for Gaussian sampling and 1 for acceptance.
            // Fake values: u1=0.5, u2=0.5 and for acceptance: 0.2 (< exp(0) equals 1).
            double[] fakeRandomValues = new double[]
            {
                0.5,
                0.5,
                0.2
            };
            FakeRandom fakeRandom = new FakeRandom(fakeRandomValues);
            FakePotential potential = new FakePotential();
            HybridMonteCarloSampler sampler = new HybridMonteCarloSampler(potential, 0.1, 5, fakeRandom);
            double[] newState = sampler.Sample(initialState);
            Assert.IsNotNull(newState, "Sampler returned null state with FakeRandom.");
            Assert.AreEqual(initialState.Length, newState.Length, "Sampler state dimension mismatch with FakeRandom.");
            // Calculate expected new state:
            // GaussianSample computes: r = sqrt(-2*log(0.5)) ~ 1.17741, theta = 2*pi*0.5 = pi,
            // so sample = 1.17741 * cos(pi) = -1.17741.
            // Leapfrog updates: newPosition = initialState + (stepSize * momentum * numberOfSteps)
            // = 0.0 + (0.1 * -1.17741 * 5) = -0.588705.
            double expected = 0.0 + (-1.17741 * 0.1 * 5);
            Assert.AreEqual(expected, newState[0], 1e-3, "Sampler did not compute expected new state using FakeRandom.");
        }

        [TestMethod]
        public void TestSamplerWithLargeDimensions()
        {
            int dim = 1000;
            double[] initialState = new double[dim];
            for (int i = 0; i < dim; i++)
            {
                initialState[i] = i * 0.01;
            }

            QuadraticPotential potential = new QuadraticPotential();
            Random random = new Random(12345);
            HybridMonteCarloSampler sampler = new HybridMonteCarloSampler(potential, 0.05, 20, random);
            double[] newState = sampler.Sample(initialState);
            Assert.IsNotNull(newState, "Sampler returned null state for large dimension.");
            Assert.AreEqual(dim, newState.Length, "Sampler did not return state of correct dimension for large dimension.");
        }
    }
}