using Microsoft.EntityFrameworkCore;
using WebReddkaApi.Data.Entities;

namespace WebReddkaApi.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    public DbSet<TopicEntity> Topics { get; set; }
    public DbSet<PostEntity> Posts { get; set; }
}
