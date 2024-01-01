using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.CompilerServices;
using System.Text;

namespace EcommerceProject.Services.CouponAPI.Extensions
{
    public static class WebApplicationBuilderExtensions
    {
        public static  WebApplicationBuilder AddAppAuthentication(this WebApplicationBuilder builder) 
        {
            var secret = builder.Configuration.GetValue<string>("ApiSettings:Secret"); //additional
            var issuer = builder.Configuration.GetValue<string>("ApiSettings:Issuer"); //additional
            var audience = builder.Configuration.GetValue<string>("ApiSettings:Audience"); //additional

            var key = Encoding.ASCII.GetBytes(secret); //additional

            builder.Services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(x =>
            {
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidIssuer = issuer,
                    ValidAudience = audience,
                    ValidateAudience = true
                };
            });

            return builder;
        }
    }
}
