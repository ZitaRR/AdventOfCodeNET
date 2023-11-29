using AdventOfCodeNet.Cli.Settings;
using AdventOfCodeNet.Core.IO;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AdventOfCodeNet.Cli.Commands;

public class SetSessionCookieCommand : AsyncCommand<SessionCookieArgumentSettings>
{
    private readonly IFileManager fileManager;

    public SetSessionCookieCommand(IFileManager fileManager)
    {
        this.fileManager = fileManager;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, SessionCookieArgumentSettings settings)
    {
        try
        {
            await fileManager.SetSessionCookieAsync(settings.SessionCookie);
        }
        catch
        {
            AnsiConsole.MarkupLine($"[red]Failed to set session cookie.[/]");
            return -1;
        }

        AnsiConsole.MarkupLine($"[green]Successfully set session cookie![/]");
        return 0;
    }
}
