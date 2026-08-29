using System.Globalization;
using System.Text;
using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

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
        if (SyntaxFacts.GetKeywordKind(name) != SyntaxKind.None
            || SyntaxFacts.GetContextualKeywordKind(name) != SyntaxKind.None)
        {
            return "@" + name;
        }

        return name;
    }

    internal static string FormatDefaultValue(IParameterSymbol parameter)
    {
        var value = parameter.ExplicitDefaultValue;
        var type = parameter.Type;

        if (value is null)
        {
            return IsNonNullableValueType(type) ? "default" : "null";
        }

        var enumType = GetEnumType(type);
        if (enumType is not null)
        {
            return FormatEnumDefault(enumType, value);
        }

        return value switch
        {
            bool flag => flag ? "true" : "false",
            string text => SymbolDisplay.FormatLiteral(text, quote: true),
            char character => SymbolDisplay.FormatLiteral(character, quote: true),
            float number => FormatFloat(number),
            double number => FormatDouble(number),
            decimal number => number.ToString(CultureInfo.InvariantCulture) + "M",
            uint number => number.ToString(CultureInfo.InvariantCulture) + "U",
            long number => number.ToString(CultureInfo.InvariantCulture) + "L",
            ulong number => number.ToString(CultureInfo.InvariantCulture) + "UL",
            byte or sbyte or short or ushort or int => Convert.ToString(value, CultureInfo.InvariantCulture) ?? "0",
            TimeSpan duration => duration == TimeSpan.Zero
                ? "global::System.TimeSpan.Zero"
                : "new global::System.TimeSpan(" + duration.Ticks.ToString(CultureInfo.InvariantCulture) + "L)",
            _ => Convert.ToString(value, CultureInfo.InvariantCulture) ?? "default",
        };
    }

    internal static void AppendRefKind(StringBuilder builder, RefKind refKind)
    {
        if (refKind == RefKind.Ref)
        {
            builder.Append("ref ");
            return;
        }

        if (refKind == RefKind.Out)
        {
            builder.Append("out ");
            return;
        }

        if (refKind == RefKind.In)
        {
            builder.Append("in ");
        }
    }

    private static string FormatFloat(float number)
    {
        if (float.IsNegativeInfinity(number))
        {
            return "float.NegativeInfinity";
        }

        if (float.IsPositiveInfinity(number))
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
        if (double.IsNegativeInfinity(number))
        {
            return "double.NegativeInfinity";
        }

        if (double.IsPositiveInfinity(number))
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
        foreach (var field in enumType.GetMembers().OfType<IFieldSymbol>())
        {
            if (field.HasConstantValue && Equals(field.ConstantValue, value))
            {
                return FormatType(enumType) + "." + field.Name;
            }
        }

        return "(" + FormatType(enumType) + ")" + Convert.ToString(value, CultureInfo.InvariantCulture);
    }

    private static INamedTypeSymbol? GetEnumType(ITypeSymbol type)
    {
        if (type.TypeKind == TypeKind.Enum)
        {
            return (INamedTypeSymbol)type;
        }

        if (type is INamedTypeSymbol named
            && named.OriginalDefinition.SpecialType == SpecialType.System_Nullable_T
            && named.TypeArguments[0].TypeKind == TypeKind.Enum)
        {
            return (INamedTypeSymbol)named.TypeArguments[0];
        }

        return null;
    }

    private static bool IsNonNullableValueType(ITypeSymbol type) =>
        type.IsValueType && type.OriginalDefinition.SpecialType != SpecialType.System_Nullable_T;
}
