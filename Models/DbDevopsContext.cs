using Microsoft.EntityFrameworkCore;

namespace TalentoLocal.Models;

public partial class DbDevopsContext : DbContext
{
    public DbDevopsContext(DbContextOptions<DbDevopsContext> options) : base(options)
    {
    }

    public virtual DbSet<Evaluation> Evaluations { get; set; }
    public virtual DbSet<Offer> Offers { get; set; }
    public virtual DbSet<OfferCategory> OfferCategories { get; set; }
    public virtual DbSet<Postulation> Postulations { get; set; }
    public virtual DbSet<PostulationStatus> PostulationStatus { get; set; }
    public virtual DbSet<Favorite> Favorites { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.HasDefaultSchema("TalentoLocal");
        base.OnModelCreating(modelBuilder);

        // =======================
        // OfferCategory
        // =======================
        modelBuilder.Entity<OfferCategory>(entity =>
        {
            entity.HasKey(c => c.Id);

            entity.Property(c => c.Name)
                  .IsRequired()
                  .HasMaxLength(100);
            //Seeders
            var seedDate = new DateTime(2024, 01, 01);

            modelBuilder.Entity<OfferCategory>().HasData(
                new OfferCategory { Id = 3, Name = "Tecnología", CreatedAt = seedDate },
                new OfferCategory { Id = 4, Name = "Administración", CreatedAt = seedDate },
                new OfferCategory { Id = 5, Name = "Salud", CreatedAt = seedDate },
                new OfferCategory { Id = 6, Name = "Educación", CreatedAt = seedDate },
                new OfferCategory { Id = 7, Name = "Marketing y Comunicación", CreatedAt = seedDate },
                new OfferCategory { Id = 8, Name = "Ingeniería", CreatedAt = seedDate },
                new OfferCategory { Id = 9, Name = "Ciencias Ambientales", CreatedAt = seedDate },
                new OfferCategory { Id = 10, Name = "Turismo y Hotelería", CreatedAt = seedDate },
                new OfferCategory { Id = 11, Name = "Logística y Operaciones", CreatedAt = seedDate },
                new OfferCategory { Id = 12, Name = "Arte y Cultura", CreatedAt = seedDate }
            );

            // Relación 1:N con Offer
            entity.HasMany(c => c.Offers)
                  .WithOne(o => o.Category)
                  .HasForeignKey(o => o.CategoryId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // =======================
        // Offer
        // =======================
        modelBuilder.Entity<Offer>(entity =>
        {
            entity.HasKey(o => o.Id);

            entity.Property(o => o.Title)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(o => o.Description)
                  .IsRequired();

            entity.Property(o => o.Modality)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(o => o.Requeriments)
                  .IsRequired();

            entity.Property(o => o.Benefits)
                  .IsRequired();

            entity.Property(o => o.Location)
                  .IsRequired()
                  .HasMaxLength(200);

            entity.Property(o => o.Journey)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(o => o.Status)
                  .IsRequired()
                  .HasMaxLength(50);

            entity.Property(o => o.ContractType)
                  .IsRequired()
                  .HasMaxLength(100);

            entity.Property(o => o.PaymentType)
                  .IsRequired()
                  .HasMaxLength(100);

            // Relación 1:N con Postulation
            entity.HasMany(o => o.Postulations)
                  .WithOne(p => p.Offer)
                  .HasForeignKey(p => p.OfferId)
                  .OnDelete(DeleteBehavior.Cascade);
        });

        // =======================
        // PostulationStatus
        // =======================
        modelBuilder.Entity<PostulationStatus>(entity =>
        {
            entity.HasKey(s => s.Id);

            entity.Property(s => s.Name)
                  .IsRequired()
                  .HasMaxLength(50);

            // Relación 1:N
            entity.HasMany(s => s.Postulations)
                  .WithOne(p => p.Status)
                  .HasForeignKey(p => p.StatusId)
                  .OnDelete(DeleteBehavior.Restrict);
        });

        // =======================
        // Postulation
        // =======================
        modelBuilder.Entity<Postulation>(entity =>
        {
            entity.HasKey(p => p.Id);

            entity.Property(p => p.UserId)
                  .IsRequired();

            entity.Property(p => p.DocumentFile)
                  .IsRequired(false);

            // Offer (N:1)
            entity.HasOne(p => p.Offer)
                  .WithMany(o => o.Postulations)
                  .HasForeignKey(p => p.OfferId)
                  .OnDelete(DeleteBehavior.Cascade);

            // Status (N:1)
            entity.HasOne(p => p.Status)
                  .WithMany()
                  .HasForeignKey(p => p.StatusId)
                  .OnDelete(DeleteBehavior.Restrict);

            // Relación 1:1 con Evaluation
            entity.HasOne(p => p.Evaluation)
                  .WithOne(e => e.Postulation)
                  .HasForeignKey<Evaluation>(e => e.PostulationId);
        });

        // =======================
        // Evaluation
        // =======================
        modelBuilder.Entity<Evaluation>(entity =>
        {
            entity.HasKey(e => e.Id);

            entity.Property(e => e.Justification)
                  .IsRequired();
        });

        // =======================
        // Favorite
        // =======================
        modelBuilder.Entity<Favorite>(entity =>
        {
            entity.HasKey(f => f.Id);

            entity.Property(f => f.UserId)
                  .IsRequired();

            entity.Property(f => f.OfferId)
                  .IsRequired();

            entity.Property(f => f.CreatedAt)
                  .IsRequired();

            // Relación N:1 con Offer
            entity.HasOne(f => f.Offer)
                  .WithMany()
                  .HasForeignKey(f => f.OfferId)
                  .OnDelete(DeleteBehavior.Cascade);

            entity.HasIndex(f => new { f.UserId, f.OfferId })
                  .IsUnique();
        });
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
