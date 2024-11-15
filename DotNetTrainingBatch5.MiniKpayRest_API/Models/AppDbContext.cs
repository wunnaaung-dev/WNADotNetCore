using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;

namespace DotNetTrainingBatch5.MiniKpayRest_API.Models;

public partial class AppDbContext : DbContext
{
    public AppDbContext()
    {
    }

    public AppDbContext(DbContextOptions<AppDbContext> options)
        : base(options)
    {
    }


    public virtual DbSet<TblDeposit> TblDeposits { get; set; }

    public virtual DbSet<TblTransfer> TblTransfers { get; set; }

    public virtual DbSet<TblUser> TblUsers { get; set; }

    public virtual DbSet<TblWithdraw> TblWithdraws { get; set; }


    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        if (!optionsBuilder.IsConfigured)
        {
            string connectionString = "Data Source=.;Initial Catalog=DotNetTrainingBatch5;User ID=sa;Password=sasa@123;TrustServerCertificate=True;";

            optionsBuilder.UseSqlServer(connectionString);
        }
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TblDeposit>(entity =>
        {
            entity.HasKey(e => e.DepositId).HasName("PK_Deposit");

            entity.ToTable("Tbl_Deposit");

            entity.Property(e => e.Balance).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblTransfer>(entity =>
        {
            entity.HasKey(e => e.TransferId).HasName("PK_Transfer");

            entity.ToTable("Tbl_Transfer");

            entity.Property(e => e.Amount).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.FromMobileNo)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.ToMobileNo)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        modelBuilder.Entity<TblUser>(entity =>
        {
            entity.HasKey(e => e.UserId).HasName("PK_User");

            entity.ToTable("Tbl_User");

            entity.Property(e => e.Balance).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(15)
                .IsUnicode(false);
            entity.Property(e => e.Name)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.Pin)
                .HasMaxLength(6)
                .IsUnicode(false)
                .IsFixedLength();
        });

        modelBuilder.Entity<TblWithdraw>(entity =>
        {
            entity.HasKey(e => e.WithdrawId).HasName("PK_Withdraw");

            entity.ToTable("Tbl_Withdraw");

            entity.Property(e => e.Balance).HasColumnType("decimal(10, 2)");
            entity.Property(e => e.MobileNo)
                .HasMaxLength(15)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
