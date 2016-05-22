using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SwiftCourier.Web.Data
{
    public class ApplicationDbContext : IdentityDbContext<User, Role, int>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.Entity<IdentityRoleClaim<int>>(entity =>
            {
                entity.ToTable("RoleClaims");
            });

            builder.Entity<Role>(entity =>
            {
                entity.HasIndex(e => e.NormalizedName).HasName("RoleNameIndex");

                entity.Property(e => e.Name).HasMaxLength(256);

                entity.Property(e => e.NormalizedName).HasMaxLength(256);

                entity.HasMany(d => d.RolePermissions).WithOne(p => p.Role).HasForeignKey(d => d.RoleId);

                entity.ToTable("Roles");
            });

            builder.Entity<User>(entity =>
            {
                entity.HasIndex(e => e.NormalizedEmail).HasName("EmailIndex");

                entity.HasIndex(e => e.NormalizedUserName).HasName("UserNameIndex");

                entity.Property(e => e.Email).HasMaxLength(256);

                entity.Property(e => e.NormalizedEmail).HasMaxLength(256);

                entity.Property(e => e.NormalizedUserName).HasMaxLength(256);

                entity.Property(e => e.UserName).HasMaxLength(256);
                
                entity.HasMany(d => d.DeliveredPackages).WithOne(p => p.DeliveredBy).HasForeignKey(d => d.DeliveredByUserId);
                entity.HasMany(d => d.AssignedPackages).WithOne(p => p.AssignedTo).HasForeignKey(d => d.AssignedToUserId);
                entity.HasMany(d => d.Payments).WithOne(p => p.User).HasForeignKey(d => d.UserId);
                entity.HasMany(d => d.UserPermissions).WithOne(p => p.User).HasForeignKey(d => d.UserId);
                entity.HasMany(d => d.CreatedBookings).WithOne(p => p.CreatedBy).HasForeignKey(d => d.CreatedByUserId);

                entity.ToTable("Users");
            });

            builder.Entity<IdentityUserClaim<int>>(entity =>
            {
                entity.ToTable("UserClaims");
            });

            builder.Entity<IdentityUserLogin<int>>(entity =>
            {
                entity.HasKey(e => new { e.LoginProvider, e.ProviderKey });

                entity.Property(e => e.LoginProvider).HasMaxLength(450);

                entity.Property(e => e.ProviderKey).HasMaxLength(450);

                entity.ToTable("UserLogins");
            });

            builder.Entity<IdentityUserRole<int>>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.RoleId });
                entity.ToTable("UserRoles");
            });

            builder.Entity<Booking>(entity =>
            {
                entity.Property(e => e.CreatedAt).HasColumnType("datetime");

                entity.HasOne<Customer>(d => d.Customer).WithMany(p => p.Bookings).HasForeignKey(d => d.CustomerId);
                entity.HasOne<Service>(d => d.Service).WithMany(p => p.Bookings).HasForeignKey(d => d.ServiceId);
                entity.HasOne<User>(d => d.CreatedBy).WithMany(p => p.CreatedBookings).HasForeignKey(d => d.CreatedByUserId);
                entity.HasOne<Invoice>(d => d.Invoice).WithOne(p => p.Booking);
                entity.HasOne<Package>(d => d.Package).WithOne(p => p.Booking);
                entity.HasOne<Location>(d => d.Origin).WithMany(p => p.OriginBookings).HasForeignKey(d => d.OriginLocationId);
                entity.HasOne<Location>(d => d.Destination).WithMany(p => p.DestinationBookings).HasForeignKey(d => d.DestinationLocationId);
                entity.ToTable("Bookings");
            });

            builder.Entity<Customer>(entity =>
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

            builder.Entity<Invoice>(entity =>
            {
                entity.HasKey(e => e.BookingId);

                entity.Property(e => e.BookingId).ValueGeneratedNever();

                entity.Property(e => e.Total).HasColumnType("decimal");

                entity.HasOne(d => d.Booking).WithOne(p => p.Invoice).HasForeignKey<Invoice>(d => d.BookingId);
                entity.ToTable("Invoices");
            });

            builder.Entity<Location>(entity =>
            {
                entity.HasIndex(e => e.Name).HasName("UK_Location").IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.HasMany<Booking>(d => d.OriginBookings).WithOne(p => p.Origin).HasForeignKey(p => p.OriginLocationId);
                entity.HasMany<Booking>(d => d.DestinationBookings).WithOne(p => p.Destination).HasForeignKey(p => p.DestinationLocationId);

                entity.ToTable("Locations");
            });

            builder.Entity<PackageLog>(entity =>
            {
                entity.Property(e => e.LoggedAt).HasColumnType("datetime");

                entity.HasOne<Package>(d => d.Package).WithMany(p => p.PackageLogs).HasForeignKey(d => d.PackageId);

                entity.ToTable("PackageLogs");
            });

            builder.Entity<PackageType>(entity =>
            {
                entity.HasKey(p => p.Id);

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.HasMany(p => p.Packages).WithOne(p => p.PackageType).HasForeignKey(p => p.PackageTypeId);

                entity.ToTable("PackageTypes");
            });

            builder.Entity<Package>(entity =>
            {
                entity.HasKey(e => e.BookingId);

                entity.Property(e => e.BookingId).ValueGeneratedNever();

                entity.Property(e => e.PackageTypeId)
                    .IsRequired();

                entity.Property(e => e.TrackingNumber)
                    .HasMaxLength(256);

                entity.HasOne(d => d.Booking).WithOne(p => p.Package).HasForeignKey<Package>(d => d.BookingId);
                entity.HasOne(d => d.PackageType).WithMany(p => p.Packages).HasForeignKey(p => p.PackageTypeId);
                entity.ToTable("Packages");
            });

            builder.Entity<PaymentMethodFieldValue>(entity =>
            {
                entity.HasKey(e => new { e.PaymentId, e.PaymentMethodFieldId });

                entity.Property(e => e.Value)
                    .IsRequired()
                    .HasColumnType("text");

                entity.HasOne(d => d.Payment).WithMany(p => p.PaymentMethodFieldValues).HasForeignKey(d => d.PaymentId).OnDelete(DeleteBehavior.Restrict);

                entity.HasOne(d => d.PaymentMethodField).WithMany(p => p.PaymentMethodFieldValues).HasForeignKey(d => d.PaymentMethodFieldId);
                entity.ToTable("PaymentMethodFieldValues");
            });

            builder.Entity<PaymentMethodField>(entity =>
            {
                entity.HasIndex(e => e.Name).HasName("UK_PaymentMethodField").IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();

                entity.HasOne(d => d.PaymentMethod).WithMany(p => p.PaymentMethodFields).HasForeignKey(d => d.PaymentMethodId);
                entity.ToTable("PaymentMethodFields");
            });

            builder.Entity<PaymentMethod>(entity =>
            {
                entity.HasIndex(e => e.Name).HasName("UK_PaymentMethod").IsUnique();

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();
                entity.ToTable("PaymentMethods");
            });

            builder.Entity<Payment>(entity =>
            {
                entity.Property(e => e.Amount).HasColumnType("decimal");

                entity.Property(e => e.ProcessedAt).HasColumnType("datetime");

                entity.HasOne(d => d.Invoice).WithMany(p => p.Payments).HasForeignKey(d => d.InvoiceId);

                entity.HasOne(d => d.PaymentMethod).WithMany(p => p.Payments).HasForeignKey(d => d.PaymentMethodId);

                entity.HasOne(d => d.User).WithMany(p => p.Payments).HasForeignKey(d => d.UserId).OnDelete(DeleteBehavior.Restrict);
                entity.ToTable("Payments");
            });

            builder.Entity<Permission>(entity =>
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

            builder.Entity<Service>(entity =>
            {
                entity.HasIndex(e => e.Name).HasName("UK_Service").IsUnique();

                entity.Property(e => e.Cost).HasColumnType("decimal");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();
                entity.HasMany(d => d.Bookings).WithOne(p => p.Service).HasForeignKey(d => d.ServiceId);
                entity.ToTable("Services");
            });

            builder.Entity<Setting>(entity =>
            {
                entity.HasKey(e => e.Name);

                entity.Property(e => e.DisplayName)
                    .HasColumnType("text");

                entity.Property(e => e.Value)
                    .HasColumnType("text");

                entity.Property(e => e.Name)
                    .HasMaxLength(100)
                    .IsRequired();
                entity.ToTable("Settings");
            });

            builder.Entity<RolePermission>(entity =>
            {
                entity.HasKey(e => new { e.RoleId, e.PermissionId });

                entity.HasOne(d => d.Permission).WithMany(p => p.RolePermissions).HasForeignKey(d => d.PermissionId);

                entity.HasOne(d => d.Role).WithMany(p => p.RolePermissions).HasForeignKey(d => d.RoleId);
                entity.ToTable("RolePermissions");
            });

            builder.Entity<UserPermission>(entity =>
            {
                entity.HasKey(e => new { e.UserId, e.PermissionId });

                entity.HasOne(d => d.Permission).WithMany(p => p.UserPermissions).HasForeignKey(d => d.PermissionId);

                entity.HasOne(d => d.User).WithMany(p => p.UserPermissions).HasForeignKey(d => d.UserId);
                entity.ToTable("UserPermissions");
            });
        }
        
        public virtual new DbSet<Role> Roles { get; set; }
        public virtual new DbSet<User> Users { get; set; }

        public virtual DbSet<Booking> Bookings { get; set; }
        public virtual DbSet<Customer> Customers { get; set; }
        public virtual DbSet<Invoice> Invoices { get; set; }
        public virtual DbSet<Location> Locations { get; set; }
        public virtual DbSet<PackageLog> PackageLogs { get; set; }
        public virtual DbSet<Package> Packages { get; set; }
        public virtual DbSet<PackageType> PackageTypes { get; set; }
        public virtual DbSet<PaymentMethodFieldValue> PaymentMethodFieldValues { get; set; }
        public virtual DbSet<PaymentMethodField> PaymentMethodFields { get; set; }
        public virtual DbSet<PaymentMethod> PaymentMethods { get; set; }
        public virtual DbSet<Payment> Payments { get; set; }
        public virtual DbSet<Permission> Permissions { get; set; }
        public virtual DbSet<Service> Services { get; set; }
        public virtual DbSet<Setting> Settings { get; set; }
        public virtual DbSet<UserPermission> UserPermissions { get; set; }
        public virtual DbSet<RolePermission> RolePermissions { get; set; }
    }
}
