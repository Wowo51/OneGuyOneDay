using System;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RichSalzWildmat;

namespace RichSalzWildmatTest
{
    [TestClass]
    public class WildmatTests
    {
        [TestMethod]
        public void Match_NullPattern_ReturnsFalse()
        {
            bool result = Wildmat.Match(null, "text");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Match_NullText_ReturnsFalse()
        {
            bool result = Wildmat.Match("pattern", null);
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Match_BothEmpty_ReturnsTrue()
        {
            bool result = Wildmat.Match("", "");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Match_EmptyPattern_NonEmptyText_ReturnsFalse()
        {
            bool result = Wildmat.Match("", "nonempty");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Match_ExactMatch_ReturnsTrue()
        {
            bool result = Wildmat.Match("hello", "hello");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Match_ExactMismatch_ReturnsFalse()
        {
            bool result = Wildmat.Match("hello", "Hello");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Match_QuestionMark_Wildcard_ReturnsTrue()
        {
            bool result = Wildmat.Match("h?llo", "hello");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Match_Asterisk_Wildcard_ReturnsTrue()
        {
            bool result = Wildmat.Match("he*o", "hello");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Match_AsteriskAtEnd_MatchesRemaining()
        {
            bool result = Wildmat.Match("he*", "hello");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Match_CharacterSet_ReturnsTrue()
        {
            bool result = Wildmat.Match("h[ae]llo", "hello");
            Assert.IsTrue(result);
            result = Wildmat.Match("h[ae]llo", "hallo");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Match_CharacterSet_ReturnsFalseForMismatch()
        {
            bool result = Wildmat.Match("h[ae]llo", "hxllo");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Match_CharacterSet_Negation_ReturnsTrue()
        {
            bool result = Wildmat.Match("h[^a]llo", "hello");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Match_CharacterSet_Negation_ReturnsFalse()
        {
            bool result = Wildmat.Match("h[^e]llo", "hello");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Match_ComplexPattern_ReturnsTrue()
        {
            bool result = Wildmat.Match("t*st?", "testa");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Match_LongRandomString_WithAsterisk_ReturnsTrue()
        {
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < 1000; i++)
            {
                stringBuilder.Append("a");
            }

            string longText = stringBuilder.ToString();
            bool result = Wildmat.Match("*", longText);
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Match_UnclosedBracket_ReturnsFalse()
        {
            bool result = Wildmat.Match("[a", "a");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Match_MixedComplexPattern_ReturnsTrue()
        {
            bool result = Wildmat.Match("h?l*o", "hello");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Match_SingleCharacter_ReturnsTrue()
        {
            bool result = Wildmat.Match("a", "a");
            Assert.IsTrue(result);
        }

        [TestMethod]
        public void Match_SingleCharacter_ReturnsFalse()
        {
            bool result = Wildmat.Match("a", "b");
            Assert.IsFalse(result);
        }

        [TestMethod]
        public void Match_MultipleWildcards_ReturnsTrue()
        {
            bool result = Wildmat.Match("a*b?c", "axybzc");
            Assert.IsTrue(result);
        }
    }
}