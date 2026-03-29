namespace Orbital7.RapidApp.Components;

public interface IRADisposableComponentExceptionHandler
{
    Task OnExceptionAsync(
        Exception ex);
}
