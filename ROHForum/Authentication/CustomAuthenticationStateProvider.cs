using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.Server.ProtectedBrowserStorage;
using System.Security.Claims;
using ViewModels;

namespace ROHForum.Authentication
{
    public class CustomAuthenticationStateProvider : AuthenticationStateProvider
    {
        private readonly ProtectedSessionStorage _sessionStorage;
        private ClaimsPrincipal _anonymous = new ClaimsPrincipal(new ClaimsIdentity());

        public CustomAuthenticationStateProvider(ProtectedSessionStorage sessionStorage)
        {
            _sessionStorage = sessionStorage;
        }
    

    public override async Task<AuthenticationState> GetAuthenticationStateAsync()
        {

            try
            {
                var userSessionStorageResult = await _sessionStorage.GetAsync<UserSessionViewModel>("UserSession");
                var userSession = userSessionStorageResult.Success ? userSessionStorageResult.Value : null;
                if (userSession == null)
                    return await Task.FromResult(new AuthenticationState(_anonymous));

                var claims = _setClaims(userSession);


                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity(claims, "CustomAuth"));
                return await Task.FromResult(new AuthenticationState(claimsPrincipal));
            }

            catch
            {
                return await Task.FromResult(new AuthenticationState(_anonymous));

            }
        }

        public async Task UpdateAuthenticationState(UserSessionViewModel viewModel)
        {
            ClaimsPrincipal claimsPrincipal;
            if (viewModel != null)
            {
                var claims = _setClaims(viewModel);
                await _sessionStorage.SetAsync("UserSession", viewModel);
            }

            else
            {
                await _sessionStorage.DeleteAsync("UserSession");
                claimsPrincipal = _anonymous;
            }

        }


        private List<Claim> _setClaims(UserSessionViewModel viewModel)
        {
            /* @context.User.Identity.Name"*/

            var claims = new List<Claim>();
            claims.Add(new Claim(ClaimTypes.Name, viewModel.Username));

            return claims;
        }
    }
}