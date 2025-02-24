using System;
using System.Collections.Generic;

namespace GibbsSampling
{
    public static class Program
    {
        public static void Main()
        {
            System.Random random = new System.Random();
            double[] initialState = new double[]
            {
                0.0,
                0.0
            };
            List<GibbsSampler.ConditionalSampler> samplers = new List<GibbsSampler.ConditionalSampler>
            {
                delegate (double[] state)
                {
                    if (state == null || state.Length < 2)
                    {
                        return 0.0;
                    }

                    return state[1] + (random.NextDouble() - 0.5);
                },
                delegate (double[] state)
                {
                    if (state == null || state.Length < 1)
                    {
                        return 0.0;
                    }

                    return state[0] + (random.NextDouble() - 0.5);
                }
            };
            GibbsSampler sampler = new GibbsSampler(initialState, samplers);
            List<double[]> samples = sampler.Run(10);
            foreach (double[] sample in samples)
            {
                Console.WriteLine("Sample: " + string.Join(", ", sample));
            }
        }
    }
}