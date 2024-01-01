using EcommerceProject.Web.UI.Models;
using EcommerceProject.Web.UI.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Text.Json;

namespace EcommerceProject.Web.UI.Controllers
{
    public class ProductController : Controller
    {
        private readonly IProductService _productService;
        public ProductController(IProductService productService)
        {
            _productService = productService;
        }
        
        public async Task<IActionResult> ProductIndex()
        {
            List<ProductDto>? list = new();
            ResponseDto? response = await _productService.GetAllProductsAsync();
            if (response != null && response.IsSuccess == true) 
            {
                list = JsonSerializer.Deserialize<List<ProductDto>>(Convert.ToString(response.Result), options:new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return View(list);
        }

        public async Task<IActionResult> ProductCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
            if(ModelState.IsValid)
            {
                ResponseDto? response = await _productService.CreateProductAsync(model);
                if (response != null && response.IsSuccess == true)
                {
                    TempData["success"] = "Product Created Successfully";
                    //return RedirectToAction("Index");
                    return RedirectToAction(nameof(ProductIndex));
                }
                else
                {
                    TempData["error"] = response?.Message;
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ProductDelete(int productId)
        {
			ResponseDto? response = await _productService.GetProductByIdAsync(productId);
			if (response != null && response.IsSuccess == true)
			{
				ProductDto? model = JsonSerializer.Deserialize<ProductDto>(Convert.ToString(response.Result), options: new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
                return View(model);
			}
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }

        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductDto productDto)
        {
            ResponseDto? response = await _productService.DeleteProductAsync(productDto.ProductId);
            if (response != null && response.IsSuccess == true)
            {
                TempData["success"] = "Product Deleted Successfully";
                return RedirectToAction(nameof(ProductIndex));
            }
            else
            {
                TempData["error"] = response?.Message;
            }
            return NotFound();
        }

		public async Task<IActionResult> ProductEdit(int productId)
		{
			ResponseDto? response = await _productService.GetProductByIdAsync(productId);
			if (response != null && response.IsSuccess == true)
			{
				ProductDto? model = JsonSerializer.Deserialize<ProductDto>(Convert.ToString(response.Result), options: new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
				return View(model);
			}
			else
			{
				TempData["error"] = response?.Message;
			}
			return NotFound();
		}

		[HttpPost]
		public async Task<IActionResult> ProductEdit(ProductDto productDto)
		{
			ResponseDto? response = await _productService.UpdateProductAsync(productDto);
			if (response != null && response.IsSuccess == true)
			{
				TempData["success"] = "Product Updated Successfully";
				return RedirectToAction(nameof(ProductIndex));
			}
			else
			{
				TempData["error"] = response?.Message;
			}
			return NotFound();
		}
	}
}
