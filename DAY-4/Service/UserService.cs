using System.Linq;
using Microsoft.EntityFrameworkCore;
using Test.Data;
using Test.Models;

// Example usage
public class UserService
{
    private readonly BookDbContext _context;

    public UserService(BookDbContext context)
    {
        _context = context;
    }

    public async Task<List<User>> GetUsersWithRolesAsync()
    {
        return await _context.Users.Include(u => u.Role).ToListAsync();
    }
}
