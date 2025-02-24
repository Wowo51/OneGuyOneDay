using System;
using RaftAlgorithm;

public static class Program
{
    public static void Main(string[] args)
    {
        RaftCluster cluster = new RaftCluster();
        cluster.AddNode(new RaftNode("1"));
        cluster.AddNode(new RaftNode("2"));
        cluster.AddNode(new RaftNode("3"));
        cluster.StartElection();
        RaftNode leader = cluster.GetLeader();
        Console.WriteLine("Leader NodeId: " + leader.NodeId);
    }
}