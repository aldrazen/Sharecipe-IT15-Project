using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;
using Sharecipe.Models;
using Sharecipe_IT15_Project.Areas.Identity.Data;

namespace Sharecipe_IT15_Project.Areas.Identity.Data;

public class SharecipeIdentityDBContext : IdentityDbContext<SharecipeUser>
{
    public SharecipeIdentityDBContext(DbContextOptions<SharecipeIdentityDBContext> options)
        : base(options)
    {
    }

    public DbSet<SharecipeUser> User {  get; set; }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.Entity<Post>()
            .HasOne(p => p.User)
            .WithMany(u => u.UserPost)
            .HasForeignKey(p => p.postUserId);
    }
    public DbSet<Post> Posts { get; set; }
}
