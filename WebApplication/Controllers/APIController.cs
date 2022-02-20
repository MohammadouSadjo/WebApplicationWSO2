using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using APIPortalLibrary.Services;
using APIPortalLibrary.Models;
using Refit;
using System;
using Microsoft.Extensions.DependencyInjection;
using APIPortalLibrary.Services.APIs;
using System.Net.Http;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class APIController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public APIController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("allapis")]
        [HttpGet]
        public JsonResult AllApis(int limit,int offset, string query)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddHttpClient<IAPIService, APIService>(c =>
            {
                c.BaseAddress = new Uri("https://localhost:9443");

            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var _serviceAPI = serviceProvider.GetRequiredService<IAPIService>();

            var taskAllApis = _serviceAPI.AllApis(limit, offset, query);
            ApiResponse<AllApis> allApis;
            allApis = taskAllApis.Result;

            return new JsonResult(allApis.Content);
        }

        [Route("apidetails")]
        [HttpGet]
        public JsonResult ApiDetails(string apiId)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddHttpClient<IAPIService, APIService>(c =>
            {
                c.BaseAddress = new Uri("https://localhost:9443");

            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var _serviceAPI = serviceProvider.GetRequiredService<IAPIService>();

            var taskApiDetails = _serviceAPI.APIDetails(apiId);
            ApiResponse<API> apiDetails;
            apiDetails = taskApiDetails.Result;

            return new JsonResult(apiDetails.Content);
        }

        [Route("swagger")]
        [HttpGet]
        public JsonResult GetSwaggerDefinition(string apiId)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddHttpClient<IAPIService, APIService>(c =>
            {
                c.BaseAddress = new Uri("https://localhost:9443");

            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var _serviceAPI = serviceProvider.GetRequiredService<IAPIService>();

            var taskSwaggerDefinition = _serviceAPI.SwaggerDefinition(apiId);
            ApiResponse<string> swaggerDefinition;
            swaggerDefinition = taskSwaggerDefinition.Result;

            return new JsonResult(swaggerDefinition.Content);
        }

    }
}
