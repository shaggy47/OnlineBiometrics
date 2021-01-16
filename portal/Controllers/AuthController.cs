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
            string hashedPassword = string.Empty;

            if (dbUser != null)
            {
                hashedPassword = _userManager.PasswordHasher.HashPassword(dbUser, user.PasswordHash);

                PasswordVerificationResult result = _userManager.PasswordHasher.VerifyHashedPassword(dbUser, hashedPassword, user.PasswordHash);
                if (result == PasswordVerificationResult.Success || result == PasswordVerificationResult.SuccessRehashNeeded)
                {
                    var role = _userManager.GetRolesAsync(dbUser).Result;
                    string[] roles = new string[role.Count];
                    role.CopyTo(roles, 0);
                    string token = string.Empty;
                    var claims = new List<Claim>()
                    {
                        new Claim(ClaimTypes.Name, dbUser.UserName),
                        new Claim(ClaimTypes.Role, string.Join(';', roles))
                    };

                    token = _tokenGenerator.GenerateJwtToken(dbUser, claims);

                    return Ok(new { user = dbUser.UserName, token = token });
                }
                else
                    return Unauthorized();
            }
            else
                return Unauthorized();

        }
    }
}