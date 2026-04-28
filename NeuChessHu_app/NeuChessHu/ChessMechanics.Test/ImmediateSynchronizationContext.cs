namespace ChessMechanics.Test;

internal sealed class ImmediateSynchronizationContext : SynchronizationContext
{
    public override void Post(SendOrPostCallback delegateEvent, object? state) =>
        delegateEvent(state);
}