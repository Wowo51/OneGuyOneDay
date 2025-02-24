using Microsoft.VisualStudio.TestTools.UnitTesting;
using VelvetAlgorithmSuite;
using System.Collections.Generic;
using System;

namespace VelvetAlgorithmSuiteTest
{
    [TestClass]
    public class SequenceAssemblerTests
    {
        [TestMethod]
        public void Assemble_NullReads_ReturnsEmptyString()
        {
            SequenceAssembler assembler = new SequenceAssembler();
            string result = assembler.Assemble(null, 3);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void Assemble_EmptyReads_ReturnsEmptyString()
        {
            SequenceAssembler assembler = new SequenceAssembler();
            string result = assembler.Assemble(new List<string>(), 3);
            Assert.AreEqual(string.Empty, result);
        }

        [TestMethod]
        public void Assemble_SingleRead_ReturnsOriginalSequence()
        {
            string sequence = "ATGC";
            SequenceAssembler assembler = new SequenceAssembler();
            string result = assembler.Assemble(new List<string> { sequence }, 3);
            Assert.AreEqual(sequence, result);
        }

        [TestMethod]
        public void Assemble_SingleRead_RandomSequence_ReturnsOriginalSequence()
        {
            string sequence = GenerateRandomSequence(50);
            SequenceAssembler assembler = new SequenceAssembler();
            string result = assembler.Assemble(new List<string> { sequence }, 5);
            Assert.AreEqual(sequence, result);
        }

        [TestMethod]
        public void Assemble_MultipleReads_ReturnsCorrectAssembly()
        {
            string original = "ABCDE";
            List<string> reads = new List<string>
            {
                "ABC",
                "BCD",
                "CDE"
            };
            SequenceAssembler assembler = new SequenceAssembler();
            string result = assembler.Assemble(reads, 3);
            Assert.AreEqual(original, result);
        }

        [TestMethod]
        public void Assemble_MultipleReads_LargeRandomSequence_ReturnsCorrectAssembly()
        {
            string original = GenerateRandomSequence(100);
            List<string> reads = new List<string>();
            for (int i = 0; i <= original.Length - 3; i++)
            {
                reads.Add(original.Substring(i, 3));
            }

            SequenceAssembler assembler = new SequenceAssembler();
            string result = assembler.Assemble(reads, 3);
            Assert.AreEqual(original, result);
        }

        private string GenerateRandomSequence(int length)
        {
            System.Text.StringBuilder sb = new System.Text.StringBuilder();
            Random random = new Random(42);
            for (int i = 0; i < length; i++)
            {
                int value = random.Next(0, 4);
                char nucleotide;
                switch (value)
                {
                    case 0:
                        nucleotide = 'A';
                        break;
                    case 1:
                        nucleotide = 'C';
                        break;
                    case 2:
                        nucleotide = 'G';
                        break;
                    default:
                        nucleotide = 'T';
                        break;
                }

                sb.Append(nucleotide);
            }

            return sb.ToString();
        }
    }
}