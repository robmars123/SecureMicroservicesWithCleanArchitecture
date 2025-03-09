using BTM.Account.Client.Constants;
using BTM.Account.Shared.Common;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using System.Net.Http;
using System.Text;

namespace BTM.Account.Client.Controllers
{
    public class AdminPanelController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        private readonly ILogger<AdminPanelController> _logger;

        public AdminPanelController(IHttpClientFactory httpClientFactory,
            ILogger<AdminPanelController> logger)
        {
            _httpClientFactory = httpClientFactory;
            _logger = logger;
        }

        [Authorize(Roles = GlobalConstants.Roles.Admin)]
        public async Task<IActionResult> Index()
        {
            await LogIdentityInformation();

            var httpClient = _httpClientFactory.CreateClient("APIClient");

            var request = new HttpRequestMessage(
                HttpMethod.Get,
                "/api/AdminPanel/");

            var response = await httpClient.SendAsync(
                request, HttpCompletionOption.ResponseHeadersRead);

            response.EnsureSuccessStatusCode();

            using (var responseStream = await response.Content.ReadAsStreamAsync())
            {
                var result = await System.Text.Json.JsonSerializer.DeserializeAsync<string>(responseStream);
                return View("Index", result);
            }
        }

        public async Task LogIdentityInformation()
        {
            // get the saved identity token
            var identityToken = await HttpContext
                .GetTokenAsync(OpenIdConnectParameterNames.IdToken);

            // get the saved access token
            var accessToken = await HttpContext
                .GetTokenAsync(OpenIdConnectParameterNames.AccessToken);

            // get the refresh token
            var refreshToken = await HttpContext
                .GetTokenAsync(OpenIdConnectParameterNames.RefreshToken);

            var userClaimsStringBuilder = new StringBuilder();
            foreach (var claim in User.Claims)
            {
                userClaimsStringBuilder.AppendLine(
                    $"Claim type: {claim.Type} - Claim value: {claim.Value}");
            }

            // log token & claims
            _logger.LogInformation($"Identity token & user claims: " +
                $"\n{identityToken} \n{userClaimsStringBuilder}");
            _logger.LogInformation($"Access token: " +
                $"\n{accessToken}");
            _logger.LogInformation($"Refresh token: " +
                $"\n{refreshToken}");
        }
    }
}
