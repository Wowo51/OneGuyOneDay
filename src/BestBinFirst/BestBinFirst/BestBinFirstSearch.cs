using System;
using System.Collections.Generic;

namespace BestBinFirst
{
    public class BestBinFirstSearch
    {
        private KdTree kdTree;
        public BestBinFirstSearch(List<Point> points)
        {
            if (points == null)
            {
                points = new List<Point>();
            }

            kdTree = new KdTree(points);
        }

        public Point? NearestNeighbor(Point query, int maxChecks)
        {
            if (query == null || kdTree.Root == null)
            {
                return null;
            }

            double bestDistance = double.MaxValue;
            Point? bestPoint = null;
            int checks = 0;
            PriorityQueue<PriorityQueueItem, double> queue = new PriorityQueue<PriorityQueueItem, double>();
            KdTree.Node root = kdTree.Root;
            double rootDistance = BoundingBoxDistance(root, query);
            queue.Enqueue(new PriorityQueueItem(root), rootDistance);
            while (queue.Count > 0 && checks < maxChecks)
            {
                PriorityQueueItem currentItem = queue.Dequeue();
                KdTree.Node currentNode = currentItem.Node;
                double d = query.DistanceSquared(currentNode.Point);
                checks++;
                if (d < bestDistance)
                {
                    bestDistance = d;
                    bestPoint = currentNode.Point;
                }

                if (currentNode.Left != null)
                {
                    double leftDist = BoundingBoxDistance(currentNode.Left, query);
                    if (leftDist < bestDistance)
                    {
                        queue.Enqueue(new PriorityQueueItem(currentNode.Left), leftDist);
                    }
                }

                if (currentNode.Right != null)
                {
                    double rightDist = BoundingBoxDistance(currentNode.Right, query);
                    if (rightDist < bestDistance)
                    {
                        queue.Enqueue(new PriorityQueueItem(currentNode.Right), rightDist);
                    }
                }
            }

            return bestPoint;
        }

        private double BoundingBoxDistance(KdTree.Node node, Point query)
        {
            double distance = 0;
            for (int i = 0; i < query.Dimension; i++)
            {
                double diff = 0;
                if (query.Coordinates[i] < node.LowerBounds[i])
                {
                    diff = node.LowerBounds[i] - query.Coordinates[i];
                }
                else if (query.Coordinates[i] > node.UpperBounds[i])
                {
                    diff = query.Coordinates[i] - node.UpperBounds[i];
                }

                distance += diff * diff;
            }

            return distance;
        }

        private class PriorityQueueItem
        {
            public KdTree.Node Node;
            public PriorityQueueItem(KdTree.Node node)
            {
                Node = node;
            }
        }
    }
}