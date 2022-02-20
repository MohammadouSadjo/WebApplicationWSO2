using APIPortalLibrary.Models;
using APIPortalLibrary.Services.Subscriptions;
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
    public class SubscriptionController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public SubscriptionController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("allsubscriptions")]
        [HttpGet]
        public JsonResult AllSubscriptions(int limit, int offset, string accessToken, string tokenType, string applicationId)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddHttpClient<ISubscriptionService, SubscriptionService>(c =>
            {
                c.BaseAddress = new Uri("https://localhost:9443");

            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var _serviceSubscription = serviceProvider.GetRequiredService<ISubscriptionService>();

            var taskAllSubscriptions = _serviceSubscription.AllSubscriptions(accessToken, tokenType, applicationId, offset, limit);
            ApiResponse<AllSubscriptions> allSubscriptions;
            allSubscriptions = taskAllSubscriptions.Result;

            return new JsonResult(allSubscriptions.Content);
        }

        [Route("getsubscription")]
        [HttpGet]
        public JsonResult GetSubscription(string accessToken, string tokenType, string subscriptionId)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddHttpClient<ISubscriptionService, SubscriptionService>(c =>
            {
                c.BaseAddress = new Uri("https://localhost:9443");

            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var _serviceSubscription = serviceProvider.GetRequiredService<ISubscriptionService>();

            var taskGetSubscription = _serviceSubscription.SubscriptionDetails(accessToken, tokenType, subscriptionId);
            ApiResponse<Subscription> getSubscription;
            getSubscription = taskGetSubscription.Result;

            return new JsonResult(getSubscription.Content);
        }

        [Route("addsubscription")]
        [HttpPost]
        public JsonResult AddSubscription(string accessToken, string tokenType, string tier, string apiIdentifier, string applicationId)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddHttpClient<ISubscriptionService, SubscriptionService>(c =>
            {
                c.BaseAddress = new Uri("https://localhost:9443");

            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var _serviceSubscription = serviceProvider.GetRequiredService<ISubscriptionService>();

            var taskAddSubscription = _serviceSubscription.AddSubscription(accessToken, tokenType, tier, apiIdentifier, applicationId);
            ApiResponse<Subscription> addSubscription;
            addSubscription = taskAddSubscription.Result;

            return new JsonResult(addSubscription.Content);
        }

        [Route("addmultiplesubscriptions")]
        [HttpPost]
        public JsonResult AddMultipleSubscriptions(string accessToken, string tokenType, List<Subscription> subscriptions)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddHttpClient<ISubscriptionService, SubscriptionService>(c =>
            {
                c.BaseAddress = new Uri("https://localhost:9443");

            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var _serviceSubscription = serviceProvider.GetRequiredService<ISubscriptionService>();

            var taskAddMultipleSubscriptions = _serviceSubscription.AddSubscriptionMultiple(accessToken, tokenType, subscriptions);
            ApiResponse<List<Subscription>> addMultipleSubscriptions;
            addMultipleSubscriptions = taskAddMultipleSubscriptions.Result;

            return new JsonResult(addMultipleSubscriptions.Content);
        }

        [Route("deletesubscription")]
        [HttpDelete]
        public JsonResult DeleteSubscription(string accessToken, string tokenType, string subscriptionId)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddHttpClient<ISubscriptionService, SubscriptionService>(c =>
            {
                c.BaseAddress = new Uri("https://localhost:9443");

            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var _serviceSubscription = serviceProvider.GetRequiredService<ISubscriptionService>();

            var taskDeleteSubscription = _serviceSubscription.DeleteSubscription(accessToken, tokenType, subscriptionId);
            ApiResponse<Subscription> deleteSubscription;
            deleteSubscription = taskDeleteSubscription.Result;

            return new JsonResult(deleteSubscription.Content);
        }
    }
}
