using System;

namespace GoertzelAlgorithm
{
    public class Goertzel
    {
        public double Process(double[] samples, double targetFrequency, double sampleRate)
        {
            int numSamples = samples.Length;
            if (numSamples == 0)
            {
                return 0.0;
            }

            int k = (int)Math.Round((double)numSamples * targetFrequency / sampleRate);
            double omega = 2.0 * Math.PI * k / numSamples;
            double cosine = Math.Cos(omega);
            double coeff = 2.0 * cosine;
            double sPrev = 0.0;
            double sPrev2 = 0.0;
            double s = 0.0;
            for (int i = 0; i < numSamples; i++)
            {
                s = samples[i] + coeff * sPrev - sPrev2;
                sPrev2 = sPrev;
                sPrev = s;
            }

            double power = sPrev2 * sPrev2 + sPrev * sPrev - coeff * sPrev * sPrev2;
            return Math.Sqrt(power);
        }
    }
}