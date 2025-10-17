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

    public virtual DbSet<Postulacion> Postulaciones { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        => optionsBuilder.UseSqlServer("Server=tcp:s-devopsg1.database.windows.net,1433;Initial Catalog=db-devops;Persist Security Info=False;User ID=cagarcias@unimagdalena.edu.co;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Authentication=\"Active Directory Integrated\";");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Postulacion>(entity =>
        {
            entity.HasKey(e => e.Id).HasName("PK__Postulac__3213E83FFAF8BFE7");

            entity.ToTable("Postulaciones", "TalentoLocal");

            entity.Property(e => e.Id).HasColumnName("id");
            entity.Property(e => e.CreateTime)
                .HasColumnType("datetime")
                .HasColumnName("create_time");
            entity.Property(e => e.Descripcion).HasColumnName("descripcion");
            entity.Property(e => e.Titulo)
                .HasMaxLength(255)
                .HasColumnName("titulo");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
