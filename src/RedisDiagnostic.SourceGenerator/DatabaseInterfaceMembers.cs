using System.Collections.Generic;
using System.Text;
using Microsoft.CodeAnalysis;

namespace RedisDiagnostic.SourceGenerator;

internal static class DatabaseInterfaceMembers
{
    internal static IEnumerable<ISymbol> Enumerate(INamedTypeSymbol databaseType)
    {
        foreach (var member in databaseType.GetMembers())
        {
            if (!member.IsStatic)
            {
                yield return member;
            }
        }

        foreach (var iface in databaseType.AllInterfaces)
        {
            foreach (var member in iface.GetMembers())
            {
                if (!member.IsStatic)
                {
                    yield return member;
                }
            }
        }
    }

    internal static string GetSignatureKey(IMethodSymbol method)
    {
        var builder = new StringBuilder();
        builder.Append("M:").Append(method.Name);
        if (method.TypeParameters.Length > 0)
        {
            builder.Append('`').Append(method.TypeParameters.Length);
        }

        builder.Append('(');
        for (var i = 0; i < method.Parameters.Length; i++)
        {
            if (i > 0)
            {
                builder.Append(',');
            }

            var parameter = method.Parameters[i];
            builder.Append(parameter.RefKind).Append(':');
            builder.Append(parameter.Type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat));
        }

        builder.Append(')');
        return builder.ToString();
    }
}
