namespace Orbital7.RapidApp.Components;

public abstract class RAMessengerComponentBase :
    RADisposableComponentBase
{
    [Inject]
    protected IMessenger? Messenger { get; init; }

    protected override ValueTask DisposeAsyncCore()
    {
        this.Messenger?.UnregisterAll(this);

        return base.DisposeAsyncCore();
    }
}