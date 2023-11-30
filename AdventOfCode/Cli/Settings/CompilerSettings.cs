using Spectre.Console.Cli;

namespace AdventOfCodeNet.Cli.Settings;

public class CompilerSettings : CommandSettings
{
    [CommandArgument(0, "<path>")]
    public string Path { get; set; } = string.Empty;
}
