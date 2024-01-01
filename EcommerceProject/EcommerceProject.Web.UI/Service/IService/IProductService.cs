﻿using EcommerceProject.Web.UI.Models;

namespace EcommerceProject.Web.UI.Service.IService
{
    public interface IProductService
    {
        Task<ResponseDto?> GetProductAsync(string ProductCode);
        Task<ResponseDto?> GetAllProductsAsync();
        Task<ResponseDto?> GetProductByIdAsync(int id);
        Task<ResponseDto?> CreateProductAsync(ProductDto productDto);
        Task<ResponseDto?> UpdateProductAsync(ProductDto productDto);
        Task<ResponseDto?> DeleteProductAsync(int id);
    }
}
