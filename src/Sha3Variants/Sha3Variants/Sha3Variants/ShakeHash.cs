namespace Sha3Variants
{
    public class ShakeHash : KeccakSponge
    {
        public ShakeHash(int rateBytes, byte padding) : base(rateBytes, padding)
        {
        }

        public byte[] ComputeHash(byte[] message, int outputLength)
        {
            return ComputeHashInternal(message, outputLength);
        }
    }
}