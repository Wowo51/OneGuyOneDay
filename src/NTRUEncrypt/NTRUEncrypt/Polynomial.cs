using System;

namespace NTRUEncrypt
{
    public class Polynomial
    {
        public int[] Coefficients { get; }

        public int Degree
        {
            get
            {
                return Coefficients.Length;
            }
        }

        public Polynomial(int[] coefficients)
        {
            Coefficients = coefficients ?? new int[0];
        }

        public Polynomial Add(Polynomial other, int mod)
        {
            int maxDegree = this.Degree > other.Degree ? this.Degree : other.Degree;
            int[] result = new int[maxDegree];
            for (int i = 0; i < maxDegree; i++)
            {
                int a = i < this.Degree ? this.Coefficients[i] : 0;
                int b = i < other.Degree ? other.Coefficients[i] : 0;
                int sum = (a + b) % mod;
                if (sum < 0)
                {
                    sum += mod;
                }

                result[i] = sum;
            }

            return new Polynomial(result);
        }

        public Polynomial Subtract(Polynomial other, int mod)
        {
            int maxDegree = this.Degree > other.Degree ? this.Degree : other.Degree;
            int[] result = new int[maxDegree];
            for (int i = 0; i < maxDegree; i++)
            {
                int a = i < this.Degree ? this.Coefficients[i] : 0;
                int b = i < other.Degree ? other.Coefficients[i] : 0;
                int diff = (a - b) % mod;
                if (diff < 0)
                {
                    diff += mod;
                }

                result[i] = diff;
            }

            return new Polynomial(result);
        }

        public override string ToString()
        {
            return string.Join(",", Coefficients);
        }

        public static Polynomial FromBytes(byte[] bytes, int n, int mod)
        {
            int[] coeff = new int[n];
            for (int i = 0; i < n; i++)
            {
                if (i < bytes.Length)
                {
                    coeff[i] = bytes[i] % mod;
                }
                else
                {
                    coeff[i] = 0;
                }
            }

            return new Polynomial(coeff);
        }

        public byte[] ToBytes(int n, int mod)
        {
            byte[] result = new byte[n];
            for (int i = 0; i < n; i++)
            {
                int value = i < this.Degree ? this.Coefficients[i] : 0;
                value = value % mod;
                if (value < 0)
                {
                    value += mod;
                }

                result[i] = (byte)value;
            }

            return result;
        }
    }
}