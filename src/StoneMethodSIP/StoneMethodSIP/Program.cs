using System;

namespace StoneMethodSIP
{
    public class Program
    {
        public static void Main()
        {
            int n = 3;
            SparseMatrix A = new SparseMatrix(n, n);
            A.Set(0, 0, 4);
            A.Set(0, 1, -1);
            A.Set(1, 0, -1);
            A.Set(1, 1, 4);
            A.Set(1, 2, -1);
            A.Set(2, 1, -1);
            A.Set(2, 2, 4);
            Vector b = new Vector(n);
            b.Values[0] = 3;
            b.Values[1] = 4;
            b.Values[2] = 5;
            StoneSIPSolver solver = new StoneSIPSolver();
            Vector x = solver.Solve(A, b, 100, 1e-6);
            Console.WriteLine("Solution:");
            for (int i = 0; i < n; i++)
            {
                Console.WriteLine(x.Values[i]);
            }
        }
    }
}