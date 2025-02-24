using System;
using System.Collections.Generic;

namespace HopfieldNet
{
    public class HopfieldNetwork
    {
        private int _size;
        private double[, ] _weights;
        public HopfieldNetwork(int size)
        {
            _size = (size > 0) ? size : 1;
            _weights = new double[_size, _size];
            for (int i = 0; i < _size; i++)
            {
                for (int j = 0; j < _size; j++)
                {
                    _weights[i, j] = 0.0;
                }
            }
        }

        public void Train(IEnumerable<int[]> patterns)
        {
            if (patterns == null)
            {
                return;
            }

            foreach (int[] pattern in patterns)
            {
                if (pattern == null || pattern.Length != _size)
                {
                    continue;
                }

                for (int i = 0; i < _size; i++)
                {
                    for (int j = i + 1; j < _size; j++)
                    {
                        double change = pattern[i] * pattern[j];
                        _weights[i, j] += change;
                        _weights[j, i] = _weights[i, j];
                    }
                }
            }

            for (int i = 0; i < _size; i++)
            {
                _weights[i, i] = 0.0;
            }
        }

        public int[] Recall(int[] inputState)
        {
            int[] currentState;
            if (inputState == null || inputState.Length != _size)
            {
                currentState = new int[_size];
                for (int i = 0; i < _size; i++)
                {
                    currentState[i] = 1;
                }
            }
            else
            {
                currentState = new int[_size];
                for (int i = 0; i < _size; i++)
                {
                    currentState[i] = inputState[i];
                }
            }

            bool stateChanged = true;
            while (stateChanged)
            {
                stateChanged = false;
                for (int i = 0; i < _size; i++)
                {
                    double sum = 0.0;
                    for (int j = 0; j < _size; j++)
                    {
                        sum += _weights[i, j] * currentState[j];
                    }

                    int updatedState = (sum >= 0) ? 1 : -1;
                    if (updatedState != currentState[i])
                    {
                        currentState[i] = updatedState;
                        stateChanged = true;
                    }
                }
            }

            return currentState;
        }
    }
}