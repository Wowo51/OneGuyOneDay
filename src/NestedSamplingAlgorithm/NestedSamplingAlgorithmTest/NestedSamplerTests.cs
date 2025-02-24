using Microsoft.VisualStudio.TestTools.UnitTesting;
using NestedSamplingAlgorithm;

namespace NestedSamplingAlgorithmTest
{
    [TestClass]
    public class NestedSamplerTests
    {
        private class DummyPrior : IPrior
        {
            private System.Random random;
            public DummyPrior()
            {
                this.random = new System.Random(1234);
            }

            public double[] Sample()
            {
                double[] sample = new double[2];
                sample[0] = this.random.NextDouble();
                sample[1] = this.random.NextDouble();
                return sample;
            }
        }

        private class DummyLikelihoodEvaluator : ILikelihoodEvaluator
        {
            public double Evaluate(double[] parameters)
            {
                double sum = 0.0;
                for (int i = 0; i < parameters.Length; i++)
                {
                    sum += parameters[i];
                }

                return sum;
            }
        }

        [TestMethod]
        public void Test_NestedSampler_Basic()
        {
            IPrior prior = new DummyPrior();
            ILikelihoodEvaluator likelihoodEvaluator = new DummyLikelihoodEvaluator();
            int livePoints = 10;
            int maxIterations = 20;
            double tolerance = 1e-6;
            NestedSampler sampler = new NestedSampler(likelihoodEvaluator, prior, livePoints, maxIterations, tolerance);
            NestedSamplingResult result = sampler.Run();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Evidence > 0.0);
            Assert.IsNotNull(result.Samples);
            Assert.AreEqual(livePoints, result.Samples.Count);
        }

        [TestMethod]
        public void Test_NestedSampler_LargeData()
        {
            IPrior prior = new DummyPrior();
            ILikelihoodEvaluator likelihoodEvaluator = new DummyLikelihoodEvaluator();
            int livePoints = 50;
            int maxIterations = 100;
            double tolerance = 1e-6;
            NestedSampler sampler = new NestedSampler(likelihoodEvaluator, prior, livePoints, maxIterations, tolerance);
            NestedSamplingResult result = sampler.Run();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Evidence > 0.0);
            Assert.IsNotNull(result.Samples);
            Assert.AreEqual(livePoints, result.Samples.Count);
        }

        [TestMethod]
        public void Test_NestedSampler_Tolerance()
        {
            IPrior prior = new DummyPrior();
            ILikelihoodEvaluator likelihoodEvaluator = new DummyLikelihoodEvaluator();
            int livePoints = 5;
            int maxIterations = 500;
            double tolerance = 0.1;
            NestedSampler sampler = new NestedSampler(likelihoodEvaluator, prior, livePoints, maxIterations, tolerance);
            NestedSamplingResult result = sampler.Run();
            Assert.IsNotNull(result);
            Assert.IsTrue(result.Uncertainty <= tolerance);
            Assert.AreEqual(livePoints, result.Samples.Count);
        }
    }
}