using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Authorization;

namespace portal
{
    [Authorize(Roles = "RegisteredUser")]
    [Route("api/[controller]")]
    public class DocumentsController : Controller
    {
        private readonly ApplicationDbContext _dbContext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        public DocumentsController(UserManager<ApplicationUser> userManager,
        ApplicationDbContext dbContext,
        RoleManager<IdentityRole> roleManager)
        {
            this._dbContext = dbContext;
            this._roleManager = roleManager;
            this._userManager = userManager;
        }

        [Route("getlist")]
        [HttpGet]
        public IActionResult GetList()
        {
            return Ok(this._dbContext.Users.ToListAsync().Result);
        }


    }
}