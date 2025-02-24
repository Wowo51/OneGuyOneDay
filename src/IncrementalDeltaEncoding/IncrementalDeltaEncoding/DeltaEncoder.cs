using System;

namespace IncrementalDeltaEncoding
{
    public static class DeltaEncoder
    {
        public static string[] Encode(string[] input)
        {
            if (input == null)
            {
                return new string[0];
            }

            if (input.Length == 0)
            {
                return new string[0];
            }

            string[] encoded = new string[input.Length];
            encoded[0] = input[0] ?? string.Empty;
            for (int i = 1; i < input.Length; i++)
            {
                string previous = input[i - 1] ?? string.Empty;
                string current = input[i] ?? string.Empty;
                int minLength = previous.Length < current.Length ? previous.Length : current.Length;
                int prefix = 0;
                while (prefix < minLength && previous[prefix] == current[prefix])
                {
                    prefix++;
                }

                string suffix = current.Substring(prefix);
                encoded[i] = prefix.ToString() + ":" + suffix;
            }

            return encoded;
        }

        public static string[] Decode(string[] deltas)
        {
            if (deltas == null)
            {
                return new string[0];
            }

            if (deltas.Length == 0)
            {
                return new string[0];
            }

            string[] decoded = new string[deltas.Length];
            decoded[0] = deltas[0];
            for (int i = 1; i < deltas.Length; i++)
            {
                string encoded = deltas[i];
                int colonIndex = encoded.IndexOf(':');
                if (colonIndex == -1)
                {
                    decoded[i] = encoded;
                }
                else
                {
                    string prefixStr = encoded.Substring(0, colonIndex);
                    int prefixLength;
                    if (!Int32.TryParse(prefixStr, out prefixLength))
                    {
                        decoded[i] = encoded;
                    }
                    else
                    {
                        string previous = decoded[i - 1] ?? string.Empty;
                        string commonPart = previous.Substring(0, prefixLength);
                        string suffix = encoded.Substring(colonIndex + 1);
                        decoded[i] = commonPart + suffix;
                    }
                }
            }

            return decoded;
        }
    }
}