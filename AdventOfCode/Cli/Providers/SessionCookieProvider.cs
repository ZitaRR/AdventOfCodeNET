namespace AdventOfCodeNet.Cli.Providers;

public static class SessionCookieProvider
{
    private static readonly AsyncLocal<string> LocalSessionCookie = new();

    public static string SessionCookie
    {
        get => LocalSessionCookie.Value!;
        set => LocalSessionCookie.Value = value;
    }
}
