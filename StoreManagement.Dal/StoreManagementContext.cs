using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StoreManagement.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

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

        public virtual DbSet<Customer> Customer { get; set; }
        public virtual DbSet<Operation> Operation { get; set; }
        public virtual DbSet<Product> Product { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<User> User { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=StoreManagement;Integrated Security=True;Pooling=False");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Customer>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

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

            modelBuilder.Entity<Operation>(entity =>
            {
                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasMaxLength(60);

                entity.Property(e => e.Operation1)
                    .IsRequired()
                    .HasColumnName("Operation")
                    .HasMaxLength(10);

                entity.Property(e => e.Table)
                    .IsRequired()
                    .HasMaxLength(50);
            });

            modelBuilder.Entity<Product>(entity =>
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

            modelBuilder.Entity<Supplier>(entity =>
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

            modelBuilder.Entity<User>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("ID");

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Lastname).HasMaxLength(20);

                entity.Property(e => e.Login).HasMaxLength(256);
            });
        }
    }
}
