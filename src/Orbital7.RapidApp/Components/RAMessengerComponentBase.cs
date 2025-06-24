namespace Glacier.TradingPlatform.UI;

public abstract class RAMessengerComponentBase :
    ComponentBase, IDisposable
{
    [Inject]
    protected IMessenger? Messenger { get; init; }

    public void Dispose()
    {
        this.Messenger?.UnregisterAll(this);
    }
}