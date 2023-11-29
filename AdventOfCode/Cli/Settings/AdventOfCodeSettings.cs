using Spectre.Console.Cli;
using System.ComponentModel;

namespace AdventOfCodeNet.Cli.Settings;

public class AdventOfCodeSettings : SessionCookieOptionSettings
{
    [CommandArgument(0, "<year>")]
    public int Year { get; set; }

    [CommandArgument(1, "<day>")]
    public int Day { get; set; }
}
