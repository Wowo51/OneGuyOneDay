using System;

namespace PartialLeastSquares
{
    public class PLSRegression
    {
        private int _components;
        private double[][] _coefficients = new double[0][];
        public PLSRegression(int components)
        {
            _components = (components > 0) ? components : 1;
        }

        public void Fit(double[][] X, double[][] Y)
        {
            int nObservations = X.Length;
            int nPredictors = (nObservations > 0 && X[0] != null) ? X[0].Length : 0;
            int nResponses = (Y.Length > 0 && Y[0] != null) ? Y[0].Length : 0;
            double[][] W = new double[_components][];
            double[][] P = new double[_components][];
            double[][] Q = new double[_components][];
            double[][] Xdeflate = MatrixHelper.Clone(X);
            double[][] Ydeflate = MatrixHelper.Clone(Y);
            for (int comp = 0; comp < _components; comp++)
            {
                double[] t = new double[nObservations];
                for (int i = 0; i < nObservations; i++)
                {
                    t[i] = Ydeflate[i][0];
                }

                double[] w = new double[nPredictors];
                int iteration = 0;
                while (iteration < 100)
                {
                    for (int j = 0; j < nPredictors; j++)
                    {
                        double sum = 0;
                        for (int i = 0; i < nObservations; i++)
                        {
                            sum += Xdeflate[i][j] * t[i];
                        }

                        w[j] = sum;
                    }

                    double normW = MatrixHelper.Norm(w);
                    if (normW < 1e-10)
                    {
                        normW = 1e-10;
                    }

                    for (int j = 0; j < nPredictors; j++)
                    {
                        w[j] /= normW;
                    }

                    double[] tNew = new double[nObservations];
                    for (int i = 0; i < nObservations; i++)
                    {
                        double sum = 0;
                        for (int j = 0; j < nPredictors; j++)
                        {
                            sum += Xdeflate[i][j] * w[j];
                        }

                        tNew[i] = sum;
                    }

                    double diff = 0;
                    for (int i = 0; i < nObservations; i++)
                    {
                        double d = tNew[i] - t[i];
                        diff += d * d;
                    }

                    if (Math.Sqrt(diff) < 1e-6)
                    {
                        t = tNew;
                        break;
                    }
                    else
                    {
                        t = tNew;
                    }

                    iteration++;
                }

                double[] p = new double[nPredictors];
                double tDotT = 0;
                for (int i = 0; i < nObservations; i++)
                {
                    tDotT += t[i] * t[i];
                }

                double denom = (tDotT < 1e-10) ? 1e-10 : tDotT;
                for (int j = 0; j < nPredictors; j++)
                {
                    double sum = 0;
                    for (int i = 0; i < nObservations; i++)
                    {
                        sum += Xdeflate[i][j] * t[i];
                    }

                    p[j] = sum / denom;
                }

                double[] q = new double[nResponses];
                for (int col = 0; col < nResponses; col++)
                {
                    double sum = 0;
                    for (int i = 0; i < nObservations; i++)
                    {
                        sum += Ydeflate[i][col] * t[i];
                    }

                    q[col] = sum / denom;
                }

                W[comp] = new double[nPredictors];
                for (int j = 0; j < nPredictors; j++)
                {
                    W[comp][j] = w[j];
                }

                P[comp] = p;
                Q[comp] = q;
                for (int i = 0; i < nObservations; i++)
                {
                    for (int j = 0; j < nPredictors; j++)
                    {
                        Xdeflate[i][j] = Xdeflate[i][j] - t[i] * p[j];
                    }
                }

                for (int i = 0; i < nObservations; i++)
                {
                    for (int col = 0; col < nResponses; col++)
                    {
                        Ydeflate[i][col] = Ydeflate[i][col] - t[i] * q[col];
                    }
                }
            }

            double[][] WMatrix = MatrixHelper.Transpose(W);
            double[][] PMatrix = MatrixHelper.Transpose(P);
            double[][] PTW = MatrixHelper.Multiply(MatrixHelper.Transpose(PMatrix), WMatrix);
            double[][] invPTW = MatrixHelper.Inverse(PTW);
            double[][] temp = MatrixHelper.Multiply(WMatrix, invPTW);
            _coefficients = MatrixHelper.Multiply(temp, Q);
        }

        public double[][] Predict(double[][] X)
        {
            if (X == null || _coefficients == null)
            {
                return new double[0][];
            }

            int nObservations = X.Length;
            int nPredictors = (nObservations > 0 && X[0] != null) ? X[0].Length : 0;
            int nResponses = (_coefficients.Length > 0 && _coefficients[0] != null) ? _coefficients[0].Length : 0;
            double[][] predictions = new double[nObservations][];
            for (int i = 0; i < nObservations; i++)
            {
                predictions[i] = new double[nResponses];
                for (int j = 0; j < nResponses; j++)
                {
                    double sum = 0;
                    for (int k = 0; k < nPredictors; k++)
                    {
                        sum += X[i][k] * _coefficients[k][j];
                    }

                    predictions[i][j] = sum;
                }
            }

            return predictions;
        }

        public double[][] Coefficients
        {
            get
            {
                return _coefficients;
            }
        }
    }
}