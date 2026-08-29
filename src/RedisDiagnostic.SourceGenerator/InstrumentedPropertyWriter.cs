using System.Text;
using Microsoft.CodeAnalysis;

namespace RedisDiagnostic.SourceGenerator;

internal static class InstrumentedPropertyWriter
{
    internal static void Write(StringBuilder builder, IPropertySymbol property)
    {
        builder.AppendLine();
        builder.Append("    public ").Append(CSharpSymbolFormatter.FormatType(property.Type)).Append(' ').Append(property.Name);
        builder.AppendLine();
        builder.AppendLine("    {");
        if (property.GetMethod is not null)
        {
            builder.Append("        get => _inner.").Append(property.Name).AppendLine(";");
        }

        if (property.SetMethod is not null)
        {
            builder.Append("        set => _inner.").Append(property.Name).AppendLine(" = value;");
        }

        builder.AppendLine("    }");
    }
}
