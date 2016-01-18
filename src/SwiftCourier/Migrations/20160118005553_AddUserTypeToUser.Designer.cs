using System;
using Microsoft.Data.Entity;
using Microsoft.Data.Entity.Infrastructure;
using Microsoft.Data.Entity.Metadata;
using Microsoft.Data.Entity.Migrations;
using SwiftCourier.Models;

namespace SwiftCourier.Migrations
{
    [DbContext(typeof(ApplicationDbContext))]
    [Migration("20160118005553_AddUserTypeToUser")]
    partial class AddUserTypeToUser
    {
        protected override void BuildTargetModel(ModelBuilder modelBuilder)
        {
            modelBuilder
                .HasAnnotation("ProductVersion", "7.0.0-rc1-16348")
                .HasAnnotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("RoleId");

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "RoleClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<int>", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ClaimType");

                    b.Property<string>("ClaimValue");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "UserClaims");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<int>", b =>
                {
                    b.Property<string>("LoginProvider")
                        .HasAnnotation("MaxLength", 450);

                    b.Property<string>("ProviderKey")
                        .HasAnnotation("MaxLength", 450);

                    b.Property<string>("ProviderDisplayName");

                    b.Property<int>("UserId");

                    b.HasKey("LoginProvider", "ProviderKey");

                    b.HasAnnotation("Relational:TableName", "UserLogins");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<int>", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("RoleId");

                    b.HasKey("UserId", "RoleId");

                    b.HasAnnotation("Relational:TableName", "UserRoles");
                });

