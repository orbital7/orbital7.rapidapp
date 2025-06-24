using Microsoft.JSInterop;

namespace Orbital7.RapidApp.Components;

public sealed class RASetBrowserTimeConverter : 
    ComponentBase
{
    [Inject] 
    private TimeConverter TimeConverter { get; init; } = default!;

    [Inject] 
    private IJSRuntime JSRuntime { get; init; } = default!;

    protected override async Task OnAfterRenderAsync(
        bool firstRender)
    {
        if (firstRender && 
            !this.TimeConverter.IsCustomLocalTimeZoneSet)
        {
            try
            {
                await using var module = await JSRuntime.InvokeAsync<IJSObjectReference>(
                    "import",
                    "./_content/Orbital7.RapidApp/js/browserTimeZone.js");
                var timeZone = await module.InvokeAsync<string>("getBrowserTimeZone");
                TimeConverter.SetCustomLocalTimeZone(timeZone);
            }
            catch (JSDisconnectedException)
            {
            }
        }
    }
}