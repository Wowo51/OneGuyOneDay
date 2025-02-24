using System;

namespace Salsa20AndChaCha20
{
    public class Salsa20
    {
        private byte[] key;
        private byte[] nonce;
        private uint counter;
        public Salsa20(byte[] key, byte[] nonce, uint counter = 0)
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
                nonce = new byte[8];
            }
            else if (nonce.Length != 8)
            {
                byte[] newNonce = new byte[8];
                int copyLength = nonce.Length < 8 ? nonce.Length : 8;
                Array.Copy(nonce, newNonce, copyLength);
                nonce = newNonce;
            }

            this.key = new byte[32];
            this.nonce = new byte[8];
            Array.Copy(key, this.key, 32);
            Array.Copy(nonce, this.nonce, 8);
            this.counter = counter;
        }

        private static uint RotateLeft(uint x, int n)
        {
            return (x << n) | (x >> (32 - n));
        }

        private void Salsa20Core(uint[] output, uint[] input)
        {
            uint x0 = input[0];
            uint x1 = input[1];
            uint x2 = input[2];
            uint x3 = input[3];
            uint x4 = input[4];
            uint x5 = input[5];
            uint x6 = input[6];
            uint x7 = input[7];
            uint x8 = input[8];
            uint x9 = input[9];
            uint x10 = input[10];
            uint x11 = input[11];
            uint x12 = input[12];
            uint x13 = input[13];
            uint x14 = input[14];
            uint x15 = input[15];
            for (int i = 0; i < 10; i++)
            {
                // Column rounds
                x4 ^= RotateLeft(x0 + x12, 7);
                x8 ^= RotateLeft(x4 + x0, 9);
                x12 ^= RotateLeft(x8 + x4, 13);
                x0 ^= RotateLeft(x12 + x8, 18);
                x9 ^= RotateLeft(x5 + x1, 7);
                x13 ^= RotateLeft(x9 + x5, 9);
                x1 ^= RotateLeft(x13 + x9, 13);
                x5 ^= RotateLeft(x1 + x13, 18);
                x14 ^= RotateLeft(x10 + x6, 7);
                x2 ^= RotateLeft(x14 + x10, 9);
                x6 ^= RotateLeft(x2 + x14, 13);
                x10 ^= RotateLeft(x6 + x2, 18);
                x3 ^= RotateLeft(x15 + x11, 7);
                x7 ^= RotateLeft(x3 + x15, 9);
                x11 ^= RotateLeft(x7 + x3, 13);
                x15 ^= RotateLeft(x11 + x7, 18);
                // Row rounds
                x1 ^= RotateLeft(x0 + x3, 7);
                x2 ^= RotateLeft(x1 + x0, 9);
                x3 ^= RotateLeft(x2 + x1, 13);
                x0 ^= RotateLeft(x3 + x2, 18);
                x6 ^= RotateLeft(x5 + x4, 7);
                x7 ^= RotateLeft(x6 + x5, 9);
                x4 ^= RotateLeft(x7 + x6, 13);
                x5 ^= RotateLeft(x4 + x7, 18);
                x11 ^= RotateLeft(x10 + x9, 7);
                x8 ^= RotateLeft(x11 + x10, 9);
                x9 ^= RotateLeft(x8 + x11, 13);
                x10 ^= RotateLeft(x9 + x8, 18);
                x12 ^= RotateLeft(x15 + x14, 7);
                x13 ^= RotateLeft(x12 + x15, 9);
                x14 ^= RotateLeft(x13 + x12, 13);
                x15 ^= RotateLeft(x14 + x13, 18);
            }

            output[0] = x0 + input[0];
            output[1] = x1 + input[1];
            output[2] = x2 + input[2];
            output[3] = x3 + input[3];
            output[4] = x4 + input[4];
            output[5] = x5 + input[5];
            output[6] = x6 + input[6];
            output[7] = x7 + input[7];
            output[8] = x8 + input[8];
            output[9] = x9 + input[9];
            output[10] = x10 + input[10];
            output[11] = x11 + input[11];
            output[12] = x12 + input[12];
            output[13] = x13 + input[13];
            output[14] = x14 + input[14];
            output[15] = x15 + input[15];
        }

        private void GenerateKeyStream(byte[] block, uint blockCounter)
        {
            uint[] state = new uint[16];
            state[0] = 0x61707865;
            state[1] = BitConverter.ToUInt32(this.key, 0);
            state[2] = BitConverter.ToUInt32(this.key, 4);
            state[3] = BitConverter.ToUInt32(this.key, 8);
            state[4] = BitConverter.ToUInt32(this.key, 12);
            state[5] = 0x3320646e;
            state[6] = blockCounter;
            state[7] = BitConverter.ToUInt32(this.nonce, 0);
            state[8] = BitConverter.ToUInt32(this.nonce, 4);
            state[9] = 0;
            state[10] = 0x79622d32;
            state[11] = BitConverter.ToUInt32(this.key, 16);
            state[12] = BitConverter.ToUInt32(this.key, 20);
            state[13] = BitConverter.ToUInt32(this.key, 24);
            state[14] = BitConverter.ToUInt32(this.key, 28);
            state[15] = 0x6b206574;
            uint[] outputState = new uint[16];
            Salsa20Core(outputState, state);
            for (int j = 0; j < 16; j++)
            {
                byte[] wordBytes = BitConverter.GetBytes(outputState[j]);
                Buffer.BlockCopy(wordBytes, 0, block, j * 4, 4);
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