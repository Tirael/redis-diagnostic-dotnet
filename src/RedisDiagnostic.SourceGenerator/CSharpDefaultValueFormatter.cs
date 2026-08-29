namespace RedisDiagnostic.SourceGenerator;

internal static class CSharpDefaultValueFormatter
{
    internal static string Format(IParameterSymbol parameter)
    {
        var value = parameter.ExplicitDefaultValue;
        if (value is null)
        {
            return FormatNullDefault(parameter.Type);
        }

        if (GetEnumType(parameter.Type) is { } enumType)
        {
            return FormatEnumDefault(enumType, value);
        }

        return FormatPrimitive(value);
    }

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
        duration is { Ticks: 0 }
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

    private static string FormatFloat(float number)
    {
        if (number is float.NegativeInfinity)
        {
            return "float.NegativeInfinity";
        }

        if (number is float.PositiveInfinity)
        {
            return "float.PositiveInfinity";
        }

        if (float.IsNaN(number))
        {
            return "float.NaN";
        }

        return number.ToString("R", CultureInfo.InvariantCulture) + "F";
    }

    private static string FormatDouble(double number)
    {
        if (number is double.NegativeInfinity)
        {
            return "double.NegativeInfinity";
        }

        if (number is double.PositiveInfinity)
        {
            return "double.PositiveInfinity";
        }

        if (double.IsNaN(number))
        {
            return "double.NaN";
        }

        return number.ToString("R", CultureInfo.InvariantCulture) + "D";
    }

    private static string FormatEnumDefault(INamedTypeSymbol enumType, object value)
    {
        var field = enumType.GetMembers()
            .OfType<IFieldSymbol>()
            .FirstOrDefault(member => member is { HasConstantValue: true } && Equals(member.ConstantValue, value));
        if (field is null)
        {
            return "(" + CSharpSymbolFormatter.FormatType(enumType) + ")" + Convert.ToString(value, CultureInfo.InvariantCulture);
        }

        return CSharpSymbolFormatter.FormatType(enumType) + "." + field.Name;
    }

    private static INamedTypeSymbol? GetEnumType(ITypeSymbol type)
    {
        if (type is INamedTypeSymbol { TypeKind: TypeKind.Enum } enumType)
        {
            return enumType;
        }

        if (type is INamedTypeSymbol
            {
                OriginalDefinition.SpecialType: SpecialType.System_Nullable_T,
                TypeArguments.Length: 1
            } named
            && named.TypeArguments[0] is INamedTypeSymbol { TypeKind: TypeKind.Enum } underlying)
        {
            return underlying;
        }

        return null;
    }

    private static bool IsNonNullableValueType(ITypeSymbol type) =>
        type is { IsValueType: true, OriginalDefinition.SpecialType: not SpecialType.System_Nullable_T };
}
