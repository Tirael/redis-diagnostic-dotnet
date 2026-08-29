namespace RedisDiagnostic.SourceGenerator;

internal static class DatabaseInterfaceMembers
{
    internal static IEnumerable<ISymbol> Enumerate(INamedTypeSymbol databaseType)
    {
        foreach (var member in EnumerateInstanceMembers(databaseType))
            yield return member;

        foreach (var iface in databaseType.AllInterfaces)
        {
            foreach (var member in EnumerateInstanceMembers(iface))
                yield return member;
        }
    }

    internal static string GetSignatureKey(IMethodSymbol method)
    {
        StringBuilder builder = new();
        builder.Append("M:").Append(method.Name);
        if (method.TypeParameters.Length is not 0)
            builder.Append('`').Append(method.TypeParameters.Length);

        AppendParameterTypes(builder, method);
        return builder.ToString();
    }

    private static IEnumerable<ISymbol> EnumerateInstanceMembers(INamedTypeSymbol type)
    {
        foreach (var member in type.GetMembers())
        {
            if (member.IsStatic)
                continue;

            yield return member;
        }
    }

    private static void AppendParameterTypes(StringBuilder builder, IMethodSymbol method)
    {
        builder.Append('(');
        for (var i = 0; i < method.Parameters.Length; i++)
        {
            if (i is > 0)
                builder.Append(',');

            var parameter = method.Parameters[i];
            builder.Append(parameter.RefKind).Append(':');
            builder.Append(parameter.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));
        }

        builder.Append(')');
    }
}
