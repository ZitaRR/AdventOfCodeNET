using Spectre.Console.Cli;
using System.ComponentModel;

namespace AdventOfCodeNet.Cli.Settings;

public class SessionCookieOptionSettings : CommandSettings
{
    [Description("Session cookie")]
    [CommandOption("-s|--session")]
    public string SessionCookie { get; set; } = string.Empty;
}
