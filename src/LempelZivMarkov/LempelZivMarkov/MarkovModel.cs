using System;
using System.Collections.Generic;

namespace LempelZivMarkov
{
    public class MarkovModel
    {
        private Dictionary<byte, Dictionary<byte, int>> transitions;
        public MarkovModel()
        {
            this.transitions = new Dictionary<byte, Dictionary<byte, int>>();
        }

        public void Build(byte[] data)
        {
            if (data == null || data.Length < 2)
            {
                return;
            }

            for (int i = 0; i < data.Length - 1; i++)
            {
                byte current = data[i];
                byte next = data[i + 1];
                if (!this.transitions.ContainsKey(current))
                {
                    this.transitions[current] = new Dictionary<byte, int>();
                }

                if (!this.transitions[current].ContainsKey(next))
                {
                    this.transitions[current][next] = 0;
                }

                this.transitions[current][next] = this.transitions[current][next] + 1;
            }
        }

        public byte[] Serialize()
        {
            List<byte> output = new List<byte>();
            byte stateCount = (byte)this.transitions.Count;
            output.Add(stateCount);
            foreach (KeyValuePair<byte, Dictionary<byte, int>> state in this.transitions)
            {
                output.Add(state.Key);
                Dictionary<byte, int> nextStates = state.Value;
                byte nextCount = (byte)nextStates.Count;
                output.Add(nextCount);
                foreach (KeyValuePair<byte, int> pair in nextStates)
                {
                    output.Add(pair.Key);
                    byte[] countBytes = BitConverter.GetBytes(pair.Value);
                    for (int i = 0; i < countBytes.Length; i++)
                    {
                        output.Add(countBytes[i]);
                    }
                }
            }

            return output.ToArray();
        }

        public static MarkovModel Deserialize(byte[] data)
        {
            MarkovModel model = new MarkovModel();
            if (data == null || data.Length < 1)
            {
                return model;
            }

            int index = 0;
            byte stateCount = data[index];
            index++;
            for (int i = 0; i < stateCount; i++)
            {
                if (index >= data.Length)
                {
                    break;
                }

                byte stateKey = data[index++];
                if (index >= data.Length)
                {
                    break;
                }

                byte nextCount = data[index++];
                for (int j = 0; j < nextCount; j++)
                {
                    if (index >= data.Length)
                    {
                        break;
                    }

                    byte nextKey = data[index++];
                    if (index + 3 >= data.Length)
                    {
                        break;
                    }

                    int count = BitConverter.ToInt32(data, index);
                    index += 4;
                    if (!model.transitions.ContainsKey(stateKey))
                    {
                        model.transitions[stateKey] = new Dictionary<byte, int>();
                    }

                    model.transitions[stateKey][nextKey] = count;
                }
            }

            return model;
        }
    }
}