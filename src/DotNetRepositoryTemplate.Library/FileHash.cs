#if !NETSTANDARD2_0
using System;
using System.Buffers;
#endif
using System.Globalization;
using System.IO;
using System.Security.Cryptography;
#if NETSTANDARD2_0
using System.Text;
#endif
using System.Threading;
using System.Threading.Tasks;

namespace DotNetRepositoryTemplate.Library;

public static class FileHash
{
    public static async ValueTask<string> GetHashCode(string filePath, CancellationToken cancellationToken)
    {
        if (!File.Exists(filePath))
        {
            throw new FileNotFoundException($"ファイルが存在しません。", nameof(filePath));
        }
#if NETSTANDARD2_0
        byte[] buffer;

        using (var fileStream = File.OpenRead(filePath))
        {
            buffer = new byte[fileStream.Length];

            _ = await fileStream.ReadAsync(buffer, 0, buffer.Length, cancellationToken).ConfigureAwait(false);
        }
#else
        var buffer = await File.ReadAllBytesAsync(filePath, cancellationToken).ConfigureAwait(false);
#endif

        return GetHashCode(buffer);
    }

    private static string GetHashCode(byte[] bytes)
    {
#if NETSTANDARD2_0
        using var sha256 = SHA256.Create();
        var hash = sha256.ComputeHash(bytes);
        var stringBuilder = new StringBuilder();

        foreach (var b in hash)
        {
            stringBuilder.Append(b.ToString("x2", CultureInfo.InvariantCulture));
        }

        return stringBuilder.ToString();
#else
        var hash = SHA256.HashData(bytes);
        var hexBuffer = ArrayPool<char>.Shared.Rent(hash.Length * 2);
        var charBuffer = ArrayPool<char>.Shared.Rent(2);

        try
        {
            var hexWritten = 0;

            for (var index = 0; index < hash.Length; index++)
            {
                hash[index].TryFormat(charBuffer, out var charsWritten, "x2", CultureInfo.InvariantCulture);

                for (var charIndex = 0; charIndex < charsWritten; charIndex++)
                {
                    hexBuffer[hexWritten] = charBuffer[charIndex];
                    hexWritten++;
                }
            }

            return hexBuffer[..hexWritten].AsSpan().ToString();
        }
        finally
        {
            ArrayPool<char>.Shared.Return(hexBuffer);
            ArrayPool<char>.Shared.Return(charBuffer);
        }
#endif
    }
}
