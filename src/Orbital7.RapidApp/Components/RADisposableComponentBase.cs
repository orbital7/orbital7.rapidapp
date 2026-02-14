namespace Orbital7.RapidApp.Components;

public abstract class RADisposableComponentBase :
    ComponentBase, IAsyncDisposable
{
    private readonly CancellationTokenSource _tokenSource = new();

    protected bool IsDisposed { get; private set; } = false;

    protected CancellationToken CancellationToken => _tokenSource.Token;

    async ValueTask IAsyncDisposable.DisposeAsync()
    {
        await DisposeAsyncCore().ConfigureAwait(false);
        GC.SuppressFinalize(this);
    }

    protected virtual ValueTask DisposeAsyncCore()
    {
        if (!this.IsDisposed)
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

            this.IsDisposed = true;
        }

        return ValueTask.CompletedTask;
    }
}