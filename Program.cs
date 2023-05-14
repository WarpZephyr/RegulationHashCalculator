using System.Security.Cryptography;

namespace RegulationHashCalculator
{
    internal class Program
    {
        static void Main(string[] args)
        {
            if (args.Length == 0)
            {
                Console.WriteLine("Regulation hash calculator is used to calculate the MD5 hash and file size at the end of Armored Core 4 regulations.");
                Console.WriteLine("Drag and drop your regulation.bin file after changes have been made into the exe of this program.");
                Console.WriteLine("Press any key to close this program, or click the exit button.");
                Console.ReadKey();
            }

            if (args == null)
                return;

            foreach (string arg in args)
            {
                if (arg == null)
                    continue;
                if (!File.Exists(arg))
                    continue;

                // Read the file, write its hash and length, then write it back.
                File.WriteAllBytes(arg, ReadFile(arg));
            }
        }

        static byte[] ReadFile(string path)
        {
            // Read and convert the file to a list of bytes.
            var list = new List<byte>();
            list.AddRange(File.ReadAllBytes(path));

            // Correct the raw format if it is not already, it should be read in binary, but this is simplier.
            list[12] = 228;

            // Add the hash.
            list.AddRange(GetHash(list));

            // Add the file length after converting it into a byte array and reversing it for big endian.
            var length = BitConverter.GetBytes(list.Count + 4);
            Array.Reverse(length);
            list.AddRange(length);

            // Return the entire file as a byte array.
            return list.ToArray();
        }

        static byte[] GetHash(List<byte> list)
        {
            // Remove the last 20 bytes to calculate the hash.
            list.RemoveRange(list.Count - 20, 20);
            var bytes = list.ToArray();

            // Calculate the hash.
            MD5 md5 = MD5.Create();
            return md5.ComputeHash(bytes);
        }
    }
}