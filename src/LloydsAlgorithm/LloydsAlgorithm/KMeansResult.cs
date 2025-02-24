using System.Collections.Generic;

namespace LloydsAlgorithm
{
    public class KMeansResult
    {
        public List<double[]> Centroids { get; set; }
        public int[] Labels { get; set; }

        public KMeansResult(List<double[]> centroids, int[] labels)
        {
            Centroids = centroids;
            Labels = labels;
        }
    }
}