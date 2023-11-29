using AdventOfCodeNet.Cli.Commands;
using AdventOfCodeNet.Cli.Interceptors;
using AdventOfCodeNet.Core.Clients;
using AdventOfCodeNet.Core.Clients.Internal;
using AdventOfCodeNet.Core.IO;
using AdventOfCodeNet.Core.IO.Internal;
using AdventOfCodeNet.Infrastructure;
using Microsoft.Extensions.DependencyInjection;
using Spectre.Console;
using Spectre.Console.Cli;

IServiceCollection services = new ServiceCollection()
    .AddTransient((_) =>
        new HttpClient(new SessionCookieHandler()) { BaseAddress = new Uri("https://adventofcode.com") })
    .AddTransient<IAdventOfCodeClient, AdventOfCodeClient>()
    .AddTransient<IFileManager, FileManager>();

TypeRegistrar registrar = new (services);
ITypeResolver resolver = registrar.Build();

CommandApp app = new(registrar);
app.Configure(config =>
{
    config.SetApplicationName("aoc");

    config
        .SetInterceptor(new SessionCookieInterceptor((IFileManager)resolver.Resolve(typeof(IFileManager))!));

    config
        .AddCommand<AdventOfCodeNet.Cli.Commands.Command>("process")
        .WithAlias("p")
        .WithDescription("Test process. Alias: p");

    config
        .AddBranch("set", setConfig =>
        {
            setConfig
                .AddCommand<SetSessionCookieCommand>("session-cookie")
                .WithAlias("sc")
                .WithDescription("Caches the provided session cookie for future use. Alias: sc");
        });

    config
        .AddCommand<InputDownloadCommand>("download")
        .WithAlias("dl")
        .WithDescription("Downloads the input for a specific day. Alias: dl");

    config
        .SetExceptionHandler(ex =>
        {
            if (ex is not ArgumentException argumentEx)
            {
                AnsiConsole.MarkupLineInterpolated($"[red]{ex}[/]");
                return;
            }

            AnsiConsole.MarkupLineInterpolated($"[red]{argumentEx.Message}[/]");
        });
});

IEnumerable<string> myArgs = new List<string>
{
    "dl", "2022", "1"
};

await app.RunAsync(args);