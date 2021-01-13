using System;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Microsoft.AspNetCore.Authorization;

namespace portal
{
    [Route("api/[controller]")]
    public class SeedController : Controller
    {
        private readonly ApplicationDbContext dbContext;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly UserManager<ApplicationUser> _userManager;

        private readonly IWebHostEnvironment environment;
        public SeedController(
            ApplicationDbContext context,
            RoleManager<IdentityRole> roleManager,
            UserManager<ApplicationUser> userManager,
            IWebHostEnvironment env
        )
        {
            this.dbContext = context;
            this._roleManager = roleManager;
            this._userManager = userManager;
            this.environment = env;
        }

        
        [Route("list")]
        [HttpGet]
        public IActionResult ListUsers()
        {
            return Ok(this.dbContext.Users.ToList());
        }

        [Route("createDefaultUsers")]
        [HttpGet]
        public async Task<IActionResult> CreateDefaultUsers()
        {
            string role_RegisteredUser = "RegisteredUser";
            string role_Administrator = "Administrator";
            if (await _roleManager.FindByNameAsync(role_RegisteredUser) == null)
                await _roleManager.CreateAsync(new IdentityRole(role_RegisteredUser));


            if (await _roleManager.FindByNameAsync(role_Administrator) == null)
                await _roleManager.CreateAsync(new IdentityRole(role_Administrator));

            var addedUserList = new List<ApplicationUser>();

            var email_Admin = "admin@geekdunia.com";
            if(await _userManager.FindByNameAsync(email_Admin) == null)
            {
                var user_Admin = new ApplicationUser
                {
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = email_Admin,
                    Email = email_Admin
                };

                await _userManager.CreateAsync(user_Admin, "V@ibhav123");
                await _userManager.AddToRoleAsync(user_Admin, role_RegisteredUser);
                await _userManager.AddToRoleAsync(user_Admin, role_Administrator);
                user_Admin.EmailConfirmed = true;
                user_Admin.LockoutEnabled = false;

                addedUserList.Add(user_Admin);
            }

            var email_User= "vaibhav@geekdunia.com";

            if(await _userManager.FindByNameAsync(email_User) == null)
            {
                var user_User = new ApplicationUser
                {
                    SecurityStamp = Guid.NewGuid().ToString(),
                    UserName = email_User,
                    Email = email_User
                };

                await _userManager.CreateAsync(user_User, "V@ibhav123");
                await _userManager.AddToRoleAsync(user_User, role_RegisteredUser);
                user_User.EmailConfirmed = true;
                user_User.LockoutEnabled = false;
                addedUserList.Add(user_User);

            }

            if(addedUserList.Count >0)
                await dbContext.SaveChangesAsync();

            return new JsonResult(new
            {
                Count = addedUserList.Count,
                Users = addedUserList 
            });

        }


    }
}