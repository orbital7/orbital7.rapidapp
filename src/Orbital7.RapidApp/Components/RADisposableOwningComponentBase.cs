namespace Orbital7.RapidApp.Components;

public abstract class RADisposableOwningComponentBase<TService> :
    OwningComponentBase<TService>
    where TService : notnull
{
    private readonly CancellationTokenSource _tokenSource = new();

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
        catch (OperationCanceledException)
        {
            // Do nothing.
        }
        catch (Exception ex)
        {
            try
            {
                await OnInitializedAsyncException(ex);
            }
            catch { }
        }
    }

    protected virtual Task OnInitializedAsyncCore()
    {
        return Task.CompletedTask;
    }

    protected virtual async Task OnInitializedAsyncException(
        Exception ex)
    {
        if (this.ExceptionHandler != null)
        {
            //await DispatchExceptionAsync(ex);
            await this.ExceptionHandler.OnExceptionAsync(ex);
        }
    }

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