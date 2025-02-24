using System;

namespace GlauberDynamics
{
    public class IsingModel
    {
        public int Rows { get; }
        public int Columns { get; }
        public double Temperature { get; }
        public double Coupling { get; }
        public double ExternalField { get; }
        public int[, ] Spins { get; set; }
        public Random Random { get; }

        public IsingModel(int rows, int columns, double temperature, double coupling, double externalField)
        {
            this.Rows = rows;
            this.Columns = columns;
            this.Temperature = temperature;
            this.Coupling = coupling;
            this.ExternalField = externalField;
            this.Spins = new int[rows, columns];
            this.Random = new Random();
        }

        public void RandomizeSpins()
        {
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    double rand = Random.NextDouble();
                    Spins[i, j] = (rand < 0.5) ? 1 : -1;
                }
            }
        }

        public int SumNeighbors(int row, int col)
        {
            int up = Spins[(row - 1 + Rows) % Rows, col];
            int down = Spins[(row + 1) % Rows, col];
            int left = Spins[row, (col - 1 + Columns) % Columns];
            int right = Spins[row, (col + 1) % Columns];
            return up + down + left + right;
        }

        public double CalculateDeltaEnergy(int row, int col)
        {
            int spin = Spins[row, col];
            int neighborSum = SumNeighbors(row, col);
            return 2.0 * spin * (Coupling * neighborSum + ExternalField);
        }

        public double TotalEnergy()
        {
            double energy = 0.0;
            for (int i = 0; i < Rows; i++)
            {
                for (int j = 0; j < Columns; j++)
                {
                    int spin = Spins[i, j];
                    int rightNeighbor = Spins[i, (j + 1) % Columns];
                    int bottomNeighbor = Spins[(i + 1) % Rows, j];
                    energy += -Coupling * spin * (rightNeighbor + bottomNeighbor);
                    energy += -ExternalField * spin;
                }
            }

            return energy;
        }
    }
}