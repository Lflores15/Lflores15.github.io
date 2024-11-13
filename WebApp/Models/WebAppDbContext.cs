using Microsoft.EntityFrameworkCore;
using WebApp.Models;

public class WebAppDbContext : DbContext
{
    public WebAppDbContext(DbContextOptions<WebAppDbContext> options) : base(options) { }

    public DbSet<User> Users { get; set; }
    public DbSet<Country> Countries { get; set; }
}
