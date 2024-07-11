using System.Formats.Tar;
using System.IO.Compression;
using System.Text;

namespace Blazor.Heroicons.Updater;

public class ReleaseDownloader
{
    public static async Task<string> Download(ReleaseInformation release, string targetDirectory)
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", typeof(ReleaseInformation).Namespace);
        using var response = await client.GetAsync(release.ZipballUrl);
        response.EnsureSuccessStatusCode();
        await using var zipStream = await response.Content.ReadAsStreamAsync();
        ZipFile.ExtractToDirectory(zipStream, targetDirectory, Encoding.Default, true);
        
        return Path.Combine(targetDirectory, response.Content.Headers.ContentDisposition?.FileName ?? "unknown.zip");
    }
}