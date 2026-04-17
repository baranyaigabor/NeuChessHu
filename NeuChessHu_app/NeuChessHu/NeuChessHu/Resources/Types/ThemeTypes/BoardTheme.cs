using System.Reflection;

namespace NeuChessHu.Resources.Types.ThemeTypes;

public readonly struct BoardTheme(string value) : IEquatable<BoardTheme>
{
    public string Value { get; } = value ?? throw new ArgumentNullException(nameof(Value));

    public static BoardTheme PastelGreen { get; } = new("Pastel Green");
    public static BoardTheme Modern { get; } = new("Modern");
    public static BoardTheme Wooden { get; } = new("Wooden");
    public static BoardTheme Royal { get; } = new("Royal");

    public static readonly Dictionary<string, BoardTheme> AllBoardThemes = typeof(BoardTheme)
        .GetProperties(BindingFlags.Public | BindingFlags.Static)
        .Where(x => x.PropertyType == typeof(BoardTheme))
        .Select(x => (BoardTheme)x.GetValue(null)!)
        .ToDictionary(x => x.Value);

    public override bool Equals(object? obj) =>
        obj is BoardTheme other && Equals(other);

    public bool Equals(BoardTheme other) =>
        string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);

    public override int GetHashCode() =>
        Value?.GetHashCode(StringComparison.OrdinalIgnoreCase) ?? 0;

    public override string ToString() => Value;

    public static bool operator ==(BoardTheme left, BoardTheme right) =>
        left.Equals(right);

    public static bool operator !=(BoardTheme left, BoardTheme right) =>
        !left.Equals(right);
}