using System.Collections.Generic;

namespace FuzzyCMeans
{
    public class FuzzyCMeansResult
    {
        public List<double[]> Centroids { get; set; }
        public double[, ] Memberships { get; set; }

        public FuzzyCMeansResult(List<double[]> centroids, double[, ] memberships)
        {
            this.Centroids = centroids;
            this.Memberships = memberships;
        }
    }
}