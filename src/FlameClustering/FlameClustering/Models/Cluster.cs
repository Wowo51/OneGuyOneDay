using System.Collections.Generic;

namespace FlameClustering.Models
{
    public class Cluster
    {
        public int Id { get; set; }
        public DataPoint Center { get; set; }
        public Dictionary<DataPoint, double> Memberships { get; }

        public Cluster(int id, DataPoint center)
        {
            this.Id = id;
            this.Center = center;
            this.Memberships = new Dictionary<DataPoint, double>();
        }
    }
}