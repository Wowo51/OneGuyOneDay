using System;

namespace LinearFeedbackRegister
{
    public class Lfsr
    {
        private int _state;
        private readonly int _tapMask;
        public Lfsr(int seed, int tapMask)
        {
            if (seed == 0)
            {
                _state = 1;
            }
            else
            {
                _state = seed;
            }

            _tapMask = tapMask;
        }

        public int Next()
        {
            int feedback = Parity(_state & _tapMask) ? 1 : 0;
            _state = (_state >> 1) | (feedback << 31);
            return _state;
        }

        private bool Parity(int value)
        {
            int count = 0;
            int temp = value;
            while (temp != 0)
            {
                count += temp & 1;
                temp = temp >> 1;
            }

            return (count % 2) != 0;
        }
    }
}