using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;
using Microsoft.CodeAnalysis.CSharp;
using Spectre.Console;
using System.Reflection;

namespace AdventOfCodeNet.Core.CodeDOM.Internal;

internal class Compiler : ICompiler
{
    public CompileResult Compile(string path)
    {
        if (!File.Exists(path))
        {
            throw new ArgumentException("File does not exist.");
        }
        
        if (!path[path.LastIndexOf('.')..].Equals(".cs", StringComparison.Ordinal))
        {
            throw new ArgumentException("The provided path must point to a .cs file");
        }

        try
        {
            // Currently only referencing DLLs for the bare minimum
            // TODO: add every single DLL from System.Runtime.InteropServices.RuntimeEnvironment.GetRuntimeDirectory() - kinda overkill
            // TODO: or add only necessary DLLs - complicated, needs some sort of parsing
            // TODO: also nuget packages ): 
            string[] referencePaths = 
            {
                typeof(object).GetTypeInfo().Assembly.Location,
                typeof(Console).GetTypeInfo().Assembly.Location,
                Path.Combine(Path.GetDirectoryName(typeof(System.Runtime.GCSettings).GetTypeInfo().Assembly.Location)!, "System.Runtime.dll")
            };

            MetadataReference[] references = referencePaths.Select(r => MetadataReference.CreateFromFile(r)).ToArray();
            SyntaxTree syntaxTree = CSharpSyntaxTree.ParseText(File.ReadAllText(path));

            CSharpCompilation compilation = CSharpCompilation.Create(
                Path.GetRandomFileName(),
                new[] { syntaxTree },
                references,
                new CSharpCompilationOptions(OutputKind.DynamicallyLinkedLibrary));

            MemoryStream stream = new MemoryStream();
            EmitResult result = compilation.Emit(stream);
            return new(result, stream);
        }
        catch (Exception)
        {
            AnsiConsole.MarkupLine("[red]Failed to compile.[/]");
            throw;
        }
    }
}
