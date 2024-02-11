using LoginApi.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

public class AppDbContext : IdentityDbContext<ApplicationUser>
{
    public DbSet<User> Users { get; set; }
    public DbSet<CookInfo> CookInfos { get; set; }
    public DbSet<Favourite> Favourites { get; set; }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Favourite>()
            .HasOne(f => f.User)
            .WithMany(u => u.Favourites)
            .HasForeignKey(f => f.UserId)
            .OnDelete(DeleteBehavior.NoAction); // Specify the desired behavior

        modelBuilder.Entity<Favourite>()
            .HasOne(f => f.CookInfo)
            .WithMany(c => c.Favourites)
            .HasForeignKey(f => f.CookInfoId)
            .OnDelete(DeleteBehavior.NoAction); // Specify the desired behavior
        modelBuilder.Entity<OrderRequest>()
        .HasOne(or => or.User)
        .WithMany(u => u.OrderRequests)
        .HasForeignKey(or => or.UserId)
        .OnDelete(DeleteBehavior.NoAction); // Specify the desired behavior

        modelBuilder.Entity<OrderRequest>()
            .HasOne(or => or.CookInfo)
            .WithMany(c => c.OrderRequests)
            .HasForeignKey(or => or.CookInfoId)
            .OnDelete(DeleteBehavior.NoAction); // Specify the desired behavior

    }
    public DbSet<LoginApi.Models.OrderRequest>? OrderRequest { get; set; }
    public DbSet<LoginApi.Models.Order>? Order { get; set; }

}
