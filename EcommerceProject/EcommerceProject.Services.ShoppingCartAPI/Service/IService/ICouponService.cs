using EcommerceProject.Services.ShoppingCartAPI.Models.Dto;

namespace EcommerceProject.Services.ShoppingCartAPI.Service.IService
{
    public interface ICouponService
    {
        Task<CouponDto> GetCoupon(string couponCode);
    }
}
