using System;
using System.Collections.Generic;

namespace ChaseDependency
{
    public class Dependency
    {
        public List<Atom> Antecedent;
        public List<Atom> Consequent;
        public Dependency(List<Atom> antecedent, List<Atom> consequent)
        {
            this.Antecedent = antecedent;
            this.Consequent = consequent;
        }
    }
}