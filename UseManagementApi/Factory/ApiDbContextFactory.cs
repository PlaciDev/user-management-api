using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using UseManagementApi.Data;

namespace UseManagementApi.Factory;

public class ApiDbContextFactory : IDesignTimeDbContextFactory<ApiDbContext>
{
    public ApiDbContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.Development.json")
            .Build();
        
        var connectionString = configuration.GetConnectionString("DefaultConnection");
        
        var optionsBuilder = new DbContextOptionsBuilder<ApiDbContext>();
        optionsBuilder.UseSqlServer(connectionString);
        
        return new ApiDbContext(optionsBuilder.Options);
    }
}