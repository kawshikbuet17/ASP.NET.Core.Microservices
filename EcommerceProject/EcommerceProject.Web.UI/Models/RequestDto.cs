using Microsoft.AspNetCore.Mvc;
using static EcommerceProject.Web.UI.Utility.SD;

namespace EcommerceProject.Web.UI.Models
{
    public class RequestDto
    {
        public ApiType ApiType { get; set; } = ApiType.GET;
        public string Url { get; set; }
        public object Data { get; set; }
        public string AccessToken { get; set; }
    }
}
