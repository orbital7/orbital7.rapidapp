namespace Orbital7.RapidApp.Components;

public abstract class RADisposableComponentBase :
    ComponentBase, IAsyncDisposable
{
    private readonly CancellationTokenSource _tokenSource = new();

    protected bool IsDisposed { get; private set; } = false;

    protected CancellationToken CancellationToken => _tokenSource.Token;

    [Inject]
    public IServiceProvider? ServiceProvider { get; init; }

    // Optionally injectable.
    public IRADisposableComponentExceptionHandler? ExceptionHandler { get; set; }

    protected sealed override async Task OnInitializedAsync()
    {
        try
        {
            this.ExceptionHandler = 
                this.ServiceProvider?.GetService<IRADisposableComponentExceptionHandler>();

            await OnInitializedAsyncCore();
        }
        catch(OperationCanceledException)
        {
            // Do nothing.
        }
        catch(Exception ex)
        {
            try
            {
                await OnExceptionAsync(ex);
            }
            catch { }
        }
    }

    protected virtual Task OnInitializedAsyncCore()
    {
        return Task.CompletedTask;
    }

    protected virtual async Task OnExceptionAsync(
        Exception ex)
    {
        if (this.ExceptionHandler != null)
        {
            await this.ExceptionHandler.OnExceptionAsync(ex);
        }
    }

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