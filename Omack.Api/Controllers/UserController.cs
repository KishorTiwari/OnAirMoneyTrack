using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Omack.Api.ViewModels;
using Omack.Api.Filters;
using Microsoft.AspNetCore.Identity;
using Omack.Data.Models;
using System.Net.Http;
using Newtonsoft.Json;
using System.Text;
using Omack.Core.Constants;
using Microsoft.AspNetCore.Http;

namespace Omack.Api.Controllers
{
    [Route("api/user")]
    public class UserController : Controller
    {
        private UserManager<User> _userManager;
        private IHttpContextAccessor _httpContextAccessor;

        public UserController(UserManager<User> userManager, IHttpContextAccessor httpContextAccessor)
        {
            _userManager = userManager;
            _httpContextAccessor = httpContextAccessor;
        }

        [HttpPost]
        [ValidateModel]
        public async Task<IActionResult> UserSignUp([FromBody]UserVM userModel)
        {
            if (await _userManager.FindByEmailAsync(userModel.Email) == null)
            {
                var user = new User()
                {
                    UserName = userModel.UserName,
                    Email = userModel.Email,
                };
                await _userManager.CreateAsync(user, userModel.Password);
                var client = new HttpClient();
                var baseURL = new Uri($"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.ToUriComponent()}");
                client.BaseAddress = baseURL;
                var tokenModel = new TokenVM
                {
                    Email = userModel.Email,
                    Password = userModel.Password
                };
             
                var tokenModelSerialized = JsonConvert.SerializeObject(tokenModel);
                var response =  client.PostAsync("/api/token", new StringContent(tokenModelSerialized, Encoding.UTF8, "application/json"));
                return Ok(response.Result.Content.ReadAsStringAsync().Result);
            }
            else
            {
                return Ok("This email already exists");
            }
        }

        [HttpPost(Name = "MakeGroupAdmin")]
        [ValidateModel]
        public IActionResult MakeGroupAdmin(int userId)
        {
            return null;
        }
    }
}