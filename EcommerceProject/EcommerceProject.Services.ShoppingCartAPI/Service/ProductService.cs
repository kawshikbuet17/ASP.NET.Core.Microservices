using EcommerceProject.Services.ShoppingCartAPI.Models.Dto;
using EcommerceProject.Services.ShoppingCartAPI.Service.IService;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EcommerceProject.Services.ShoppingCartAPI.Service
{
    public class ProductService : IProductService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public ProductService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            var client = _httpClientFactory.CreateClient("Product");
            var response = await client.GetAsync($"/api/product");
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonSerializer.Deserialize<ResponseDto>(apiContent, options:new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase});

            if(resp.IsSuccess == true)
            {
                return JsonSerializer.Deserialize<IEnumerable<ProductDto>>(Convert.ToString(resp.Result), options: new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase});
            }
            return new List<ProductDto>();
        }
    }
}
