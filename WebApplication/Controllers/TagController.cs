using APIPortalLibrary.Models;
using APIPortalLibrary.Services.Tags;
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
    public class TagController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public TagController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("alltags")]
        [HttpGet]
        public JsonResult AllTags(int limit, int offset)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddHttpClient<ITagService, TagService>(c =>
            {
                c.BaseAddress = new Uri("https://localhost:9443");

            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var _serviceTag = serviceProvider.GetRequiredService<ITagService>();

            var taskAllTags = _serviceTag.Alltags(limit, offset);
            ApiResponse<AllTags> allTags;
            allTags = taskAllTags.Result;

            return new JsonResult(allTags.Content);
        }
    }
}
