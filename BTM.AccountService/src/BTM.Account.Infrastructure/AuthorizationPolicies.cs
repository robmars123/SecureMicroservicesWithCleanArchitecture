using Microsoft.AspNetCore.Authorization;

namespace BTM.Account.Infrastructure
{
    public static class AuthorizationPolicies
    {
        public static AuthorizationPolicy IsAdmin()
        {
            return new AuthorizationPolicyBuilder()
                .RequireAuthenticatedUser()
                //.RequireClaim("role", "admin")
                .RequireRole("admin")
                .Build();
        }
    }
}
