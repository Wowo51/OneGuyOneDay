namespace Sha3Variants
{
    public abstract class Sha3Hash : KeccakSponge
    {
        protected int OutputLength;
        protected Sha3Hash(int outputLength, int rateBytes, byte padding) : base(rateBytes, padding)
        {
            this.OutputLength = outputLength;
        }

        public byte[] ComputeHash(byte[] message)
        {
            return ComputeHashInternal(message, this.OutputLength);
        }
    }
}