            modelBuilder.Entity("SwiftCourier.Models.Booking", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("ConsigneeAddress");

                    b.Property<string>("ConsigneeContactNumber");

                    b.Property<string>("ConsigneeName");

                    b.Property<DateTime>("CreatedAt")
                        .HasAnnotation("Relational:ColumnType", "datetime");

                    b.Property<int>("CreatedByUserId");

                    b.Property<int>("CustomerId");

                    b.Property<string>("PickupAddress");

                    b.Property<string>("PickupContactNumber");

                    b.Property<bool>("PickupRequired");

                    b.Property<string>("RequestDate");

                    b.Property<int>("ServiceId");

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "Bookings");
                });

            modelBuilder.Entity("SwiftCourier.Models.Customer", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Address")
                        .IsRequired()
                        .HasAnnotation("Relational:ColumnType", "text");

                    b.Property<string>("ContactNumber")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 15);

                    b.Property<string>("EmailAddress")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 50);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<bool>("TaxExempted");

                    b.HasKey("Id");

                    b.HasIndex("EmailAddress")
                        .IsUnique()
                        .HasAnnotation("Relational:Name", "UK_Customer_Email");

                    b.HasAnnotation("Relational:TableName", "Customers");
                });

            modelBuilder.Entity("SwiftCourier.Models.Invoice", b =>
                {
                    b.Property<int>("BookingId");

                    b.Property<decimal>("AmountDue");

                    b.Property<decimal>("AmountPaid");

                    b.Property<int>("BillingMode");

                    b.Property<decimal>("GCT");

                    b.Property<decimal>("ServiceCost");

                    b.Property<int>("Status");

                    b.Property<decimal>("Total")
                        .HasAnnotation("Relational:ColumnType", "decimal");

                    b.HasKey("BookingId");

                    b.HasAnnotation("Relational:TableName", "Invoices");
                });

            modelBuilder.Entity("SwiftCourier.Models.Location", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasAnnotation("Relational:Name", "UK_Location");

                    b.HasAnnotation("Relational:TableName", "Locations");
                });

            modelBuilder.Entity("SwiftCourier.Models.Package", b =>
                {
                    b.Property<int>("BookingId");

                    b.Property<int?>("AssignedToUserId");

                    b.Property<DateTime?>("DeliveredAt");

                    b.Property<int?>("DeliveredByUserId");

                    b.Property<string>("Description");

                    b.Property<int>("PackageTypeId");

                    b.Property<DateTime?>("PickedUpAt");

                    b.Property<int>("Pieces");

                    b.Property<string>("SpecialInstructions");

                    b.Property<int>("Status");

                    b.Property<string>("TrackingNumber")
                        .HasAnnotation("MaxLength", 256);

                    b.Property<decimal>("Weight");

                    b.HasKey("BookingId");

                    b.HasAnnotation("Relational:TableName", "Packages");
                });

            modelBuilder.Entity("SwiftCourier.Models.PackageLog", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("LogMessage");

                    b.Property<DateTime>("LoggedAt")
                        .HasAnnotation("Relational:ColumnType", "datetime");

                    b.Property<int>("PackageId");

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "PackageLogs");
                });

            modelBuilder.Entity("SwiftCourier.Models.PackageType", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "PackageTypes");
                });

            modelBuilder.Entity("SwiftCourier.Models.Payment", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Amount")
                        .HasAnnotation("Relational:ColumnType", "decimal");

                    b.Property<int>("InvoiceId");

                    b.Property<int>("PaymentMethodId");

                    b.Property<DateTime>("ProcessedAt")
                        .HasAnnotation("Relational:ColumnType", "datetime");

                    b.Property<int>("UserId");

                    b.HasKey("Id");

                    b.HasAnnotation("Relational:TableName", "Payments");
                });

            modelBuilder.Entity("SwiftCourier.Models.PaymentMethod", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasAnnotation("Relational:Name", "UK_PaymentMethod");

                    b.HasAnnotation("Relational:TableName", "PaymentMethods");
                });

            modelBuilder.Entity("SwiftCourier.Models.PaymentMethodField", b =>
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
                        .HasAnnotation("Relational:Name", "UK_PaymentMethodField");

                    b.HasAnnotation("Relational:TableName", "PaymentMethodFields");
                });

            modelBuilder.Entity("SwiftCourier.Models.PaymentMethodFieldValue", b =>
                {
                    b.Property<int>("PaymentId");

                    b.Property<int>("PaymentMethodFieldId");

                    b.Property<string>("Value")
                        .IsRequired()
                        .HasAnnotation("Relational:ColumnType", "text");

                    b.HasKey("PaymentId", "PaymentMethodFieldId");

                    b.HasAnnotation("Relational:TableName", "PaymentMethodFieldValues");
                });

            modelBuilder.Entity("SwiftCourier.Models.Permission", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<string>("Description")
                        .IsRequired()
                        .HasAnnotation("Relational:ColumnType", "text");

                    b.Property<string>("Group")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasAnnotation("Relational:Name", "UK_Permission");

                    b.HasAnnotation("Relational:TableName", "Permissions");
                });

            modelBuilder.Entity("SwiftCourier.Models.Role", b =>
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
                        .HasAnnotation("Relational:Name", "RoleNameIndex");

                    b.HasAnnotation("Relational:TableName", "Roles");
                });

            modelBuilder.Entity("SwiftCourier.Models.RolePermission", b =>
                {
                    b.Property<int>("RoleId");

                    b.Property<int>("PermissionId");

                    b.HasKey("RoleId", "PermissionId");

                    b.HasAnnotation("Relational:TableName", "RolePermissions");
                });

            modelBuilder.Entity("SwiftCourier.Models.Service", b =>
                {
                    b.Property<int>("Id")
                        .ValueGeneratedOnAdd();

                    b.Property<decimal>("Cost")
                        .HasAnnotation("Relational:ColumnType", "decimal");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasAnnotation("MaxLength", 100);

                    b.HasKey("Id");

                    b.HasIndex("Name")
                        .IsUnique()
                        .HasAnnotation("Relational:Name", "UK_Service");

                    b.HasAnnotation("Relational:TableName", "Services");
                });

            modelBuilder.Entity("SwiftCourier.Models.Setting", b =>
                {
                    b.Property<string>("Name")
                        .HasAnnotation("MaxLength", 100);

                    b.Property<string>("DisplayName")
                        .HasAnnotation("Relational:ColumnType", "text");

                    b.Property<string>("Value")
                        .HasAnnotation("Relational:ColumnType", "text");

                    b.HasKey("Name");

                    b.HasAnnotation("Relational:TableName", "Settings");
                });

            modelBuilder.Entity("SwiftCourier.Models.User", b =>
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
                        .HasAnnotation("Relational:Name", "EmailIndex");

                    b.HasIndex("NormalizedUserName")
                        .HasAnnotation("Relational:Name", "UserNameIndex");

                    b.HasAnnotation("Relational:TableName", "Users");
                });

            modelBuilder.Entity("SwiftCourier.Models.UserPermission", b =>
                {
                    b.Property<int>("UserId");

                    b.Property<int>("PermissionId");

                    b.HasKey("UserId", "PermissionId");

                    b.HasAnnotation("Relational:TableName", "UserPermissions");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityRoleClaim<int>", b =>
                {
                    b.HasOne("SwiftCourier.Models.Role")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserClaim<int>", b =>
                {
                    b.HasOne("SwiftCourier.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserLogin<int>", b =>
                {
                    b.HasOne("SwiftCourier.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("Microsoft.AspNet.Identity.EntityFramework.IdentityUserRole<int>", b =>
                {
                    b.HasOne("SwiftCourier.Models.Role")
                        .WithMany()
                        .HasForeignKey("RoleId");

                    b.HasOne("SwiftCourier.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SwiftCourier.Models.Booking", b =>
                {
                    b.HasOne("SwiftCourier.Models.User")
                        .WithMany()
                        .HasForeignKey("CreatedByUserId");

                    b.HasOne("SwiftCourier.Models.Customer")
                        .WithMany()
                        .HasForeignKey("CustomerId");

                    b.HasOne("SwiftCourier.Models.Service")
                        .WithMany()
                        .HasForeignKey("ServiceId");
                });

            modelBuilder.Entity("SwiftCourier.Models.Invoice", b =>
                {
                    b.HasOne("SwiftCourier.Models.Booking")
                        .WithOne()
                        .HasForeignKey("SwiftCourier.Models.Invoice", "BookingId");
                });

            modelBuilder.Entity("SwiftCourier.Models.Package", b =>
                {
                    b.HasOne("SwiftCourier.Models.User")
                        .WithMany()
                        .HasForeignKey("AssignedToUserId");

                    b.HasOne("SwiftCourier.Models.Booking")
                        .WithOne()
                        .HasForeignKey("SwiftCourier.Models.Package", "BookingId");

                    b.HasOne("SwiftCourier.Models.User")
                        .WithMany()
                        .HasForeignKey("DeliveredByUserId");

                    b.HasOne("SwiftCourier.Models.PackageType")
                        .WithMany()
                        .HasForeignKey("PackageTypeId");
                });

            modelBuilder.Entity("SwiftCourier.Models.PackageLog", b =>
                {
                    b.HasOne("SwiftCourier.Models.Package")
                        .WithMany()
                        .HasForeignKey("PackageId");
                });

            modelBuilder.Entity("SwiftCourier.Models.Payment", b =>
                {
                    b.HasOne("SwiftCourier.Models.Invoice")
                        .WithMany()
                        .HasForeignKey("InvoiceId");

                    b.HasOne("SwiftCourier.Models.PaymentMethod")
                        .WithMany()
                        .HasForeignKey("PaymentMethodId");

                    b.HasOne("SwiftCourier.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });

            modelBuilder.Entity("SwiftCourier.Models.PaymentMethodField", b =>
                {
                    b.HasOne("SwiftCourier.Models.PaymentMethod")
                        .WithMany()
                        .HasForeignKey("PaymentMethodId");
                });

            modelBuilder.Entity("SwiftCourier.Models.PaymentMethodFieldValue", b =>
                {
                    b.HasOne("SwiftCourier.Models.Payment")
                        .WithMany()
                        .HasForeignKey("PaymentId");

                    b.HasOne("SwiftCourier.Models.PaymentMethodField")
                        .WithMany()
                        .HasForeignKey("PaymentMethodFieldId");
                });

            modelBuilder.Entity("SwiftCourier.Models.RolePermission", b =>
                {
                    b.HasOne("SwiftCourier.Models.Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId");

                    b.HasOne("SwiftCourier.Models.Role")
                        .WithMany()
                        .HasForeignKey("RoleId");
                });

            modelBuilder.Entity("SwiftCourier.Models.UserPermission", b =>
                {
                    b.HasOne("SwiftCourier.Models.Permission")
                        .WithMany()
                        .HasForeignKey("PermissionId");

                    b.HasOne("SwiftCourier.Models.User")
                        .WithMany()
                        .HasForeignKey("UserId");
                });
        }
    }
}
