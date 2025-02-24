using System.Collections.Generic;

namespace AssociationRuleLearning
{
    public class AssociationRule
    {
        public List<string> Antecedent { get; set; } = new List<string>();
        public List<string> Consequent { get; set; } = new List<string>();
        public double Support { get; set; }
        public double Confidence { get; set; }
    }
}