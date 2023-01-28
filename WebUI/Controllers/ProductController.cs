﻿using Microsoft.AspNetCore.Mvc;
using Microsoft.Net.Http.Headers;
using System.Text.Json;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public ProductController(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IActionResult> Index()
        {

            var httpRequestMessage = new HttpRequestMessage(
            HttpMethod.Get,
            "http://localhost:5131/Category")
            {
                Headers =
            {
                { HeaderNames.Accept, "text/plain" },
                { HeaderNames.UserAgent, "HttpRequestsSample" }
            }
            };

            var httpClient = _httpClientFactory.CreateClient();
            var httpResponseMessage = await httpClient.SendAsync(httpRequestMessage);

            if (httpResponseMessage.IsSuccessStatusCode)
            {
                using var contentStream =
                    await httpResponseMessage.Content.ReadAsStreamAsync();

                var  result = await JsonSerializer.DeserializeAsync
                    <IEnumerable<Category>>(contentStream);
            }
            return View();
        }

    }
}
