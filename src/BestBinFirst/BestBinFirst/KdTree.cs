using System;
using System.Collections.Generic;

namespace BestBinFirst
{
    public class KdTree
    {
        public Node? Root { get; private set; }

        public KdTree(List<Point> points)
        {
            if (points == null || points.Count == 0)
            {
                Root = null;
            }
            else
            {
                int dimension = points[0].Dimension;
                double[] lowerBounds = new double[dimension];
                double[] upperBounds = new double[dimension];
                for (int i = 0; i < dimension; i++)
                {
                    lowerBounds[i] = double.NegativeInfinity;
                    upperBounds[i] = double.PositiveInfinity;
                }

                Root = BuildKdTree(points, 0, lowerBounds, upperBounds);
            }
        }

        public class Node
        {
            public Point Point;
            public int SplittingDimension;
            public Node? Left;
            public Node? Right;
            public double[] LowerBounds;
            public double[] UpperBounds;
            public Node(Point point, int splittingDimension, double[] lowerBounds, double[] upperBounds)
            {
                Point = point;
                SplittingDimension = splittingDimension;
                LowerBounds = lowerBounds;
                UpperBounds = upperBounds;
                Left = null;
                Right = null;
            }
        }

        private Node? BuildKdTree(List<Point> points, int depth, double[] lowerBounds, double[] upperBounds)
        {
            if (points == null || points.Count == 0)
            {
                return null;
            }

            int dimension = points[0].Dimension;
            int axis = depth % dimension;
            points.Sort((Point a, Point b) => a.Coordinates[axis].CompareTo(b.Coordinates[axis]));
            int medianIndex = points.Count / 2;
            Point medianPoint = points[medianIndex];
            double[] leftUpper = (double[])upperBounds.Clone();
            leftUpper[axis] = medianPoint.Coordinates[axis];
            double[] rightLower = (double[])lowerBounds.Clone();
            rightLower[axis] = medianPoint.Coordinates[axis];
            List<Point> leftPoints = new List<Point>();
            List<Point> rightPoints = new List<Point>();
            for (int i = 0; i < points.Count; i++)
            {
                if (i == medianIndex)
                {
                    continue;
                }

                if (points[i].Coordinates[axis] <= medianPoint.Coordinates[axis])
                {
                    leftPoints.Add(points[i]);
                }
                else
                {
                    rightPoints.Add(points[i]);
                }
            }

            Node node = new Node(medianPoint, axis, lowerBounds, upperBounds);
            node.Left = BuildKdTree(leftPoints, depth + 1, lowerBounds, leftUpper);
            node.Right = BuildKdTree(rightPoints, depth + 1, rightLower, upperBounds);
            return node;
        }
    }
}