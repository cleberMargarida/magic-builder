using System;
using System.Runtime.Serialization;

namespace MagicBuilder;

/// <summary>
/// Provides methods for creating strongly-typed builders.
/// </summary>
public class Builder
{
    /// <summary>
    /// Prevents external instantiation of the <see cref="Builder"/> base class.
    /// </summary>
    internal Builder() { }

    /// <summary>
    /// Creates a new builder instance for the specified type <typeparamref name="T"/>.
    /// </summary>
    /// <typeparam name="T">The type of object to build.</typeparam>
    /// <returns>A new <see cref="Builder{T}"/> instance.</returns>
    public static Builder<T> Create<T>()
    {
        return new Builder<T>((T)FormatterServices.GetUninitializedObject(typeof(T)));
    }
}

/// <summary>
/// A generic builder class for fluently setting properties or fields of an object.
/// </summary>
/// <typeparam name="T">The type of object being built.</typeparam>
public sealed class Builder<T> : Builder
{
    private readonly T value;

    /// <summary>
    /// Initializes a new instance of the <see cref="Builder{T}"/> class with the specified value.
    /// </summary>
    /// <param name="value">The object to be built.</param>
    internal Builder(T value)
    {
        this.value = value;
    }

    /// <summary>
    /// Applies the specified action to the object being built.
    /// </summary>
    /// <param name="action">An action that modifies the object.</param>
    /// <returns>The current <see cref="Builder{T}"/> instance.</returns>
    public Builder<T> Apply(Func<T, T> action)
    {
        return new Builder<T>(action(value));
    }

    /// <summary>
    /// Applies the specified action to the object being built.
    /// </summary>
    /// <param name="action">An action that modifies the object.</param>
    /// <returns>The current <see cref="Builder{T}"/> instance.</returns>
    public Builder<T> Apply(Action<T> action)
    {
        action(value);
        return new Builder<T>(value);
    }

    /// <summary>
    /// Finalizes and returns the built object.
    /// </summary>
    /// <returns>The constructed object of type <typeparamref name="T"/>.</returns>
    public T Build()
    {
        return value;
    }
}
