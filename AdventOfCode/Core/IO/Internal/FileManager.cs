namespace AdventOfCodeNet.Core.IO.Internal;

internal class FileManager : IFileManager
{
    private const string IO_EXT = ".txt";
    private const string OUT_DIR = "Output";
    private const string IN_DIR = "Input";
    private const string CACHE_NAME = "cache";

    private readonly string appName = "AdventOfCode";
    private readonly string appDataPath;

    public FileManager()
    {
        appDataPath = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);   
    }

    public async Task WriteInputAsync(int year, int day, string content)
    {
        string filePath = ConstructFilePath(IN_DIR, year, day);
        await File.WriteAllTextAsync(filePath, content);
    }

    public async Task WriteOutputAsync(int year, int day, string content)
    {
        string filePath = ConstructFilePath(OUT_DIR, year, day);
        await File.WriteAllTextAsync(filePath, content);
    }

    public async Task<string> ReadInputAsync(int year, int day)
    {
        string filePath = ConstructFilePath(IN_DIR, year, day);
        return await File.ReadAllTextAsync(filePath);
    }

    public async Task<string> ReadOutputAsync(int year, int day)
    {
        string filePath = ConstructFilePath(OUT_DIR, year, day);
        return await File.ReadAllTextAsync(filePath);
    }

    public Task<bool> InputExistsAsync(int year, int day) =>
        Task.FromResult(File.Exists(ConstructFilePath(IN_DIR, year, day)));

    public Task<bool> OutputExistsAsync(int year, int day) =>
        Task.FromResult(File.Exists(ConstructFilePath(OUT_DIR, year, day)));

    public async Task SetSessionCookieAsync(string sessionCookie)
    {
        string directory = $@"{appDataPath}\{appName}";
        string filePath = $@"{directory}\{CACHE_NAME}{IO_EXT}";
        Directory.CreateDirectory(directory);
        await File.WriteAllTextAsync(filePath, sessionCookie);
    }

    public async Task<string> GetSessionCookieAsync()
    {
        string filePath = $@"{appDataPath}\{appName}\{CACHE_NAME}{IO_EXT}";
        return await File.ReadAllTextAsync(filePath);
    }

    private string ConstructFilePath(string io, int year, int day)
    {
        string directoryPath = $@"{appDataPath}\{appName}\{io}\{year}";
        Directory.CreateDirectory(directoryPath);
        return $@"{directoryPath}\{day:00}{IO_EXT}";
    }
}