using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using RaftAlgorithm;

namespace RaftAlgorithmTest
{
    [TestClass]
    public class RaftAlgorithmTests
    {
        [TestMethod]
        public void TestSuccessfulElection()
        {
            RaftCluster cluster = new RaftCluster();
            RaftNode node1 = new RaftNode("1");
            RaftNode node2 = new RaftNode("2");
            RaftNode node3 = new RaftNode("3");
            cluster.AddNode(node1);
            cluster.AddNode(node2);
            cluster.AddNode(node3);
            cluster.StartElection();
            Assert.AreEqual(RaftState.Leader, node1.State, "Node1 should become leader.");
            RaftNode leader = cluster.GetLeader();
            Assert.IsNotNull(leader, "Leader should be present.");
            Assert.AreEqual("1", leader.NodeId, "Leader should be Node1.");
        }

        [TestMethod]
        public void TestFailedElectionDueToHigherTermNodes()
        {
            RaftCluster cluster = new RaftCluster();
            RaftNode node1 = new RaftNode("1");
            RaftNode node2 = new RaftNode("2");
            RaftNode node3 = new RaftNode("3");
            // Set nodes 2 and 3 with a higher term to force vote rejection.
            node2.CurrentTerm = 2;
            node3.CurrentTerm = 2;
            cluster.AddNode(node1);
            cluster.AddNode(node2);
            cluster.AddNode(node3);
            cluster.StartElection();
            // Election should fail if candidate does not receive majority.
            Assert.AreEqual(RaftState.Follower, node1.State, "Node1 should revert to follower on failed election.");
            Assert.IsNull(node1.VotedFor, "Node1's VotedFor should be null after failed election.");
            bool leaderExists = false;
            foreach (RaftNode node in cluster.Nodes)
            {
                if (node.State == RaftState.Leader)
                {
                    leaderExists = true;
                    break;
                }
            }

            Assert.IsFalse(leaderExists, "No leader should be elected in a failed election.");
        }

        [TestMethod]
        public void TestAppendEntriesUpdatesLogAndTerm()
        {
            RaftNode node = new RaftNode("1");
            node.State = RaftState.Candidate;
            node.CurrentTerm = 1;
            List<RaftLogEntry> entries = new List<RaftLogEntry>();
            entries.Add(new RaftLogEntry(1, 1, "command1"));
            entries.Add(new RaftLogEntry(2, 1, "command2"));
            node.AppendEntries(1, "Leader1", entries);
            Assert.AreEqual(1, node.CurrentTerm, "Node's term should be updated to 1.");
            Assert.AreEqual(RaftState.Follower, node.State, "Node should become follower after receiving AppendEntries.");
            Assert.AreEqual("Leader1", node.VotedFor, "VotedFor should be set to Leader1.");
            Assert.AreEqual(2, node.Log.Count, "Log should contain 2 entries.");
            int oldLogCount = node.Log.Count;
            node.AppendEntries(0, "Leader0", entries);
            Assert.AreEqual(oldLogCount, node.Log.Count, "Log should not change on outdated AppendEntries.");
        }

        [TestMethod]
        public void TestRequestVoteVotingLogic()
        {
            RaftNode node = new RaftNode("1");
            node.CurrentTerm = 1;
            node.VotedFor = null;
            bool vote1 = node.RequestVote(1, "A");
            Assert.IsTrue(vote1, "Vote should be granted if no prior vote.");
            Assert.AreEqual("A", node.VotedFor, "VotedFor should be set to candidate 'A'.");
            bool vote2 = node.RequestVote(1, "B");
            Assert.IsFalse(vote2, "Vote should be rejected if already voted for a different candidate.");
            bool vote3 = node.RequestVote(2, "C");
            Assert.IsTrue(vote3, "Vote should be granted if candidate term is higher.");
            Assert.AreEqual(2, node.CurrentTerm, "Current term should update to new term.");
            Assert.AreEqual("C", node.VotedFor, "VotedFor should be set to candidate 'C'.");
        }

        [TestMethod]
        public void TestAddLogEntry()
        {
            RaftNode node = new RaftNode("1");
            Assert.AreEqual(0, node.Log.Count, "Initial log should be empty.");
            RaftLogEntry entry = new RaftLogEntry(1, 1, "command");
            node.AddLogEntry(entry);
            Assert.AreEqual(1, node.Log.Count, "Log should contain one entry.");
            Assert.AreEqual("command", node.Log[0].Command, "Log entry command should match.");
        }

        [TestMethod]
        public void TestSyntheticLargeLogEntries()
        {
            RaftNode node = new RaftNode("1");
            int numberOfEntries = 1000;
            for (int i = 1; i <= numberOfEntries; i++)
            {
                node.AddLogEntry(new RaftLogEntry(i, 1, "cmd" + i));
            }

            Assert.AreEqual(numberOfEntries, node.Log.Count, "Log should have 1000 entries.");
            Assert.AreEqual("cmd1", node.Log[0].Command, "First command should match.");
            Assert.AreEqual("cmd1000", node.Log[999].Command, "Last command should match.");
        }
    }
}