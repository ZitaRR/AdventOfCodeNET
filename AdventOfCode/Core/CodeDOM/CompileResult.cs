using System.Collections.Immutable;
using System.Reflection;
using System.Runtime.Loader;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.Emit;
using TypeInfo = System.Reflection.TypeInfo;

namespace AdventOfCodeNet.Core.CodeDOM;

public class CompileResult
{
    public Assembly Assembly { get; private set; } = null!;
    public bool Success => result.Success;
    public IImmutableList<Diagnostic> Diagnostics => result.Diagnostics;

    private EmitResult result;
    private TypeInfo implementation = null!;
    private MethodInfo method = null!;

    public CompileResult(EmitResult result, MemoryStream stream)
    {
        this.result = result;
        InitializeAssembly(stream);
    }

    private void InitializeAssembly(MemoryStream stream)
    {
        stream.Seek(0, SeekOrigin.Begin);
        Assembly = AssemblyLoadContext.Default.LoadFromStream(stream);

        IImmutableList<TypeInfo> interfaceImplementations = Assembly.DefinedTypes
            .Where(t => 
                t.ImplementedInterfaces.FirstOrDefault(i => i.Name.Equals("IAdventOfCode")) != null)
            .ToImmutableList();

        if (!interfaceImplementations.Any())
        {
            throw new ArgumentException("No classes implements IAdventOfCode.");
        }

        if (interfaceImplementations.Count > 1)
        {
            throw new ArgumentOutOfRangeException(nameof(stream), "Multiple implementations of IAdventOfCode. There must only be one.");
        }

        implementation = interfaceImplementations.First();
        method = implementation.GetMethod("Solve")!;

        if (method == null)
        {
            throw new ArgumentException("Found implementation but couldn't find method. Explicit interface implementations are not supported.");
        }
    }

    public string Invoke(string input)
    {
        object instance = Assembly.CreateInstance(implementation.FullName!)!;
        return (string)method.Invoke(instance, new object[] { input })!;
    }
}
