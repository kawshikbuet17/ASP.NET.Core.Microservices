using EcommerceProject.Web.UI.Models;
using EcommerceProject.Web.UI.Service.IService;
using EcommerceProject.Web.UI.Utility;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text.Json;
using Microsoft.AspNetCore.Authentication.Cookies;

namespace EcommerceProject.Web.UI.Controllers
{
    public class AuthController : Controller
    {
        private readonly IAuthService _authService;
        private readonly ITokenProvider _tokenProvider;
        public AuthController(IAuthService authService, ITokenProvider tokenProvider)
        {
            _authService = authService;
            _tokenProvider = tokenProvider;
        }

        [HttpGet]
        public IActionResult Login()
        {
            LoginRequestDto loginRequestDto = new LoginRequestDto();
            return View(loginRequestDto);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginRequestDto obj)
        {
            ResponseDto responseDto = await _authService.LoginAsync(obj);

            if (responseDto != null && responseDto.IsSuccess == true)
            {
                LoginResponseDto loginResponseDto = JsonSerializer.Deserialize<LoginResponseDto>(Convert.ToString(responseDto.Result), options: new JsonSerializerOptions 
                { PropertyNamingPolicy = JsonNamingPolicy.CamelCase});

                await SignInUser(loginResponseDto);
                _tokenProvider.SetToken(loginResponseDto.Token);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ModelState.AddModelError("CustomError", responseDto.Message);
                return View(obj);
            }
        }

        [HttpGet]
        public IActionResult Register()
        {
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=SD.RoleAdmin, Value=SD.RoleAdmin},
                new SelectListItem{Text=SD.RoleCustomer, Value=SD.RoleCustomer}
            };
            ViewBag.RoleList = roleList;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegistrationRequestDto obj)
        {
            ResponseDto result = await _authService.RegisterAsync(obj);
            ResponseDto assingRole;

            if(result != null && result.IsSuccess == true)
            {
                if (string.IsNullOrEmpty(obj.Role))
                {
                    obj.Role = SD.RoleCustomer;
                }

                assingRole = await _authService.AssignRoleAsync(obj);
                if(assingRole != null && assingRole.IsSuccess == true) 
                {
                    TempData["success"] = "Registration Successful";
                    return RedirectToAction(nameof(Login));
                }
            }
            
            var roleList = new List<SelectListItem>()
            {
                new SelectListItem{Text=SD.RoleAdmin, Value=SD.RoleAdmin},
                new SelectListItem{Text=SD.RoleCustomer, Value=SD.RoleCustomer}
            };
            ViewBag.RoleList = roleList;
            return View();
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync();
            _tokenProvider.ClearToken();
            return RedirectToAction("Index", "Home");
        }

        private async Task SignInUser(LoginResponseDto model)
        {
            var handler = new JwtSecurityTokenHandler();
            var jwt = handler.ReadJwtToken(model.Token);

            var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
            identity.AddClaim(
                new Claim(
                    JwtRegisteredClaimNames.Email, 
                    jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Email).Value
                    )
                );
            identity.AddClaim(
                new Claim(JwtRegisteredClaimNames.Sub, 
                    jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Sub).Value
                    )
                );
            identity.AddClaim(
                new Claim(
                    JwtRegisteredClaimNames.Name, 
                    jwt.Claims.FirstOrDefault(u => u.Type == JwtRegisteredClaimNames.Name).Value
                    )
                );

            var principal = new ClaimsPrincipal(identity);
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);
        }
    }
}
