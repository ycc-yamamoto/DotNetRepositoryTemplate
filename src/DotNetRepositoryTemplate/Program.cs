using System;
using System.Runtime.InteropServices;
using System.Threading;
using DotNetRepositoryTemplate.Library;

if (args.Length != 1)
{
    await Console.Error.WriteLineAsync("引数としてファイルパスをひとつ指定してください。").ConfigureAwait(false);
    return 1;
}

using var cancellationTokenSource = new CancellationTokenSource();

Console.CancelKeyPress += (_, _) =>
{
    if (cancellationTokenSource?.IsCancellationRequested is not false)
    {
        return;
    }

    cancellationTokenSource.Cancel();
};
Console.WriteLine($"Runtime: {RuntimeInformation.FrameworkDescription}");

var filePath = args[0];
var hashCode = await FileHash.GetHashCode(filePath, cancellationTokenSource.Token).ConfigureAwait(false);

Console.WriteLine(hashCode);
return 0;
