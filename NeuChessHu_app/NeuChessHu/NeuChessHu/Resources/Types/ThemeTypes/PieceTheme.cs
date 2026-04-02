using System.Reflection;

namespace NeuChessHu.Resources.Types.ThemeTypes;

public readonly struct PieceTheme(string value) : IEquatable<PieceTheme>
{
    public string Value { get; } = value ?? throw new ArgumentNullException(nameof(Value));

    public static PieceTheme Default { get; } = new("Default");
    public static PieceTheme ThemeName { get; } = new("ThemeName");

    public static readonly Dictionary<string, PieceTheme> AllPieceThemes = typeof(PieceTheme)
        .GetProperties(BindingFlags.Public | BindingFlags.Static)
        .Where(x => x.PropertyType == typeof(PieceTheme))
        .Select(x => (PieceTheme)x.GetValue(null)!)
        .ToDictionary(x => x.Value);

    public override bool Equals(object? obj) =>
        obj is PieceTheme other && Equals(other);

    public bool Equals(PieceTheme other) =>
        string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);

    public override int GetHashCode() =>
        Value?.GetHashCode(StringComparison.OrdinalIgnoreCase) ?? 0;

    public override string ToString() => Value;

    public static bool operator ==(PieceTheme left, PieceTheme right) =>
        left.Equals(right);

    public static bool operator !=(PieceTheme left, PieceTheme right) =>
        !left.Equals(right);
}