using System.Reflection;

namespace NeuChessHu.Resources.Types;

public readonly struct Language(string value) : IEquatable<Language>
{
    public string Value { get; } = value ?? throw new ArgumentNullException(nameof(Value));

    public static Language System { get; } = new("System");
    public static Language Hungarian { get; } = new("Hungarian");
    public static Language English { get; } = new("English");

    public static readonly Dictionary<string, Language> AllLanguages = typeof(Language)
        .GetProperties(BindingFlags.Public | BindingFlags.Static)
        .Where(x => x.PropertyType == typeof(Language))
        .Select(x => (Language)x.GetValue(null)!)
        .ToDictionary(x => x.Value);

    public override bool Equals(object? obj) =>
        obj is Language other && Equals(other);

    public bool Equals(Language other) =>
        string.Equals(Value, other.Value, StringComparison.OrdinalIgnoreCase);

    public override int GetHashCode() =>
        Value?.GetHashCode(StringComparison.OrdinalIgnoreCase) ?? 0;

    public override string ToString() => Value;

    public static bool operator ==(Language left, Language right) =>
        left.Equals(right);

    public static bool operator !=(Language left, Language right) =>
        !left.Equals(right);
}
