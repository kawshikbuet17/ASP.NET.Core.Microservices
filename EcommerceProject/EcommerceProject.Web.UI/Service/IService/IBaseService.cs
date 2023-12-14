using EcommerceProject.Web.UI.Models;

namespace EcommerceProject.Web.UI.Service.IService
{
    public interface IBaseService
    {
        Task<ResponseDto?> SendAsync(RequestDto requestDto);
    }
}
