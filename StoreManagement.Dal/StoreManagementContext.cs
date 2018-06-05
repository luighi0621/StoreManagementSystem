using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StoreManagement.Dal
{
    public partial class StoreManagementContext : DbContext
    {
        public StoreManagementContext()
        {
        }

        public StoreManagementContext(DbContextOptions<StoreManagementContext> options)
            : base(options)
        {
        }

        public virtual DbSet<StoreManagement.Model.Customer> Customer { get; set; }
        public virtual DbSet<StoreManagement.Model.Product> Product { get; set; }
        public virtual DbSet<StoreManagement.Model.Supplier> Supplier { get; set; }
        public virtual DbSet<StoreManagement.Model.User> User { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StoreManagement.Model.Customer>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID");

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.CustomerCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.Property(e => e.Email).HasMaxLength(30);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Lastname).HasMaxLength(20);

                entity.Property(e => e.Phone).HasMaxLength(15);
            });

            modelBuilder.Entity<StoreManagement.Model.Product>(entity =>
            {
                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ProductCode)
                    .IsRequired()
                    .HasMaxLength(10);

                entity.HasOne(d => d.Supplier)
                    .WithMany(p => p.Products)
                    .HasForeignKey(d => d.IdSupplier)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Product_Supplier");
            });

            modelBuilder.Entity<StoreManagement.Model.Supplier>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Description).HasMaxLength(200);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.SupplierCode)
                    .IsRequired()
                    .HasMaxLength(20);
            });

            modelBuilder.Entity<StoreManagement.Model.User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Lastname).HasMaxLength(20);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20);
            });
        }
    }
}
