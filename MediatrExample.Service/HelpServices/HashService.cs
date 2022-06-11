using MediatrExample.Core.Interfaces.Service;
using System.Text;
using System.Security.Cryptography;

namespace MediatrExample.Service.HelpServices
{
    public class HashService : IHashService
    {
        public async Task<bool> CheckMD5HashAsync(string hashValue, string value)
        {
            string md5HashStr = await SetMD5HashAsync(value);
            return md5HashStr.SequenceEqual(hashValue);
        }

        public async Task<bool> CheckSHA256HashAsync(string hashValue, string value)
        {
            string sha256HashStr = await SetSHA256HashAsync(value);
            return sha256HashStr.SequenceEqual(hashValue);
        }

        public async Task<string> SetMD5HashAsync(string value)
        {
            Byte[] valueBytes = Encoding.UTF8.GetBytes(value);

            using var md5Service = new MD5CryptoServiceProvider();

            using Stream stream = new MemoryStream(valueBytes);

            Byte[] hashBytes = await md5Service.ComputeHashAsync(stream);

            return BitConverter.ToString(hashBytes).Replace("-", string.Empty);
        }

        public async Task<string> SetSHA256HashAsync(string value)
        {
            Byte[] valueBytes = Encoding.UTF8.GetBytes(value);

            using var sha256Service = new SHA256CryptoServiceProvider();

            using Stream stream = new MemoryStream(valueBytes);

            Byte[] hashBytes = await sha256Service.ComputeHashAsync(stream);

            return BitConverter.ToString(hashBytes).Replace("-", string.Empty);
        }

        public ValueTask DisposeAsync()
        {
            GC.Collect();
            GC.SuppressFinalize(this);
            GC.WaitForPendingFinalizers();
            GC.Collect();
            return ValueTask.CompletedTask;
        }
    }
}
