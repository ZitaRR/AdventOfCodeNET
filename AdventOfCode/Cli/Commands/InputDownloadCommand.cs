using AdventOfCodeNet.Cli.Interceptors;
using AdventOfCodeNet.Cli.Providers;
using AdventOfCodeNet.Cli.Settings;
using AdventOfCodeNet.Core.Clients;
using AdventOfCodeNet.Core.IO;
using AdventOfCodeNet.PInvoke;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AdventOfCodeNet.Cli.Commands;

public sealed class InputDownloadCommand : SessionCookieCommandBase<AdventOfCodeSettings>
{
    private readonly IAdventOfCodeClient client;
    private readonly IFileManager fileManager;

    public InputDownloadCommand(
        IAdventOfCodeClient client,
        IFileManager fileManager)
    {
        this.client = client;
        this.fileManager = fileManager;
    }

    public override async Task<int> ExecuteAsync(CommandContext context, AdventOfCodeSettings settings)
    {
        if (await fileManager.InputExistsAsync(settings.Year, settings.Day))
        {
            AnsiConsole.MarkupLineInterpolated($"[orange1]Input file for {settings.Year}-{settings.Day:00} already exists.[/]");
            string content = await fileManager.ReadInputAsync(settings.Year, settings.Day);
            Clipboard.SetText(content);
            AnsiConsole.MarkupLine("[green]Contents copied to your clipboard.[/]");
            return -1;
        }

        if (!Validate(settings))
        {
            AnsiConsole.MarkupLine("[red]No session cookie provided![/]");
            return -1;
        }

        try
        {
            string input = await AnsiConsole
                .Status()
                .StartAsync(
                    $"[green]Fetching input data for {settings.Year}-{settings.Day:00}[/]",
                    (_) => client.DownloadInputAsync(settings.Year, settings.Day));

            await fileManager.WriteInputAsync(settings.Year, settings.Day, input);
        }
        catch (HttpRequestException)
        {
            AnsiConsole.MarkupLineInterpolated($"[red]Failed to download input for {settings.Year}-{settings.Day:00}.[/]");
            return -1;
        }

        AnsiConsole.MarkupLine($"Downloaded input for {settings.Year}-{settings.Day:00}.");
        return 0;
    }
}
