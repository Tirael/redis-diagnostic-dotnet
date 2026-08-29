namespace RedisDiagnostic.SourceGenerator;

internal static class CSharpSymbolFormatter
{
    internal static string FormatType(ITypeSymbol type) =>
        type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat
            .WithMiscellaneousOptions(
                SymbolDisplayMiscellaneousOptions.UseSpecialTypes
                | SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier
                | SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers));

    internal static string EscapeIdentifier(string name)
    {
        if (SyntaxFacts.GetKeywordKind(name) is SyntaxKind.None
            && SyntaxFacts.GetContextualKeywordKind(name) is SyntaxKind.None)
        {
            return name;
        }

        return "@" + name;
    }

    internal static void AppendRefKind(StringBuilder builder, RefKind refKind)
    {
        if (refKind is RefKind.Ref)
        {
            builder.Append("ref ");
            return;
        }

        if (refKind is RefKind.Out)
        {
            builder.Append("out ");
            return;
        }

        if (refKind is RefKind.In)
        {
            builder.Append("in ");
        }
    }
}
