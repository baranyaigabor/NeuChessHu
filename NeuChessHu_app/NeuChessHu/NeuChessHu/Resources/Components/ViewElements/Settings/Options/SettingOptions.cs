using ChessMechanics.Common;

namespace NeuChessHu.Resources.Components.ViewElements.Settings.Options;

public class SettingOption<T>(T value, string label) : ObservableBase
{
    string label = label;

    public T Value { get; } = value;
    public string Label
    {
        get => label;
        set { label = value; RaisePropertyChanged(); }
    }
}