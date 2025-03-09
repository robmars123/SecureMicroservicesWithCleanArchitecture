/*
 * TODO: Add this to separate solution and its own repository
 */


using System.Diagnostics;

var builder = DistributedApplication.CreateBuilder(args);

var cache = builder.AddRedis("cache");

//APIs
var apiService = builder.AddProject<Projects.BTM_AccountService_Api>("apiservice");
apiService.WithCommand(
    "swagger-ui-docs",
    "Swagger UI Documentation",
    executeCommand: async _ =>
    {
        try
        {
            // Base URL
            var endpoint = apiService.GetEndpoint("https");
            var url = $"{endpoint.Url}/swagger";

            Process.Start(new ProcessStartInfo(url) { UseShellExecute = true });
            return new ExecuteCommandResult { Success = true };
        }
        catch (Exception ex)
        {
            return new ExecuteCommandResult { Success = false, ErrorMessage = ex.Message };
        }

    },
    updateState: context => context.ResourceSnapshot.HealthStatus == Microsoft.Extensions.Diagnostics.HealthChecks.HealthStatus.Healthy ?
    ResourceCommandState.Enabled : ResourceCommandState.Disabled,
    iconName: "Document",
    iconVariant: IconVariant.Filled
    );

//Frontend UI
//builder.AddProject<Projects.BTM_AccountService_Web>("webfrontend")
//    .WithExternalHttpEndpoints()
//    .WithReference(cache)
//    .WaitFor(cache)
//    .WithReference(apiService)
//    .WaitFor(apiService);

builder.AddProject<Projects.BTM_Account_Client>("btm-account-client")
        .WithExternalHttpEndpoints()
    .WithReference(cache)
    .WaitFor(cache)
    .WithReference(apiService)
    .WaitFor(apiService);
builder.AddProject<Projects.BTM_IdentityProvider>("btm-identityprovider");
builder.Build().Run();
