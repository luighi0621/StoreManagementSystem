using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using StoreManagement.Model;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace StoreManagement.Dal
{
    public partial class StoreManagementContext : IdentityDbContext<User, Rol, int>
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
        public virtual DbSet<Rol> Rol { get; set; }
        public virtual DbSet<Supplier> Supplier { get; set; }
        public virtual DbSet<User> User { get; set; }
        public virtual DbSet<UserClaim> UserClaim { get; set; }
        public virtual DbSet<UserRol> UserRol { get; set; }

//        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
//        {
//            if (!optionsBuilder.IsConfigured)
//            {
//#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
//                optionsBuilder.UseSqlServer("Data Source=.;Initial Catalog=StoreManagement;Integrated Security=True;Pooling=False;");
//            }
//        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

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

            //modelBuilder.Entity<Rol>(entity =>
            //{
            //    entity.HasIndex(e => e.NormalizedName)
            //        .HasName("RoleNameIndex");

            //    entity.Property(e => e.Id).ValueGeneratedNever();

            //    entity.Property(e => e.Name).HasMaxLength(256);

            //    entity.Property(e => e.NormalizedName).HasMaxLength(256);
            //});

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

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.Firstname)
                    .IsRequired()
                    .HasMaxLength(20);

                entity.Property(e => e.Lastname).HasMaxLength(20);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
                entity.Ignore(p => p.UserClaim);
            });

            //modelBuilder.Entity<UserClaim>(entity =>
            //{
            //    entity.HasIndex(e => e.UserId);

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.UserClaim)
            //        .HasForeignKey(d => d.UserId)
            //        .HasConstraintName("FK_UserClaims_User_UserId");
            //});

            //modelBuilder.Entity<UserRol>(entity =>
            //{
            //    entity.HasKey(e => new { e.UserId, e.RoleId });

            //    entity.HasIndex(e => e.RoleId)
            //        .HasName("IX_AspNetUserRoles_RoleId");

            //    entity.HasIndex(e => e.UserId)
            //        .HasName("IX_AspNetUserRoles_UserId");

            //    entity.HasOne(d => d.Role)
            //        .WithMany(p => p.UserRol)
            //        .HasForeignKey(d => d.RoleId)
            //        .HasConstraintName("FK_AspNetUserRoles_AspNetRoles_RoleId");

            //    entity.HasOne(d => d.User)
            //        .WithMany(p => p.UserRol)
            //        .HasForeignKey(d => d.UserId)
            //        .HasConstraintName("FK_AspNetUserRoles_AspNetUsers_UserId");
            //});

            //modelBuilder.Entity<IdentityUser<int>>().ToTable("User");
            ////modelBuilder.Entity<IdentityUserClaim<int>>().ToTable("UserClaim");
            //modelBuilder.Entity<IdentityUserRole<int>>().ToTable("UserRol");
            //modelBuilder.Entity<IdentityRole<int>>().ToTable("Rol");
            //modelBuilder.Ignore<IdentityUserClaim<int>>();
            //modelBuilder.Ignore<IdentityUserToken<int>>();
            //modelBuilder.Ignore<IdentityUserLogin<int>>();
            //modelBuilder.Ignore<IdentityRoleClaim<int>>();

        }
    }
}
