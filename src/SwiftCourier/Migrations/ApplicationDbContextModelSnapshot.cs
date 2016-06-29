using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;
using SwiftCourier.Data;

namespace SwiftCourier.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    partial class ApplicationDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "1.0.0-rtm-21431")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasIndex("RoleId");

                    b.ToTable("RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasAnnotation("MaxLength", 450);

                    b.Property<string>("ProviderKey")
                        .HasAnnotation("MaxLength", 450);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasIndex("UserId");

                    b.ToTable("UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasIndex("RoleId");

                    b.HasIndex("UserId");

                    b.ToTable("UserRoles");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserToken<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<string>("LoginProvider");

                    b.Property<string>("Name");

                    b.Property<string>("Value");

                    b.HasKey("UserId", "LoginProvider", "Name");

                    b.ToTable("AspNetUserTokens");
                });

            modelBuilder.Entity("SwiftCourier.Data.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConsigneeAddress");

                    b.Property<string>("ConsigneeContactNumber");

                    b.Property<string>("ConsigneeName");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("datetime");

                    b.Property<int>("CreatedByUserId");

                    b.Property<int>("CustomerId");

                    b.Property<int>("DestinationLocationId");

                    b.Property<int>("OriginLocationId");

                    b.Property<string>("PickupAddress");

                    b.Property<string>("PickupContactNumber");

                    b.Property<bool>("PickupRequired");

                    b.Property<string>("RequestDate");

                    b.Property<int>("ServiceId");

                    b.HasKey("Id");

                    b.HasIndex("CreatedByUserId");

                    b.HasIndex("CustomerId");

                    b.HasIndex("DestinationLocationId");

                    b.HasIndex("OriginLocationId");

                    b.HasIndex("ServiceId");

                    b.ToTable("Bookings");
                });

            modelBuilder.Entity("SwiftCourier.Data.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 15);

                    b.Property<string>("EmailAddress")
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<bool>("TaxExempted");

                    b.HasKey("Id");

                    b.ToTable("Customers");
                });

            modelBuilder.Entity("SwiftCourier.Data.Invoice", b =>
                {
                    b.Property<int>("BookingId");

                    b.Property<decimal>("AmountDue");

                    b.Property<decimal>("AmountPaid");

                    b.Property<int>("BillingMode");

                    b.Property<decimal>("DiscountAmount");

                    b.Property<int>("DiscountType");

                    b.Property<decimal>("DiscountValue");

                    b.Property<decimal>("GCT");

                    b.Property<decimal>("ServiceCost");

                    b.Property<int>("Status");

                    b.Property<decimal>("Total")
                        .HasColumnType("decimal");

                    b.HasKey("BookingId");

                    b.HasIndex("BookingId")
                        .IsUnique();

                    b.ToTable("Invoices");
                });

            modelBuilder.Entity("SwiftCourier.Data.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("UK_Location");

                    b.ToTable("Locations");
                });

            modelBuilder.Entity("SwiftCourier.Data.Package", b =>
                {
                    b.Property<int>("BookingId");

                    b.Property<int?>("AssignedToUserId");

                    b.Property<DateTime?>("DeliveredAt");

                    b.Property<int?>("DeliveredByUserId");

                    b.Property<string>("Description");

                    b.Property<int>("PackageTypeId");

                    b.Property<DateTime?>("PickedUpAt");

                    b.Property<int>("Pieces");

                    b.Property<string>("ReferenceNumber");

                    b.Property<string>("SpecialInstructions");

                    b.Property<int>("Status");

                    b.Property<string>("TrackingNumber")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<decimal>("Weight");

                    b.HasKey("BookingId");

                    b.HasIndex("AssignedToUserId");

                    b.HasIndex("BookingId")
                        .IsUnique();

                    b.HasIndex("DeliveredByUserId");

                    b.HasIndex("PackageTypeId");

                    b.ToTable("Packages");
                });

            modelBuilder.Entity("SwiftCourier.Data.PackageLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LogMessage");

                    b.Property<DateTime>("LoggedAt")
                        .HasColumnType("datetime");

                    b.Property<int>("PackageId");

                    b.HasKey("Id");

                    b.HasIndex("PackageId");

                    b.ToTable("PackageLogs");
                });

            modelBuilder.Entity("SwiftCourier.Data.PackageType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");

                    b.ToTable("PackageTypes");
                });

            modelBuilder.Entity("SwiftCourier.Data.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount")
                        .HasColumnType("decimal");

                    b.Property<int>("InvoiceId");

                    b.Property<int>("PaymentMethodId");

                    b.Property<DateTime>("ProcessedAt")
                        .HasColumnType("datetime");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasIndex("InvoiceId");

                    b.HasIndex("PaymentMethodId");

                    b.HasIndex("UserId");

                    b.ToTable("Payments");
                });

            modelBuilder.Entity("SwiftCourier.Data.PaymentMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("UK_PaymentMethod");

                    b.ToTable("PaymentMethods");
                });

            modelBuilder.Entity("SwiftCourier.Data.PaymentMethodField", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<int>("PaymentMethodId");

                    b.Property<int>("Type");

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("UK_PaymentMethodField");

                    b.HasIndex("PaymentMethodId");

                    b.ToTable("PaymentMethodFields");
                });

            modelBuilder.Entity("SwiftCourier.Data.PaymentMethodFieldValue", b =>
                {
                    b.Property<int>("PaymentId");

                    b.Property<int>("PaymentMethodFieldId");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("PaymentId", "PaymentMethodFieldId");

                    b.HasIndex("PaymentId");

                    b.HasIndex("PaymentMethodFieldId");

                    b.ToTable("PaymentMethodFieldValues");
                });

            modelBuilder.Entity("SwiftCourier.Data.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Group")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("UK_Permission");

                    b.ToTable("Permissions");
                });

            modelBuilder.Entity("SwiftCourier.Data.Role", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedName")
                        .HasAnnotation("MaxLength", 256);

                    b.HasKey("Id");

                    b.HasIndex("NormalizedName")
                        .HasName("RoleNameIndex");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("SwiftCourier.Data.RolePermission", b =>
                {
                    b.Property<int>("RoleId");

                    b.Property<int>("PermissionId");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.HasIndex("RoleId");

                    b.ToTable("RolePermissions");
                });

            modelBuilder.Entity("SwiftCourier.Data.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Cost")
                        .HasColumnType("decimal");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasName("UK_Service");

                    b.ToTable("Services");
                });

            modelBuilder.Entity("SwiftCourier.Data.Setting", b =>
                {
                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Description");

                    b.Property<string>("DisplayName")
                        .HasColumnType("text");

                    b.Property<string>("Value")
                        .HasColumnType("text");

                    b.HasKey("Name");

                    b.ToTable("Settings");
                });

            modelBuilder.Entity("SwiftCourier.Data.User", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<int>("AccessFailedCount");

                    b.Property<string>("ConcurrencyStamp")
                        .IsConcurrencyToken();

                    b.Property<string>("Email")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<bool>("EmailConfirmed");

                    b.Property<bool>("LockoutEnabled");

                    b.Property<DateTimeOffset?>("LockoutEnd");

                    b.Property<string>("NormalizedEmail")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("NormalizedUserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<string>("PasswordHash");

                    b.Property<string>("PhoneNumber");

                    b.Property<bool>("PhoneNumberConfirmed");

                    b.Property<string>("SecurityStamp");

                    b.Property<bool>("TwoFactorEnabled");

                    b.Property<string>("UserName")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<int>("UserType");

                    b.HasKey("Id");

                    b.HasIndex("NormalizedEmail")
                        .HasName("EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .IsUnique()
                        .HasName("UserNameIndex");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("SwiftCourier.Data.UserPermission", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("PermissionId");

                    b.HasKey("UserId", "PermissionId");

                    b.HasIndex("PermissionId");

                    b.HasIndex("UserId");

                    b.ToTable("UserPermissions");
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("SwiftCourier.Data.Role")
                        .WithMany("Claims")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("SwiftCourier.Data.User")
                        .WithMany("Claims")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("SwiftCourier.Data.User")
                        .WithMany("Logins")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("Microsoft.AspNetCore.Identity.EntityFrameworkCore.IdentityUserRole<int>", b =>
                {
                    b.HasOne("SwiftCourier.Data.Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SwiftCourier.Data.User")
                        .WithMany("Roles")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SwiftCourier.Data.Booking", b =>
                {
                    b.HasOne("SwiftCourier.Data.User", "CreatedBy")
                        .WithMany("CreatedBookings")
                        .HasForeignKey("CreatedByUserId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SwiftCourier.Data.Customer", "Customer")
                        .WithMany("Bookings")
                        .HasForeignKey("CustomerId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SwiftCourier.Data.Location", "Destination")
                        .WithMany("DestinationBookings")
                        .HasForeignKey("DestinationLocationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SwiftCourier.Data.Location", "Origin")
                        .WithMany("OriginBookings")
                        .HasForeignKey("OriginLocationId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SwiftCourier.Data.Service", "Service")
                        .WithMany("Bookings")
                        .HasForeignKey("ServiceId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SwiftCourier.Data.Invoice", b =>
                {
                    b.HasOne("SwiftCourier.Data.Booking", "Booking")
                        .WithOne("Invoice")
                        .HasForeignKey("SwiftCourier.Data.Invoice", "BookingId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SwiftCourier.Data.Package", b =>
                {
                    b.HasOne("SwiftCourier.Data.User", "AssignedTo")
                        .WithMany("AssignedPackages")
                        .HasForeignKey("AssignedToUserId");

                    b.HasOne("SwiftCourier.Data.Booking", "Booking")
                        .WithOne("Package")
                        .HasForeignKey("SwiftCourier.Data.Package", "BookingId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SwiftCourier.Data.User", "DeliveredBy")
                        .WithMany("DeliveredPackages")
                        .HasForeignKey("DeliveredByUserId");

                    b.HasOne("SwiftCourier.Data.PackageType", "PackageType")
                        .WithMany("Packages")
                        .HasForeignKey("PackageTypeId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SwiftCourier.Data.PackageLog", b =>
                {
                    b.HasOne("SwiftCourier.Data.Package", "Package")
                        .WithMany("PackageLogs")
                        .HasForeignKey("PackageId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SwiftCourier.Data.Payment", b =>
                {
                    b.HasOne("SwiftCourier.Data.Invoice", "Invoice")
                        .WithMany("Payments")
                        .HasForeignKey("InvoiceId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SwiftCourier.Data.PaymentMethod", "PaymentMethod")
                        .WithMany("Payments")
                        .HasForeignKey("PaymentMethodId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SwiftCourier.Data.User", "User")
                        .WithMany("Payments")
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SwiftCourier.Data.PaymentMethodField", b =>
                {
                    b.HasOne("SwiftCourier.Data.PaymentMethod", "PaymentMethod")
                        .WithMany("PaymentMethodFields")
                        .HasForeignKey("PaymentMethodId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SwiftCourier.Data.PaymentMethodFieldValue", b =>
                {
                    b.HasOne("SwiftCourier.Data.Payment", "Payment")
                        .WithMany("PaymentMethodFieldValues")
                        .HasForeignKey("PaymentId");

                    b.HasOne("SwiftCourier.Data.PaymentMethodField", "PaymentMethodField")
                        .WithMany("PaymentMethodFieldValues")
                        .HasForeignKey("PaymentMethodFieldId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SwiftCourier.Data.RolePermission", b =>
                {
                    b.HasOne("SwiftCourier.Data.Permission", "Permission")
                        .WithMany("RolePermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SwiftCourier.Data.Role", "Role")
                        .WithMany("RolePermissions")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade);
                });

            modelBuilder.Entity("SwiftCourier.Data.UserPermission", b =>
                {
                    b.HasOne("SwiftCourier.Data.Permission", "Permission")
                        .WithMany("UserPermissions")
                        .HasForeignKey("PermissionId")
                        .OnDelete(DeleteBehavior.Cascade);

                    b.HasOne("SwiftCourier.Data.User", "User")
                        .WithMany("UserPermissions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade);
                });
        }
    }
}
