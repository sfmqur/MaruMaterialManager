using MaruMaterialManager.Model.Schema;
using Microsoft.EntityFrameworkCore;
using System;

namespace MaruMaterialManager.Model
{
    /// <summary>
    /// The database context for the Maru Material Manager application.
    /// </summary>
    /// <remarks>
    /// This context represents a session with the database and can be used to query and save
    /// instances of your entities. It is typically used in conjunction with dependency injection.
    /// </remarks>
    public class PartsDbContext : DbContext
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="PartsDbContext"/> class.
        /// </summary>
        /// <param name="options">The options to be used by the context.</param>
        public PartsDbContext(DbContextOptions<PartsDbContext> options) : base(options)
        {
            // Enable null check for all queries (EF Core 6.0+)
            ChangeTracker.QueryTrackingBehavior = QueryTrackingBehavior.TrackAll;
            
            // Disable tracking by default for better performance
            ChangeTracker.LazyLoadingEnabled = false;
            ChangeTracker.AutoDetectChangesEnabled = false;
        }

        public DbSet<Part> Parts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Location> Locations { get; set; }
        public DbSet<Supplier> Suppliers { get; set; }
        public DbSet<InventoryTransaction> InventoryTransactions { get; set; }

        /// <summary>
        /// Configures the schema needed for the application's data model.
        /// </summary>
        /// <param name="modelBuilder">The builder being used to construct the model for this context.</param>
        /// <remarks>
        /// This method is called when the model for a derived context has been initialized but before
        /// the model has been locked down and used to initialize the context.
        /// </remarks>
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure default schema if needed
            // modelBuilder.HasDefaultSchema("inventory");
            
            // Apply all configurations from assembly (alternative to fluent API)
            // modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);

            // Configure Part entity with table name and properties
            modelBuilder.Entity<Part>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.PartNumber).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.Description).HasMaxLength(1000);
                entity.Property(e => e.UnitOfMeasure).HasMaxLength(50);
                entity.Property(e => e.CreatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.UpdatedAt).HasDefaultValueSql("CURRENT_TIMESTAMP");
                
                // Relationships
                entity.HasOne(p => p.Category)
                    .WithMany(c => c.Parts)
                    .HasForeignKey(p => p.CategoryId)
                    .OnDelete(DeleteBehavior.Restrict);
                    
                entity.HasOne(p => p.PreferredSupplier)
                    .WithMany(s => s.Parts)
                    .HasForeignKey(p => p.PreferredSupplierId)
                    .OnDelete(DeleteBehavior.SetNull);
            });

            // Configure Category entity with table name and properties
            modelBuilder.Entity<Category>(entity =>
            {
                entity.ToTable("Categories");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
            });

            // Configure Location entity with table name and properties
            modelBuilder.Entity<Location>(entity =>
            {
                entity.ToTable("Locations");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(100);
                entity.Property(e => e.Description).HasMaxLength(500);
            });

            // Configure Supplier entity with table name and properties
            modelBuilder.Entity<Supplier>(entity =>
            {
                entity.ToTable("Suppliers");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Name).IsRequired().HasMaxLength(200);
                entity.Property(e => e.ContactPerson).HasMaxLength(200);
                entity.Property(e => e.Email).HasMaxLength(100);
                entity.Property(e => e.Phone).HasMaxLength(50);
                entity.Property(e => e.Address).HasMaxLength(500);
            });

            // Configure InventoryTransaction entity with table name and properties
            modelBuilder.Entity<InventoryTransaction>(entity =>
            {
                entity.ToTable("InventoryTransactions");
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.TransactionDate).HasDefaultValueSql("CURRENT_TIMESTAMP");
                entity.Property(e => e.Notes).HasMaxLength(1000);
                
                // Relationships
                entity.HasOne(t => t.Part)
                    .WithMany(p => p.InventoryTransactions)
                    .HasForeignKey(t => t.PartId)
                    .OnDelete(DeleteBehavior.Cascade);
                    
                entity.HasOne(t => t.Location)
                    .WithMany()
                    .HasForeignKey(t => t.LocationId)
                    .OnDelete(DeleteBehavior.Restrict);
            });

            // Configure the many-to-many relationship between Part and Location
            modelBuilder.Entity<PartLocation>(entity =>
            {
                entity.ToTable("PartLocations");
                entity.HasKey(pl => new { pl.PartId, pl.LocationId });
                
                entity.HasOne(pl => pl.Part)
                    .WithMany(p => p.PartLocations)
                    .HasForeignKey(pl => pl.PartId)
                    .OnDelete(DeleteBehavior.Cascade);
                    
                entity.HasOne(pl => pl.Location)
                    .WithMany(l => l.PartLocations)
                    .HasForeignKey(pl => pl.LocationId)
                    .OnDelete(DeleteBehavior.Cascade);
            });
        }
    }
}
