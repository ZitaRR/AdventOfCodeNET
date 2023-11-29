using Spectre.Console;
using Spectre.Console.Cli;

namespace AdventOfCodeNet.Cli.Commands;

public sealed class Command : AsyncCommand
{
    public override async Task<int> ExecuteAsync(CommandContext context)
    {
        await AnsiConsole
            .Status()
            .StartAsync("Waiting... Processing...\n", async (_) =>
            {
                await Task.Delay(4000);
                AnsiConsole.MarkupLine("[green]Processed successfully![/]");
            });

        return 0;
    }
}
