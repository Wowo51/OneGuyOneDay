using System;

namespace HybridMonteCarlo
{
    public class HybridMonteCarloSampler
    {
        private readonly IPotentialEnergy _potentialEnergy;
        private readonly double _stepSize;
        private readonly int _numberOfSteps;
        private readonly Random _random;
        public HybridMonteCarloSampler(IPotentialEnergy potentialEnergy, double stepSize, int numberOfSteps, Random random)
        {
            _potentialEnergy = potentialEnergy;
            _stepSize = stepSize;
            _numberOfSteps = numberOfSteps;
            _random = random;
        }

        private double ComputeHamiltonian(double[] position, double[] momentum)
        {
            double potential = _potentialEnergy.Potential(position);
            double kinetic = 0.0;
            for (int i = 0; i < momentum.Length; i++)
            {
                kinetic += momentum[i] * momentum[i];
            }

            kinetic *= 0.5;
            return potential + kinetic;
        }

        private void Leapfrog(double[] position, double[] momentum, out double[] newPosition, out double[] newMomentum)
        {
            int dim = position.Length;
            newPosition = new double[dim];
            newMomentum = new double[dim];
            Array.Copy(position, newPosition, dim);
            Array.Copy(momentum, newMomentum, dim);
            double[] grad = _potentialEnergy.Gradient(newPosition);
            for (int i = 0; i < dim; i++)
            {
                newMomentum[i] -= 0.5 * _stepSize * grad[i];
            }

            for (int step = 0; step < _numberOfSteps; step++)
            {
                for (int i = 0; i < dim; i++)
                {
                    newPosition[i] += _stepSize * newMomentum[i];
                }

                if (step != _numberOfSteps - 1)
                {
                    grad = _potentialEnergy.Gradient(newPosition);
                    for (int i = 0; i < dim; i++)
                    {
                        newMomentum[i] -= _stepSize * grad[i];
                    }
                }
            }

            grad = _potentialEnergy.Gradient(newPosition);
            for (int i = 0; i < dim; i++)
            {
                newMomentum[i] -= 0.5 * _stepSize * grad[i];
            }

            for (int i = 0; i < dim; i++)
            {
                newMomentum[i] = -newMomentum[i];
            }
        }

        public double[] Sample(double[] currentPosition)
        {
            int dim = currentPosition.Length;
            double[] momentum = new double[dim];
            for (int i = 0; i < dim; i++)
            {
                momentum[i] = GaussianSample();
            }

            double currentHamiltonian = ComputeHamiltonian(currentPosition, momentum);
            double[] proposalPosition;
            double[] proposalMomentum;
            Leapfrog(currentPosition, momentum, out proposalPosition, out proposalMomentum);
            double proposalHamiltonian = ComputeHamiltonian(proposalPosition, proposalMomentum);
            double acceptanceProbability = Math.Exp(currentHamiltonian - proposalHamiltonian);
            double u = _random.NextDouble();
            if (u < acceptanceProbability)
            {
                return proposalPosition;
            }
            else
            {
                return currentPosition;
            }
        }

        private double GaussianSample()
        {
            double u1 = _random.NextDouble();
            double u2 = _random.NextDouble();
            double r = Math.Sqrt(-2.0 * Math.Log(u1));
            double theta = 2.0 * Math.PI * u2;
            return r * Math.Cos(theta);
        }
    }
}