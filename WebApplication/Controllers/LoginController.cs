using APIPortalLibrary.Models;
using APIPortalLibrary.Services.Login;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using System.Net.Http;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public LoginController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("clientidsecret")]
        [HttpPost]
        public JsonResult ClientIdSecret(string username, string password, string callbackUrl, string clientName, string owner, string grantTypes, bool saasApp)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddHttpClient<IClientIdAndSecretService, ClientIdAndSecretService>(c =>
            {
                c.BaseAddress = new Uri("https://localhost:9443");

            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var _serviceClientIdSecret = serviceProvider.GetRequiredService<IClientIdAndSecretService>();

            var taskClientIdSecret = _serviceClientIdSecret.ClientIDSecret(username, password, callbackUrl, clientName, owner, grantTypes, saasApp);
            ApiResponse<ClientIDAndSecret> clientIdSecret;
            clientIdSecret = taskClientIdSecret.Result;

            return new JsonResult(clientIdSecret.Content);
        }

        [Route("accesstoken")]
        [HttpPost]
        public JsonResult AccessToken(string username, string password, string clientId, string clientSecret)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddHttpClient<IAccessTokenService, AccessTokenService>(c =>
            {
                c.BaseAddress = new Uri("https://localhost:9443");

            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var _serviceAccessToken = serviceProvider.GetRequiredService<IAccessTokenService>();

            var taskAccessToken = _serviceAccessToken.AccessToken(username, password, clientId, clientSecret);
            ApiResponse<AccessToken> accessToken;
            accessToken = taskAccessToken.Result;

            return new JsonResult(accessToken.Content);
        }
    }
}
