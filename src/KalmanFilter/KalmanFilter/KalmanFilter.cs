using System;

namespace KalmanFilter
{
    public class KalmanFilter
    {
        private double[, ] stateTransition;
        private double[, ]? controlMatrix;
        private double[, ] observationMatrix;
        private double[, ] processNoiseCovariance;
        private double[, ] measurementNoiseCovariance;
        private double[, ] estimateCovariance;
        private double[] stateEstimate;
        public KalmanFilter(double[, ] A, double[, ] H, double[, ] Q, double[, ] R, double[] x0, double[, ] P0)
        {
            stateTransition = A ?? new double[0, 0];
            observationMatrix = H ?? new double[0, 0];
            processNoiseCovariance = Q ?? new double[0, 0];
            measurementNoiseCovariance = R ?? new double[0, 0];
            stateEstimate = x0 ?? new double[0];
            estimateCovariance = P0 ?? new double[0, 0];
            controlMatrix = null;
        }

        public double[] Predict(double[] controlInput)
        {
            double[] predictedState = MatrixMath.MultiplyMatrixVector(stateTransition, stateEstimate);
            if (controlMatrix != null && controlInput != null && controlInput.Length > 0)
            {
                double[] controlEffect = MatrixMath.MultiplyMatrixVector(controlMatrix, controlInput);
                predictedState = MatrixMath.AddVectors(predictedState, controlEffect);
            }

            double[, ] A_P = MatrixMath.MultiplyMatrices(stateTransition, estimateCovariance);
            double[, ] predictedCovariance = MatrixMath.AddMatrices(MatrixMath.MultiplyMatrices(A_P, MatrixMath.Transpose(stateTransition)), processNoiseCovariance);
            stateEstimate = predictedState;
            estimateCovariance = predictedCovariance;
            return stateEstimate;
        }

        public double[] Update(double[] measurement)
        {
            if (measurement == null)
            {
                return stateEstimate;
            }

            double[] predictedMeasurement = MatrixMath.MultiplyMatrixVector(observationMatrix, stateEstimate);
            double[] innovation = MatrixMath.SubtractVectors(measurement, predictedMeasurement);
            double[, ] H_P = MatrixMath.MultiplyMatrices(observationMatrix, estimateCovariance);
            double[, ] innovationCovariance = MatrixMath.AddMatrices(MatrixMath.MultiplyMatrices(H_P, MatrixMath.Transpose(observationMatrix)), measurementNoiseCovariance);
            double[, ]? invInnovation = MatrixMath.InvertMatrix(innovationCovariance);
            if (invInnovation == null)
            {
                return stateEstimate;
            }

            double[, ] P_HT = MatrixMath.MultiplyMatrices(estimateCovariance, MatrixMath.Transpose(observationMatrix));
            double[, ] kalmanGain = MatrixMath.MultiplyMatrices(P_HT, invInnovation);
            double[] correction = MatrixMath.MultiplyMatrixVector(kalmanGain, innovation);
            stateEstimate = MatrixMath.AddVectors(stateEstimate, correction);
            int size = estimateCovariance.GetLength(0);
            double[, ] identity = MatrixMath.IdentityMatrix(size);
            double[, ] K_H = MatrixMath.MultiplyMatrices(kalmanGain, observationMatrix);
            double[, ] I_minus_KH = MatrixMath.SubtractMatrices(identity, K_H);
            estimateCovariance = MatrixMath.MultiplyMatrices(I_minus_KH, estimateCovariance);
            return stateEstimate;
        }

        public double[, ]? ControlMatrix
        {
            get
            {
                return controlMatrix;
            }

            set
            {
                controlMatrix = value;
            }
        }
    }
}