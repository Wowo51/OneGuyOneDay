using System;
using System.Text;

namespace DataEncryptionStandard
{
    public class Program
    {
        public static int Main(string[] args)
        {
            try
            {
                string message = "Test Message";
                byte[] data = Encoding.UTF8.GetBytes(message);
                byte[] key = new byte[8]
                {
                    1,
                    2,
                    3,
                    4,
                    5,
                    6,
                    7,
                    8
                };
                byte[] iv = new byte[8]
                {
                    8,
                    7,
                    6,
                    5,
                    4,
                    3,
                    2,
                    1
                };
                byte[] encryptedData = DesEncryption.Encrypt(data, key, iv);
                byte[] decryptedData = DesEncryption.Decrypt(encryptedData, key, iv);
                string decryptedMessage = Encoding.UTF8.GetString(decryptedData);
                Console.WriteLine("Decrypted Message: " + decryptedMessage);
                return 0;
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine("Unhandled error: " + ex.ToString());
                return 1;
            }
        }
    }
}