using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using ChaseDependency;

namespace ChaseDependencyTest
{
    [TestClass]
    public class ChaseProcessorTests
    {
        [TestMethod]
        public void TestSimpleChase()
        {
            Database database = new Database();
            Atom initialAtom = new Atom("R", new List<string> { "a", "b" });
            database.AddAtom(initialAtom);
            Dependency dependency = new Dependency(new List<Atom> { new Atom("R", new List<string> { "?x", "?y" }) }, new List<Atom> { new Atom("S", new List<string> { "?x", "?y" }) });
            List<Dependency> dependencies = new List<Dependency>
            {
                dependency
            };
            Database chasedDb = ChaseProcessor.RunChase(database, dependencies);
            bool hasR = chasedDb.ContainsAtom(new Atom("R", new List<string> { "a", "b" }));
            bool hasS = chasedDb.ContainsAtom(new Atom("S", new List<string> { "a", "b" }));
            Assert.IsTrue(hasR, "Database should contain the initial R atom.");
            Assert.IsTrue(hasS, "Database should contain the new S atom after applying dependency.");
        }

        [TestMethod]
        public void TestNoChangeWhenDependencyNotApplicable()
        {
            Database database = new Database();
            Atom initialAtom = new Atom("R", new List<string> { "a", "b" });
            database.AddAtom(initialAtom);
            Dependency dependency = new Dependency(new List<Atom> { new Atom("S", new List<string> { "?x", "?y" }) }, new List<Atom> { new Atom("T", new List<string> { "?x", "?y" }) });
            List<Dependency> dependencies = new List<Dependency>
            {
                dependency
            };
            Database chasedDb = ChaseProcessor.RunChase(database, dependencies);
            Assert.IsTrue(chasedDb.ContainsAtom(initialAtom), "Database should remain unchanged if dependency is not applicable.");
            Assert.AreEqual(1, chasedDb.Atoms.Count, "No new atoms should be added when dependency is not applicable.");
        }

        [TestMethod]
        public void TestMultipleMappingsChase()
        {
            Database database = new Database();
            database.AddAtom(new Atom("R", new List<string> { "a", "b" }));
            database.AddAtom(new Atom("R", new List<string> { "a", "c" }));
            Dependency dependency = new Dependency(new List<Atom> { new Atom("R", new List<string> { "?x", "?y" }) }, new List<Atom> { new Atom("S", new List<string> { "?x", "?y" }) });
            List<Dependency> dependencies = new List<Dependency>
            {
                dependency
            };
            Database chasedDb = ChaseProcessor.RunChase(database, dependencies);
            Assert.IsTrue(chasedDb.ContainsAtom(new Atom("S", new List<string> { "a", "b" })), "Database should contain S(a,b).");
            Assert.IsTrue(chasedDb.ContainsAtom(new Atom("S", new List<string> { "a", "c" })), "Database should contain S(a,c).");
            Assert.AreEqual(4, chasedDb.Atoms.Count, "Database should contain 2 R atoms and 2 S atoms.");
        }

        [TestMethod]
        public void TestChainedDependencies()
        {
            Database database = new Database();
            database.AddAtom(new Atom("R", new List<string> { "a", "b" }));
            Dependency dependency1 = new Dependency(new List<Atom> { new Atom("R", new List<string> { "?x", "?y" }) }, new List<Atom> { new Atom("S", new List<string> { "?x", "?y" }) });
            Dependency dependency2 = new Dependency(new List<Atom> { new Atom("S", new List<string> { "?x", "?y" }) }, new List<Atom> { new Atom("T", new List<string> { "?x", "?y" }) });
            List<Dependency> dependencies = new List<Dependency>
            {
                dependency1,
                dependency2
            };
            Database chasedDb = ChaseProcessor.RunChase(database, dependencies);
            Assert.IsTrue(chasedDb.ContainsAtom(new Atom("R", new List<string> { "a", "b" })), "R atom should be present.");
            Assert.IsTrue(chasedDb.ContainsAtom(new Atom("S", new List<string> { "a", "b" })), "S atom should be generated.");
            Assert.IsTrue(chasedDb.ContainsAtom(new Atom("T", new List<string> { "a", "b" })), "T atom should be generated from chained dependency.");
            Assert.AreEqual(3, chasedDb.Atoms.Count, "Database should contain exactly 3 atoms.");
        }

        [TestMethod]
        public void TestLargeDatabaseChase()
        {
            Database database = new Database();
            int count = 100;
            for (int i = 0; i < count; i++)
            {
                database.AddAtom(new Atom("R", new List<string> { "a" + i.ToString(), "b" + i.ToString() }));
            }

            Dependency dependency = new Dependency(new List<Atom> { new Atom("R", new List<string> { "?x", "?y" }) }, new List<Atom> { new Atom("S", new List<string> { "?x", "?y" }) });
            List<Dependency> dependencies = new List<Dependency>
            {
                dependency
            };
            Database chasedDb = ChaseProcessor.RunChase(database, dependencies);
            for (int i = 0; i < count; i++)
            {
                Atom expectedS = new Atom("S", new List<string> { "a" + i.ToString(), "b" + i.ToString() });
                Assert.IsTrue(chasedDb.ContainsAtom(expectedS), "Database should contain S atom for index " + i.ToString());
            }

            Assert.AreEqual(count * 2, chasedDb.Atoms.Count, "Database should contain both R and S atoms for each entry.");
        }
    }
}