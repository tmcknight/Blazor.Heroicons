
//get latest heroicons release

using Blazor.Heroicons.Updater;

Console.WriteLine("Getting latest Heroicons release information...");
var latestRelease = await ReleaseInformation.GetLatest();
if (latestRelease is null)
{
    Console.WriteLine("Failed to get latest release information.");
    return;
}
Console.WriteLine($"Latest release: {latestRelease.Version}");
Console.WriteLine($"Tarball URL: {latestRelease.ZipballUrl}");

//download latest heroicons release
var downloadPath = Path.Combine(Path.GetTempPath(), "Blazor.Heroicons.Updater");
Console.WriteLine($"Downloading latest Heroicons release to {downloadPath}...");
var zipPath = await ReleaseDownloader.Download(latestRelease, downloadPath);
Console.WriteLine($"Downloaded release to {zipPath}");