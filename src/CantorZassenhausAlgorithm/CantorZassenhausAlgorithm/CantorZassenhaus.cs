using System;
using System.Collections.Generic;

namespace CantorZassenhausAlgorithm
{
    public static class CantorZassenhaus
    {
        private static Random random = new Random();
        public static List<Polynomial> Factor(Polynomial f)
        {
            if (f.Degree <= 0)
            {
                return new List<Polynomial>
                {
                    f
                };
            }

            for (int attempt = 0; attempt < 100; attempt++)
            {
                List<int> coeffs = new List<int>();
                for (int i = 0; i < f.Degree; i++)
                {
                    int coefficient = random.Next(f.Modulus);
                    coeffs.Add(coefficient);
                }

                Polynomial a = new Polynomial(coeffs, f.Modulus);
                Polynomial d = Polynomial.GCD(a, f);
                if (d.Degree >= 1 && d.Degree < f.Degree)
                {
                    List<Polynomial> factors1 = Factor(d);
                    Tuple<Polynomial, Polynomial> divResult = Polynomial.Divide(f, d);
                    Polynomial quotient = divResult.Item1;
                    List<Polynomial> factors2 = Factor(quotient);
                    List<Polynomial> result = new List<Polynomial>();
                    result.AddRange(factors1);
                    result.AddRange(factors2);
                    return result;
                }

                int exponent = (f.Modulus - 1) / 2;
                Polynomial h = a.ModExp(exponent, f);
                Polynomial one = new Polynomial(new List<int> { 1 }, f.Modulus);
                Polynomial hMinusOne = Polynomial.Subtract(h, one);
                Polynomial g = Polynomial.GCD(hMinusOne, f);
                if (g.Degree >= 1 && g.Degree < f.Degree)
                {
                    List<Polynomial> factors1 = Factor(g);
                    Tuple<Polynomial, Polynomial> divResult = Polynomial.Divide(f, g);
                    Polynomial quotient = divResult.Item1;
                    List<Polynomial> factors2 = Factor(quotient);
                    List<Polynomial> result = new List<Polynomial>();
                    result.AddRange(factors1);
                    result.AddRange(factors2);
                    return result;
                }
            }

            return new List<Polynomial>
            {
                f
            };
        }
    }
}