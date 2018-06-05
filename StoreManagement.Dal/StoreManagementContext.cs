using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace StoreManagement.Dal
{
    public partial class StoreManagementContext : DbContext
    {
        public StoreManagementContext(DbContextOptions<StoreManagementContext> options)
            :base(options)
        {
        }

        public StoreManagementContext() { }

        public virtual DbSet<StoreManagement.Model.Customer> Customer { get; set; }
        public virtual DbSet<StoreManagement.Model.Product> Product { get; set; }
        public virtual DbSet<StoreManagement.Model.Supplier> Supplier { get; set; }
        public virtual DbSet<StoreManagement.Model.User> User { get; set; }



        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
                //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
                optionsBuilder.UseSqlServer(@"Data Source=.;Initial Catalog=StoreManagement;Integrated Security=True;Pooling=False;");
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<StoreManagement.Model.Customer>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Address).HasMaxLength(50);

                entity.Property(e => e.CustomerCode)
                    .IsRequired()
                    .HasColumnType("nchar(10)");

                entity.Property(e => e.Email).HasMaxLength(30);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Lastname).HasMaxLength(20);

                entity.Property(e => e.Phone).HasMaxLength(15);
            });

            modelBuilder.Entity<StoreManagement.Model.Product>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .ValueGeneratedNever();

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.ProductCode)
                    .IsRequired()
                    .HasColumnType("nchar(10)");
            });

            modelBuilder.Entity<StoreManagement.Model.Supplier>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseSqlServerIdentityColumn();

                entity.Property(e => e.Description).HasMaxLength(100);

                entity.Property(e => e.Name)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.SupplierCode)
                    .IsRequired()
                    .HasColumnType("nchar(10)");
            });

            modelBuilder.Entity<StoreManagement.Model.User>(entity =>
            {
                entity.Property(e => e.Id)
                    .HasColumnName("ID")
                    .UseSqlServerIdentityColumn();

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Lastname).HasMaxLength(20);

                entity.Property(e => e.Login)
                    .IsRequired()
                    .HasColumnType("nchar(20)");

                entity.Property(e => e.Password)
                    .IsRequired()
                    .HasMaxLength(20);
            });
        }
    }
}
