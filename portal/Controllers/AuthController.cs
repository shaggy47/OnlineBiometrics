using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace portal
{
    [AllowAnonymous]
    [Route("api/auth")]
    public class AuthController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AuthController(ApplicationDbContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager
            )
        {
            this.dbContext = context;
            this._roleManager = roleManager;
            this._userManager = userManager;
        }

        [Route("signin")]
        public IActionResult SignIn([FromBody] string userName, [FromBody] string password)
        {
            return Ok();
        }
    }
}