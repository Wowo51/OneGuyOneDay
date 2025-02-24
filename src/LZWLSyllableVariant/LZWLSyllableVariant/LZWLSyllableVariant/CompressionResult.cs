using System;
using System.Collections.Generic;

namespace LZWLSyllableVariant
{
    public class CompressionResult
    {
        public List<int> CompressedData { get; }
        public Dictionary<int, SyllableSequence> InitialDictionary { get; }

        public CompressionResult(List<int> compressedData, Dictionary<int, SyllableSequence> initialDictionary)
        {
            if (compressedData == null)
            {
                CompressedData = new List<int>();
            }
            else
            {
                CompressedData = compressedData;
            }

            if (initialDictionary == null)
            {
                InitialDictionary = new Dictionary<int, SyllableSequence>();
            }
            else
            {
                InitialDictionary = initialDictionary;
            }
        }
    }
}