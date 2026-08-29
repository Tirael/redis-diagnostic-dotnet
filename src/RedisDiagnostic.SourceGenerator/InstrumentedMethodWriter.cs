namespace RedisDiagnostic.SourceGenerator;

internal static class InstrumentedMethodWriter
{
    internal static void Write(StringBuilder builder, IMethodSymbol method)
    {
        builder.AppendLine();
        builder.Append("    public ");
        AppendReturnTypeAndName(builder, method);
        AppendParameters(builder, method);
        AppendConstraints(builder, method);
        builder.AppendLine();
        AppendBody(builder, method, BuildInvocation(method));
    }

    internal static void WriteExplicit(StringBuilder builder, IMethodSymbol method)
    {
        builder.AppendLine();
        builder.Append("    ");
        builder.Append(CSharpSymbolFormatter.FormatType(method.ReturnType)).Append(' ');
        builder.Append(CSharpSymbolFormatter.FormatType(method.ContainingType)).Append('.');
        builder.Append(method.Name);
        AppendTypeArguments(builder, method);
        AppendParametersWithoutDefaults(builder, method);
        builder.AppendLine();
        AppendBody(builder, method, BuildExplicitInvocation(method));
    }

    private static void AppendReturnTypeAndName(StringBuilder builder, IMethodSymbol method)
    {
        builder.Append(CSharpSymbolFormatter.FormatType(method.ReturnType)).Append(' ');
        builder.Append(method.Name);
        AppendTypeArguments(builder, method);
    }

    private static void AppendBody(StringBuilder builder, IMethodSymbol method, string invocation)
    {
        builder.Append("        => ");
        if (method.Name is "CreateBatch" or "CreateTransaction")
        {
            builder.Append(invocation).AppendLine(";");
            return;
        }

        var measureMethod = IsAsyncTask(method.ReturnType) ? "MeasureAsync" : "Measure";
        builder.Append("_metrics.").Append(measureMethod).Append("(nameof(").Append(method.Name)
            .Append("), () => ").Append(invocation).AppendLine(");");
    }

    private static void AppendParameters(StringBuilder builder, IMethodSymbol method)
    {
        builder.Append('(');
        for (var i = 0; i < method.Parameters.Length; i++)
        {
            if (i is > 0)
                builder.Append(", ");

            AppendParameter(builder, method.Parameters[i]);
        }

        builder.Append(')');
    }

    private static void AppendParametersWithoutDefaults(StringBuilder builder, IMethodSymbol method)
    {
        builder.Append('(');
        for (var i = 0; i < method.Parameters.Length; i++)
        {
            if (i is > 0)
                builder.Append(", ");

            AppendParameterCore(builder, method.Parameters[i]);
        }

        builder.Append(')');
    }

    private static void AppendParameter(StringBuilder builder, IParameterSymbol parameter)
    {
        AppendParameterCore(builder, parameter);
        if (parameter is { HasExplicitDefaultValue: true })
            builder.Append(" = ").Append(CSharpDefaultValueFormatter.Format(parameter));
    }

    private static void AppendParameterCore(StringBuilder builder, IParameterSymbol parameter)
    {
        CSharpSymbolFormatter.AppendRefKind(builder, parameter.RefKind);
        if (parameter is { IsParams: true })
            builder.Append("params ");

        builder.Append(CSharpSymbolFormatter.FormatType(parameter.Type))
            .Append(' ')
            .Append(CSharpSymbolFormatter.EscapeIdentifier(parameter.Name));
    }

    private static void AppendConstraints(StringBuilder builder, IMethodSymbol method)
    {
        foreach (var typeParameter in method.TypeParameters)
        {
            var constraints = CollectConstraints(typeParameter);
            if (constraints.Count is 0)
                continue;

            builder.Append(" where ").Append(typeParameter.Name).Append(" : ").Append(string.Join(", ", constraints));
        }
    }

    private static List<string> CollectConstraints(ITypeParameterSymbol typeParameter)
    {
        List<string> constraints = new();
        if (typeParameter is { HasReferenceTypeConstraint: true })
            constraints.Add(typeParameter.ReferenceTypeConstraintNullableAnnotation is NullableAnnotation.Annotated
                ? "class?"
                : "class");

        if (typeParameter is { HasValueTypeConstraint: true })
            constraints.Add("struct");

        if (typeParameter is { HasNotNullConstraint: true })
            constraints.Add("notnull");

        if (typeParameter is { HasUnmanagedTypeConstraint: true })
            constraints.Add("unmanaged");

        foreach (var constraintType in typeParameter.ConstraintTypes)
            constraints.Add(CSharpSymbolFormatter.FormatType(constraintType));

        if (typeParameter is { HasConstructorConstraint: true })
            constraints.Add("new()");

        return constraints;
    }

    private static string BuildInvocation(IMethodSymbol method) =>
        BuildCall(method, "_inner.");

    private static string BuildExplicitInvocation(IMethodSymbol method) =>
        BuildCall(method, "((" + CSharpSymbolFormatter.FormatType(method.ContainingType) + ")_inner).");

    private static string BuildCall(IMethodSymbol method, string receiver)
    {
        StringBuilder builder = new();
        builder.Append(receiver);
        builder.Append(method.Name);
        AppendTypeArguments(builder, method);
        builder.Append('(');
        for (var i = 0; i < method.Parameters.Length; i++)
        {
            if (i is > 0)
                builder.Append(", ");

            CSharpSymbolFormatter.AppendRefKind(builder, method.Parameters[i].RefKind);
            builder.Append(CSharpSymbolFormatter.EscapeIdentifier(method.Parameters[i].Name));
        }

        builder.Append(')');
        return builder.ToString();
    }

    private static void AppendTypeArguments(StringBuilder builder, IMethodSymbol method)
    {
        if (method is { TypeParameters.Length: 0 })
            return;

        builder.Append('<');
        for (var i = 0; i < method.TypeParameters.Length; i++)
        {
            if (i is > 0)
                builder.Append(", ");

            builder.Append(method.TypeParameters[i].Name);
        }

        builder.Append('>');
    }

    private static bool IsAsyncTask(ITypeSymbol returnType) =>
        returnType is INamedTypeSymbol named
        && named.OriginalDefinition.ToDisplayString() is
            "System.Threading.Tasks.Task" or "System.Threading.Tasks.Task<TResult>";
}
