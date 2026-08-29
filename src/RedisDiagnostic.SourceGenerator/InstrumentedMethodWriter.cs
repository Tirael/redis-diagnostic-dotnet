namespace RedisDiagnostic.SourceGenerator;

internal static class InstrumentedMethodWriter
{
    private static readonly HashSet<string> s_passThroughMethods = new(StringComparer.Ordinal)
    {
        "CreateBatch",
        "CreateTransaction",
    };

    internal static void Write(StringBuilder builder, IMethodSymbol method, bool explicitInterface)
    {
        builder.AppendLine();
        builder.Append("    ");
        if (!explicitInterface)
        {
            builder.Append("public ");
        }

        builder.Append(CSharpSymbolFormatter.FormatType(method.ReturnType));
        builder.Append(' ');
        if (explicitInterface)
        {
            builder.Append(CSharpSymbolFormatter.FormatType(method.ContainingType)).Append('.');
        }

        builder.Append(method.Name);
        AppendTypeArguments(builder, method);
        builder.Append('(');
        for (var i = 0; i < method.Parameters.Length; i++)
        {
            if (i > 0)
            {
                builder.Append(", ");
            }

            AppendParameter(builder, method.Parameters[i], includeDefault: !explicitInterface);
        }

        builder.Append(')');
        if (!explicitInterface)
        {
            AppendConstraints(builder, method);
        }

        builder.AppendLine();
        builder.Append("        => ");

        var invocation = BuildInvocation(method, explicitInterface);
        if (s_passThroughMethods.Contains(method.Name))
        {
            builder.Append(invocation).AppendLine(";");
            return;
        }

        var measureMethod = IsAsyncTask(method.ReturnType) ? "MeasureAsync" : "Measure";
        builder.Append("_metrics.").Append(measureMethod).Append("(nameof(").Append(method.Name)
            .Append("), () => ").Append(invocation).AppendLine(");");
    }

    private static void AppendParameter(StringBuilder builder, IParameterSymbol parameter, bool includeDefault)
    {
        CSharpSymbolFormatter.AppendRefKind(builder, parameter.RefKind);
        if (parameter is { IsParams: true })
        {
            builder.Append("params ");
        }

        builder.Append(CSharpSymbolFormatter.FormatType(parameter.Type))
            .Append(' ')
            .Append(CSharpSymbolFormatter.EscapeIdentifier(parameter.Name));
        if (!includeDefault || parameter is not { HasExplicitDefaultValue: true })
        {
            return;
        }

        builder.Append(" = ").Append(CSharpDefaultValueFormatter.Format(parameter));
    }

    private static void AppendConstraints(StringBuilder builder, IMethodSymbol method)
    {
        foreach (var typeParameter in method.TypeParameters)
        {
            var constraints = CollectConstraints(typeParameter);
            if (constraints.Count is 0)
            {
                continue;
            }

            builder.Append(" where ").Append(typeParameter.Name).Append(" : ").Append(string.Join(", ", constraints));
        }
    }

    private static List<string> CollectConstraints(ITypeParameterSymbol typeParameter)
    {
        List<string> constraints = new();
        if (typeParameter.HasReferenceTypeConstraint)
        {
            constraints.Add(typeParameter.ReferenceTypeConstraintNullableAnnotation is NullableAnnotation.Annotated
                ? "class?"
                : "class");
        }

        if (typeParameter.HasValueTypeConstraint)
        {
            constraints.Add("struct");
        }

        if (typeParameter.HasNotNullConstraint)
        {
            constraints.Add("notnull");
        }

        if (typeParameter.HasUnmanagedTypeConstraint)
        {
            constraints.Add("unmanaged");
        }

        foreach (var constraintType in typeParameter.ConstraintTypes)
        {
            constraints.Add(CSharpSymbolFormatter.FormatType(constraintType));
        }

        if (typeParameter.HasConstructorConstraint)
        {
            constraints.Add("new()");
        }

        return constraints;
    }

    private static string BuildInvocation(IMethodSymbol method, bool explicitInterface)
    {
        StringBuilder builder = new();
        if (explicitInterface)
        {
            builder.Append("((").Append(CSharpSymbolFormatter.FormatType(method.ContainingType)).Append(")_inner).");
        }
        else
        {
            builder.Append("_inner.");
        }

        builder.Append(method.Name);
        AppendTypeArguments(builder, method);
        builder.Append('(');
        for (var i = 0; i < method.Parameters.Length; i++)
        {
            if (i > 0)
            {
                builder.Append(", ");
            }

            CSharpSymbolFormatter.AppendRefKind(builder, method.Parameters[i].RefKind);
            builder.Append(CSharpSymbolFormatter.EscapeIdentifier(method.Parameters[i].Name));
        }

        builder.Append(')');
        return builder.ToString();
    }

    private static void AppendTypeArguments(StringBuilder builder, IMethodSymbol method)
    {
        if (method.TypeParameters.Length is 0)
        {
            return;
        }

        builder.Append('<');
        for (var i = 0; i < method.TypeParameters.Length; i++)
        {
            if (i > 0)
            {
                builder.Append(", ");
            }

            builder.Append(method.TypeParameters[i].Name);
        }

        builder.Append('>');
    }

    private static bool IsAsyncTask(ITypeSymbol returnType) =>
        returnType is INamedTypeSymbol named
        && named.OriginalDefinition.ToDisplayString() is
            "System.Threading.Tasks.Task" or "System.Threading.Tasks.Task<TResult>";
}
