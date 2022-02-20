using APIPortalLibrary.Models;
using APIPortalLibrary.Services.Applications;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Refit;
using System;
using System.Collections.Generic;
using System.Net.Http;

namespace WebApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public ApplicationController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("allapplications")]
        [HttpGet]
        public JsonResult AllApplication(int limit, int offset, string query, string accessToken, string tokenType)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddHttpClient<IApplicationService, ApplicationService>(c =>
            {
                c.BaseAddress = new Uri("https://localhost:9443");

            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var _serviceApplication = serviceProvider.GetRequiredService<IApplicationService>();

            var taskAllApplications = _serviceApplication.AllApplications(accessToken, tokenType, limit, offset, query);
            ApiResponse<AllApplications> allApplications;
            allApplications = taskAllApplications.Result;

            return new JsonResult(allApplications.Content);
        }

        [Route("applicationdetails")]
        [HttpGet]
        public JsonResult ApplicationDetails(string accessToken, string tokenType, string applicationId)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddHttpClient<IApplicationService, ApplicationService>(c =>
            {
                c.BaseAddress = new Uri("https://localhost:9443");

            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var _serviceApplication = serviceProvider.GetRequiredService<IApplicationService>();

            var taskApplicationDetails = _serviceApplication.ApplicationDetails(accessToken, tokenType, applicationId);
            ApiResponse<Application> applicationDetails;
            applicationDetails = taskApplicationDetails.Result;

            return new JsonResult(applicationDetails.Content);
        }

        [Route("keydetails")]
        [HttpGet]
        public JsonResult ApplicationKeyDetails(string accessToken, string tokenType, string applicationId, string keyType)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddHttpClient<IApplicationService, ApplicationService>(c =>
            {
                c.BaseAddress = new Uri("https://localhost:9443");

            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var _serviceApplication = serviceProvider.GetRequiredService<IApplicationService>();

            var taskApplicationKeyDetails = _serviceApplication.ApplicationKeyDetailsOfGivenType(accessToken, tokenType, applicationId, keyType);
            ApiResponse<Key> applicationKeyDetails;
            applicationKeyDetails = taskApplicationKeyDetails.Result;

            return new JsonResult(applicationKeyDetails.Content);
        }

        [Route("addapplication")]
        [HttpPost]
        public JsonResult AddApplication(string accessToken, string tokenType, string throttlingTier, string description, string name, string callbackUrl, string groupId)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddHttpClient<IApplicationService, ApplicationService>(c =>
            {
                c.BaseAddress = new Uri("https://localhost:9443");

            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var _serviceApplication = serviceProvider.GetRequiredService<IApplicationService>();

            var taskAddApplication = _serviceApplication.AddApplication(accessToken, tokenType,  throttlingTier, description, name, callbackUrl, groupId);
            ApiResponse<Application> addApplication;
            addApplication = taskAddApplication.Result;

            return new JsonResult(addApplication.Content);
        }

        [Route("updateapplication")]
        [HttpPut]
        public JsonResult UpdateApplication(string accessToken, string tokenType, string applicationId, string throttlingTier, string description, string name, string callbackUrl, string groupId)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddHttpClient<IApplicationService, ApplicationService>(c =>
            {
                c.BaseAddress = new Uri("https://localhost:9443");

            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var _serviceApplication = serviceProvider.GetRequiredService<IApplicationService>();

            var taskUpdateApplication = _serviceApplication.UpdateApplication(accessToken, tokenType, applicationId, throttlingTier, description, name, callbackUrl, groupId);
            ApiResponse<Application> updateApplication;
            updateApplication = taskUpdateApplication.Result;

            return new JsonResult(updateApplication.Content);
        }

        [Route("updategranttypescallbackurl")]
        [HttpPut]
        public JsonResult UpdateGrantTypesCallbackUrl(string accessToken, string tokenType, string applicationId, string keyType, List<string> supportedGrantTypes, string callbackUrl)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddHttpClient<IApplicationService, ApplicationService>(c =>
            {
                c.BaseAddress = new Uri("https://localhost:9443");

            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var _serviceApplication = serviceProvider.GetRequiredService<IApplicationService>();

            var taskUpdateGrantTypesCallbackUrl = _serviceApplication.UpdateGrantTypesAndCallbackUrl(accessToken, tokenType, applicationId, keyType, supportedGrantTypes, callbackUrl);
            ApiResponse<Key> updateGrantTypesCallbackUrl;
            updateGrantTypesCallbackUrl = taskUpdateGrantTypesCallbackUrl.Result;

            return new JsonResult(updateGrantTypesCallbackUrl.Content);
        }

        [Route("deleteapplication")]
        [HttpDelete]
        public JsonResult DeleteApplication(string accessToken, string tokenType, string applicationId)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddHttpClient<IApplicationService, ApplicationService>(c =>
            {
                c.BaseAddress = new Uri("https://localhost:9443");

            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var _serviceApplication = serviceProvider.GetRequiredService<IApplicationService>();

            var taskDeleteApplication = _serviceApplication.DeleteApplication(accessToken, tokenType, applicationId);
            ApiResponse<Application> deleteApplication;
            deleteApplication = taskDeleteApplication.Result;

            return new JsonResult(deleteApplication.Content);
        }

        [Route("generateapplicationkeys")]
        [HttpPost]
        public JsonResult GenerateApplicationKeys(string accessToken, string tokenType, string applicationId, int validityTime, string keyType, List<string> supportedGrantTypes)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddHttpClient<IApplicationService, ApplicationService>(c =>
            {
                c.BaseAddress = new Uri("https://localhost:9443");

            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var _serviceApplication = serviceProvider.GetRequiredService<IApplicationService>();

            var taskGenerateApplicationKeys = _serviceApplication.GenerateApplicationKeys(accessToken, tokenType, applicationId, validityTime, keyType, supportedGrantTypes);
            ApiResponse<GenerateApplicationKeys> generateApplicationKeys;
            generateApplicationKeys = taskGenerateApplicationKeys.Result;

            return new JsonResult(generateApplicationKeys.Content);
        }

    }

}
