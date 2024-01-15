using EcommerceProject.Services.ShoppingCartAPI.Models.Dto;

namespace EcommerceProject.Services.ShoppingCartAPI.Service.IService
{
    public interface IProductService
    {
        Task<IEnumerable<ProductDto>> GetProducts();
    }
}
