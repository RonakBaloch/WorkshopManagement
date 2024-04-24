using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Workshop.Models;

namespace Workshop.Data;

public partial class WorkShopContext : DbContext
{
    public WorkShopContext()
    {
    }

    public WorkShopContext(DbContextOptions<WorkShopContext> options)
        : base(options)
    {
    }

    public virtual DbSet<CustomerDetail> CustomerDetails { get; set; }

    public virtual DbSet<CustomerVehicleDetail> CustomerVehicleDetails { get; set; }

    public virtual DbSet<VehicleBookingDetail> VehicleBookingDetails { get; set; }

    public virtual DbSet<VehicleDetail> VehicleDetails { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see https://go.microsoft.com/fwlink/?LinkId=723263.
        => optionsBuilder.UseSqlServer("Data Source=DESKTOP-H4O23TB\\SQLEXPRESS;Initial Catalog=WorkShop;Trusted_Connection=True;TrustServerCertificate=True;");

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<CustomerDetail>(entity =>
        {
            entity.HasKey(e => e.CustomerId);

            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.EmailId).HasMaxLength(50);
            entity.Property(e => e.MobileNo).HasMaxLength(50);
            entity.Property(e => e.Name).HasMaxLength(200);
        });

        modelBuilder.Entity<CustomerVehicleDetail>(entity =>
        {
            entity.Property(e => e.ServiceDueDate).HasColumnType("datetime");
            entity.Property(e => e.VehicleNo).HasMaxLength(50);

            entity.HasOne(d => d.Customer).WithMany(p => p.CustomerVehicleDetails)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_CustomerVehicleDetails_CustomerDetails");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.CustomerVehicleDetails)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK_CustomerVehicleDetails_VehicleDetails");
        });

        modelBuilder.Entity<VehicleBookingDetail>(entity =>
        {
            entity.HasKey(e => e.BookingId);

            entity.Property(e => e.BookingDate).HasColumnType("datetime");
            entity.Property(e => e.CreatedDate).HasColumnType("datetime");
            entity.Property(e => e.UpdatedDate).HasColumnType("datetime");

            entity.HasOne(d => d.Customer).WithMany(p => p.VehicleBookingDetails)
                .HasForeignKey(d => d.CustomerId)
                .HasConstraintName("FK_VehicleBookingDetails_CustomerDetails");

            entity.HasOne(d => d.Vehicle).WithMany(p => p.VehicleBookingDetails)
                .HasForeignKey(d => d.VehicleId)
                .HasConstraintName("FK_VehicleBookingDetails_VehicleDetails");
        });

        modelBuilder.Entity<VehicleDetail>(entity =>
        {
            entity.HasKey(e => e.VehicleId);

            entity.Property(e => e.Color)
                .HasMaxLength(20)
                .IsUnicode(false)
                .HasColumnName("COLOR");
            entity.Property(e => e.Model)
                .HasMaxLength(50)
                .IsUnicode(false)
                .HasColumnName("MODEL");
            entity.Property(e => e.VehicleName)
                .HasMaxLength(50)
                .IsUnicode(false);
            entity.Property(e => e.VehicleType)
                .HasMaxLength(50)
                .IsUnicode(false);
        });

        OnModelCreatingPartial(modelBuilder);
    }

    partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
}
