using System;

namespace StrassenAlgorithm
{
    public class StrassenMultiplier
    {
        public static Matrix? Multiply(Matrix A, Matrix B)
        {
            if (A == null || B == null || A.Size != B.Size)
            {
                return null;
            }

            int n = A.Size;
            if (n == 1)
            {
                double[, ] data = new double[1, 1];
                data[0, 0] = A.Data[0, 0] * B.Data[0, 0];
                return new Matrix(data);
            }

            int newSize = n / 2;
            Matrix A11 = A.GetSubMatrix(0, 0, newSize);
            Matrix A12 = A.GetSubMatrix(0, newSize, newSize);
            Matrix A21 = A.GetSubMatrix(newSize, 0, newSize);
            Matrix A22 = A.GetSubMatrix(newSize, newSize, newSize);
            Matrix B11 = B.GetSubMatrix(0, 0, newSize);
            Matrix B12 = B.GetSubMatrix(0, newSize, newSize);
            Matrix B21 = B.GetSubMatrix(newSize, 0, newSize);
            Matrix B22 = B.GetSubMatrix(newSize, newSize, newSize);
            Matrix? M1 = Multiply(Matrix.Add(A11, A22)!, Matrix.Add(B11, B22)!);
            Matrix? M2 = Multiply(Matrix.Add(A21, A22)!, B11);
            Matrix? M3 = Multiply(A11, Matrix.Subtract(B12, B22)!);
            Matrix? M4 = Multiply(A22, Matrix.Subtract(B21, B11)!);
            Matrix? M5 = Multiply(Matrix.Add(A11, A12)!, B22);
            Matrix? M6 = Multiply(Matrix.Subtract(A21, A11)!, Matrix.Add(B11, B12)!);
            Matrix? M7 = Multiply(Matrix.Subtract(A12, A22)!, Matrix.Add(B21, B22)!);
            Matrix? C11 = Matrix.Add(Matrix.Subtract(Matrix.Add(M1!, M4!)!, M5!)!, M7!);
            Matrix? C12 = Matrix.Add(M3!, M5!);
            Matrix? C21 = Matrix.Add(M2!, M4!);
            Matrix? C22 = Matrix.Add(Matrix.Subtract(Matrix.Add(M1!, M3!)!, M2!)!, M6!);
            Matrix C = new Matrix(n);
            C.SetSubMatrix(0, 0, C11!);
            C.SetSubMatrix(0, newSize, C12!);
            C.SetSubMatrix(newSize, 0, C21!);
            C.SetSubMatrix(newSize, newSize, C22!);
            return C;
        }
    }
}