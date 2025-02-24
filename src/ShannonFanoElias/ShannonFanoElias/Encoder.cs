using System;
using System.Collections.Generic;
using System.Text;

namespace ShannonFanoElias
{
    public class Encoder
    {
        public Dictionary<string, string> Encode(Dictionary<string, double> symbolProbabilities)
        {
            if (symbolProbabilities == null)
            {
                symbolProbabilities = new Dictionary<string, double>();
            }

            Dictionary<string, string> codewords = new Dictionary<string, string>();
            double cumulative = 0.0;
            List<KeyValuePair<string, double>> entries = new List<KeyValuePair<string, double>>(symbolProbabilities);
            entries.Sort((KeyValuePair<string, double> a, KeyValuePair<string, double> b) =>
            {
                return String.Compare(a.Key, b.Key);
            });
            foreach (KeyValuePair<string, double> entry in entries)
            {
                double probability = entry.Value;
                double q = cumulative + (probability / 2.0);
                int length = (int)Math.Ceiling(-Math.Log(probability, 2)) + 1;
                string codeword = GenerateBinaryFraction(q, length);
                codewords.Add(entry.Key, codeword);
                cumulative += probability;
            }

            return codewords;
        }

        public string GenerateBinaryFraction(double value, int length)
        {
            StringBuilder binary = new StringBuilder();
            double fraction = value - Math.Floor(value);
            for (int i = 0; i < length; i++)
            {
                fraction = fraction * 2;
                int bit = (int)Math.Floor(fraction);
                binary.Append(bit.ToString());
                fraction -= bit;
            }

            return binary.ToString();
        }
    }
}