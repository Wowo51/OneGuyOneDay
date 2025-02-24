using System;

namespace Salsa20AndChaCha20
{
    public class ChaCha20
    {
        private byte[] key;
        private byte[] nonce;
        private uint counter;
        public ChaCha20(byte[] key, byte[] nonce, uint counter = 0)
        {
            if (key == null)
            {
                key = new byte[32];
            }
            else if (key.Length != 32)
            {
                byte[] newKey = new byte[32];
                int copyLength = key.Length < 32 ? key.Length : 32;
                Array.Copy(key, newKey, copyLength);
                key = newKey;
            }

            if (nonce == null)
            {
                nonce = new byte[12];
            }
            else if (nonce.Length != 12)
            {
                byte[] newNonce = new byte[12];
                int copyLength = nonce.Length < 12 ? nonce.Length : 12;
                Array.Copy(nonce, newNonce, copyLength);
                nonce = newNonce;
            }

            this.key = new byte[32];
            this.nonce = new byte[12];
            Array.Copy(key, this.key, 32);
            Array.Copy(nonce, this.nonce, 12);
            this.counter = counter;
        }

        private static uint RotateLeft(uint x, int n)
        {
            return (x << n) | (x >> (32 - n));
        }

        private static void QuarterRound(ref uint a, ref uint b, ref uint c, ref uint d)
        {
            a = a + b;
            d ^= a;
            d = RotateLeft(d, 16);
            c = c + d;
            b ^= c;
            b = RotateLeft(b, 12);
            a = a + b;
            d ^= a;
            d = RotateLeft(d, 8);
            c = c + d;
            b ^= c;
            b = RotateLeft(b, 7);
        }

        private void GenerateKeyStream(byte[] block, uint blockCounter)
        {
            uint[] state = new uint[16];
            state[0] = 0x61707865;
            state[1] = 0x3320646e;
            state[2] = 0x79622d32;
            state[3] = 0x6b206574;
            state[4] = BitConverter.ToUInt32(this.key, 0);
            state[5] = BitConverter.ToUInt32(this.key, 4);
            state[6] = BitConverter.ToUInt32(this.key, 8);
            state[7] = BitConverter.ToUInt32(this.key, 12);
            state[8] = BitConverter.ToUInt32(this.key, 16);
            state[9] = BitConverter.ToUInt32(this.key, 20);
            state[10] = BitConverter.ToUInt32(this.key, 24);
            state[11] = BitConverter.ToUInt32(this.key, 28);
            state[12] = blockCounter;
            state[13] = BitConverter.ToUInt32(this.nonce, 0);
            state[14] = BitConverter.ToUInt32(this.nonce, 4);
            state[15] = BitConverter.ToUInt32(this.nonce, 8);
            uint[] workingState = new uint[16];
            for (int i = 0; i < 16; i++)
            {
                workingState[i] = state[i];
            }

            for (int i = 0; i < 10; i++)
            {
                // Column rounds
                QuarterRound(ref workingState[0], ref workingState[4], ref workingState[8], ref workingState[12]);
                QuarterRound(ref workingState[1], ref workingState[5], ref workingState[9], ref workingState[13]);
                QuarterRound(ref workingState[2], ref workingState[6], ref workingState[10], ref workingState[14]);
                QuarterRound(ref workingState[3], ref workingState[7], ref workingState[11], ref workingState[15]);
                // Diagonal rounds
                QuarterRound(ref workingState[0], ref workingState[5], ref workingState[10], ref workingState[15]);
                QuarterRound(ref workingState[1], ref workingState[6], ref workingState[11], ref workingState[12]);
                QuarterRound(ref workingState[2], ref workingState[7], ref workingState[8], ref workingState[13]);
                QuarterRound(ref workingState[3], ref workingState[4], ref workingState[9], ref workingState[14]);
            }

            for (int i = 0; i < 16; i++)
            {
                uint result = workingState[i] + state[i];
                byte[] wordBytes = BitConverter.GetBytes(result);
                Buffer.BlockCopy(wordBytes, 0, block, i * 4, 4);
            }
        }

        public void ProcessBytes(byte[] input, int inputOffset, int inputCount, byte[] output, int outputOffset)
        {
            if (input == null || output == null)
            {
                return;
            }

            if (inputOffset < 0 || inputCount < 0 || inputOffset + inputCount > input.Length)
            {
                return;
            }

            if (outputOffset < 0 || outputOffset + inputCount > output.Length)
            {
                return;
            }

            byte[] keyStream = new byte[64];
            int processed = 0;
            while (inputCount > 0)
            {
                GenerateKeyStream(keyStream, this.counter);
                this.counter = this.counter + 1;
                int blockSize = (inputCount < 64) ? inputCount : 64;
                for (int i = 0; i < blockSize; i++)
                {
                    output[outputOffset + processed + i] = (byte)(input[inputOffset + processed + i] ^ keyStream[i]);
                }

                inputCount -= blockSize;
                processed += blockSize;
            }
        }
    }
}