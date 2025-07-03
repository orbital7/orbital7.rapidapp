using Microsoft.AspNetCore.Components.Authorization;

namespace Orbital7.RapidApp;

public class RAAuthenticationStateProviderUserProvider :
    IUserProvider
{
    private readonly AuthenticationStateProvider _authenticationStateProvider;

    public RAAuthenticationStateProviderUserProvider(
        AuthenticationStateProvider authenticationStateProvider)
    {
        _authenticationStateProvider = authenticationStateProvider;
    }

    public async Task<string?> GetCurrentUserIdAsync()
    {
        var state = await _authenticationStateProvider.GetAuthenticationStateAsync();
        return state?.User.GetUserId<string>();
    }
}
