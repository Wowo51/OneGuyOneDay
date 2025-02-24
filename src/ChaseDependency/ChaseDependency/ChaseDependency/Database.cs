using System;
using System.Collections.Generic;

namespace ChaseDependency
{
    public class Database
    {
        public List<Atom> Atoms;
        public Database()
        {
            this.Atoms = new List<Atom>();
        }

        public bool ContainsAtom(Atom atom)
        {
            foreach (Atom a in this.Atoms)
            {
                if (a.Equals(atom))
                {
                    return true;
                }
            }

            return false;
        }

        public void AddAtom(Atom atom)
        {
            if (!ContainsAtom(atom))
            {
                this.Atoms.Add(atom);
            }
        }
    }
}