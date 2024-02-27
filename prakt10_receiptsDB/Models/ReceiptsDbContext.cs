using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace prakt10_receiptsDB.Models;

public partial class ReceiptsDbContext : DbContext
{
    public ReceiptsDbContext()
    {
    }

    public ReceiptsDbContext(DbContextOptions<ReceiptsDbContext> options)
        : base(options)
    {
    }

    public virtual DbSet<Ingridient> Ingridients { get; set; }

    public virtual DbSet<Receipt> Receipts { get; set; }

    public virtual DbSet<ReceiptIngridient> ReceiptIngridients { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Server = DESKTOP-D0U93NL\\SQLEXPRESS; Database = ReceiptsWpf;TrustServerCertificate=True; Integrated Security = true");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Ingridient>(entity =>
        {
            entity.ToTable("Ingridient");

            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<Receipt>(entity =>
        {
            entity.ToTable("Receipt");

            entity.Property(e => e.Image).HasMaxLength(500);
            entity.Property(e => e.Name).HasMaxLength(100);
        });

        modelBuilder.Entity<ReceiptIngridient>(entity =>
        {
            entity.ToTable("ReceiptIngridient");

            entity.Property(e => e.Count).HasMaxLength(80);

            entity.HasOne(d => d.IngridientNavigation).WithMany(p => p.ReceiptIngridients)
                .HasForeignKey(d => d.Ingridient)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReceiptIngridient_Ingridient");

            entity.HasOne(d => d.ReceiptNavigation).WithMany(p => p.ReceiptIngridients)
                .HasForeignKey(d => d.Receipt)
                .OnDelete(DeleteBehavior.ClientSetNull)
                .HasConstraintName("FK_ReceiptIngridient_Receipt");
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
