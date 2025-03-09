using BTM.AccountService.Api.Configurations;

namespace BTM.AccountService.Api.Dependencies
{
    public class CorsConfiguration
    {
        public static void ConfigureCors(IServiceCollection services, IConfiguration configuration, IHostEnvironment environment)
        {
            // Bind AllowedOrigins section to AllowedOriginsConfig class
            var originsConfig = new AllowedOriginsConfig();
            configuration.GetSection("AllowedOrigins").Bind(originsConfig);

            // Get the current environment
            var env = environment.EnvironmentName; // "Development", "Staging", "Production"

            // Select the appropriate environment origins
            var selectedOrigins = env switch
            {
                "Development" => originsConfig.Development,
                "Staging" => originsConfig.Staging,
                _ => originsConfig.Production // Default to Production
            };

            // Extract URLs from the selected environment
            var allowedOrigins = new[]
            {
            selectedOrigins.AccountClient,
            selectedOrigins.AccountAPI,
            selectedOrigins.IdentityService
        };

            // Add CORS policy dynamically
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(policy =>
                {
                    policy.WithOrigins(allowedOrigins)
                          .AllowAnyMethod()
                          .AllowAnyHeader();
                });
            });

            // Log allowed origins for debugging
            Console.WriteLine($"CORS Allowed Origins for {env}: {string.Join(", ", allowedOrigins)}");
        }
    }
}
