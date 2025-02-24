using System;

namespace CannonsAlgorithm
{
    public class ProcessorGrid
    {
        public int GridSize { get; }

        private Processor[, ] processors;
        private int blockRows;
        private int blockCols;
        private Matrix A;
        private Matrix B;
        public ProcessorGrid(Matrix a, Matrix b, int gridSize)
        {
            A = a;
            B = b;
            GridSize = gridSize;
            blockRows = a.Rows / gridSize;
            blockCols = a.Columns / gridSize;
            processors = new Processor[gridSize, gridSize];
            // Initial alignment:
            // For matrix A: shift left by row index -> processor (i, j) gets block from A at (i, (j+i) mod gridSize)
            // For matrix B: shift upward by column index -> processor (i, j) gets block from B at ((i+j) mod gridSize, j)
            for (int i = 0; i < gridSize; i++)
            {
                for (int j = 0; j < gridSize; j++)
                {
                    int sourceColA = (j + i) % gridSize;
                    int sourceRowB = (i + j) % gridSize;
                    Matrix blockA = a.GetSubMatrix(i * blockRows, sourceColA * blockCols, blockRows, blockCols);
                    Matrix blockB = b.GetSubMatrix(sourceRowB * blockRows, j * blockCols, blockRows, blockCols);
                    processors[i, j] = new Processor(blockA, blockB);
                }
            }
        }

        public Matrix ExecuteCannonAlgorithm()
        {
            for (int step = 0; step < GridSize; step++)
            {
                // Each processor computes its local multiplication and accumulates.
                for (int i = 0; i < GridSize; i++)
                {
                    for (int j = 0; j < GridSize; j++)
                    {
                        processors[i, j].MultiplyAndAccumulate();
                    }
                }

                // Shift A left by one in every row.
                Matrix[, ] tempA = new Matrix[GridSize, GridSize];
                for (int i = 0; i < GridSize; i++)
                {
                    for (int j = 0; j < GridSize; j++)
                    {
                        int sourceIndex = (j + 1) % GridSize;
                        tempA[i, j] = processors[i, sourceIndex].ABlock;
                    }
                }

                for (int i = 0; i < GridSize; i++)
                {
                    for (int j = 0; j < GridSize; j++)
                    {
                        processors[i, j].ABlock = tempA[i, j];
                    }
                }

                // Shift B upward by one in every column.
                Matrix[, ] tempB = new Matrix[GridSize, GridSize];
                for (int i = 0; i < GridSize; i++)
                {
                    for (int j = 0; j < GridSize; j++)
                    {
                        int sourceIndex = (i + 1) % GridSize;
                        tempB[i, j] = processors[sourceIndex, j].BBlock;
                    }
                }

                for (int i = 0; i < GridSize; i++)
                {
                    for (int j = 0; j < GridSize; j++)
                    {
                        processors[i, j].BBlock = tempB[i, j];
                    }
                }
            }

            // Assemble the full result from each processor's C block.
            Matrix result = new Matrix(A.Rows, B.Columns);
            for (int i = 0; i < GridSize; i++)
            {
                for (int j = 0; j < GridSize; j++)
                {
                    result.SetSubMatrix(i * blockRows, j * blockCols, processors[i, j].CBlock);
                }
            }

            return result;
        }
    }
}