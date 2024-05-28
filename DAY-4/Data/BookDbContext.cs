using Microsoft.EntityFrameworkCore;
using Test.Models;

namespace Test.Data
{
    public class BookDbContext : DbContext
    {
        public BookDbContext(DbContextOptions<BookDbContext> options) : base(options) { }

        public DbSet<Book> Books { get; set; }
        public DbSet<User> Users { get; set; } // Add this DbSet for User entity
        public DbSet<Role> Roles { get; set; } // Add this DbSet for Role entity

        // Override OnModelCreating if you need to configure relationships or entities
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure relationships or entities here if needed
            base.OnModelCreating(modelBuilder);
        }
    }
}
