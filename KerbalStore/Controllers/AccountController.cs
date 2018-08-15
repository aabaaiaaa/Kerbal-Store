using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using KerbalStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KerbalStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> logger;
        private readonly SignInManager<ShopUser> signInManager;
        private readonly UserManager<ShopUser> userManager;
        private readonly IConfiguration configuration;

        public AccountController(ILogger<AccountController> logger, SignInManager<ShopUser> signInManager, UserManager<ShopUser> userManager, IConfiguration configuration)
        {
            this.logger = logger;
            this.signInManager = signInManager;
            this.userManager = userManager;
            this.configuration = configuration;
        }

        [HttpGet]
        public IActionResult Login()
        {
            // If user is already authenticated then don't show the login screen
            if (User.Identity.IsAuthenticated)
            {
                return RedirectionStrategy();
            }
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return View();
            }

            var result = await signInManager.PasswordSignInAsync(model.Username, model.Password, model.RememberMe, false);
            if (!result.Succeeded)
            {
                logger.LogWarning("Incorrect username or password");
                ModelState.AddModelError("", "Incorrect username or password");
                return View();
            }

            return RedirectionStrategy();
        }

        public async Task<IActionResult> Logout()
        {
            await signInManager.SignOutAsync();
            return RedirectToAction("Index", "StorePage");
        }

        private IActionResult RedirectionStrategy()
        {
            if (Request.Query.Keys.Contains("ReturnUrl"))
            {
                // Go to ReturnUrl if set
                return Redirect(Request.Query["ReturnUrl"].First());
            }
            else
            {
                // Default to index page
                return RedirectToAction("Index", "StorePage");
            }
        }

        [HttpPost]
        public async Task<IActionResult> CreateToken([FromBody] LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await userManager.FindByNameAsync(model.Username);
                if (user != null)
                {
                    var result = await signInManager.CheckPasswordSignInAsync(user, model.Password, false);
                    if (result.Succeeded)
                    {
                        // Create JWT bearer token
                        var claims = new[] {
                            new Claim(JwtRegisteredClaimNames.Sub, user.Email),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.UniqueName, user.UserName)
                        };

                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(configuration["Token:key"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

                        var token = new JwtSecurityToken(
                            configuration["Token:Issuer"],
                            configuration["Token:Audience"],
                            claims,
                            expires: DateTime.Now.AddMinutes(10),
                            signingCredentials: creds
                            );

                        var results = new
                        {
                            token = new JwtSecurityTokenHandler().WriteToken(token),
                            expiration = token.ValidTo
                        };

                        return Created("", results);
                    }
                }
            }

            return BadRequest();
        }
    }
}
