using App.Domain;
using App.Domain.Identity;
using Base.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

namespace App.DAL;

public class AppDbContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DbSet<Amenity> Amenities { get; set; } = default!;
    public DbSet<Client> Clients { get; set; } = default!;
    public DbSet<Hotel> Hotels { get; set; } = default!;
    public DbSet<Reservation> Reservations { get; set; } = default!;
    public DbSet<Room> Rooms { get; set; } = default!;
    public DbSet<RoomType> RoomType { get; set; } = default!;
    public DbSet<Section> Section { get; set; } = default!;
    public DbSet<Stay> Stays { get; set; } = default!;
    public DbSet<Ticket> Tickets { get; set; } = default!;
    public DbSet<UserHotel> UserHotels { get; set; } = default!;
    public DbSet<RefreshToken> RefreshTokens { get; set; } = default!;

    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) {}
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
        
        builder.Entity<Hotel>()
            .HasMany(x => x.HotelSections)
            .WithOne(x => x.Hotel!)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Hotel>()
            .HasMany(x => x.RoomTypes)
            .WithOne(x => x.Hotel!)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Hotel>()
            .HasMany(x => x.Amenities)
            .WithOne(x => x.Hotel!)
            .OnDelete(DeleteBehavior.Cascade);

        builder.Entity<Hotel>()
            .HasMany(x => x.Tickets)
            .WithOne(x => x.Hotel!)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Section>()
            .HasMany(x => x.Rooms)
            .WithOne(x => x.Section!)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Room>()
            .HasMany(x => x.Tickets)
            .WithOne(x => x.Room!)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<Room>()
            .HasMany(x => x.Stays)
            .WithOne(x => x.Room!)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<RoomType>()
            .HasMany(x => x.Rooms)
            .WithOne(x => x.RoomType!)
            .OnDelete(DeleteBehavior.Cascade);
        
        builder.Entity<RoomType>()
            .HasMany(x => x.Amenities)
            .WithMany(x => x.RoomTypes)
            .UsingEntity<Dictionary<string, object>>(
                "RoomTypeAmenity",
                j => j.HasOne<Amenity>().WithMany().OnDelete(DeleteBehavior.Cascade),
                j => j.HasOne<RoomType>().WithMany().OnDelete(DeleteBehavior.Cascade));

        builder.Entity<RoomType>()
            .HasMany(x => x.Reservations)
            .WithOne(x => x.RoomType)
            .OnDelete(DeleteBehavior.Cascade);

        // https://stackoverflow.com/a/61243301
        // saving to DB = (localtime -> UTC) and viceversa
        var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
            v => v.ToUniversalTime(),
            v => v.ToLocalTime());

        var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
            v => v.HasValue ? v.Value.ToUniversalTime() : v,
            v => v.HasValue ? v.Value.ToLocalTime() : v);

        foreach (var entityType in builder.Model.GetEntityTypes())
        {
            if (entityType.IsKeyless)
                continue;

            foreach (var property in entityType.GetProperties())
            {
                if (property.ClrType == typeof(DateTime))
                    property.SetValueConverter(dateTimeConverter);
                else if (property.ClrType == typeof(DateTime?))
                    property.SetValueConverter(nullableDateTimeConverter);
            }
        }
        
        // used for testing
        if (Database.ProviderName == "Microsoft.EntityFrameworkCore.InMemory")
        {
            builder
                .Entity<Hotel>()
                .Property(e => e.Name)
                .HasConversion(
                    v => SerialiseLangStr(v),
                    v => DeserializeLangStr(v));
            builder
                .Entity<Hotel>()
                .Property(e => e.Description)
                .HasConversion(
                    v => SerialiseLangStr(v),
                    v => DeserializeLangStr(v));
            builder
                .Entity<Amenity>()
                .Property(e => e.Name)
                .HasConversion(
                    v => SerialiseLangStr(v),
                    v => DeserializeLangStr(v));
            builder
                .Entity<Amenity>()
                .Property(e => e.Description)
                .HasConversion(
                    v => SerialiseLangStr(v),
                    v => DeserializeLangStr(v));
            builder
                .Entity<RoomType>()
                .Property(e => e.Name)
                .HasConversion(
                    v => SerialiseLangStr(v),
                    v => DeserializeLangStr(v));
            builder
                .Entity<RoomType>()
                .Property(e => e.Description)
                .HasConversion(
                    v => SerialiseLangStr(v),
                    v => DeserializeLangStr(v));
        }
    }
    private static string SerialiseLangStr(LangStr lStr) => System.Text.Json.JsonSerializer.Serialize(lStr);

    private static LangStr DeserializeLangStr(string jsonStr) =>
        System.Text.Json.JsonSerializer.Deserialize<LangStr>(jsonStr) ?? new LangStr();

}