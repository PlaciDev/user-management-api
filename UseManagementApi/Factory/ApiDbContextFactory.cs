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
            .AddJsonFile("appsettings.json")
            .Build();

        var optionsBuilder = new DbContextOptionsBuilder<ApiDbContext>();
        
        optionsBuilder.UseSqlServer(configuration.GetConnectionString("DefaultConnection"));
        
        return new ApiDbContext(optionsBuilder.Options);
        
    }
}