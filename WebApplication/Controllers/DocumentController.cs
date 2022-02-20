using APIPortalLibrary.Models;
using APIPortalLibrary.Services.Documents;
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
    public class DocumentController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public DocumentController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("alldocuments")]
        [HttpGet]
        public JsonResult AllDocuments(string apiId)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddHttpClient<IDocumentService, DocumentService>(c =>
            {
                c.BaseAddress = new Uri("https://localhost:9443");

            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var _serviceDocument = serviceProvider.GetRequiredService<IDocumentService>();

            var taskAllDocuments = _serviceDocument.AllDocuments(apiId);
            ApiResponse<AllDocuments> allDocuments;
            allDocuments = taskAllDocuments.Result;

            return new JsonResult(allDocuments.Content);
        }

        [Route("getdocument")]
        [HttpGet]
        public JsonResult GetDocument(string apiId, string documentId)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddHttpClient<IDocumentService, DocumentService>(c =>
            {
                c.BaseAddress = new Uri("https://localhost:9443");

            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var _serviceDocument = serviceProvider.GetRequiredService<IDocumentService>();

            var taskGetDocument = _serviceDocument.GetDocument(apiId, documentId);
            ApiResponse<Document> getDocument;
            getDocument = taskGetDocument.Result;

            return new JsonResult(getDocument.Content);
        }

        [Route("getdocumentcontent")]
        [HttpGet]
        public JsonResult GetDocumentContent(string apiId, string documentId)
        {
            IServiceCollection services = new ServiceCollection();

            services.AddHttpClient<IDocumentService, DocumentService>(c =>
            {
                c.BaseAddress = new Uri("https://localhost:9443");

            }).ConfigurePrimaryHttpMessageHandler(() => new HttpClientHandler
            {
                ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => { return true; }
            });

            ServiceProvider serviceProvider = services.BuildServiceProvider();
            var _serviceDocument = serviceProvider.GetRequiredService<IDocumentService>();

            var taskGetDocumentContent = _serviceDocument.GetDocumentContent(apiId, documentId);
            ApiResponse<string> getDocumentContent;
            getDocumentContent = taskGetDocumentContent.Result;

            return new JsonResult(getDocumentContent.Content);
        }
    }
}
