using System.Text.Json.Serialization;

namespace EcommerceProject.Web.UI.Models
{
    public class CouponDto
    {
        //[JsonPropertyName("couponId")]
        public int CouponId { get; set; }
        //[JsonPropertyName("couponCode")]
        public string CouponCode { get; set; }
        //[JsonPropertyName("discountAmount")]
        public double DiscountAmount { get; set; }
        //[JsonPropertyName("minAmount")]
        public int MinAmount { get; set; }
    }
}
