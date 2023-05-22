using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.OpenIdConnect;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Swashbuckle.AspNetCore.Filters;
using System.Text;

namespace APIs.Dependencies
{
    public static class Auth0Authentication
    {
        public static void AddAuthCode(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(options =>
            {

                options.DefaultAuthenticateScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultSignInScheme = CookieAuthenticationDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = CookieAuthenticationDefaults.AuthenticationScheme;

            }).AddCookie()
    .AddOpenIdConnect("Auth0", options =>
    {
        // Set The Authority to the Auth0 Domain
        options.Authority = $"https://{builder.Configuration["Auth0:Domain"]}";

        // Configure Auth0 Creds

        options.ClientId = builder.Configuration["Auth0:ClientID"];
        options.ClientSecret = builder.Configuration["Auth0:ClientSecret"];

        // Set Response Type to Code

        options.ResponseType = OpenIdConnectResponseType.Code;
        options.Scope.Clear();
        options.Scope.Add("openid");
        // Callback Path
        options.CallbackPath = new PathString("/callback");

        // Set Issuer

        options.ClaimsIssuer = "Auth0";

        options.Events = new OpenIdConnectEvents()
        {
            OnRedirectToIdentityProviderForSignOut = (context) =>
            {
                var logoutUri = $"https://{builder.Configuration["Auth0:Domain"]}/v2logout?client_id={builder.Configuration["Auth0:ClientID"]}/v2logout?client_secret={builder.Configuration["Auth0:ClientSecret"]}";
                var postlogoutUri = context.Properties.RedirectUri;
                if (!postlogoutUri.IsNullOrEmpty())
                {
                    if (postlogoutUri.StartsWith("/"))
                    {
                        var request = context.Request;
                        postlogoutUri = request.Scheme + "://" + request.Host + request.PathBase;
                    }
                    logoutUri += $"&returnTo={Uri.EscapeDataString(postlogoutUri)}";
                }
                context.Response.Redirect(logoutUri);
                context.HandleResponse();
                return Task.CompletedTask;
            }
        };
    });
        }
        public static void AddAuthentication(WebApplicationBuilder builder)
        {
            builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
#pragma warning disable CS8604 // Possible null reference argument.
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuerSigningKey = true,
                        ValidateLifetime = true,
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration.GetSection("AppSettings:Token").Value)),
                        ValidateIssuer = false,
                        ValidateAudience = false,

                    };
#pragma warning restore CS8604 // Possible null reference argument.
                });
        }

        public static void AddLocker(WebApplicationBuilder builder)
        {
            builder.Services.AddSwaggerGen(options =>
            {
                options.AddSecurityDefinition("oauth2", new OpenApiSecurityScheme
                {
                    Description = "Standard Authorization",
                    In = ParameterLocation.Header,
                    Name = "Authorization",
                    Type = SecuritySchemeType.ApiKey,
                    //Scheme = "Bearer"
                });
                options.OperationFilter<SecurityRequirementsOperationFilter>();
            });
        }
    }


}
