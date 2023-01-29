using Microsoft.AspNetCore.Mvc;
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

            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, "http://localhost:5131/api/Category")
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
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                var result = await JsonSerializer.DeserializeAsync
                    <DataResultModel<List<CategoryModel>>>(contentStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return View(result);
            }
            return View(null);
        }

        public async Task<List<ProductModel>> GetAllProductByCategory(int categoryId)
        {
            var httpRequestMessage = new HttpRequestMessage(HttpMethod.Get, String.Concat("http://localhost:5131/api/Category/",categoryId.ToString()))
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
                using var contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();

                var result = await JsonSerializer.DeserializeAsync
                    <DataResultModel<List<ProductModel>>>(contentStream, new JsonSerializerOptions { PropertyNameCaseInsensitive = true });
                return result.Data;
            }
            return null;
        }

    }
}
