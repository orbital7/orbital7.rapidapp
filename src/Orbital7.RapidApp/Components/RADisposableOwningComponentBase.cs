namespace Orbital7.RapidApp.Components;

public abstract class RADisposableOwningComponentBase<TService> :
    OwningComponentBase<TService>
    where TService : notnull
{
    private readonly CancellationTokenSource _tokenSource = new();

    protected CancellationToken CancellationToken => _tokenSource.Token;

    protected override ValueTask DisposeAsyncCore()
    {
        // Dispose of the cancellation token source to signal cancellation
        // to any ongoing operations.
        try
        {
            if (!_tokenSource.IsCancellationRequested)
            {
                _tokenSource.Cancel();
            }
        }
        catch { }
        _tokenSource.Dispose();

        return base.DisposeAsyncCore();
    }
}