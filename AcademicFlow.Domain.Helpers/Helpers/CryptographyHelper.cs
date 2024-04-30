using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace AcademicFlow.Domain.Helpers.Helpers
{
    public static class CryptographyHelper
    {
        private static readonly char[] chars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890".ToCharArray();
        public const int DefaultSaltLength = 8;
        public static (string Hash, string Salt) GetHash(string data)
        {
            var salt = GetSalt(DefaultSaltLength);
            var hash = HashData(data, salt);

            return (hash, salt);
        }

        public static bool IsHashSame(string originalHash, string data, string salt)
        {
            var newHash = HashData(data, salt);
            return originalHash == newHash;
        }

        private static string GetSalt(int size)
        {
            byte[] data = new byte[4 * size];
            using (var crypto = RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new(size);
            for (int i = 0; i < size; i++)
            {
                var randomValue = BitConverter.ToUInt32(data, i * 4);
                var index = randomValue % chars.Length;

                result.Append(chars[index]);
            }

            return result.ToString();
        }

        private static string HashData(string data, string salt)
        {
            data = salt + data;
            var dataBytes = Encoding.UTF8.GetBytes(data);
            using var crypto = SHA256.Create();
            var hashBytes = crypto.ComputeHash(dataBytes);

            return BitConverter.ToString(hashBytes).Replace("-", string.Empty);
        }
    }
}
