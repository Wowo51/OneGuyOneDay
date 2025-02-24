namespace CoppersmithWinogradAlgorithm
{
    public class Matrix
    {
        public int Size { get; }
        public double[, ] Data { get; }

        public Matrix(int size)
        {
            if (size <= 0)
            {
                Size = 0;
                Data = new double[0, 0];
            }
            else
            {
                Size = size;
                Data = new double[size, size];
            }
        }

        public Matrix(double[, ] data)
        {
            if (data == null || data.GetLength(0) != data.GetLength(1))
            {
                int tempSize = 0;
                if (data != null)
                {
                    int rows = data.GetLength(0);
                    int cols = data.GetLength(1);
                    tempSize = (rows < cols) ? rows : cols;
                }

                Size = tempSize;
                Data = new double[tempSize, tempSize];
                if (data != null)
                {
                    for (int i = 0; i < tempSize; i++)
                    {
                        for (int j = 0; j < tempSize; j++)
                        {
                            Data[i, j] = data[i, j];
                        }
                    }
                }
            }
            else
            {
                Size = data.GetLength(0);
                Data = data;
            }
        }
    }
}