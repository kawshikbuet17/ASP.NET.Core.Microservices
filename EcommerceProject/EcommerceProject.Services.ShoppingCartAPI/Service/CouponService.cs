using EcommerceProject.Services.ShoppingCartAPI.Models.Dto;
using EcommerceProject.Services.ShoppingCartAPI.Service.IService;
using System.Text.Json;

namespace EcommerceProject.Services.ShoppingCartAPI.Service
{
    public class CouponService : ICouponService
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public CouponService(IHttpClientFactory httpClientFactory)
        {
            _httpClientFactory = httpClientFactory;
        }
        public async Task<CouponDto> GetCoupon(string couponCode)
        {
            var client = _httpClientFactory.CreateClient("Coupon");
            var response = await client.GetAsync($"/api/coupon/GetByCode/{couponCode}");
            var apiContent = await response.Content.ReadAsStringAsync();
            var resp = JsonSerializer.Deserialize<ResponseDto>(apiContent, options: new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });

            if (resp != null && resp.IsSuccess == true)
            {
                return JsonSerializer.Deserialize<CouponDto>(Convert.ToString(resp.Result), options: new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            return new CouponDto();
        }
    }
}
