using System.Threading;

namespace PetersonAlgorithm
{
    public class PetersonLock
    {
        private volatile bool flag0;
        private volatile bool flag1;
        private volatile int turn;
        public PetersonLock()
        {
            flag0 = false;
            flag1 = false;
            turn = 0;
        }

        public void Lock(int processId)
        {
            if (processId == 0)
            {
                flag0 = true;
                turn = 1;
                while (flag1 && turn == 1)
                {
                    Thread.Yield();
                }
            }
            else if (processId == 1)
            {
                flag1 = true;
                turn = 0;
                while (flag0 && turn == 0)
                {
                    Thread.Yield();
                }
            }
            else
            {
            // Invalid processId; do nothing
            }
        }

        public void Unlock(int processId)
        {
            if (processId == 0)
            {
                flag0 = false;
            }
            else if (processId == 1)
            {
                flag1 = false;
            }
            else
            {
            // Invalid processId; do nothing
            }
        }
    }
}