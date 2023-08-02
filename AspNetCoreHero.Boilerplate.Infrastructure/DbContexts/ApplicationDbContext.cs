using AspNetCoreHero.Abstractions.Domain;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Contexts;
using AspNetCoreHero.Boilerplate.Application.Interfaces.Shared;
using AspNetCoreHero.Boilerplate.Domain.Entities.Catalog;
using AspNetCoreHero.EntityFrameworkCore.AuditTrail;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace AspNetCoreHero.Boilerplate.Infrastructure.DbContexts
{
    public class ApplicationDbContext : AuditableContext, IApplicationDbContext
    {
        private readonly IDateTimeService _dateTime;
        private readonly IAuthenticatedUserService _authenticatedUser;

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options, IDateTimeService dateTime, IAuthenticatedUserService authenticatedUser) : base(options)
        {
            _dateTime = dateTime;
            _authenticatedUser = authenticatedUser;
        }

        public DbSet<Product> Products { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<ProductCategory> ProductCategories { get; set; }
        public DbSet<ProductSale> ProductSales { get; set; }
        public DbSet<ProductPromotion> ProductPromotions { get; set; }
        public DbSet<ProductPromotionFree> ProductPromotionFrees { get; set; }
        public DbSet<Order> Orders { get; set; }
        public DbSet<OrderDetail> OrderDetails { get; set; }

        public IDbConnection Connection => Database.GetDbConnection();

        public bool HasChanges => ChangeTracker.HasChanges();

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = new CancellationToken())
        {
            foreach (var entry in ChangeTracker.Entries<AuditableEntity>().ToList())
            {
                switch (entry.State)
                {
                    case EntityState.Added:
                        entry.Entity.CreatedOn = _dateTime.NowUtc;
                        entry.Entity.CreatedBy = _authenticatedUser.UserId;
                        break;

                    case EntityState.Modified:
                        entry.Entity.LastModifiedOn = _dateTime.NowUtc;
                        entry.Entity.LastModifiedBy = _authenticatedUser.UserId;
                        break;
                }
            }
            if (_authenticatedUser.UserId == null)
            {
                return await base.SaveChangesAsync(cancellationToken);
            }
            else
            {
                return await base.SaveChangesAsync(_authenticatedUser.UserId);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            foreach (var property in builder.Model.GetEntityTypes()
            .SelectMany(t => t.GetProperties())
            .Where(p => p.ClrType == typeof(decimal) || p.ClrType == typeof(decimal?)))
            {
                property.SetColumnType("decimal(18,2)");
            }
            base.OnModelCreating(builder);

            builder.Entity<ProductCategory>().HasKey(sc => new { sc.ProductId, sc.CategoryId });
            builder.Entity<ProductCategory>()
                .HasOne<Product>(sc => sc.Product)
                .WithMany(s => s.ProductCategories)
                .HasForeignKey(sc => sc.ProductId);

            builder.Entity<ProductCategory>()
                .HasOne<Category>(sc => sc.Category)
                .WithMany(s => s.ProductCategories)
                .HasForeignKey(sc => sc.CategoryId);

            builder.Entity<Product>()
                .HasOne<ProductSale>(sc => sc.Sale)
                .WithOne(s => s.Product)
                .HasForeignKey<ProductSale>(sc => sc.ProductId);

            builder.Entity<Product>()
               .HasOne<ProductPromotionFree>(sc => sc.ProductPromotionFree)
               .WithOne(s => s.Product)
               .HasForeignKey<ProductPromotionFree>(sc => sc.ProductId);

            builder.Entity<ProductPromotion>()
                .HasOne<Product>(sc => sc.Product)
                .WithMany(s => s.Promotions)
                .HasForeignKey(sc => sc.ProductId);

            builder.Entity<OrderDetail>()
                .HasOne<Order>(sc => sc.Order)
                .WithMany(s => s.OrderDetails)
                .HasForeignKey(sc => sc.OrderId);
        }
    }
}