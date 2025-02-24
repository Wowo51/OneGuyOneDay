using System;

namespace Md5Collisions
{
    public class Program
    {
        public static void Main(string[] args)
        {
            Tuple<byte[], byte[]> collisionPair = Md5Utilities.GenerateCollisionPair();
            string hash1 = Md5Utilities.ComputeMd5Hash(collisionPair.Item1);
            string hash2 = Md5Utilities.ComputeMd5Hash(collisionPair.Item2);
            Console.WriteLine("Collision 1 MD5: " + hash1);
            Console.WriteLine("Collision 2 MD5: " + hash2);
            if (hash1 == hash2)
            {
                Console.WriteLine("MD5 collision generated successfully.");
            }
            else
            {
                Console.WriteLine("MD5 collision generation failed.");
            }
        }
    }
}