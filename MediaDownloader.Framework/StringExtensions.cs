using System.Diagnostics.CodeAnalysis;
using System.Diagnostics.Contracts;

namespace MediaDownloader.Framework;

public static class StringExtensions
{
    [Pure]
    public static bool IsNullOrEmpty([NotNullWhen(false)] this string? source)
        => string.IsNullOrEmpty(source);

    [Pure]
    public static bool IsNullOrWhiteSpace([NotNullWhen(false)] this string? source)
        => string.IsNullOrWhiteSpace(source);
}