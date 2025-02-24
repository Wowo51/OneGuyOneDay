using Microsoft.VisualStudio.TestTools.UnitTesting;
using HindleyMilnerAlgorithm;
using System;
using System.Collections.Generic;

namespace HindleyMilnerAlgorithmTest
{
    [TestClass]
    public class TypeInferenceTests
    {
        [TestMethod]
        public void TestVarNotDeclared()
        {
            Dictionary<string, Scheme> env = new Dictionary<string, Scheme>();
            Expr expr = new Var("x");
            Tuple<Substitution, HMType> result = TypeInference.AlgorithmW(env, expr);
            Assert.IsTrue(result.Item1.Failed, "Expected failure for undeclared variable.");
        }

        [TestMethod]
        public void TestIdentityLambda()
        {
            Dictionary<string, Scheme> env = new Dictionary<string, Scheme>();
            Expr expr = new Lam("x", new Var("x"));
            Tuple<Substitution, HMType> result = TypeInference.AlgorithmW(env, expr);
            Assert.IsFalse(result.Item1.Failed, "Identity lambda should not fail.");
            HMType inferredType = result.Item2;
            Assert.IsTrue(inferredType is TypeOperator, "Expected function type.");
            TypeOperator funcType = (TypeOperator)inferredType;
            Assert.AreEqual("->", funcType.Name, "Expected arrow type.");
            Assert.AreEqual(2, funcType.Types.Count, "Arrow type should have two components.");
            string paramType = funcType.Types[0]!.ToString();
            string returnType = funcType.Types[1]!.ToString();
            Assert.AreEqual(paramType, returnType, "Parameter and return type should be the same in identity lambda.");
        }

        [TestMethod]
        public void TestApplicationOfIdentities()
        {
            Dictionary<string, Scheme> env = new Dictionary<string, Scheme>();
            Expr identity1 = new Lam("x", new Var("x"));
            Expr identity2 = new Lam("y", new Var("y"));
            Expr expr = new App(identity1, identity2);
            Tuple<Substitution, HMType> result = TypeInference.AlgorithmW(env, expr);
            Assert.IsFalse(result.Item1.Failed, "Application of identities should not fail.");
            HMType inferredType = result.Item2;
            Assert.IsTrue(inferredType is TypeOperator, "Expected function type as result.");
            TypeOperator funcType = (TypeOperator)inferredType;
            Assert.AreEqual("->", funcType.Name, "Expected arrow type.");
            Assert.AreEqual(2, funcType.Types.Count, "Arrow type should have two components.");
            string paramType = funcType.Types[0]!.ToString();
            string returnType = funcType.Types[1]!.ToString();
            Assert.AreEqual(paramType, returnType, "Parameter and return type should be the same in the resulting identity function.");
        }

        [TestMethod]
        public void TestLetIdentity()
        {
            Dictionary<string, Scheme> env = new Dictionary<string, Scheme>();
            Expr idLambda = new Lam("y", new Var("y"));
            Expr letExpr = new Let("id", idLambda, new Var("id"));
            Tuple<Substitution, HMType> result = TypeInference.AlgorithmW(env, letExpr);
            Assert.IsFalse(result.Item1.Failed, "Let identity should not fail.");
            HMType inferredType = result.Item2;
            Assert.IsTrue(inferredType is TypeOperator, "Expected function type as result.");
            TypeOperator funcType = (TypeOperator)inferredType;
            Assert.AreEqual("->", funcType.Name, "Expected arrow type.");
            Assert.AreEqual(2, funcType.Types.Count, "Arrow type should have two components.");
            string paramType = funcType.Types[0]!.ToString();
            string returnType = funcType.Types[1]!.ToString();
            Assert.AreEqual(paramType, returnType, "Parameter and return type should be identical in identity function.");
        }

        [TestMethod]
        public void TestUnifyFailure()
        {
            HMType typeVar = new TypeVariable("a");
            HMType arrowType = new TypeOperator("->", new List<HMType> { new TypeVariable("a"), new TypeVariable("b") });
            Substitution sub = TypeInference.Unify(typeVar, arrowType);
            Assert.IsTrue(sub.Failed, "Unification should fail due to cyclic dependency.");
        }

        [TestMethod]
        public void TestNestedLambda()
        {
            Dictionary<string, Scheme> env = new Dictionary<string, Scheme>();
            Expr expr = new Lam("x", new Lam("y", new App(new Var("x"), new Var("y"))));
            Tuple<Substitution, HMType> result = TypeInference.AlgorithmW(env, expr);
            Assert.IsFalse(result.Item1.Failed, "Nested lambda should not fail.");
            Assert.IsTrue(result.Item2 is TypeOperator, "Expected function type.");
        }

        [TestMethod]
        public void TestUnifySuccess()
        {
            HMType typeVar = new TypeVariable("a");
            Substitution sub = TypeInference.Unify(typeVar, typeVar);
            Assert.IsFalse(sub.Failed, "Unification should succeed when types are identical.");
        }

        [TestMethod]
        public void TestUnifyArrowSuccess()
        {
            HMType a = new TypeVariable("a");
            HMType b = new TypeVariable("b");
            HMType arrow1 = new TypeOperator("->", new List<HMType> { a, b });
            HMType arrow2 = new TypeOperator("->", new List<HMType> { a, b });
            Substitution sub = TypeInference.Unify(arrow1, arrow2);
            Assert.IsFalse(sub.Failed, "Unification should succeed for identical arrow types.");
        }
    }
}