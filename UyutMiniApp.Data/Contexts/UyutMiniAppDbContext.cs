using Microsoft.EntityFrameworkCore;
using UyutMiniApp.Domain.Entities;

namespace UyutMiniApp.Data.Contexts
{
    public class UyutMiniAppDbContext : DbContext
    {
        public UyutMiniAppDbContext(DbContextOptions<UyutMiniAppDbContext> options)
        : base(options) 
        {
            Users.IgnoreAutoIncludes();
            Couriers.IgnoreAutoIncludes();
            Categories.IgnoreAutoIncludes();
            MenuItems.IgnoreAutoIncludes();
            SetItems.IgnoreAutoIncludes();
            SetItemReplacementOptions.IgnoreAutoIncludes();
            Ingredients.IgnoreAutoIncludes();
            CustomMeals.IgnoreAutoIncludes();
            CustomMealIngredients.IgnoreAutoIncludes();
            Orders.IgnoreAutoIncludes();
            DeliveryInfos.IgnoreAutoIncludes();
            OrderItems.IgnoreAutoIncludes();
            SetReplacementSelections.IgnoreAutoIncludes();
            IngredientRecommendations.IgnoreAutoIncludes();
            SavedAddresses.IgnoreAutoIncludes();
        }

        public DbSet<User> Users { get; set; }
        public DbSet<Courier> Couriers { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<MenuItem> MenuItems { get; set; }
        public DbSet<SetItem> SetItems { get; set; }
        public DbSet<SetItemReplacementOption> SetItemReplacementOptions { get; set; }
        public DbSet<Ingredient> Ingredients { get; set; }
        public DbSet<CustomMeal> CustomMeals { get; set; }
        public DbSet<CustomMealIngredient> CustomMealIngredients { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<DeliveryInfo> DeliveryInfos { get; set; }
        public DbSet<OrderItem> OrderItems { get; set; }
        public DbSet<SetReplacementSelection> SetReplacementSelections { get; set; }
        public DbSet<IngredientRecommendation> IngredientRecommendations { get; set; }
        public DbSet<SavedAddress> SavedAddresses { get; set; }


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Unique Telegram ID
            modelBuilder.Entity<User>()
                .HasIndex(u => u.TelegramUserId)
                .IsUnique();

            // Optional: restrict deletes where needed (to prevent cascade issues)

            modelBuilder.Entity<SetItem>()
                .HasOne(si => si.MenuItem)
                .WithMany(s => s.SetItems)
                .OnDelete(DeleteBehavior.Restrict);
            
            modelBuilder.Entity<SetItem>()
                .HasOne(s => s.IncludedItem)
                .WithMany();

            modelBuilder.Entity<SetItemReplacementOption>()
                .HasOne(r => r.ReplacementMenuItem)
                .WithMany()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<SetItemReplacementOption>().
                HasOne(r => r.SetItem)
                .WithMany(s => s.ReplacementOptions)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Order>()
                .HasOne(o => o.Courier)
                .WithMany()
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<OrderItem>()
                .HasOne(i => i.MenuItem)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<OrderItem>()
                .HasOne(i => i.CustomMeal)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SetReplacementSelection>()
                .HasOne(s => s.OriginalSetItem)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);

            modelBuilder.Entity<SetReplacementSelection>()
                .HasOne(s => s.ReplacementMenuItem)
                .WithMany()
                .OnDelete(DeleteBehavior.Restrict);
            modelBuilder.Entity<Category>()
                .HasMany(s => s.MenuItems)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<Category>()
                .HasMany(s => s.Ingredients)
                .WithOne()
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}
