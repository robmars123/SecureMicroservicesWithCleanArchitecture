namespace BTM.AccountService.Api.Configurations
{
    public class EnvironmentOrigins
    {
        public string AccountClient { get; set; } = string.Empty;
        public string AccountAPI { get; set; } = string.Empty;
        public string IdentityService { get; set; } = string.Empty;
    }

    public class AllowedOriginsConfig
    {
        public EnvironmentOrigins Development { get; set; } = new();
        public EnvironmentOrigins Staging { get; set; } = new();
        public EnvironmentOrigins Production { get; set; } = new();
    }
}
