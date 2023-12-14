using EcommerceProject.Web.UI.Models;
using EcommerceProject.Web.UI.Service.IService;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

namespace EcommerceProject.Web.UI.Controllers
{
    public class CouponController : Controller
    {
        private readonly ICouponService _couponService;
        public CouponController(ICouponService couponService)
        {
            _couponService = couponService;
        }
        
        public async Task<IActionResult> CouponIndex()
        {
            List<CouponDto>? list = new();
            ResponseDto? response = await _couponService.GetAllCouponsAsync();
            if (response != null && response.IsSuccess == true) 
            {
                list = JsonSerializer.Deserialize<List<CouponDto>>(Convert.ToString(response.Result), options:new JsonSerializerOptions { PropertyNamingPolicy = JsonNamingPolicy.CamelCase });
            }
            return View(list);
        }
    }
}
