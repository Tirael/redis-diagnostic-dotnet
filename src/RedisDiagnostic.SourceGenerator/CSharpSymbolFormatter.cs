namespace RedisDiagnostic.SourceGenerator;

internal static class CSharpSymbolFormatter
{
    internal static string FormatType(ITypeSymbol type) =>
        type.ToDisplayString(SymbolDisplayFormat.FullyQualifiedFormat
            .WithMiscellaneousOptions(
                SymbolDisplayMiscellaneousOptions.UseSpecialTypes
                | SymbolDisplayMiscellaneousOptions.IncludeNullableReferenceTypeModifier
                | SymbolDisplayMiscellaneousOptions.EscapeKeywordIdentifiers));

    internal static string EscapeIdentifier(string name) =>
        SyntaxFacts.GetKeywordKind(name) is not SyntaxKind.None
        || SyntaxFacts.GetContextualKeywordKind(name) is not SyntaxKind.None
            ? "@" + name
            : name;

    internal static void AppendRefKind(StringBuilder builder, RefKind refKind)
    {
        switch (refKind)
        {
            case RefKind.Ref:
                builder.Append("ref ");
                return;
            case RefKind.Out:
                builder.Append("out ");
                return;
            case RefKind.In:
                builder.Append("in ");
                return;
        }
    }
}
