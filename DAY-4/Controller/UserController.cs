using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Test.Data.Repositories;
using Test.Data.Repositories.IRepository;
using Test.Models;
using Test.Service;

namespace Test.Controller
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        [HttpGet("all-in-memory")]
        public ActionResult<List<User>> GetAllUsersInMemory()
        {
            return _userRepository.GetAllUsersInMemory();
        }

        [HttpGet("all-from-database")]
        public ActionResult<List<User>> GetAllUsersFromDatabase()
        {
            return _userRepository.GetAllUsersFromDatabase();
        }

        [HttpGet("{id}")]
        public ActionResult<User> GetUserById(int id)
        {
            var user = _userRepository.GetUserById(id);
            if (user == null)
            {
                return NotFound();
            }
            return user;
        }
       

        [HttpPost]
        public IActionResult AddUser(User user)
        {
            _userRepository.AddUser(user);
            return CreatedAtAction(nameof(GetUserById), new { id = user.UserId }, user);
        }

  

        [HttpGet("ordered-by-username")]
        public ActionResult<List<User>> GetUsersOrderedByUsername()
        {
            return _userRepository.GetUsersOrderedByUsername();
        }

        [HttpGet("grouped-by-role")]
        public ActionResult<List<IGrouping<int, User>>> GetUsersGroupedByRole()
        {
            return _userRepository.GetUsersGroupedByRole();
        }

        [HttpGet("user-with-roles")]
        public ActionResult<List<UserRoleDto>> GetUsersWithRoles()
        {
            return _userRepository.GetUsersWithRoles();
        }
    }
}
