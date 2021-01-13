using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore;
using Microsoft.IdentityModel.JsonWebTokens;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using System.Linq;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Builder;

namespace portal
{
    // public class JwtMiddlewareExtension
    // {
    //     public static IApplicationBuilder UseJwtMiddleware(this IApplicationBuilder builder)
    //     {
    //         return builder.UseMiddleware()
    //     }
    // }
    public class TokenHandlerMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly IConfiguration _config;

        public TokenHandlerMiddleware(RequestDelegate next, IConfiguration configuration)
        {
            _next = next;
            _config = configuration;
        }

        public async Task Invoke(HttpContext context, UserManager<ApplicationUser> userManager)
        {
            var token = context.Request.Headers["Authorization"].FirstOrDefault()?.Split(" ").Last();
            if (token != null)
                addToken(context, userManager, token);

            await _next(context);
        }

        private void addToken(HttpContext context, UserManager<ApplicationUser> userManager, string token)
        {
            try
            {
                var tokenHandler = new JwtSecurityTokenHandler();
                var key = Encoding.ASCII.GetBytes(_config["Auth:Secret"]);
                tokenHandler.ValidateToken(token, new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key),
                    ValidateIssuer = true,
                    ValidateActor = true,
                    ClockSkew = TimeSpan.Zero

                }, out SecurityToken validatedToken);

                var jwtToken = (JwtSecurityToken)validatedToken;
                var user = jwtToken.Claims.First(v => v.Type == "id").Value;

                context.Items["User"] = userManager.FindByIdAsync(user).Result;

            }
            catch (System.Exception)
            {

                throw;
            }
        }
    }
}