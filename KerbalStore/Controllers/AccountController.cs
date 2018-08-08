using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KerbalStore.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace KerbalStore.Controllers
{
    public class AccountController : Controller
    {
        private readonly ILogger<AccountController> logger;
        private readonly Microsoft.AspNetCore.Identity.SignInManager<ShopUser> signInManager;

        public AccountController(ILogger<AccountController> logger, SignInManager<ShopUser> signInManager)
        {
            this.logger = logger;
            this.signInManager = signInManager;
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
    }
}
