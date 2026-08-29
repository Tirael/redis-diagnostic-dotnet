namespace RedisDiagnostic.SourceGenerator;

internal static class CSharpDefaultValueFormatter
{
    internal static string Format(IParameterSymbol parameter) => parameter.ExplicitDefaultValue switch
    {
        null => FormatNullDefault(parameter.Type),
        var value when GetEnumType(parameter.Type) is INamedTypeSymbol enumType => FormatEnumDefault(enumType, value),
        var value => FormatPrimitive(value),
    };

    private static string FormatNullDefault(ITypeSymbol type) =>
        IsNonNullableValueType(type) ? "default" : "null";

    private static string FormatPrimitive(object value) => value switch
    {
        bool or string or char => FormatText(value),
        TimeSpan duration => FormatTimeSpan(duration),
        _ => FormatNumber(value),
    };

    private static string FormatText(object value) => value switch
    {
        bool flag => flag ? "true" : "false",
        string text => SymbolDisplay.FormatLiteral(text, quote: true),
        char character => SymbolDisplay.FormatLiteral(character, quote: true),
        _ => Convert.ToString(value, CultureInfo.InvariantCulture) ?? "default",
    };

    private static string FormatTimeSpan(TimeSpan duration) =>
        duration.Ticks is 0
            ? "global::System.TimeSpan.Zero"
            : "new global::System.TimeSpan(" + duration.Ticks.ToString(CultureInfo.InvariantCulture) + "L)";

    private static string FormatNumber(object value) => value switch
    {
        float number => FormatFloat(number),
        double number => FormatDouble(number),
        decimal number => number.ToString(CultureInfo.InvariantCulture) + "M",
        _ => FormatInteger(value),
    };

    private static string FormatInteger(object value) => Type.GetTypeCode(value.GetType()) switch
    {
        TypeCode.UInt32 => ((uint)value).ToString(CultureInfo.InvariantCulture) + "U",
        TypeCode.Int64 => ((long)value).ToString(CultureInfo.InvariantCulture) + "L",
        TypeCode.UInt64 => ((ulong)value).ToString(CultureInfo.InvariantCulture) + "UL",
        _ => Convert.ToString(value, CultureInfo.InvariantCulture) ?? "0",
    };

    private static string FormatFloat(float number) => number switch
    {
        float.NegativeInfinity => "float.NegativeInfinity",
        float.PositiveInfinity => "float.PositiveInfinity",
        _ when float.IsNaN(number) => "float.NaN",
        _ => number.ToString("R", CultureInfo.InvariantCulture) + "F",
    };

    private static string FormatDouble(double number) => number switch
    {
        double.NegativeInfinity => "double.NegativeInfinity",
        double.PositiveInfinity => "double.PositiveInfinity",
        _ when double.IsNaN(number) => "double.NaN",
        _ => number.ToString("R", CultureInfo.InvariantCulture) + "D",
    };

    private static string FormatEnumDefault(INamedTypeSymbol enumType, object value)
    {
        var field = enumType.GetMembers()
            .OfType<IFieldSymbol>()
            .FirstOrDefault(member => member.HasConstantValue && Equals(member.ConstantValue, value));
        return field is not null
            ? CSharpSymbolFormatter.FormatType(enumType) + "." + field.Name
            : "(" + CSharpSymbolFormatter.FormatType(enumType) + ")" + Convert.ToString(value, CultureInfo.InvariantCulture);
    }

    private static INamedTypeSymbol? GetEnumType(ITypeSymbol type) => type switch
    {
        INamedTypeSymbol { TypeKind: TypeKind.Enum } enumType => enumType,
        INamedTypeSymbol
        {
            OriginalDefinition.SpecialType: SpecialType.System_Nullable_T,
            TypeArguments.Length: 1
        } named
            when named.TypeArguments[0] is INamedTypeSymbol { TypeKind: TypeKind.Enum } underlying => underlying,
        _ => null,
    };

    private static bool IsNonNullableValueType(ITypeSymbol type) =>
        type.IsValueType && type.OriginalDefinition.SpecialType is not SpecialType.System_Nullable_T;
}
