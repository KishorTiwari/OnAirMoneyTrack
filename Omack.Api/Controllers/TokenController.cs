using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Omack.Api.ViewModels;
using Microsoft.AspNetCore.Identity;
using Omack.Data.Models;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace Omack.Api.Controllers
{
    [Route("api/token")]
    public class TokenController : Controller
    {
        private UserManager<User> _userManager;
        private IPasswordHasher<User> _hasher;
        private IConfigurationRoot _config;

        public TokenController(UserManager<User> userManager, IPasswordHasher<User> hasher, IConfigurationRoot config)
        {
            _userManager = userManager;
            _hasher = hasher;
            _config = config;
        }

        [HttpPost]
        public async Task<IActionResult> GenerateToken([FromBody] TokenViewModel model)
        {
            try
            {
                var user = await _userManager.FindByEmailAsync(model.Email);
                if (user != null)
                {
                    if (_hasher.VerifyHashedPassword(user, user.PasswordHash, model.Password) == PasswordVerificationResult.Success)
                    {
                        var claims = new[]
                        {
                            new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                            new Claim(JwtRegisteredClaimNames.Email, user.Email)
                        };
                        var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config["Tokens:Key"]));
                        var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);
                        var token = new JwtSecurityToken(
                            issuer: _config["Tokens:Issuer"],
                            audience: _config["Tokens:Audience"],
                            claims: claims,
                            expires: DateTime.UtcNow.AddDays(30),
                            signingCredentials: creds
                            );
                        return Ok(
                            new
                            {
                                token = new JwtSecurityTokenHandler().WriteToken(token),
                                expiration = token.ValidTo
                            });
                    }
                }
                return BadRequest("");
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}