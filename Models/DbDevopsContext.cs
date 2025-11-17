using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;

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

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        // Only configure the SQL Server provider if no other provider has been configured
        // (for example during tests the provider is configured in Program.cs with InMemory).
        if (!optionsBuilder.IsConfigured)
        {
            optionsBuilder.UseSqlServer("Server=tcp:s-devopsg1.database.windows.net,1433;Initial Catalog=db-devops;Persist Security Info=False;User ID=cagarcias;Password=2z3KerkP8k8EfvU;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");
        }
    }
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {

        modelBuilder.Entity<Offer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Offer_3214EC075BB4753F");
            entity.ToTable("Offer", "TalentoLocal");

            entity.HasMany(e => e.Postulations).WithOne(p => p.Offer).HasForeignKey(p => p.OfferId);

            entity.HasOne(e => e.Category).WithMany(c => c.Offers).HasForeignKey(c => c.CategoryId);
        });

        modelBuilder.Entity<Postulation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Postulat_3214EC07FF507F73");
            entity.ToTable("Postulation", "TalentoLocal");

            entity.HasOne(e => e.Status)
                  .WithOne(c => c.Postulations)
                  .HasForeignKey<Postulation>(e => e.StatusId);

            entity.HasOne(e => e.Evaluation)
                  .WithOne(ev => ev.Postulation)
                  .HasForeignKey<Evaluation>(ev => ev.PostulationId);
        });

        modelBuilder.Entity<Evaluation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Evaluati_3214EC078857662D");
            entity.ToTable("Evaluation", "TalentoLocal");
        });

        modelBuilder.Entity<PostulationStatus>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_PostulationStatus");
            entity.ToTable("PostulationStatus", "TalentoLocal");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
