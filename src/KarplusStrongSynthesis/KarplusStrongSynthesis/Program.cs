using System;

namespace KarplusStrongSynthesis
{
    public static class Program
    {
        public static void Main(string[] args)
        {
            int sampleRate = 44100;
            double frequency = 440.0;
            double decay = 0.99;
            int durationInSeconds = 3;
            int totalSamples = sampleRate * durationInSeconds;
            KarplusStrongSynth synth = new KarplusStrongSynth(sampleRate, frequency, decay);
            double[] samples = synth.GenerateSamples(totalSamples);
            Console.WriteLine("First 10 samples:");
            for (int i = 0; i < 10 && i < samples.Length; i++)
            {
                Console.WriteLine(samples[i]);
            }
        }
    }
}