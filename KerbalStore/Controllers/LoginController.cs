using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using KerbalStore.Data;
using KerbalStore.Data.Entities;
using KerbalStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace KerbalStore.Controllers
{
    [Route("api/Login")]
    public class LoginController : Controller
    {
        private readonly ILoginRepository loginRepository;
        private readonly ILogger<LoginController> logger;

        public LoginController(ILoginRepository loginRepository, ILogger<LoginController> logger)
        {
            this.loginRepository = loginRepository;
            this.logger = logger;
        }

        public IActionResult Post([FromBody]LoginViewModel creds)
        {
            var login = loginRepository.Login(new Login()
            {
                Username = creds.Username,
                Password = creds.Password
            });
            if (login.Token.Length > 0)
            {
                // Login
                return Ok(login);
            }
            logger.LogWarning("Incorrect login crendentials");
            return BadRequest("Incorrect login credentials");
        }
    }
}