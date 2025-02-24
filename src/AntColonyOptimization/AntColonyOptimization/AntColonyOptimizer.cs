using System;
using System.Collections.Generic;

namespace AntColonyOptimization
{
    public class AntColonyOptimizer
    {
        private double[, ] _distances;
        private int _numberOfCities;
        private int _numberOfAnts;
        private int _iterations;
        private double _alpha;
        private double _beta;
        private double _evaporationRate;
        private double[, ] _pheromones;
        private Random _random;
        public AntColonyOptimizer(double[, ] distances, int numberOfAnts, int iterations, double alpha, double beta, double evaporationRate)
        {
            _distances = distances;
            _numberOfCities = distances.GetLength(0);
            _numberOfAnts = numberOfAnts;
            _iterations = iterations;
            _alpha = alpha;
            _beta = beta;
            _evaporationRate = evaporationRate;
            _pheromones = new double[_numberOfCities, _numberOfCities];
            _random = new Random();
            InitializePheromones();
        }

        private void InitializePheromones()
        {
            double initialPheromone = 1.0;
            for (int i = 0; i < _numberOfCities; i++)
            {
                for (int j = 0; j < _numberOfCities; j++)
                {
                    _pheromones[i, j] = initialPheromone;
                }
            }
        }

        public Tuple<List<int>, double> Optimize()
        {
            List<int> bestRoute = new List<int>();
            double bestRouteLength = Double.MaxValue;
            for (int iteration = 0; iteration < _iterations; iteration++)
            {
                List<List<int>> allRoutes = new List<List<int>>();
                List<double> allRouteLengths = new List<double>();
                for (int ant = 0; ant < _numberOfAnts; ant++)
                {
                    List<int> route = ConstructRoute();
                    double routeLength = CalculateRouteLength(route);
                    allRoutes.Add(route);
                    allRouteLengths.Add(routeLength);
                    if (routeLength < bestRouteLength)
                    {
                        bestRouteLength = routeLength;
                        bestRoute = new List<int>(route);
                    }
                }

                UpdatePheromones(allRoutes, allRouteLengths);
            }

            return new Tuple<List<int>, double>(bestRoute, bestRouteLength);
        }

        private List<int> ConstructRoute()
        {
            List<int> route = new List<int>();
            bool[] visited = new bool[_numberOfCities];
            int currentCity = _random.Next(_numberOfCities);
            route.Add(currentCity);
            visited[currentCity] = true;
            for (int step = 1; step < _numberOfCities; step++)
            {
                int nextCity = SelectNextCity(currentCity, visited);
                route.Add(nextCity);
                visited[nextCity] = true;
                currentCity = nextCity;
            }

            return route;
        }

        private int SelectNextCity(int currentCity, bool[] visited)
        {
            double[] probabilities = new double[_numberOfCities];
            double sum = 0.0;
            for (int j = 0; j < _numberOfCities; j++)
            {
                if (!visited[j])
                {
                    double pheromone = Math.Pow(_pheromones[currentCity, j], _alpha);
                    double visibility = _distances[currentCity, j] > 0 ? Math.Pow(1.0 / _distances[currentCity, j], _beta) : 0.0;
                    probabilities[j] = pheromone * visibility;
                    sum += probabilities[j];
                }
                else
                {
                    probabilities[j] = 0.0;
                }
            }

            double threshold = _random.NextDouble() * sum;
            double cumulative = 0.0;
            for (int j = 0; j < _numberOfCities; j++)
            {
                if (!visited[j])
                {
                    cumulative += probabilities[j];
                    if (cumulative >= threshold)
                    {
                        return j;
                    }
                }
            }

            for (int j = 0; j < _numberOfCities; j++)
            {
                if (!visited[j])
                {
                    return j;
                }
            }

            return 0;
        }

        private double CalculateRouteLength(List<int> route)
        {
            double length = 0.0;
            for (int i = 0; i < route.Count - 1; i++)
            {
                length += _distances[route[i], route[i + 1]];
            }

            length += _distances[route[route.Count - 1], route[0]];
            return length;
        }

        private void UpdatePheromones(List<List<int>> allRoutes, List<double> allRouteLengths)
        {
            for (int i = 0; i < _numberOfCities; i++)
            {
                for (int j = 0; j < _numberOfCities; j++)
                {
                    _pheromones[i, j] *= (1.0 - _evaporationRate);
                }
            }

            for (int k = 0; k < allRoutes.Count; k++)
            {
                List<int> route = allRoutes[k];
                double routeLength = allRouteLengths[k];
                double depositAmount = 1.0 / routeLength;
                for (int i = 0; i < route.Count - 1; i++)
                {
                    int cityI = route[i];
                    int cityJ = route[i + 1];
                    _pheromones[cityI, cityJ] += depositAmount;
                    _pheromones[cityJ, cityI] += depositAmount;
                }

                int startCity = route[0];
                int endCity = route[route.Count - 1];
                _pheromones[endCity, startCity] += depositAmount;
                _pheromones[startCity, endCity] += depositAmount;
            }
        }
    }
}