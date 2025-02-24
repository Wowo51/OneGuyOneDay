using System;

namespace CannonsAlgorithm
{
    public class Processor
    {
        public Matrix ABlock { get; set; }
        public Matrix BBlock { get; set; }
        public Matrix CBlock { get; set; }

        public Processor(Matrix a, Matrix b)
        {
            ABlock = a;
            BBlock = b;
            CBlock = new Matrix(a.Rows, b.Columns);
        }

        public void MultiplyAndAccumulate()
        {
            for (int i = 0; i < ABlock.Rows; i++)
            {
                for (int j = 0; j < BBlock.Columns; j++)
                {
                    double sum = 0.0;
                    for (int k = 0; k < ABlock.Columns; k++)
                    {
                        sum += ABlock.Data[i, k] * BBlock.Data[k, j];
                    }

                    CBlock.Data[i, j] += sum;
                }
            }
        }
    }
}