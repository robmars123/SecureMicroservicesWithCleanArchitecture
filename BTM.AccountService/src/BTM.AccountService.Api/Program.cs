using BTM.AccountService.Api.Dependencies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.JsonWebTokens;

namespace BTM.AccountService.Api;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        builder.AddServiceDefaults();

        //Add CORS configuration dependency
        CorsConfiguration.ConfigureCors(builder.Services, builder.Configuration, builder.Environment);

        builder.Services.AddControllers()
            .AddJsonOptions(configure => configure.JsonSerializerOptions.PropertyNamingPolicy = null);
        // Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddOpenApi();

        JsonWebTokenHandler.DefaultInboundClaimTypeMap.Clear();
        builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
            //option 1
        .AddJwtBearer(options =>
        {
            options.Authority = "https://localhost:44301"; // url of IDP
            options.Audience = "btmaccountapi"; // this is the client id of the API
            options.TokenValidationParameters = new()
            {
                NameClaimType = "name",
                RoleClaimType = "role",
                ValidTypes = new[] { "at+jwt" }
            };
        });

        //options 2
        //.AddOAuth2Introspection(options =>
        //{
        //    options.Authority = "https://localhost:44301";
        //    options.ClientId = "btmaccountapi";
        //    options.ClientSecret = "apisecret";
        //    options.NameClaimType = "given_name";
        //    options.RoleClaimType = "role";
        //});

        //Authorization builder
        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("admin", policy =>
policy.RequireClaim("role", "admin"));
        });

        var app = builder.Build();

        app.MapDefaultEndpoints();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.MapOpenApi();
            app.UseSwaggerUI(options =>
            {
                options.SwaggerEndpoint("/openapi/v1.json", "OpenApi V1");
                options.RoutePrefix = string.Empty;
            });
        }

        app.UseCors();
        app.UseHttpsRedirection();

        app.UseAuthentication();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
