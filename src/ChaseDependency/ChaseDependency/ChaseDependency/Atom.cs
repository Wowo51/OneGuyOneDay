using System;
using System.Collections.Generic;

namespace ChaseDependency
{
    public class Atom
    {
        public string Relation;
        public List<string> Arguments;
        public Atom(string relation, List<string> arguments)
        {
            this.Relation = relation;
            this.Arguments = arguments;
        }

        public override string ToString()
        {
            return this.Relation + "(" + string.Join(", ", this.Arguments) + ")";
        }

        public override bool Equals(object? obj)
        {
            if (obj is Atom)
            {
                Atom other = (Atom)obj;
                if (!this.Relation.Equals(other.Relation))
                {
                    return false;
                }

                if (this.Arguments.Count != other.Arguments.Count)
                {
                    return false;
                }

                for (int i = 0; i < this.Arguments.Count; i++)
                {
                    if (!this.Arguments[i].Equals(other.Arguments[i]))
                    {
                        return false;
                    }
                }

                return true;
            }

            return false;
        }

        public override int GetHashCode()
        {
            int hash = this.Relation.GetHashCode();
            foreach (string arg in this.Arguments)
            {
                hash = hash * 31 + arg.GetHashCode();
            }

            return hash;
        }
    }
}