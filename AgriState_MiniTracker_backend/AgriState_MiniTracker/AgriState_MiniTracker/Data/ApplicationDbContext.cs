using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AgriState_MiniTracker.Models;

namespace AgriState_MiniTracker.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

        public DbSet<Location> Locations => Set<Location>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<InventoryItem> InventoryItems => Set<InventoryItem>();
        public DbSet<User> Users => Set<User>();

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // 1. Decimal Precision
            modelBuilder.Entity<InventoryItem>()
                .Property(i => i.Quantity)
                .HasPrecision(18, 2);

            modelBuilder.Entity<InventoryItem>()
                .Property(i => i.MinStockLevel)
                .HasPrecision(18, 2);

            // 2. Cascade Delete Relationships
            modelBuilder.Entity<InventoryItem>()
                .HasOne(i => i.Location)
                .WithMany(l => l.InventoryItems)
                .HasForeignKey(i => i.LocationId)
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<InventoryItem>()
                .HasOne(i => i.Category)
                .WithMany(c => c.InventoryItems)
                .HasForeignKey(c => c.CategoryId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>()
                .HasOne(c => c.Location)
                .WithMany(l => l.Categories)
                .HasForeignKey(c => c.LocationId)
                .OnDelete(DeleteBehavior.Cascade);

            // 3. Primary Keys & Indexes
            modelBuilder.Entity<Location>().HasKey(l => l.LocationId);
            modelBuilder.Entity<Category>().HasKey(c => c.CategoryId);
            modelBuilder.Entity<InventoryItem>().HasKey(i => i.ItemId);
            modelBuilder.Entity<User>().HasKey(u => u.Id);
            modelBuilder.Entity<User>().HasIndex(u => u.Username).IsUnique();

            // Seed Admin and Standard User
            var hasher = new PasswordHasher<User>();
            var adminUser = new User
            {
                Id = 1,
                Username = "admin",
                Email = "admin@agristate.com",
                Role = "Admin",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            };
            adminUser.PasswordHash = hasher.HashPassword(adminUser, "admin123");

            var normalUser = new User
            {
                Id = 2,
                Username = "user",
                Email = "user@agristate.com",
                Role = "User",
                CreatedAt = new DateTime(2026, 1, 1, 0, 0, 0, DateTimeKind.Utc)
            };
            normalUser.PasswordHash = hasher.HashPassword(normalUser, "user123");

            modelBuilder.Entity<User>().HasData(adminUser, normalUser);

            // Seed Locations & Categories
            modelBuilder.Entity<Location>().HasData(
                new Location { LocationId = 1, Name = "Rathnapura", Code = "LOC-RAT", Description = "Main Barn Storage" },
                new Location { LocationId = 2, Name = "Kandy", Code = "LOC-KDY", Description = "Highland Greenhouse" },
                new Location { LocationId = 3, Name = "Kegalle", Code = "LOC-KEG", Description = "Processing Facility" }
            );

            modelBuilder.Entity<Category>().HasData(
                new Category { CategoryId = 101, LocationId = 1, Name = "Fertilizers", Description = "Nutrients and fertilizers" },
                new Category { CategoryId = 102, LocationId = 1, Name = "Tools", Description = "Farm and estate tools" },
                new Category { CategoryId = 103, LocationId = 2, Name = "Seeds", Description = "Crop seeds and seedlings" },
                new Category { CategoryId = 104, LocationId = 2, Name = "Pesticides", Description = "Eco pesticides and fungicides" }
            );

            // Seed Inventory Items
            modelBuilder.Entity<InventoryItem>().HasData(
                new InventoryItem { ItemId = 1, LocationId = 1, CategoryId = 101, Name = "Fertilizer", Quantity = 45, Unit = "Bags", MinStockLevel = 10 },
                new InventoryItem { ItemId = 2, LocationId = 1, CategoryId = 101, Name = "Organic Compost", Quantity = 8, Unit = "Tons", MinStockLevel = 15 },
                new InventoryItem { ItemId = 3, LocationId = 1, CategoryId = 102, Name = "Pruning Shears", Quantity = 24, Unit = "Units", MinStockLevel = 5 },
                new InventoryItem { ItemId = 4, LocationId = 1, CategoryId = 102, Name = "Tractor Oil", Quantity = 3, Unit = "Pcs", MinStockLevel = 5 },
                new InventoryItem { ItemId = 5, LocationId = 2, CategoryId = 103, Name = "Tomato Seeds", Quantity = 120, Unit = "Packets", MinStockLevel = 20 },
                new InventoryItem { ItemId = 6, LocationId = 2, CategoryId = 103, Name = "Nutrient Solution", Quantity = 60, Unit = "Liters", MinStockLevel = 15 },
                new InventoryItem { ItemId = 7, LocationId = 2, CategoryId = 104, Name = "Bio-Neem Eco Pesticide", Quantity = 5, Unit = "Bottles", MinStockLevel = 10 },
                new InventoryItem { ItemId = 8, LocationId = 2, CategoryId = 104, Name = "Fungicide Spray Max", Quantity = 18, Unit = "Bottles", MinStockLevel = 8 }
            );
        }
    }
}