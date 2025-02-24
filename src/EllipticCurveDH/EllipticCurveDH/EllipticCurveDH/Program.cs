using System;

namespace EllipticCurveDH
{
    public class Program
    {
        public static void Main(string[] args)
        {
            using (ECDHKeyExchange alice = new ECDHKeyExchange())
            using (ECDHKeyExchange bob = new ECDHKeyExchange())
            {
                byte[] alicePublicKey = alice.PublicKey;
                byte[] bobPublicKey = bob.PublicKey;
                byte[] aliceSharedSecret = alice.DeriveSharedSecret(bobPublicKey);
                byte[] bobSharedSecret = bob.DeriveSharedSecret(alicePublicKey);
                if (ByteArrayEqual(aliceSharedSecret, bobSharedSecret))
                {
                    Console.WriteLine("Shared secret established successfully.");
                }
                else
                {
                    Console.WriteLine("Shared secret mismatch.");
                }
            }
        }

        public static bool ByteArrayEqual(byte[] array1, byte[] array2)
        {
            if (array1 == null || array2 == null || array1.Length != array2.Length)
            {
                return false;
            }

            for (int index = 0; index < array1.Length; index++)
            {
                if (array1[index] != array2[index])
                {
                    return false;
                }
            }

            return true;
        }
    }
}