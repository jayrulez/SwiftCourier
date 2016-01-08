using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity.EntityFramework;
using Microsoft.Data.Entity;
using SwiftCourier.Models;
using Microsoft.Data.Entity.Metadata;

namespace SwiftCourier.Models
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            modelBuilder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName).HasName("RoleNameIndex");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);

                entity.HasMany(d => d.RolePermissions).WithOne(p => p.Role).HasForeignKey(d => d.RoleId);

                entity.ToTable("Roles");
            });

            modelBuilder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail).HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName).HasName("UserNameIndex");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);

                /*
                entity.HasMany(d => d.Claims).WithOne(p => p.User).HasForeignKey(d => d.UserId);
                entity.HasMany(d => d.Roles).WithOne(p => p.User).HasForeignKey(d => d.UserId);
                entity.HasMany(d => d.Logins).WithOne(p => p.User).HasForeignKey(d => d.UserId);
                */
                entity.HasMany(d => d.PackageLogs).WithOne(p => p.User).HasForeignKey(d => d.UserId);
                entity.HasMany(d => d.Payments).WithOne(p => p.User).HasForeignKey(d => d.UserId);
                entity.HasMany(d => d.UserPermissions).WithOne(p => p.User).HasForeignKey(d => d.UserId);

                entity.ToTable("Users");
            });

            modelBuilder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            modelBuilder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.Property(e => e.LoginProvider).HasMaxLength(450);

                entity.Property(e => e.ProviderKey).HasMaxLength(450);

                entity.ToTable("UserLogins");
            });

            modelBuilder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
                entity.ToTable("UserRoles");
            });

            modelBuilder.Entity<Booking>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.HasOne<Customer>(d => d.Customer).WithMany(p => p.Bookings).HasForeignKey(d => d.CustomerId);
                entity.HasOne<Service>(d => d.Service).WithMany(p => p.Bookings).HasForeignKey(d => d.ServiceId);
                entity.HasOne<Invoice>(d => d.Invoice).WithOne(p => p.Booking);
                entity.HasOne<Package>(d => d.Package).WithOne(p => p.Booking);
                entity.ToTable("Bookings");
            });

            modelBuilder.Entity<Customer>(entity =>
            {
                entity.HasIndex(e => e.EmailAddress).HasName("UK_Customer_Email").IsUnique();

                entity.Property(e => e.Address)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.ContactNumber)
                    .HasMaxLength(15)
                    .IsRequired();

                entity.Property(e => e.EmailAddress)
                    .HasMaxLength(50)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();
                entity.ToTable("Customers");
            });

            modelBuilder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.BookingId);

                entity.Property(e => e.BookingId).ValueGeneratedNever();

                entity.Property(e => e.Total).HasColumnType("decimal");

                entity.HasOne(d => d.Booking).WithOne(p => p.Invoice).HasForeignKey<Invoice>(d => d.BookingId);
                entity.ToTable("Invoices");
            });

            modelBuilder.Entity<Location>(entity =>
            {
                entity.HasIndex(e => e.Name).HasName("UK_Location").IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();
                entity.ToTable("Locations");
            });

            modelBuilder.Entity<PackageLog>(entity =>
            {
                entity.Property(e => e.LoggedAt).HasColumnType("datetime");

                entity.HasOne<Package>(d => d.Package).WithMany(p => p.PackageLogs).HasForeignKey(d => d.PackageId);

                entity.HasOne(d => d.User).WithMany(p => p.PackageLogs).HasForeignKey(d => d.UserId);
                entity.ToTable("PackageLogs");
            });

            modelBuilder.Entity<Package>(entity =>
            {
                entity.HasKey(e => e.BookingId);

                entity.Property(e => e.BookingId).ValueGeneratedNever();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.TrackingNumber)
                    .HasMaxLength(256);

                entity.HasOne(d => d.Booking).WithOne(p => p.Package).HasForeignKey<Package>(d => d.BookingId);
                entity.ToTable("Packages");
            });

            modelBuilder.Entity<PaymentMethodFieldValue>(entity =>
            {
                entity.HasKey(e => new { e.PaymentId, e.PaymentMethodFieldId });

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnType("text");

                entity.HasOne(d => d.Payment).WithMany(p => p.PaymentMethodFieldValues).HasForeignKey(d => d.PaymentId).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.PaymentMethodField).WithMany(p => p.PaymentMethodFieldValues).HasForeignKey(d => d.PaymentMethodFieldId);
                entity.ToTable("PaymentMethodFieldValues");
            });

            modelBuilder.Entity<PaymentMethodField>(entity =>
            {
                entity.HasIndex(e => e.Name).HasName("UK_PaymentMethodField").IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.HasOne(d => d.PaymentMethod).WithMany(p => p.PaymentMethodFields).HasForeignKey(d => d.PaymentMethodId);
                entity.ToTable("PaymentMethodFields");
            });

            modelBuilder.Entity<PaymentMethod>(entity =>
            {
                entity.HasIndex(e => e.Name).HasName("UK_PaymentMethod").IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();
                entity.ToTable("PaymentMethods");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal");

                entity.Property(e => e.ProcessedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Invoice).WithMany(p => p.Payments).HasForeignKey(d => d.InvoiceId);

                entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Payments).HasForeignKey(d => d.PaymentMethodId);

                entity.HasOne(d => d.User).WithMany(p => p.Payments).HasForeignKey(d => d.UserId).OnDelete(DeleteBehavior.Restrict);
                entity.ToTable("Payments");
            });

            modelBuilder.Entity<Permission>(entity =>
            {
                entity.HasIndex(e => e.Name).HasName("UK_Permission").IsUnique();

                entity.Property(e => e.Description)
                    .IsRequired()
                    .HasColumnType("text");

                entity.Property(e => e.Group)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();
                entity.ToTable("Permissions");
            });

            modelBuilder.Entity<Service>(entity =>
            {
                entity.HasIndex(e => e.Name).HasName("UK_Service").IsUnique();

                entity.Property(e => e.Cost).HasColumnType("decimal");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();
                entity.HasMany(d => d.Bookings).WithOne(p => p.Service).HasForeignKey(d => d.ServiceId);
                entity.ToTable("Services");
            });

            modelBuilder.Entity<RolePermission>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.PermissionId });

                entity.HasOne(d => d.Permission).WithMany(p => p.RolePermissions).HasForeignKey(d => d.PermissionId);

                entity.HasOne(d => d.Role).WithMany(p => p.RolePermissions).HasForeignKey(d => d.RoleId);
                entity.ToTable("RolePermissions");
            });

            modelBuilder.Entity<UserPermission>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.PermissionId });

                entity.HasOne(d => d.Permission).WithMany(p => p.UserPermissions).HasForeignKey(d => d.PermissionId);

                entity.HasOne(d => d.User).WithMany(p => p.UserPermissions).HasForeignKey(d => d.UserId);
                entity.ToTable("UserPermissions");
            });
        }

        //public virtual new DbSet<IdentityRoleClaim<int>> RoleClaims { get; set; }
        public virtual new DbSet<Role> Roles { get; set; }
        //public virtual new DbSet<IdentityUserClaim<int>> UserClaims { get; set; }
        //public virtual new DbSet<IdentityUserLogin<int>> UserLogins { get; set; }
        //public virtual new DbSet<IdentityUserRole<int>> UserRoles { get; set; }
        public virtual new DbSet<User> Users { get; set; }
        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<PackageLog> PackageLogs { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<PaymentMethodFieldValue> PaymentMethodFieldValues { get; set; }
        public virtual DbSet<PaymentMethodField> PaymentMethodFields { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }
    }
}
