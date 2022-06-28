using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace App.DAL;

public class AppDbContextFactory: IDesignTimeDbContextFactory<AppDbContext>
{
    private static string _connectionString =
        "";

    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        optionsBuilder.UseMySql(_connectionString, ServerVersion.AutoDetect(_connectionString));

        return new AppDbContext(optionsBuilder.Options);
    }
}