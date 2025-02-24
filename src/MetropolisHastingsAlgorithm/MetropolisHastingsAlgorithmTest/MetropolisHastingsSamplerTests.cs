using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using MetropolisHastingsAlgorithm;

namespace MetropolisHastingsAlgorithmTest
{
    [TestClass]
    public class MetropolisHastingsSamplerTests
    {
        [TestMethod]
        public void Sample_WithZeroIterations_ReturnsOneSample()
        {
            MetropolisHastingsSampler sampler = new MetropolisHastingsSampler();
            double initialState = 5.0;
            int iterations = 0;
            Func<double, double> targetDensity = delegate (double x)
            {
                return 1.0;
            };
            IEnumerable<double> samples = sampler.Sample(initialState, iterations, targetDensity, 1.0);
            List<double> sampleList = new List<double>(samples);
            Assert.AreEqual(1, sampleList.Count);
            Assert.AreEqual(initialState, sampleList[0]);
        }

        [TestMethod]
        public void Sample_WithNonZeroIterations_ReturnsExpectedCount()
        {
            MetropolisHastingsSampler sampler = new MetropolisHastingsSampler();
            double initialState = 0.0;
            int iterations = 10;
            Func<double, double> targetDensity = (double x) =>
            {
                return 1.0;
            };
            IEnumerable<double> samples = sampler.Sample(initialState, iterations, targetDensity, 1.0);
            List<double> sampleList = new List<double>(samples);
            Assert.AreEqual(iterations + 1, sampleList.Count);
            Assert.AreEqual(initialState, sampleList[0]);
        }

        [TestMethod]
        public void Sample_WithProposalStdZero_ReturnsConstantSamples()
        {
            MetropolisHastingsSampler sampler = new MetropolisHastingsSampler();
            double initialState = 7.0;
            int iterations = 20;
            Func<double, double> targetDensity = (double x) =>
            {
                return 1.0;
            };
            IEnumerable<double> samples = sampler.Sample(initialState, iterations, targetDensity, 0.0);
            List<double> sampleList = new List<double>(samples);
            foreach (double sample in sampleList)
            {
                Assert.AreEqual(initialState, sample);
            }
        }

        [TestMethod]
        public void Sample_WithNegativeIterations_ReturnsOneSample()
        {
            MetropolisHastingsSampler sampler = new MetropolisHastingsSampler();
            double initialState = 3.0;
            int iterations = -5;
            Func<double, double> targetDensity = (double x) =>
            {
                return 1.0;
            };
            IEnumerable<double> samples = sampler.Sample(initialState, iterations, targetDensity, 1.0);
            List<double> sampleList = new List<double>(samples);
            Assert.AreEqual(1, sampleList.Count);
            Assert.AreEqual(initialState, sampleList[0]);
        }

        [TestMethod]
        public void Sample_WithStandardNormalDensity_GeneratesDiverseSamples()
        {
            MetropolisHastingsSampler sampler = new MetropolisHastingsSampler();
            double initialState = 0.0;
            int iterations = 1000;
            Func<double, double> targetDensity = delegate (double x)
            {
                return Math.Exp(-0.5 * x * x);
            };
            IEnumerable<double> samples = sampler.Sample(initialState, iterations, targetDensity, 1.0);
            List<double> sampleList = new List<double>(samples);
            Assert.AreEqual(iterations + 1, sampleList.Count);
            double sum = 0.0;
            double sumSq = 0.0;
            for (int i = 0; i < sampleList.Count; i++)
            {
                sum += sampleList[i];
                sumSq += sampleList[i] * sampleList[i];
            }

            double mean = sum / sampleList.Count;
            double variance = (sumSq / sampleList.Count) - (mean * mean);
            Assert.IsTrue(variance > 0.0);
        }
    }
}