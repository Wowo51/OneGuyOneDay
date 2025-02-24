using System;
using System.Collections.Generic;
using System.Linq;

namespace HyperlinkInducedSearch
{
    public class HITSAlgorithm
    {
        public static void Compute(Graph graph, int iterations, out Dictionary<string, double> hubs, out Dictionary<string, double> authorities)
        {
            hubs = new Dictionary<string, double>();
            authorities = new Dictionary<string, double>();
            List<string> nodes = graph.GetNodes();
            foreach (string node in nodes)
            {
                hubs[node] = 1.0;
                authorities[node] = 1.0;
            }

            for (int i = 0; i < iterations; i++)
            {
                Dictionary<string, double> newAuthorities = new Dictionary<string, double>();
                Dictionary<string, double> newHubs = new Dictionary<string, double>();
                foreach (string node in nodes)
                {
                    newAuthorities[node] = 0.0;
                    newHubs[node] = 0.0;
                }

                foreach (string node in nodes)
                {
                    List<string> incoming = graph.GetIncoming(node);
                    double sumHubs = 0.0;
                    foreach (string incomingNode in incoming)
                    {
                        sumHubs = sumHubs + hubs[incomingNode];
                    }

                    newAuthorities[node] = sumHubs;
                }

                foreach (string node in nodes)
                {
                    List<string> outgoing = graph.GetOutgoing(node);
                    double sumAuthorities = 0.0;
                    foreach (string outgoingNode in outgoing)
                    {
                        sumAuthorities = sumAuthorities + authorities[outgoingNode];
                    }

                    newHubs[node] = sumAuthorities;
                }

                double authorityNorm = 0.0;
                double hubNorm = 0.0;
                foreach (string node in nodes)
                {
                    authorityNorm = authorityNorm + newAuthorities[node] * newAuthorities[node];
                    hubNorm = hubNorm + newHubs[node] * newHubs[node];
                }

                authorityNorm = System.Math.Sqrt(authorityNorm);
                hubNorm = System.Math.Sqrt(hubNorm);
                if (authorityNorm > 0)
                {
                    foreach (string node in nodes)
                    {
                        newAuthorities[node] = newAuthorities[node] / authorityNorm;
                    }
                }

                if (hubNorm > 0)
                {
                    foreach (string node in nodes)
                    {
                        newHubs[node] = newHubs[node] / hubNorm;
                    }
                }

                authorities = newAuthorities;
                hubs = newHubs;
            }
        }
    }
}