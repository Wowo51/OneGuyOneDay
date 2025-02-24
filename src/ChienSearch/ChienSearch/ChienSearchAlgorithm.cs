using System;
using System.Collections.Generic;

namespace ChienSearch
{
    public static class ChienSearchAlgorithm
    {
        // Finds the roots of the polynomial over GF(modulus) using a recursive search.
        // The polynomial is represented as an array of coefficients in increasing order.
        // That is, coefficients[0] is the constant term, coefficients[1] the coefficient of x, etc.
        public static List<int> FindRoots(int[] coefficients, int modulus)
        {
            List<int> roots = new List<int>();
            SearchRoots(0, modulus, coefficients, modulus, roots);
            return roots;
        }

        // Recursively checks candidate values from 0 to modulus - 1.
        private static void SearchRoots(int candidate, int modulusRange, int[] coefficients, int modulus, List<int> roots)
        {
            if (candidate >= modulusRange)
            {
                return;
            }

            int value = EvaluatePolynomial(coefficients, candidate, modulus);
            if ((value % modulus) == 0)
            {
                roots.Add(candidate);
            }

            SearchRoots(candidate + 1, modulusRange, coefficients, modulus, roots);
        }

        // Evaluates the polynomial at x using recursive Horner's method.
        public static int EvaluatePolynomial(int[] coefficients, int x, int modulus)
        {
            if (coefficients == null || coefficients.Length == 0)
            {
                return 0;
            }

            return EvaluateHornerRecursive(coefficients, 0, x, modulus);
        }

        // Implements Horner's method recursively.
        private static int EvaluateHornerRecursive(int[] coefficients, int index, int x, int modulus)
        {
            if (index == (coefficients.Length - 1))
            {
                int result = coefficients[index] % modulus;
                if (result < 0)
                {
                    result += modulus;
                }

                return result;
            }
            else
            {
                int subResult = EvaluateHornerRecursive(coefficients, index + 1, x, modulus);
                int result = (coefficients[index] + ((x * subResult) % modulus)) % modulus;
                if (result < 0)
                {
                    result += modulus;
                }

                return result;
            }
        }
    }
}