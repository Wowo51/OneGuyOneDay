using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TrigramSearch;

namespace TrigramSearchTest
{
    [TestClass]
    public class TrigramIndexTests
    {
        [TestMethod]
        public void Test_AddDocument_NullDocId_DoesNotAddDocument()
        {
            TrigramIndex index = new TrigramIndex();
            index.AddDocument(null, "some content");
            List<string> matches = index.Search("some", 0.0);
            Assert.AreEqual(0, matches.Count, "Document with null ID should not be added.");
        }

        [TestMethod]
        public void Test_AddDocument_NullContent_HandlesGracefully()
        {
            TrigramIndex index = new TrigramIndex();
            index.AddDocument("doc1", null);
            List<string> matches = index.Search("any", 0.1);
            Assert.AreEqual(0, matches.Count, "Searching a document with null content should not yield a match for a non-empty query.");
        }

        [TestMethod]
        public void Test_Search_SingleDocument_FindsMatch()
        {
            TrigramIndex index = new TrigramIndex();
            index.AddDocument("doc1", "abcdef");
            List<string> matches = index.Search("abcde", 0.5);
            Assert.AreEqual(1, matches.Count, "Should find the matching document.");
            Assert.AreEqual("doc1", matches[0]);
        }

        [TestMethod]
        public void Test_Search_MultipleDocuments_ReturnsAllMatches()
        {
            TrigramIndex index = new TrigramIndex();
            index.AddDocument("doc1", "abcdef");
            index.AddDocument("doc2", "bcdefg");
            index.AddDocument("doc3", "cdefgh");
            List<string> matches = index.Search("cdef", 0.5);
            Assert.AreEqual(3, matches.Count, "All documents should match with the proper threshold.");
            CollectionAssert.Contains(matches, "doc1");
            CollectionAssert.Contains(matches, "doc2");
            CollectionAssert.Contains(matches, "doc3");
        }

        [TestMethod]
        public void Test_Search_WithLargeDataSet()
        {
            TrigramIndex index = new TrigramIndex();
            int documentCount = 100;
            for (int i = 0; i < documentCount; i++)
            {
                string docId = "doc" + i.ToString();
                char letter = (char)('a' + (i % 26));
                string content = new string (letter, 50);
                index.AddDocument(docId, content);
            }

            List<string> matches = index.Search("aaaaa", 0.8);
            // Only documents with content composed of 'a' will match; expected docs: doc0, doc26, doc52, doc78.
            Assert.AreEqual(4, matches.Count, "Should match four documents with repeated 'a'.");
        }

        [TestMethod]
        public void Test_Search_NullQuery_ReturnsNoMatches()
        {
            TrigramIndex index = new TrigramIndex();
            index.AddDocument("doc1", "abcdef");
            List<string> matches = index.Search(null, 0.1);
            Assert.AreEqual(0, matches.Count, "Searching with null query should yield no matches when threshold > 0.");
        }

        [TestMethod]
        public void Test_Search_EmptyDocument_ThresholdZero()
        {
            TrigramIndex index = new TrigramIndex();
            index.AddDocument("docEmpty", "");
            List<string> matches = index.Search("", 0.0);
            Assert.AreEqual(1, matches.Count, "Searching with empty query and empty document should match when threshold is zero.");
            Assert.AreEqual("docEmpty", matches[0]);
        }

        [TestMethod]
        public void Test_Search_EmptyDocument_ThresholdGreaterThanZero()
        {
            TrigramIndex index = new TrigramIndex();
            index.AddDocument("docEmpty", "");
            List<string> matches = index.Search("", 0.1);
            Assert.AreEqual(0, matches.Count, "Searching with empty query and empty document should not match when threshold > 0.");
        }
    }
}