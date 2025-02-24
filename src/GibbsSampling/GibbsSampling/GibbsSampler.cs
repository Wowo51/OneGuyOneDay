using System;
using System.Collections.Generic;

namespace GibbsSampling
{
    public class GibbsSampler
    {
        public delegate double ConditionalSampler(double[] currentState);
        private double[] currentState;
        private List<ConditionalSampler> conditionalSamplers;
        public GibbsSampler(double[] initialState, List<ConditionalSampler> samplers)
        {
            if (initialState == null)
            {
                if (samplers != null)
                {
                    initialState = new double[samplers.Count];
                }
                else
                {
                    initialState = new double[0];
                }
            }

            if (samplers == null)
            {
                samplers = new List<ConditionalSampler>();
            }

            if (initialState.Length != samplers.Count)
            {
                int count = initialState.Length < samplers.Count ? initialState.Length : samplers.Count;
                double[] newState = new double[count];
                for (int i = 0; i < count; i++)
                {
                    newState[i] = initialState[i];
                }

                this.currentState = newState;
                List<ConditionalSampler> newSamplers = new List<ConditionalSampler>(count);
                for (int i = 0; i < count; i++)
                {
                    newSamplers.Add(samplers[i]);
                }

                this.conditionalSamplers = newSamplers;
            }
            else
            {
                this.currentState = initialState;
                this.conditionalSamplers = samplers;
            }
        }

        public double[] Next()
        {
            for (int i = 0; i < this.conditionalSamplers.Count; i++)
            {
                double newValue = this.conditionalSamplers[i](this.currentState);
                this.currentState[i] = newValue;
            }

            double[] copy = new double[this.currentState.Length];
            for (int i = 0; i < this.currentState.Length; i++)
            {
                copy[i] = this.currentState[i];
            }

            return copy;
        }

        public List<double[]> Run(int iterations)
        {
            List<double[]> samples = new List<double[]>();
            for (int i = 0; i < iterations; i++)
            {
                samples.Add(Next());
            }

            return samples;
        }
    }
}