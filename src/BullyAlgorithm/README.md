
# Bully Election Algorithm Guide

The Bully Algorithm is a classical leader election algorithm used in distributed systems. Each process in the system is assigned a unique numeric identifier; the process with the highest identifier among the active processes becomes the coordinator. When a process detects that the current coordinator is unresponsive, it starts an election by sending messages to all processes with a higher identifier. If any higher process is alive, it takes over the election; otherwise, the initiating process becomes the new coordinator and notifies all other processes.

## How to Use the Source Code

The source code is implemented in C# and demonstrates how to simulate and perform leader election using the Bully Algorithm. It includes:

1. A core library that implements the BullyElection class and the Process class.
2. A console application that simulates process failures and initiates an election.
3. A suite of unit tests covering various scenarios such as all processes active, some processes down, and all processes down.

To use the source code:
- Build the solution targeting .NET 9.0.
- Run the console application to see the election process in action.
- Review and run the unit tests in the test project to understand the algorithm’s behavior under different conditions.
- Adapt the algorithm for integration into your own distributed system projects as needed.

This guide provides an overview of the Bully Election Algorithm and practical instructions on using the associated code.
