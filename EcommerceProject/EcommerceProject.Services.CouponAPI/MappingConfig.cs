using AutoMapper;
using EcommerceProject.Services.CouponAPI.Models;
using EcommerceProject.Services.CouponAPI.Models.Dto;

namespace EcommerceProject.Services.CouponAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CouponDto, Coupon>();
                config.CreateMap<Coupon, CouponDto>();
            });
            return mappingConfig;
        }
    }
}
