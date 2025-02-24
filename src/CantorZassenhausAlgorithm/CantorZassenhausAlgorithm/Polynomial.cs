using System;
using System.Collections.Generic;
using System.Linq;
using System.Diagnostics;

namespace CantorZassenhausAlgorithm
{
    public class Polynomial
    {
        public List<int> Coefficients { get; set; }
        public int Modulus { get; }

        public int Degree
        {
            get
            {
                return Coefficients.Count - 1;
            }
        }

        public Polynomial(List<int> coefficients, int modulus)
        {
            Modulus = modulus;
            Coefficients = new List<int>();
            foreach (int c in coefficients)
            {
                int value = c % modulus;
                if (value < 0)
                {
                    value += modulus;
                }

                Coefficients.Add(value);
            }

            Normalize();
        }

        private void Normalize()
        {
            while (Coefficients.Count > 1 && Coefficients[Coefficients.Count - 1] == 0)
            {
                Coefficients.RemoveAt(Coefficients.Count - 1);
            }
        }

        public static Polynomial Add(Polynomial a, Polynomial b)
        {
            Debug.Assert(a.Modulus == b.Modulus, "Moduli must match in addition.");
            int maxCount = Math.Max(a.Coefficients.Count, b.Coefficients.Count);
            List<int> result = new List<int>();
            for (int i = 0; i < maxCount; i++)
            {
                int coeffA = (i < a.Coefficients.Count) ? a.Coefficients[i] : 0;
                int coeffB = (i < b.Coefficients.Count) ? b.Coefficients[i] : 0;
                int sum = (coeffA + coeffB) % a.Modulus;
                result.Add(sum);
            }

            return new Polynomial(result, a.Modulus);
        }

        public static Polynomial Subtract(Polynomial a, Polynomial b)
        {
            Debug.Assert(a.Modulus == b.Modulus, "Moduli must match in subtraction.");
            int maxCount = Math.Max(a.Coefficients.Count, b.Coefficients.Count);
            List<int> result = new List<int>();
            for (int i = 0; i < maxCount; i++)
            {
                int coeffA = (i < a.Coefficients.Count) ? a.Coefficients[i] : 0;
                int coeffB = (i < b.Coefficients.Count) ? b.Coefficients[i] : 0;
                int diff = coeffA - coeffB;
                diff %= a.Modulus;
                if (diff < 0)
                {
                    diff += a.Modulus;
                }

                result.Add(diff);
            }

            return new Polynomial(result, a.Modulus);
        }

        public static Polynomial Multiply(Polynomial a, Polynomial b)
        {
            Debug.Assert(a.Modulus == b.Modulus, "Moduli must match in multiplication.");
            int newDegree = a.Degree + b.Degree;
            List<int> result = new List<int>();
            for (int i = 0; i <= newDegree; i++)
            {
                result.Add(0);
            }

            for (int i = 0; i < a.Coefficients.Count; i++)
            {
                for (int j = 0; j < b.Coefficients.Count; j++)
                {
                    int product = (a.Coefficients[i] * b.Coefficients[j]) % a.Modulus;
                    result[i + j] = (result[i + j] + product) % a.Modulus;
                }
            }

            return new Polynomial(result, a.Modulus);
        }

        public static Tuple<Polynomial, Polynomial> Divide(Polynomial dividend, Polynomial divisor)
        {
            Debug.Assert(dividend.Modulus == divisor.Modulus, "Moduli must match in division.");
            Debug.Assert(!divisor.IsZero(), "Division by zero polynomial.");
            int quotientLength = dividend.Degree - divisor.Degree + 1;
            List<int> quotient = new List<int>();
            for (int i = 0; i < quotientLength; i++)
            {
                quotient.Add(0);
            }

            Polynomial remainder = new Polynomial(new List<int>(dividend.Coefficients), dividend.Modulus);
            int invLead = ModInverse(divisor.Coefficients[divisor.Degree], divisor.Modulus);
            while (remainder.Degree >= divisor.Degree && !remainder.IsZero())
            {
                int d = remainder.Degree - divisor.Degree;
                int coeff = (remainder.Coefficients[remainder.Degree] * invLead) % dividend.Modulus;
                quotient[d] = coeff;
                List<int> subtractCoeffs = new List<int>();
                for (int i = 0; i < d; i++)
                {
                    subtractCoeffs.Add(0);
                }

                for (int i = 0; i < divisor.Coefficients.Count; i++)
                {
                    int term = (divisor.Coefficients[i] * coeff) % dividend.Modulus;
                    subtractCoeffs.Add(term);
                }

                Polynomial subtractPoly = new Polynomial(subtractCoeffs, dividend.Modulus);
                remainder = Subtract(remainder, subtractPoly);
                remainder.Normalize();
            }

            return Tuple.Create(new Polynomial(quotient, dividend.Modulus), remainder);
        }

        public bool IsZero()
        {
            return (Coefficients.Count == 0) || (Coefficients.Count == 1 && Coefficients[0] == 0);
        }

        public static int ModInverse(int a, int mod)
        {
            a = a % mod;
            for (int x = 1; x < mod; x++)
            {
                if ((a * x) % mod == 1)
                {
                    return x;
                }
            }

            return 1;
        }

        public static Polynomial GCD(Polynomial a, Polynomial b)
        {
            while (!b.IsZero())
            {
                Tuple<Polynomial, Polynomial> division = Divide(a, b);
                Polynomial r = division.Item2;
                a = b;
                b = r;
            }

            return a;
        }

        public Polynomial Mod(Polynomial other)
        {
            return Divide(this, other).Item2;
        }

        public Polynomial Pow(int exponent)
        {
            Polynomial result = new Polynomial(new List<int> { 1 }, Modulus);
            Polynomial basePoly = this;
            while (exponent > 0)
            {
                if ((exponent & 1) == 1)
                {
                    result = Multiply(result, basePoly);
                }

                basePoly = Multiply(basePoly, basePoly);
                exponent = exponent >> 1;
            }

            return result;
        }

        public Polynomial ModExp(int exponent, Polynomial modulusPoly)
        {
            Polynomial result = new Polynomial(new List<int> { 1 }, Modulus);
            Polynomial basePoly = this;
            while (exponent > 0)
            {
                if ((exponent & 1) == 1)
                {
                    result = Multiply(result, basePoly).Mod(modulusPoly);
                }

                basePoly = Multiply(basePoly, basePoly).Mod(modulusPoly);
                exponent = exponent >> 1;
            }

            return result;
        }

        public override string ToString()
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            for (int i = Coefficients.Count - 1; i >= 0; i--)
            {
                if (Coefficients[i] == 0)
                {
                    continue;
                }

                if (sb.Length > 0)
                {
                    sb.Append(" + ");
                }

                if (i == 0)
                {
                    sb.Append(Coefficients[i].ToString());
                }
                else if (i == 1)
                {
                    sb.Append(Coefficients[i].ToString() + "x");
                }
                else
                {
                    sb.Append(Coefficients[i].ToString() + "x^" + i.ToString());
                }
            }

            if (sb.Length == 0)
            {
                return "0";
            }

            return sb.ToString();
        }
    }
}