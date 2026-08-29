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

    internal static void AppendRefKind(StringBuilder builder, RefKind refKind) =>
        builder.Append(refKind switch
        {
            RefKind.Ref => "ref ",
            RefKind.Out => "out ",
            RefKind.In => "in ",
            _ => string.Empty,
        });
}
