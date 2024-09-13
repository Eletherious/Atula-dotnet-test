using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using AtulaTestWebsite.Models.Modles;

namespace AtulaTestWebsite.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            SeedAdminUser(modelBuilder);

            modelBuilder.Entity<ProductCategory>()
                .HasKey(pc => new { pc.ProductId, pc.CategoryId });

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Product)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.ProductId);

            modelBuilder.Entity<ProductCategory>()
                .HasOne(pc => pc.Category)
                .WithMany(p => p.ProductCategories)
                .HasForeignKey(pc => pc.CategoryId);

            modelBuilder.Entity<Category>().HasData(
                new Category { Id = 1, Name = "Table" },
                new Category { Id = 2, Name = "Chair" },
                new Category { Id = 3, Name = "Sofa" }
            );

            modelBuilder.Entity<Product>().HasData(
                new Product { Id = 1, Sku = "SKUA", Name = "Lorem Table" },
                new Product { Id = 2, Sku = "SKUB", Name = "Ipsum Table" },
                new Product { Id = 3, Sku = "SKUC", Name = "Dolor Table" },
                new Product { Id = 4, Sku = "SKUD", Name = "Sit Chair" },
                new Product { Id = 5, Sku = "SKUE", Name = "Amet Chair" },
                new Product { Id = 6, Sku = "SKUF", Name = "Consectetur Chair" },
                new Product { Id = 7, Sku = "SKUG", Name = "Adipiscing Sofa" },
                new Product { Id = 8, Sku = "SKUH", Name = "Elit Sofa" },
                new Product { Id = 9, Sku = "SKUI", Name = "Mauris Sofa" }
            );

            modelBuilder.Entity<ProductCategory>().HasData(
                new ProductCategory { ProductId = 1, CategoryId = 1 },
                new ProductCategory { ProductId = 2, CategoryId = 1 },
                new ProductCategory { ProductId = 3, CategoryId = 1 },
                new ProductCategory { ProductId = 4, CategoryId = 2 },
                new ProductCategory { ProductId = 5, CategoryId = 2 },
                new ProductCategory { ProductId = 6, CategoryId = 2 },
                new ProductCategory { ProductId = 7, CategoryId = 3 },
                new ProductCategory { ProductId = 8, CategoryId = 3 },
                new ProductCategory { ProductId = 9, CategoryId = 3 }
            );
        }

        private void SeedAdminUser(ModelBuilder modelBuilder)
        {
            string adminRoleId = Guid.NewGuid().ToString();
            string adminUserId = Guid.NewGuid().ToString();
            var hasher = new PasswordHasher<ApplicationUser>();

            modelBuilder.Entity<IdentityRole>().HasData(
                new IdentityRole
                {
                    Id = adminRoleId,
                    Name = "Admin",
                    NormalizedName = "ADMIN"
                });

            modelBuilder.Entity<ApplicationUser>().HasData(
                new ApplicationUser
                {
                    Id = adminUserId,
                    UserName = "KiTheCode",
                    NormalizedUserName = "KITHECODE",
                    Email = "KiLetchford@Outlook.com",
                    NormalizedEmail = "KILETCHFORD@OUTLOOK.COM",
                    EmailConfirmed = true,
                    PasswordHash = hasher.HashPassword(null, "KiLikesCats!"),
                    SecurityStamp = string.Empty,
                }); 

            modelBuilder.Entity<IdentityUserRole<string>>().HasData(
                new IdentityUserRole<string>
                {
                    RoleId = adminRoleId,
                    UserId = adminUserId
                });
        }
    }
}
