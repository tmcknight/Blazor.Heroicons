using System.Text.Json;
using System.Text.Json.Serialization;

namespace Blazor.Heroicons.Updater;

public class ReleaseInformation()
{
    public const string RepositoryOwner = "tailwindlabs";
    public const string RepositoryName = "heroicons";
    public static string LatestReleaseUrl => $"https://api.github.com/repos/{RepositoryOwner}/{RepositoryName}/releases/latest";
    
    [JsonPropertyName("tag_name")]
    public required string Version { get; set; }
    [JsonPropertyName("zipball_url")]
    public required string ZipballUrl { get; set; }
    
    public static async Task<ReleaseInformation?> GetLatest()
    {
        using var client = new HttpClient();
        client.DefaultRequestHeaders.Add("User-Agent", typeof(ReleaseInformation).Namespace);
        var response = await client.GetAsync(LatestReleaseUrl);
        if (!response.IsSuccessStatusCode)
        {
            return null;
        }
        
        var json = await response.Content.ReadAsStringAsync();
        var release = JsonSerializer.Deserialize<ReleaseInformation>(json);
        
        return release;
    }
}