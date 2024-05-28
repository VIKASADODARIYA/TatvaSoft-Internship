using DataLayer.Entities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using ServiceLayer;

namespace BookAPI.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly LoginService _loginService;

        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }

        [HttpPost]
        public IActionResult RegisterUser(User user)
        {
            _loginService.AddUser(user);
            return Ok();
        }

        [HttpPost]
        public IActionResult Login(string email , string password)
        {
            var result = _loginService.GetUserByEmailAndPassword(email, password);
            if (result != "")
            {
                return Ok(result);
            }
            return BadRequest("Please check your email and passoword.");
        }
    }
}
