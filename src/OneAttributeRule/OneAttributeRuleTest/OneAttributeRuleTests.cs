using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using OneAttributeRule;

namespace OneAttributeRuleTest
{
    [TestClass]
    public class OneAttributeRuleTests
    {
        [TestMethod]
        public void TestAttributeApplicationOnClass()
        {
            System.Type dummyType = typeof(DummyWithAttribute);
            object[] attributes = dummyType.GetCustomAttributes(typeof(OneAttributeRuleAttribute), true);
            Assert.IsNotNull(attributes, "Attributes collection should not be null.");
            Assert.AreEqual(1, attributes.Length, "There should be exactly one OneAttributeRuleAttribute applied.");
        }

        [TestMethod]
        public void TestAttributeApplicationOnMethod()
        {
            System.Reflection.MethodInfo methodInfo = typeof(DummyWithAttributedMethod).GetMethod("AttributedMethod");
            object[] attributes = methodInfo.GetCustomAttributes(typeof(OneAttributeRuleAttribute), true);
            Assert.IsNotNull(attributes, "Attributes collection on method should not be null.");
            Assert.AreEqual(1, attributes.Length, "Method should have exactly one OneAttributeRuleAttribute.");
        }

        [TestMethod]
        public void TestAttributeInheritance()
        {
            System.Type derivedType = typeof(DerivedClassFromBase);
            object[] attributes = derivedType.GetCustomAttributes(typeof(OneAttributeRuleAttribute), true);
            Assert.IsNotNull(attributes, "Inherited attribute collection should not be null.");
            Assert.AreEqual(1, attributes.Length, "Derived class should inherit exactly one OneAttributeRuleAttribute.");
        }

        [TestMethod]
        public void TestAttributeApplicationOnProperty()
        {
            System.Reflection.PropertyInfo propInfo = typeof(DummyWithAttributedProperty).GetProperty("AttributedProperty");
            object[] attributes = propInfo.GetCustomAttributes(typeof(OneAttributeRuleAttribute), true);
            Assert.IsNotNull(attributes, "Attributes collection on property should not be null.");
            Assert.AreEqual(1, attributes.Length, "Property should have exactly one OneAttributeRuleAttribute.");
        }

        [TestMethod]
        public void TestNoAttribute()
        {
            System.Type dummyType = typeof(DummyWithoutAttribute);
            object[] attributes = dummyType.GetCustomAttributes(typeof(OneAttributeRuleAttribute), true);
            Assert.IsNotNull(attributes, "Attributes collection should not be null.");
            Assert.AreEqual(0, attributes.Length, "No attribute should be applied to DummyWithoutAttribute.");
        }

        [DataTestMethod]
        [DataRow(1)]
        [DataRow(2)]
        [DataRow(3)]
        public void TestSyntheticData(int value)
        {
            Assert.IsTrue(value > 0, "Value must be positive.");
        }

        [DataTestMethod]
        [DynamicData(nameof(GetLargeSyntheticTestData), DynamicDataSourceType.Method)]
        public void TestSyntheticDataLargeSet(int value)
        {
            Assert.IsTrue(value > 0, "Generated value must be positive.");
        }

        public static IEnumerable<object[]> GetLargeSyntheticTestData()
        {
            List<object[]> dataList = new List<object[]>();
            for (System.Int32 i = 1; i <= 100; i++)
            {
                dataList.Add(new object[] { i });
            }

            return dataList;
        }
    }

    [OneAttributeRuleAttribute]
    public class DummyWithAttribute
    {
    }

    public class DummyWithoutAttribute
    {
    }

    public class DummyWithAttributedMethod
    {
        [OneAttributeRuleAttribute]
        public void AttributedMethod()
        {
        }
    }

    [OneAttributeRuleAttribute]
    public class BaseClassWithAttribute
    {
    }

    public class DerivedClassFromBase : BaseClassWithAttribute
    {
    }

    public class DummyWithAttributedProperty
    {
        [OneAttributeRuleAttribute]
        public System.String AttributedProperty { get; set; }
    }
}