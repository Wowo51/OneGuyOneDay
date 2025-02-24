using System;

namespace WarpedPredictiveCoding
{
    public class WarpedLinearPredictor
    {
        public double[] ComputeCoefficients(double[] signal, int order, double warp)
        {
            if (signal == null || signal.Length == 0 || order < 1 || order >= signal.Length)
            {
                return new double[0];
            }

            double[] warpedSignal = WarpSignal(signal, warp);
            double[] r = Autocorrelation(warpedSignal, order);
            double[] coefficients = LevinsonDurbin(r, order);
            return coefficients;
        }

        private double[] WarpSignal(double[] input, double warp)
        {
            int length = input.Length;
            double[] output = new double[length];
            output[0] = input[0];
            for (int index = 1; index < length; index++)
            {
                output[index] = -warp * output[index - 1] + input[index] + warp * input[index - 1];
            }

            return output;
        }

        private double[] Autocorrelation(double[] signal, int order)
        {
            int length = signal.Length;
            double[] r = new double[order + 1];
            for (int lag = 0; lag <= order; lag++)
            {
                double sum = 0.0;
                for (int index = lag; index < length; index++)
                {
                    sum += signal[index] * signal[index - lag];
                }

                r[lag] = sum;
            }

            return r;
        }

        private double[] LevinsonDurbin(double[] r, int order)
        {
            double[] a = new double[order + 1];
            double[] prevA = new double[order + 1];
            a[0] = 1.0;
            prevA[0] = 1.0;
            double error = r[0];
            for (int i = 1; i <= order; i++)
            {
                if (Math.Abs(error) < 1e-12)
                {
                    for (int j = i; j <= order; j++)
                    {
                        a[j] = 0.0;
                    }

                    break;
                }

                double acc = 0.0;
                for (int j = 1; j < i; j++)
                {
                    acc += prevA[j] * r[i - j];
                }

                double reflection = (r[i] - acc) / error;
                a[i] = reflection;
                for (int j = 1; j < i; j++)
                {
                    a[j] = prevA[j] - reflection * prevA[i - j];
                }

                error = error * (1.0 - reflection * reflection);
                for (int j = 0; j <= i; j++)
                {
                    prevA[j] = a[j];
                }
            }

            double[] coefficients = new double[order];
            for (int i = 0; i < order; i++)
            {
                coefficients[i] = a[i + 1];
            }

            return coefficients;
        }
    }
}