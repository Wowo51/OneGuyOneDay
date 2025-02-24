using Microsoft.VisualStudio.TestTools.UnitTesting;
using DijkstraScholtenAlgorithm;

namespace DijkstraScholtenAlgorithmTest
{
    [TestClass]
    public class ProcessNodeTests
    {
        [TestMethod]
        public void TestActivateWithoutVia()
        {
            ProcessNode node = new ProcessNode("Node1");
            node.Activate(null);
            Assert.IsTrue(node.Active);
            Assert.IsNull(node.Parent);
        }

        [TestMethod]
        public void TestActivateWithVia()
        {
            ProcessNode parent = new ProcessNode("Parent");
            ProcessNode child = new ProcessNode("Child");
            child.Activate(parent);
            Assert.IsTrue(child.Active);
            Assert.AreEqual(parent, child.Parent);
        }

        [TestMethod]
        public void TestDoubleActivationDoesNotChangeParent()
        {
            ProcessNode node = new ProcessNode("Node");
            ProcessNode firstSender = new ProcessNode("Sender1");
            ProcessNode secondSender = new ProcessNode("Sender2");
            node.Activate(firstSender);
            node.Activate(secondSender);
            Assert.AreEqual(firstSender, node.Parent);
        }

        [TestMethod]
        public void TestSendWorkActivatesTarget()
        {
            ProcessNode sender = new ProcessNode("Sender");
            ProcessNode target = new ProcessNode("Target");
            sender.SendWork(target);
            Assert.AreEqual(1, sender.Credit);
            Assert.IsTrue(target.Active);
            Assert.AreEqual(sender, target.Parent);
        }

        [TestMethod]
        public void TestReceiveWorkDoesNotOverrideExistingActivation()
        {
            ProcessNode initialSender = new ProcessNode("Sender1");
            ProcessNode alternateSender = new ProcessNode("Sender2");
            ProcessNode target = new ProcessNode("Target");
            target.Activate(initialSender);
            target.ReceiveWork(alternateSender);
            Assert.AreEqual(initialSender, target.Parent);
        }

        [TestMethod]
        public void TestWorkDonePropagation()
        {
            // Create a chain: root -> child -> grandchild.
            ProcessNode root = new ProcessNode("Root");
            root.Activate(null);
            ProcessNode child = new ProcessNode("Child");
            ProcessNode grandchild = new ProcessNode("Grandchild");
            root.SendWork(child);
            child.SendWork(grandchild);
            // Complete work in reverse order.
            grandchild.WorkDone();
            child.WorkDone();
            root.WorkDone();
            Assert.IsFalse(grandchild.Active);
            Assert.IsNull(grandchild.Parent);
            Assert.IsFalse(child.Active);
            Assert.IsNull(child.Parent);
            Assert.IsFalse(root.Active);
            Assert.AreEqual(0, root.Credit);
        }

        [TestMethod]
        public void TestMultipleWorkSends()
        {
            // Root sends work to two children.
            ProcessNode root = new ProcessNode("Root");
            root.Activate(null);
            ProcessNode child1 = new ProcessNode("Child1");
            ProcessNode child2 = new ProcessNode("Child2");
            root.SendWork(child1);
            root.SendWork(child2);
            Assert.AreEqual(2, root.Credit);
            Assert.IsTrue(child1.Active);
            Assert.IsTrue(child2.Active);
            Assert.AreEqual(root, child1.Parent);
            Assert.AreEqual(root, child2.Parent);
            child1.WorkDone();
            Assert.AreEqual(1, root.Credit);
            child2.WorkDone();
            Assert.AreEqual(0, root.Credit);
            root.WorkDone();
            Assert.IsFalse(root.Active);
        }

        [TestMethod]
        public void TestExplicitReceiveAck()
        {
            // Use SendWork to set credit and then directly test ReceiveAck.
            ProcessNode sender = new ProcessNode("Sender");
            ProcessNode target = new ProcessNode("Target");
            sender.SendWork(target);
            Assert.AreEqual(1, sender.Credit);
            sender.ReceiveAck();
            Assert.AreEqual(0, sender.Credit);
        }

        [TestMethod]
        public void TestLargeChainPropagation()
        {
            const int chainLength = 100;
            ProcessNode[] nodes = new ProcessNode[chainLength];
            for (int i = 0; i < chainLength; i++)
            {
                nodes[i] = new ProcessNode("Node" + i.ToString());
                if (i == 0)
                {
                    nodes[i].Activate(null);
                }
            }

            for (int i = 0; i < chainLength - 1; i++)
            {
                nodes[i].SendWork(nodes[i + 1]);
            }

            for (int i = chainLength - 1; i >= 0; i--)
            {
                nodes[i].WorkDone();
            }

            for (int i = 0; i < chainLength; i++)
            {
                Assert.IsFalse(nodes[i].Active);
                Assert.AreEqual(0, nodes[i].Credit);
                Assert.IsNull(nodes[i].Parent);
            }
        }
    }
}