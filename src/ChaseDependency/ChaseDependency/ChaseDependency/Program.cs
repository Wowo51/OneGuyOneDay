using System;
using System.Collections.Generic;

namespace ChaseDependency
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Database database = new Database();
            database.AddAtom(new Atom("R", new List<string> { "a", "b" }));
            Dependency dependency = new Dependency(new List<Atom> { new Atom("R", new List<string> { "?x", "?y" }) }, new List<Atom> { new Atom("S", new List<string> { "?x", "?y" }) });
            List<Dependency> dependencies = new List<Dependency>();
            dependencies.Add(dependency);
            Database chased = ChaseProcessor.RunChase(database, dependencies);
            foreach (Atom atom in chased.Atoms)
            {
                Console.WriteLine(atom.ToString());
            }
        }
    }
}