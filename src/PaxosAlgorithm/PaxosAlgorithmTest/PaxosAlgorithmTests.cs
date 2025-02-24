using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using PaxosAlgorithm;

namespace PaxosAlgorithmTest
{
    [TestClass]
    public class PaxosAlgorithmTests
    {
        [TestMethod]
        public void PaxosAcceptor_ReceivePrepare_PositiveTest()
        {
            PaxosAcceptor acceptor = new PaxosAcceptor();
            bool firstPromise = acceptor.ReceivePrepare(1);
            bool secondPromise = acceptor.ReceivePrepare(1);
            Assert.IsTrue(firstPromise, "Expected first prepare to be accepted.");
            Assert.IsFalse(secondPromise, "Expected second prepare to be rejected.");
        }

        [TestMethod]
        public void PaxosAcceptor_ReceiveAccept_PositiveTest()
        {
            PaxosAcceptor acceptor = new PaxosAcceptor();
            bool prepareResult = acceptor.ReceivePrepare(5);
            bool acceptResult1 = acceptor.ReceiveAccept(5, "Value1");
            bool acceptResult2 = acceptor.ReceiveAccept(4, "Value2");
            Assert.IsTrue(prepareResult, "Expected prepare to be accepted.");
            Assert.IsTrue(acceptResult1, "Expected accept with equal proposal to be accepted.");
            Assert.IsFalse(acceptResult2, "Expected accept with lower proposal to be rejected.");
        }

        [TestMethod]
        public void PaxosProposer_RunProposal_SuccessfulTest()
        {
            List<PaxosAcceptor> acceptors = new List<PaxosAcceptor>();
            PaxosAcceptor acceptor1 = new PaxosAcceptor();
            PaxosAcceptor acceptor2 = new PaxosAcceptor();
            PaxosAcceptor acceptor3 = new PaxosAcceptor();
            acceptors.Add(acceptor1);
            acceptors.Add(acceptor2);
            acceptors.Add(acceptor3);
            PaxosProposer proposer = new PaxosProposer(3, "TestValue", acceptors);
            bool result = proposer.RunProposal();
            Assert.IsTrue(result, "Expected proposer to succeed with majority acceptance.");
        }

        [TestMethod]
        public void PaxosProposer_RunProposal_FailureTest()
        {
            List<PaxosAcceptor> acceptors = new List<PaxosAcceptor>();
            PaxosAcceptor acceptor1 = new PaxosAcceptor();
            PaxosAcceptor acceptor2 = new PaxosAcceptor();
            PaxosAcceptor acceptor3 = new PaxosAcceptor();
            // Pre-set acceptor2 and acceptor3 so that they reject a lower proposal.
            acceptor2.ReceivePrepare(10);
            acceptor3.ReceivePrepare(10);
            acceptors.Add(acceptor1);
            acceptors.Add(acceptor2);
            acceptors.Add(acceptor3);
            PaxosProposer proposer = new PaxosProposer(5, "TestValue", acceptors);
            bool result = proposer.RunProposal();
            Assert.IsFalse(result, "Expected proposer to fail because not enough promises.");
        }

        [TestMethod]
        public void PaxosLearner_Learn_MajorityTest()
        {
            List<PaxosAcceptor> acceptors = new List<PaxosAcceptor>();
            PaxosAcceptor acceptor1 = new PaxosAcceptor();
            PaxosAcceptor acceptor2 = new PaxosAcceptor();
            PaxosAcceptor acceptor3 = new PaxosAcceptor();
            acceptor1.ReceiveAccept(1, "A");
            acceptor2.ReceiveAccept(1, "A");
            acceptor3.ReceiveAccept(1, "B");
            acceptors.Add(acceptor1);
            acceptors.Add(acceptor2);
            acceptors.Add(acceptor3);
            string learned = PaxosLearner.Learn(acceptors);
            Assert.AreEqual("A", learned, "Expected majority value 'A' to be learned.");
        }

        [TestMethod]
        public void PaxosLearner_Learn_NoMajorityTest()
        {
            List<PaxosAcceptor> acceptors = new List<PaxosAcceptor>();
            PaxosAcceptor acceptor1 = new PaxosAcceptor();
            PaxosAcceptor acceptor2 = new PaxosAcceptor();
            PaxosAcceptor acceptor3 = new PaxosAcceptor();
            acceptor1.ReceiveAccept(1, "A");
            acceptor2.ReceiveAccept(1, "B");
            acceptor3.ReceiveAccept(1, "C");
            acceptors.Add(acceptor1);
            acceptors.Add(acceptor2);
            acceptors.Add(acceptor3);
            string learned = PaxosLearner.Learn(acceptors);
            Assert.AreEqual(string.Empty, learned, "Expected no majority value, so empty string.");
        }

        [TestMethod]
        public void Paxos_Synthetic_LargeTest()
        {
            System.Random random = new System.Random();
            int acceptorCount = 101;
            List<PaxosAcceptor> acceptors = new List<PaxosAcceptor>();
            for (System.Int32 i = 0; i < acceptorCount; i++)
            {
                acceptors.Add(new PaxosAcceptor());
            }

            PaxosProposer proposer = new PaxosProposer(50, "LargeValue", acceptors);
            bool proposalResult = proposer.RunProposal();
            string learned = PaxosLearner.Learn(acceptors);
            Assert.IsTrue(proposalResult, "Expected large synthetic proposal to succeed.");
            Assert.AreEqual("LargeValue", learned, "Expected majority learned value to be 'LargeValue'.");
        }
    }
}