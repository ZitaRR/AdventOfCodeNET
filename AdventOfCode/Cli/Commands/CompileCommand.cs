using AdventOfCodeNet.Cli.Settings;
using AdventOfCodeNet.Core.CodeDOM;
using Spectre.Console;
using Spectre.Console.Cli;

namespace AdventOfCodeNet.Cli.Commands;

public class CompileCommand : AsyncCommand<CompilerSettings>
{
    private readonly ICompiler compiler;

    public CompileCommand(ICompiler compiler)
    {
        this.compiler = compiler;
    }

    public override Task<int> ExecuteAsync(CommandContext context, CompilerSettings settings)
    {
        try
        {
            CompileResult result = compiler.Compile(settings.Path);
            string output = result.Invoke("INPUT");
            AnsiConsole.MarkupInterpolated($"[green]Successfully compiled the provided file.[/] Output: {output}");
            return Task.FromResult(0);
        }
        catch (Exception ex)
        {
            AnsiConsole.MarkupInterpolated($"[red]{ex.Message}[/]");
            return Task.FromResult(-1);
        }
    }
}
