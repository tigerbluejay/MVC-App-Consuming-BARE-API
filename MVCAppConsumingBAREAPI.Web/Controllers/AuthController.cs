using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using MVCAppConsumingBAREAPI.Models.IdentityDTOs;
using MVCAppConsumingBAREAPI.Models.Models;
using MVCAppConsumingBAREAPI.Utilities;
using MVCAppConsumingBAREAPI.Web.ServiceInterfaces;
using Newtonsoft.Json;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace MVCAppConsumingBAREAPI.Web.Controllers
{
	public class AuthController : Controller
	{
		private readonly IAuthService _authService;

        public AuthController(IAuthService authService)
        {
			_authService = authService;				
        }

		[HttpGet]
        public IActionResult Login()
		{
			LoginRequestDTO loginRequest = new LoginRequestDTO();
			return View(loginRequest);
		}

		[HttpPost]
		public async Task<IActionResult> Login(LoginRequestDTO loginRequest)
		{
			APIResponse apiResponse = await _authService.LoginAsync<APIResponse>(loginRequest);

			if (apiResponse != null && apiResponse.IsSuccess) 
			{
				// deserialize the apiResponse into our loginResponseDTO object
				LoginResponseDTO loginResponse = JsonConvert
					.DeserializeObject<LoginResponseDTO>(Convert.ToString(apiResponse.Result));

				var jwtSecurityTokenHandler = new JwtSecurityTokenHandler();
				var jwtToken = jwtSecurityTokenHandler.ReadJwtToken(loginResponse.Token);

				// configure claims
				var identity = new ClaimsIdentity(CookieAuthenticationDefaults.AuthenticationScheme);
				identity.AddClaim(new Claim(
					ClaimTypes.Name, jwtToken.Claims.FirstOrDefault(x => x.Type == "unique_name").Value));
				identity.AddClaim(new Claim(
					ClaimTypes.Role, jwtToken.Claims.FirstOrDefault(x => x.Type == "role").Value));

				var principal = new ClaimsPrincipal(identity);

				//this will sign in the user and add the claims we have added and configured
				await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, principal);

				//set the session
				HttpContext.Session.SetString(StaticDetails.SessionToken, loginResponse.Token);
				//redirect to home controller, Index Method
				return RedirectToAction("Index", "Home");

			}
			else
			{
				ModelState.AddModelError("CustomError", apiResponse.Errors.FirstOrDefault());
				return View(loginRequest);
			}
		}

		[HttpGet]
		public IActionResult Register()
		{
			return View();
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegistrationRequestDTO registrationRequest)
		{
			APIResponse apiResponse = await _authService.RegisterAsync<APIResponse>(registrationRequest);

			if (apiResponse != null && apiResponse.IsSuccess)
			{
				return RedirectToAction("Login");
			}

			return View();
		}

		public async Task<IActionResult> Logout()
		{
			await HttpContext.SignOutAsync();
			// clear the session
			HttpContext.Session.SetString(StaticDetails.SessionToken, "");
			return RedirectToAction("Index", "Home");
		}

		public IActionResult AccessDenied()
		{
			return View();
		}
	}
}
