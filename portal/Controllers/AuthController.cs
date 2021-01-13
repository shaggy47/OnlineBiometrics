using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Security.Claims;

namespace portal
{
    [AllowAnonymous]
    [Route("api/[controller]")]
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        private readonly TokenGenerator _tokenGenerator;
        private readonly IConfiguration _config;

        public AuthController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            TokenGenerator tokenGenerator,
            IConfiguration config
            )
        {
            this.dbContext = context;
            this._roleManager = roleManager;
            this._userManager = userManager;
            this._tokenGenerator = tokenGenerator;
            this._config = config;
        }

        [Route("signin")]
        [HttpPost]
        public IActionResult SignIn([FromBody] ApplicationUser user)
        {
            var dbUser = _userManager.FindByNameAsync(user.UserName).Result;
            
            if (dbUser == null)
                return Unauthorized();

            var role = _userManager.GetRolesAsync(dbUser).Result;
            string token = string.Empty;

            if (dbUser != null)
            {
                var claims = new List<Claim>()
                {
                    new Claim(ClaimTypes.Name, dbUser.UserName)
                };

                token = _tokenGenerator.GenerateJwtToken(dbUser, claims);
                return Ok(new { user = dbUser.UserName, token = token });
            }
            else
                return Unauthorized();

        }
    }
}