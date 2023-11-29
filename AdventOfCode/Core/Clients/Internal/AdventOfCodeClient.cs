namespace AdventOfCodeNet.Core.Clients.Internal;

internal sealed class AdventOfCodeClient : IAdventOfCodeClient
{
    private readonly HttpClient client;

    public AdventOfCodeClient(HttpClient client)
    {
        this.client = client;
    }

    public async Task<string> DownloadInputAsync(int year, int day)
    {
        HttpResponseMessage response = await client.GetAsync($"{year}/day/{day}/input");
        response.EnsureSuccessStatusCode();
        return await response.Content.ReadAsStringAsync();
    }
}
