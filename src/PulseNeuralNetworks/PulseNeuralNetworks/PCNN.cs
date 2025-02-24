using System;

namespace PulseNeuralNetworks
{
    public class PCNN
    {
        public double[, ] Input { get; set; }
        public int Width { get; private set; }
        public int Height { get; private set; }
        public double[, ] Output { get; private set; }
        public double Beta { get; set; }
        public double V_F { get; set; }
        public double V_L { get; set; }
        public double Decay { get; set; }
        public double Threshold { get; set; }
        public int MaxIterations { get; set; }

        private double[, ] U;
        private double[, ] T;
        private int[, ] Y;
        public PCNN(double[, ] input, double beta, double v_f, double v_l, double decay, double threshold, int maxIterations)
        {
            if (input == null)
            {
                input = new double[1, 1];
                input[0, 0] = 0.0;
            }

            this.Input = input;
            this.Height = input.GetLength(0);
            this.Width = input.GetLength(1);
            this.Beta = beta;
            this.V_F = v_f;
            this.V_L = v_l;
            this.Decay = decay;
            this.Threshold = threshold;
            this.MaxIterations = maxIterations;
            this.U = new double[this.Height, this.Width];
            this.T = new double[this.Height, this.Width];
            this.Y = new int[this.Height, this.Width];
            this.Output = new double[this.Height, this.Width];
            Initialize();
        }

        private void Initialize()
        {
            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    this.U[i, j] = 0.0;
                    this.T[i, j] = this.Threshold;
                    this.Y[i, j] = 0;
                }
            }
        }

        public double[, ] Process()
        {
            for (int n = 0; n < this.MaxIterations; n++)
            {
                DoIteration();
            }

            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    this.Output[i, j] = (this.Y[i, j] > 0) ? 1.0 : 0.0;
                }
            }

            return this.Output;
        }

        private void DoIteration()
        {
            int[, ] Y_new = new int[this.Height, this.Width];
            double[, ] L = new double[this.Height, this.Width];
            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    double sum = 0.0;
                    for (int di = -1; di <= 1; di++)
                    {
                        for (int dj = -1; dj <= 1; dj++)
                        {
                            if (di == 0 && dj == 0)
                            {
                                continue;
                            }

                            int ni = i + di;
                            int nj = j + dj;
                            if (ni >= 0 && ni < this.Height && nj >= 0 && nj < this.Width)
                            {
                                sum += this.Y[ni, nj];
                            }
                        }
                    }

                    L[i, j] = sum;
                }
            }

            for (int i = 0; i < this.Height; i++)
            {
                for (int j = 0; j < this.Width; j++)
                {
                    double feeding = this.V_F * this.Input[i, j];
                    double linking = this.V_L * L[i, j];
                    this.U[i, j] = feeding * (1 + this.Beta * linking);
                    if (this.U[i, j] > this.T[i, j])
                    {
                        Y_new[i, j] = 1;
                        this.T[i, j] = this.Threshold;
                    }
                    else
                    {
                        Y_new[i, j] = 0;
                        this.T[i, j] = this.T[i, j] * this.Decay;
                    }
                }
            }

            this.Y = Y_new;
        }
    }
}