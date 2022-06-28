using App.Domain;
using App.Domain.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace App.DAL;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<Amenity> Amenities { get; set; } = default!;
    public DbSet<Apartment> Apartments { get; set; } = default!;
    public DbSet<Bill> Bills { get; set; } = default!;
    public DbSet<Building> Buildings { get; set; } = default!;
    public DbSet<Contract> Contracts { get; set; } = default!;
    public DbSet<Reading> Readings { get; set; } = default!;
    public DbSet<Service> Services { get; set; } = default!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;
    
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
               
        // Remove cascade delete
        foreach (var relationship in builder.Model
                     .GetEntityTypes()
                     .SelectMany(e => e.GetForeignKeys()))
        {
            relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
        
        builder.Entity<Apartment>()
            .HasMany(x => x.Amenities)
            .WithMany(x => x.Apartments)
            .UsingEntity<Dictionary<string, object>>(
                "ApartmentAmenity",
                j => j.HasOne<Amenity>().WithMany().OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<Apartment>().WithMany().OnDelete(DeleteBehavior.Cascade));


    }
}