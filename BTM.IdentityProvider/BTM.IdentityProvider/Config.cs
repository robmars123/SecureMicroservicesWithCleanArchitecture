using Duende.IdentityServer;
using Duende.IdentityServer.Models;

namespace BTM.IdentityProvider;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource("roles", "Your role(s)", new [] { "role" }),
            //new IdentityResource("country","The country you're living in",new List<string>() { "country" })
        };

    public static IEnumerable<ApiResource> ApiResources =>
     new ApiResource[]
         {
             new ApiResource("btmaccountapi",
                 "BTM Account API",
                 new [] { "role" })
             {
                 Scopes = { "btmaccountapi.fullaccess",
                     "btmaccountapi.read",
                     "btmaccountapi.write"},
                ApiSecrets = { new Secret("apisecret".Sha256()) }
             }
         };

    public static IEnumerable<ApiScope> ApiScopes =>
    new ApiScope[]
        {
                //clients should match these scopes
                new ApiScope("btmaccountapi.fullaccess"),
                new ApiScope("btmaccountapi.read"),
                new ApiScope("btmaccountapi.write")};

    public static IEnumerable<Client> Clients =>
        new Client[]
            {
                new Client()
                {
                    ClientName = "Account Service Web Client",
                    //this is the client id that will be used by the client application to identify itself
                    //BTM.Account.Client.MVC
                    AccessTokenType = AccessTokenType.Jwt,
                    AllowOfflineAccess = true,
                    ClientId = "AccountService.WebClient",
                    AllowedGrantTypes = GrantTypes.Code,
                    RedirectUris = new List<string>()
                    {
                        "https://localhost:7237/signin-oidc"
                    },
                    PostLogoutRedirectUris = new List<string>()
                    {
                        "https://localhost:7237/signout-callback-oidc"
                    },
                    AllowedScopes = 
                    { 
                        IdentityServerConstants.StandardScopes.OpenId,
                        IdentityServerConstants.StandardScopes.Profile,
                        "roles",
                        "btmaccountapi.fullaccess",//clients should match these scopes
                        "btmaccountapi.read",//clients should match these scopes
                        "btmaccountapi.write",//clients should match these scopes
                    },
                    ClientSecrets = 
                    { 
                        new Secret("mysecret".Sha256())
                    },
                   // RequireConsent = true
                }
            };
}