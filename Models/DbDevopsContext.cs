using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace TalentoLocal.Models;

public partial class DbDevopsContext : DbContext
{
    public DbDevopsContext()
    {
    }

    public DbDevopsContext(DbContextOptions<DbDevopsContext> options)
        : base(options)
    {
    }

    public virtual DbSet<PublishingEntity> PublishingEntities { get; set; }
    public virtual DbSet<Convocation> Convocations { get; set; }
    public virtual DbSet<Offer> Offers { get; set; }
    public virtual DbSet<Evaluation> Evaluations { get; set; }
    public virtual DbSet<History> Histories { get; set; }

    public virtual DbSet<Postulation> Postulations { get; set; }

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
        modelBuilder.Entity<PublishingEntity>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Publishi_3214EC071949CE6C");
            entity.ToTable("PublishingEntity", "TalentoLocal");
        });

        modelBuilder.Entity<Convocation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Convocat_3214EC07C1741D02");
            entity.ToTable("Convocation", "TalentoLocal");

            // Relaciones
            entity.HasOne(e => e.PublishingEntity)
                  .WithMany(p => p.Convocations)
                  .HasForeignKey(e => e.PublishingEntityId);
        });

        modelBuilder.Entity<Offer>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Offer_3214EC075BB4753F");
            entity.ToTable("Offer", "TalentoLocal");

            entity.HasOne(e => e.Convocation)
                  .WithMany(c => c.Offers)
                  .HasForeignKey(e => e.IdConvocation);
        });

        modelBuilder.Entity<Postulation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Postulat_3214EC07FF507F73");
            entity.ToTable("Postulation", "TalentoLocal");

            entity.HasOne(e => e.Convocation)
                  .WithMany(c => c.Postulations)
                  .HasForeignKey(e => e.ConvocationId);

            entity.HasOne(e => e.Evaluation)
                  .WithOne(ev => ev.Postulation)
                  .HasForeignKey<Evaluation>(ev => ev.PostulationId);
        });

        modelBuilder.Entity<Evaluation>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_Evaluati_3214EC078857662D");
            entity.ToTable("Evaluation", "TalentoLocal");
        });

        modelBuilder.Entity<History>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK_History_3214EC07B442378B");
            entity.ToTable("History", "TalentoLocal");
        });

        OnModelCreatingPartial(modelBuilder);
    }


    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);

}
