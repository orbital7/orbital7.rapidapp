using Microsoft.AspNetCore.Components.Authorization;

namespace Orbital7.RapidApp;

public class RAAuthenticationStateProviderUserProvider(
    AuthenticationStateProvider authenticationStateProvider) :
    IUserProvider
{
    private readonly AuthenticationStateProvider _authenticationStateProvider = 
        authenticationStateProvider;

    public async Task<string?> GetCurrentUserIdAsync(
        CancellationToken cancellationToken = default)
    {
        var state = await _authenticationStateProvider
            .GetAuthenticationStateAsync();

        return state?.User.GetUserId<string>();
    }
}
