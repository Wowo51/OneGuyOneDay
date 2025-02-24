using System;
using System.Collections.Generic;

namespace AC3Algorithm
{
    public delegate bool Constraint<T>(T value1, T value2);
    public class CSP<T>
    {
        public List<string> Variables { get; set; }
        public Dictionary<string, List<T>> Domains { get; set; }
        public Dictionary<Tuple<string, string>, Constraint<T>> Constraints { get; set; }
        public Dictionary<string, List<string>> Neighbors { get; set; }

        public CSP()
        {
            Variables = new List<string>();
            Domains = new Dictionary<string, List<T>>();
            Constraints = new Dictionary<Tuple<string, string>, Constraint<T>>();
            Neighbors = new Dictionary<string, List<string>>();
        }

        public void AddVariable(string variable, List<T> domain)
        {
            Variables.Add(variable);
            Domains[variable] = new List<T>(domain);
            Neighbors[variable] = new List<string>();
        }

        public void AddConstraint(string variable1, string variable2, Constraint<T> constraint)
        {
            Tuple<string, string> arc = new Tuple<string, string>(variable1, variable2);
            Constraints[arc] = constraint;
            if (!Neighbors[variable1].Contains(variable2))
            {
                Neighbors[variable1].Add(variable2);
            }
        }
    }
}