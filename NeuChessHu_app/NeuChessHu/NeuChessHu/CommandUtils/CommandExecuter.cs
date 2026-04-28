using System.Windows.Input;

namespace NeuChessHu.CommandUtils;

public class CommandExecuter<T>(Action<T?> execute, Func<T?, bool>? canExecute = null) : ICommand
{
    readonly Action<T?> execute = execute ?? throw new ArgumentNullException(nameof(execute));

    public event EventHandler? CanExecuteChanged;

    public bool CanExecute(object? parameter) =>
        canExecute?.Invoke((T?)parameter) ?? true;

    public void Execute(object? parameter) =>
        execute((T?)parameter);

    public void RaiseCanExecuteChanged() =>
        CanExecuteChanged?.Invoke(this, EventArgs.Empty);
}