using System;

namespace MagicBuilder;

/// <summary>
/// Instructs the code generator to generate a builder class for the specified type.
/// </summary>
/// <param name="type">The target type for which a builder will be generated.</param>
[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
#pragma warning disable CS9113 // code generator reads it.
public sealed class GenerateBuilderAttribute(Type type) : Attribute;
