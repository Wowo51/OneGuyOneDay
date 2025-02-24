using System;
using System.Collections.Generic;
using System.Text;

namespace OpticsClusteringAlgorithm
{
    public class ClusterVisualizer
    {
        public string GenerateReachabilityPlotSvg(List<PointEntry> ordering)
        {
            if (ordering == null || ordering.Count == 0)
            {
                return string.Empty;
            }

            double maxReachability = 0;
            foreach (PointEntry entry in ordering)
            {
                double value = entry.ReachabilityDistance ?? 0;
                if (value > maxReachability)
                {
                    maxReachability = value;
                }
            }

            int width = ordering.Count * 5;
            int height = 200;
            StringBuilder svg = new StringBuilder();
            svg.AppendLine($"<svg width=\"{width}\" height=\"{height}\" xmlns=\"http://www.w3.org/2000/svg\">");
            for (int i = 0; i < ordering.Count; i++)
            {
                double reachability = ordering[i].ReachabilityDistance ?? 0;
                int barHeight = (maxReachability > 0) ? (int)(reachability / maxReachability * height) : 0;
                int x = i * 5;
                int y = height - barHeight;
                svg.AppendLine($"  <rect x=\"{x}\" y=\"{y}\" width=\"5\" height=\"{barHeight}\" fill=\"blue\" />");
            }

            svg.AppendLine("</svg>");
            return svg.ToString();
        }
    }
}