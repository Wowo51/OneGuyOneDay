using Microsoft.VisualStudio.TestTools.UnitTesting;
using KarplusStrongSynthesis;
using System;

namespace KarplusStrongSynthesisTest
{
    [TestClass]
    public class KarplusStrongSynthTests
    {
        [TestMethod]
        public void Test_Constructor_Defaults()
        {
            KarplusStrongSynth synth = new KarplusStrongSynth(0, 0, 0.99);
            Assert.AreEqual(44100, synth.SampleRate);
            Assert.AreEqual(440, synth.Frequency);
            Assert.AreEqual(0.99, synth.Decay);
        }

        [TestMethod]
        public void Test_GenerateSamples_Length()
        {
            KarplusStrongSynth synth = new KarplusStrongSynth(44100, 440, 0.99);
            int requestedSamples = 100;
            double[] samples = synth.GenerateSamples(requestedSamples);
            Assert.AreEqual(requestedSamples, samples.Length);
        }

        [TestMethod]
        public void Test_GenerateSamples_EmptyForNonPositive()
        {
            KarplusStrongSynth synth = new KarplusStrongSynth(44100, 440, 0.99);
            double[] samples = synth.GenerateSamples(0);
            Assert.AreEqual(0, samples.Length);
            samples = synth.GenerateSamples(-10);
            Assert.AreEqual(0, samples.Length);
        }

        [TestMethod]
        public void Test_GenerateSamples_LargeData()
        {
            KarplusStrongSynth synth = new KarplusStrongSynth(44100, 440, 0.99);
            int requestedSamples = 50000;
            double[] samples = synth.GenerateSamples(requestedSamples);
            Assert.AreEqual(requestedSamples, samples.Length);
        }

        [TestMethod]
        public void Test_GenerateSamples_WithinBounds()
        {
            KarplusStrongSynth synth = new KarplusStrongSynth(44100, 440, 0.99);
            double[] samples = synth.GenerateSamples(100);
            foreach (double sample in samples)
            {
                Assert.IsTrue(sample >= -1.0 && sample <= 1.0, "Sample out of bounds");
            }
        }
    }
}