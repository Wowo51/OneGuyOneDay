using System.Collections.Generic;

namespace PaxosAlgorithm
{
    public class PaxosLearner
    {
        public static string Learn(List<PaxosAcceptor> acceptors)
        {
            Dictionary<string, int> countMap = new Dictionary<string, int>();
            foreach (PaxosAcceptor acceptor in acceptors)
            {
                string value = acceptor.GetAcceptedValue();
                if (value != "")
                {
                    if (countMap.ContainsKey(value))
                    {
                        countMap[value] = countMap[value] + 1;
                    }
                    else
                    {
                        countMap[value] = 1;
                    }
                }
            }

            foreach (KeyValuePair<string, int> entry in countMap)
            {
                if (entry.Value > acceptors.Count / 2)
                {
                    return entry.Key;
                }
            }

            return "";
        }
    }
}