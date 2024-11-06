using System.Diagnostics;
using System.Net.Http.Headers;
using BusinessObject.DTO;
using Microsoft.AspNetCore.Mvc;
using ProductManagementClient.Models;

namespace ProductManagementClient.Controllers
{
    public class ProductController : Controller
    {
        private readonly ILogger<ProductController> _logger;
        private readonly HttpClient? _client = null;
        private string url = "";

        public ProductController(ILogger<ProductController> logger)
        {
            _logger = logger;
            _client = new HttpClient();
            var contentType = new MediaTypeWithQualityHeaderValue("application/json");
            _client.DefaultRequestHeaders.Accept.Add(contentType);
        }

        public async Task<IActionResult> Index()
        {
            url = "https://localhost:7085/api/Product/List";
            List<ProductDto> productDtos = new List<ProductDto>();
            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                productDtos = response.Content.ReadFromJsonAsync<List<ProductDto>>().Result;
            }
            return View(productDtos);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        [HttpGet]
        public async Task<IActionResult> CreateAsync()
        {
            ViewBag.Categories = await GetCategories();
            return View();
        }

        private async Task<List<CategoryDto>> GetCategories()
        {
            url = "https://localhost:7085/api/Product/Categories";
            List<CategoryDto> categoryDtos = new List<CategoryDto>();
            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                categoryDtos = response.Content.ReadFromJsonAsync<List<CategoryDto>>().Result;
            }

            return categoryDtos;
        }

        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] ProductDto model)
        {
            ViewBag.Categories = await GetCategories();
            if (!ModelState.IsValid)
            {
                return View(model);
            }
            url = "https://localhost:7085/api/Product/Create";
            HttpResponseMessage response = await _client.PostAsJsonAsync(url, model);
            var result = response.Content.ReadFromJsonAsync<bool>().Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK && result)
            {
                ViewData["msg"] = "Create Success!";
            }
            else
            {
                ViewData["msg"] = "Create Failed. Try again!";
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> DeleteAsync(int? id)
        {
            var product = await GetByIdAsync(id);
            if (product == null)
            {
                return NotFound();
            }

            url = $"https://localhost:7085/api/Product/Delete/{id}";
            HttpResponseMessage response = await _client.DeleteAsync(url);
            var result = response.Content.ReadFromJsonAsync<bool>().Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK && result)
            {
                TempData["msg"] = "Delete Success!";
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            else

            {
                TempData["msg"] = "Delete Failed. Try again!";
            }

            return RedirectToAction("Index");
        }

        [HttpGet]
        public async Task<IActionResult> UpdateAsync(int? id)
        {
            var product = await GetByIdAsync(id);

            if (product == null)
            {
                return NotFound();
            }

            ViewBag.Categories = await GetCategories();

            return View(product);
        }

        [HttpPost]
        public async Task<IActionResult> UpdateAsync([FromForm] ProductDto model)
        {
            ViewBag.Categories = await GetCategories();

            if (!ModelState.IsValid)
            {
                return View(model);
            }

            url = "https://localhost:7085/api/Product/Update";
            HttpResponseMessage response = await _client.PutAsJsonAsync(url, model);
            var result = response.Content.ReadFromJsonAsync<bool>().Result;
            if (response.StatusCode == System.Net.HttpStatusCode.OK && result)
            {
                ViewData["msg"] = "Update Success!";
            }
            else if (response.StatusCode == System.Net.HttpStatusCode.NotFound)
            {
                return NotFound();
            }
            else
            {
                ViewData["msg"] = "Update Failed. Try again!";
            }

            return View(model);
        }

        private async Task<ProductDto?> GetByIdAsync(int? id)
        {
            if (id == null) return null;

            url = $"https://localhost:7085/api/Product/{id}";
            HttpResponseMessage response = await _client.GetAsync(url);
            if (response.StatusCode == System.Net.HttpStatusCode.OK)
            {
                return response.Content.ReadFromJsonAsync<ProductDto>().Result;
            }

            return null;
        }
    }
}