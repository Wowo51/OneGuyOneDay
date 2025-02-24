using System.Collections.Generic;

namespace HindleyMilnerAlgorithm
{
    public abstract class HMType
    {
        public abstract HashSet<string> FreeTypeVars();
    }

    public class TypeVariable : HMType
    {
        public string Name { get; }

        public TypeVariable(string name)
        {
            Name = name;
        }

        public override HashSet<string> FreeTypeVars()
        {
            HashSet<string> set = new HashSet<string>();
            set.Add(Name);
            return set;
        }

        public override string ToString()
        {
            return Name;
        }
    }

    public class TypeOperator : HMType
    {
        public string Name { get; }
        public List<HMType> Types { get; }

        public TypeOperator(string name, List<HMType> types)
        {
            Name = name;
            Types = types;
        }

        public override HashSet<string> FreeTypeVars()
        {
            HashSet<string> set = new HashSet<string>();
            foreach (HMType type in Types)
            {
                set.UnionWith(type.FreeTypeVars());
            }

            return set;
        }

        public override string ToString()
        {
            if (Name == "->" && Types.Count == 2)
            {
                return "(" + Types[0].ToString() + " -> " + Types[1].ToString() + ")";
            }
            else
            {
                string result = Name + " ";
                foreach (HMType type in Types)
                {
                    result += type.ToString() + " ";
                }

                return result.Trim();
            }
        }
    }
}