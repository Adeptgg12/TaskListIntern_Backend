using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using TaskList.Infrastructure.Models;

namespace TaskList.Infrastructure;

public class TaskListDbContext : DbContext
{
    // Constructor with corrected syntax
    public TaskListDbContext(DbContextOptions<TaskListDbContext> options) : base(options)
    {
    }

    // Define DbSet for UserModel
    public DbSet<UserModel> UserTb { get; set; }
    public DbSet<TaskListModel> TaskListTb { get; set; }
}

public class TaskListDbContextDesignFactory : IDesignTimeDbContextFactory<TaskListDbContext>
{
    public TaskListDbContext CreateDbContext(string[] args)
    {
        // Define connection string
        string connectionString = "Server=localhost\\SQLEXPRESS;Database=TaskList;Trusted_Connection=True;TrustServerCertificate=true;";

        // Set options for DbContext with SQL Server provider
        var optionsBuilder = new DbContextOptionsBuilder<TaskListDbContext>()
            .UseSqlServer(connectionString, opts => opts.CommandTimeout(600));

        // Return new instance of TaskListDbContext
        return new TaskListDbContext(optionsBuilder.Options);
    }
}