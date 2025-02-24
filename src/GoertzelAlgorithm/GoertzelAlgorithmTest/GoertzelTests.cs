using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GoertzelAlgorithm;

namespace GoertzelAlgorithmTest
{
    [TestClass]
    public class GoertzelTests
    {
        [TestMethod]
        public void TestEmptySamples()
        {
            double[] samples = new double[0];
            Goertzel goertzel = new Goertzel();
            double result = goertzel.Process(samples, 1000, 8000);
            Assert.AreEqual(0.0, result, 1e-6, "Expected output is 0 for empty sample array.");
        }

        [TestMethod]
        public void TestSineWaveDetection()
        {
            double sampleRate = 8000;
            double targetFrequency = 1000;
            int numSamples = 80; // 10 complete cycles (8 samples per cycle)
            double[] samples = new double[numSamples];
            for (int i = 0; i < numSamples; i++)
            {
                samples[i] = Math.Sin(2.0 * Math.PI * targetFrequency * i / sampleRate);
            }

            Goertzel goertzel = new Goertzel();
            double resultAtTarget = goertzel.Process(samples, targetFrequency, sampleRate);
            double resultOffTarget = goertzel.Process(samples, targetFrequency * 1.1, sampleRate);
            // The magnitude at the sine frequency should be measurably greater than at an off frequency.
            Assert.IsTrue(resultAtTarget > resultOffTarget, "Magnitude at target frequency should be greater than at off-target frequency.");
        }

        [TestMethod]
        public void TestNoiseWithSineComponentDetection()
        {
            double sampleRate = 8000;
            double targetFrequency = 1000;
            int numSamples = 10000;
            double amplitude = 1.0;
            double[] samples = new double[numSamples];
            Random random = new Random(12345);
            for (int i = 0; i < numSamples; i++)
            {
                double noise = (random.NextDouble() - 0.5) * 0.2; // noise between -0.1 and 0.1
                double sineComponent = amplitude * Math.Sin(2.0 * Math.PI * targetFrequency * i / sampleRate);
                samples[i] = noise + sineComponent;
            }

            Goertzel goertzel = new Goertzel();
            double resultAtTarget = goertzel.Process(samples, targetFrequency, sampleRate);
            double resultOffTarget = goertzel.Process(samples, targetFrequency * 1.2, sampleRate);
            // With the sine component present, the magnitude at the target should be significantly higher.
            Assert.IsTrue(resultAtTarget > resultOffTarget, "Magnitude at target frequency should be significantly higher due to the sine component.");
        }
    }
}