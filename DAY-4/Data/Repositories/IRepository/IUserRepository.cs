using Microsoft.AspNetCore.Mvc;
using Test.Models;

namespace Test.Data.Repositories.IRepository
{
    public interface IUserRepository 
    {
        List<User> GetAllUsersInMemory();
        List<User> GetAllUsersFromDatabase();
        User GetUserById(int id);
   
        void AddUser(User user);
      
        List<User> GetUsersOrderedByUsername();
        List<IGrouping<int, User>> GetUsersGroupedByRole();
        List<UserRoleDto> GetUsersWithRoles();
    }
}
