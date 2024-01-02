using AutoMapper;
using EcommerceProject.Services.ShoppingCartAPI.Models;
using EcommerceProject.Services.ShoppingCartAPI.Models.Dto;

namespace EcommerceProject.Services.ShoppingCartAPI
{
    public class MappingConfig
    {
        public static MapperConfiguration RegisterMaps()
        {
            var mappingConfig = new MapperConfiguration(config =>
            {
                config.CreateMap<CartHeader, CartHeaderDto>().ReverseMap();
                config.CreateMap<CartDetails, CartDetailsDto>().ReverseMap();
            });
            return mappingConfig;
        }
    }
}
