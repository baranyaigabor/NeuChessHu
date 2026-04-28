using System.Reflection;

namespace NeuChessHu.Resources.Types.ThemeTypes;

public readonly struct AppTheme(string value) : IEquatable<AppTheme>
{
    public string Value { get; } = value ?? throw new ArgumentNullException(nameof(Value));

    public static AppTheme Light { get; } = new("Light");
    public static AppTheme Dark { get; } = new("Dark");

    public static readonly Dictionary<string, AppTheme> AllAppThemes = typeof(AppTheme)
        .GetProperties(BindingFlags.Public | BindingFlags.Static)
        .Where(x => x.PropertyType == typeof(AppTheme))
        .Select(x => (AppTheme)x.GetValue(null)!)
        .ToDictionary(x => x.Value);

    public override bool Equals(object? obj) =>
        obj is AppTheme other && Equals(other);

    public bool Equals(AppTheme other) =>
        string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);

    public override int GetHashCode() =>
        Value?.GetHashCode(StringComparison.OrdinalIgnoreCase) ?? 0;

    public override string ToString() => Value;

    public static bool operator ==(AppTheme left, AppTheme right) =>
        left.Equals(right);

    public static bool operator !=(AppTheme left, AppTheme right) =>
        !left.Equals(right);
}