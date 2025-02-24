using System;
using System.Security.Cryptography;

namespace NTRUEncrypt
{
    public class NTRUEncryptor
    {
        public NTRUKeyPair GenerateKeyPair(NTRUParameters parameters)
        {
            if (parameters == null)
            {
                parameters = new NTRUParameters();
            }

            int[] coeff = new int[parameters.N];
            using (RandomNumberGenerator rng = RandomNumberGenerator.Create())
            {
                byte[] buffer = new byte[1];
                for (int i = 0; i < parameters.N; i++)
                {
                    rng.GetBytes(buffer);
                    int value = buffer[0] % 3;
                    if (value == 0)
                    {
                        coeff[i] = -1;
                    }
                    else if (value == 1)
                    {
                        coeff[i] = 0;
                    }
                    else
                    {
                        coeff[i] = 1;
                    }
                }
            }

            Polynomial keyPoly = new Polynomial(coeff);
            return new NTRUKeyPair(keyPoly, keyPoly);
        }

        public byte[] Encrypt(byte[] message, Polynomial publicKey, NTRUParameters parameters)
        {
            if (message == null || publicKey == null || parameters == null)
            {
                return new byte[0];
            }

            Polynomial messagePoly = Polynomial.FromBytes(message, parameters.N, parameters.q);
            Polynomial cipherPoly = messagePoly.Add(publicKey, parameters.q);
            return cipherPoly.ToBytes(parameters.N, parameters.q);
        }

        public byte[] Decrypt(byte[] ciphertext, Polynomial privateKey, NTRUParameters parameters)
        {
            if (ciphertext == null || privateKey == null || parameters == null)
            {
                return new byte[0];
            }

            Polynomial cipherPoly = Polynomial.FromBytes(ciphertext, parameters.N, parameters.q);
            Polynomial messagePoly = cipherPoly.Subtract(privateKey, parameters.q);
            return messagePoly.ToBytes(parameters.N, parameters.q);
        }
    }
}