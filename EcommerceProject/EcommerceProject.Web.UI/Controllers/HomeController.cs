using EcommerceProject.Web.UI.Models;
using EcommerceProject.Web.UI.Service.IService;
using EcommerceProject.Web.UI.Utility;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.Json;

namespace EcommerceProject.Web.UI.Controllers
{
    public class HomeController : Controller
    {
        private readonly IProductService _productService;
        public HomeController(IProductService productService)
        {
            _productService = productService;
        }

        public async Task<IActionResult> Index()
        {
            List<ProductDto>? list = new();
            ResponseDto? response = await _productService.GetAllProductsAsync();
            if (response != null && response.IsSuccess == true)
            {
                list = JsonSerializer.Deserialize<List<ProductDto>>(Convert.ToString(response.Result), options: new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(list);
        }

        [Authorize]
        public async Task<IActionResult> ProductDetails(int productId)
        {
            ProductDto? model = new();
            ResponseDto? response = await _productService.GetProductByIdAsync(productId);
            if (response != null && response.IsSuccess == true)
            {
                model = JsonSerializer.Deserialize<ProductDto>(Convert.ToString(response.Result), options: new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(model);
        }

        [Authorize(Roles =SD.RoleAdmin)]
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}