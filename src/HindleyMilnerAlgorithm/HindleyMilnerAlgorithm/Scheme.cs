using System.Collections.Generic;

namespace HindleyMilnerAlgorithm
{
    public class Scheme
    {
        public List<string> TypeVariables { get; }
        public HMType Type { get; }

        public Scheme(List<string> typeVariables, HMType type)
        {
            TypeVariables = typeVariables;
            Type = type;
        }

        public HashSet<string> FreeTypeVars()
        {
            HashSet<string> free = Type.FreeTypeVars();
            foreach (string a in TypeVariables)
            {
                free.Remove(a);
            }

            return free;
        }
    }
}