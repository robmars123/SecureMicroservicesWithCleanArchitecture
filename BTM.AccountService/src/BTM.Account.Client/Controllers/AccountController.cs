using BTM.Account.Client.Constants;
using BTM.Account.Shared.Common;
using IdentityModel.Client;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace BTM.Account.Client.Controllers
{
    public class AccountController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public AccountController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory ??
                throw new ArgumentNullException(nameof(httpClientFactory));
        }

        [Authorize(Roles = GlobalConstants.Roles.Registered)]
        public IActionResult UserAccount()
        {
            return View();
        }

        [Authorize]
        public async Task Logout()
        {
            //var client = _httpClientFactory.CreateClient("IDPClient");

            //var discoveryDocumentResponse = await client
            //    .GetDiscoveryDocumentAsync();
            //if (discoveryDocumentResponse.IsError)
            //{
            //    throw new Exception(discoveryDocumentResponse.Error);
            //}

            //var accessTokenRevocationResponse = await client
            //    .RevokeTokenAsync(new()
            //    {
            //        Address = discoveryDocumentResponse.RevocationEndpoint,
            //        ClientId = "AccountService.WebClient",
            //        ClientSecret = "mysecret",
            //        Token = await HttpContext.GetTokenAsync(
            //            OpenIdConnectParameterNames.AccessToken)
            //    });

            //if (accessTokenRevocationResponse.IsError)
            //{
            //    throw new Exception(accessTokenRevocationResponse.Error);
            //}

            //var refreshTokenRevocationResponse = await client
            //    .RevokeTokenAsync(new()
            //    {
            //        Address = discoveryDocumentResponse.RevocationEndpoint,
            //        ClientId = "AccountService.WebClient",
            //        ClientSecret = "mysecret",
            //        Token = await HttpContext.GetTokenAsync(
            //        OpenIdConnectParameterNames.RefreshToken)
            //    });

            //if (refreshTokenRevocationResponse.IsError)
            //{
            //    throw new Exception(accessTokenRevocationResponse.Error);
            //}

            // Clears the  local cookie
            await HttpContext.SignOutAsync(
                CookieAuthenticationDefaults.AuthenticationScheme);

            // Redirects to the IDP linked to scheme
            // "OpenIdConnectDefaults.AuthenticationScheme" (oidc)
            // so it can clear its own session/cookie
            await HttpContext.SignOutAsync(
                OpenIdConnectDefaults.AuthenticationScheme);
        }

        public IActionResult AccessDenied()
        {
            return View();
        }
    }
}
