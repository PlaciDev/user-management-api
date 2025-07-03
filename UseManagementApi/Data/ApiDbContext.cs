using Microsoft.EntityFrameworkCore;
using UseManagementApi.Models;

namespace UseManagementApi.Data;

public class ApiDbContext : DbContext
{
    public ApiDbContext(DbContextOptions<ApiDbContext> options) : base(options) {}
    
    public DbSet<User> Users { get; set; }
    public DbSet<Role> Roles { get; set; }
    
    
}