namespace AdventOfCodeNet.Core.Clients;

public interface IAdventOfCodeClient
{
    Task<string> DownloadInputAsync(int year, int day);
    Task UploadAnswerAsync(int year, int day);
}
