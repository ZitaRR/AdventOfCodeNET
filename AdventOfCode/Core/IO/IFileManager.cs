namespace AdventOfCodeNet.Core.IO;

public interface IFileManager
{
    Task WriteInputAsync(int year, int day, string content);
    Task WriteOutputAsync(int year, int day, string content);

    Task<string> ReadInputAsync(int year, int day);
    Task<string> ReadOutputAsync(int year, int day);

    Task<bool> InputExistsAsync(int year, int day);
    Task<bool> OutputExistsAsync(int year, int day);

    Task SetSessionCookieAsync(string sessionCookie);
    Task<string> GetSessionCookieAsync();
}
