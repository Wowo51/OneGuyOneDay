namespace Sha3Variants
{
    public abstract class KeccakSponge
    {
        protected int RateBytes;
        protected byte Padding;
        protected KeccakSponge(int rateBytes, byte padding)
        {
            this.RateBytes = rateBytes;
            this.Padding = padding;
        }

        protected static void AbsorbBlock(ulong[] state, byte[] block, int blockLen)
        {
            for (int i = 0; i < blockLen; i++)
            {
                int lane = i / 8;
                int shift = (i % 8) * 8;
                state[lane] ^= ((ulong)block[i]) << shift;
            }
        }

        protected static void StateToBytes(ulong[] state, byte[] output, int outputOffset, int length)
        {
            for (int i = 0; i < length; i++)
            {
                int lane = i / 8;
                int shift = (i % 8) * 8;
                output[outputOffset + i] = (byte)((state[lane] >> shift) & 0xFF);
            }
        }

        protected byte[] Squeeze(ulong[] state, int outputLength)
        {
            byte[] output = new byte[outputLength];
            int outputOffset = 0;
            while (outputOffset < outputLength)
            {
                int blockSize = RateBytes;
                int chunk = (outputLength - outputOffset < blockSize) ? (outputLength - outputOffset) : blockSize;
                StateToBytes(state, output, outputOffset, chunk);
                outputOffset += chunk;
                if (outputOffset < outputLength)
                {
                    Keccak.Permute(state);
                }
            }

            return output;
        }

        protected byte[] ComputeHashInternal(byte[] message, int outputLength)
        {
            if (message == null)
            {
                message = new byte[0];
            }

            ulong[] state = new ulong[25];
            int blockSize = RateBytes;
            int offset = 0;
            while (offset < message.Length)
            {
                int chunk = ((message.Length - offset) < blockSize) ? (message.Length - offset) : blockSize;
                byte[] block = new byte[chunk];
                System.Array.Copy(message, offset, block, 0, chunk);
                AbsorbBlock(state, block, chunk);
                offset += chunk;
                if (chunk == blockSize)
                {
                    Keccak.Permute(state);
                }
            }

            int padIndex = message.Length % blockSize;
            int lane = padIndex / 8;
            int shift = (padIndex % 8) * 8;
            state[lane] ^= ((ulong)Padding) << shift;
            int lastByteIndex = blockSize - 1;
            int lane2 = lastByteIndex / 8;
            int shift2 = (lastByteIndex % 8) * 8;
            state[lane2] ^= (ulong)0x80 << shift2;
            Keccak.Permute(state);
            return Squeeze(state, outputLength);
        }
    }
}