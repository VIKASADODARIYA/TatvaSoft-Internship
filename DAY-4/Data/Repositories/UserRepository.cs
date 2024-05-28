using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using Test.Data;
using Test.Data.Repositories.IRepository;
using Test.Models;

namespace Test.Data.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly BookDbContext _context;

        public UserRepository(BookDbContext context)
        {
            _context = context;
        }

        public List<User> GetAllUsersInMemory()
        {
            var users = _context.Users.ToList(); // Load data into memory
            return users; // Filter in memory
        }

        public List<User> GetAllUsersFromDatabase()
        {
            var users = _context.Users
                .Include(u => u.Role) // Eager loading of the Role entity
                .ToList();

            return users;
        }

        public User GetUserById(int id)
        {
            return _context.Users.SingleOrDefault(u => u.UserId == id);
        }

        public void AddUser(User user)
        {
            _context.Users.Add(user);
            _context.SaveChanges();
        }

        public List<User> GetUsersOrderedByUsername()
        {
            return _context.Users.OrderBy(u => u.Username).ToList();
        }

        public List<IGrouping<int, User>> GetUsersGroupedByRole()
        {
            return _context.Users.GroupBy(u => u.RoleId).ToList();
        }

        public List<UserRoleDto> GetUsersWithRoles()
        {
            var usersWithRoles = from user in _context.Users
                                 join role in _context.Roles
                                 on user.RoleId equals role.RoleId
                                 select new UserRoleDto
                                 {
                                     Username = user.Username,
                                     RoleName = role.RoleName
                                 };
            return usersWithRoles.ToList();
        }
    }
}
