using System;
using System.Collections.Generic;

namespace ElevatorAlgorithm
{
    public class Program
    {
        public static void Main(string[] args)
        {
            ElevatorScheduler scheduler = new ElevatorScheduler();
            List<int> requests = new List<int>
            {
                10,
                90,
                30,
                70,
                20,
                50
            };
            int initialPosition = 50;
            bool movingUp = true;
            int maxCylinder = 100;
            List<int> order = scheduler.Schedule(initialPosition, requests, movingUp, maxCylinder);
            Console.WriteLine("Scheduled Order:");
            foreach (int request in order)
            {
                Console.Write(request + " ");
            }

            Console.WriteLine();
        }
    }
}