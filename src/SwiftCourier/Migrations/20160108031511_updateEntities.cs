using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;
using SwiftCourier.Models;

namespace SwiftCourier.Migrations
{
    public partial class updateEntities : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<int>_Role_RoleId", table: "RoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<int>_User_UserId", table: "UserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<int>_User_UserId", table: "UserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<int>_Role_RoleId", table: "UserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<int>_User_UserId", table: "UserRoles");
            migrationBuilder.DropForeignKey(name: "FK_Booking_Customer_CustomerId", table: "Bookings");
            migrationBuilder.DropForeignKey(name: "FK_Invoice_Booking_BookingId", table: "Invoices");
            migrationBuilder.DropForeignKey(name: "FK_Package_Booking_BookingId", table: "Packages");
            migrationBuilder.DropForeignKey(name: "FK_PackageLog_Package_PackageId", table: "PackageLogs");
            migrationBuilder.DropForeignKey(name: "FK_PackageLog_User_UserId", table: "PackageLogs");
            migrationBuilder.DropForeignKey(name: "FK_Payment_Invoice_InvoiceId", table: "Payments");
            migrationBuilder.DropForeignKey(name: "FK_Payment_PaymentMethod_PaymentMethodId", table: "Payments");
            migrationBuilder.DropForeignKey(name: "FK_PaymentMethodField_PaymentMethod_PaymentMethodId", table: "PaymentMethodFields");
            migrationBuilder.DropForeignKey(name: "FK_PaymentMethodFieldValue_PaymentMethodField_PaymentMethodFieldId", table: "PaymentMethodFieldValues");
            migrationBuilder.DropForeignKey(name: "FK_RolePermission_Permission_PermissionId", table: "RolePermissions");
            migrationBuilder.DropForeignKey(name: "FK_RolePermission_Role_RoleId", table: "RolePermissions");
            migrationBuilder.DropForeignKey(name: "FK_UserPermission_Permission_PermissionId", table: "UserPermissions");
            migrationBuilder.DropForeignKey(name: "FK_UserPermission_User_UserId", table: "UserPermissions");
            migrationBuilder.DropTable("Deliveries");
            migrationBuilder.DropTable("Pickups");
            migrationBuilder.AddColumn<string>(
                name: "ConsigneeAddress",
                table: "Bookings",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "ConsigneeContactNumber",
                table: "Bookings",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "ConsigneeName",
                table: "Bookings",
                nullable: true);
            migrationBuilder.AddColumn<int>(
                name: "DeliveryStatus",
                table: "Bookings",
                nullable: false,
                defaultValue: DeliveryStatus.Pending);
            migrationBuilder.AddColumn<string>(
                name: "PickupAddress",
                table: "Bookings",
                nullable: true);
            migrationBuilder.AddColumn<string>(
                name: "PickupContactNumber",
                table: "Bookings",
                nullable: true);
            migrationBuilder.AddColumn<int>(
                name: "PickupStatus",
                table: "Bookings",
                nullable: false,
                defaultValue: PickupStatus.Pending);
            migrationBuilder.AddColumn<int>(
                name: "ServiceId",
                table: "Bookings",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRoleClaim<int>_Role_RoleId",
                table: "RoleClaims",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserClaim<int>_User_UserId",
                table: "UserClaims",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserLogin<int>_User_UserId",
                table: "UserLogins",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<int>_Role_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<int>_User_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Customer_CustomerId",
                table: "Bookings",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Service_ServiceId",
                table: "Bookings",
                column: "ServiceId",
                principalTable: "Services",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Booking_BookingId",
                table: "Invoices",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Package_Booking_BookingId",
                table: "Packages",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_PackageLog_Package_PackageId",
                table: "PackageLogs",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_PackageLog_User_UserId",
                table: "PackageLogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Invoice_InvoiceId",
                table: "Payments",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Payment_PaymentMethod_PaymentMethodId",
                table: "Payments",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethodField_PaymentMethod_PaymentMethodId",
                table: "PaymentMethodFields",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethodFieldValue_PaymentMethodField_PaymentMethodFieldId",
                table: "PaymentMethodFieldValues",
                column: "PaymentMethodFieldId",
                principalTable: "PaymentMethodFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Permission_PermissionId",
                table: "RolePermissions",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Role_RoleId",
                table: "RolePermissions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_UserPermission_Permission_PermissionId",
                table: "UserPermissions",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_UserPermission_User_UserId",
                table: "UserPermissions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<int>_Role_RoleId", table: "RoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<int>_User_UserId", table: "UserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<int>_User_UserId", table: "UserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<int>_Role_RoleId", table: "UserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<int>_User_UserId", table: "UserRoles");
            migrationBuilder.DropForeignKey(name: "FK_Booking_Customer_CustomerId", table: "Bookings");
            migrationBuilder.DropForeignKey(name: "FK_Booking_Service_ServiceId", table: "Bookings");
            migrationBuilder.DropForeignKey(name: "FK_Invoice_Booking_BookingId", table: "Invoices");
            migrationBuilder.DropForeignKey(name: "FK_Package_Booking_BookingId", table: "Packages");
            migrationBuilder.DropForeignKey(name: "FK_PackageLog_Package_PackageId", table: "PackageLogs");
            migrationBuilder.DropForeignKey(name: "FK_PackageLog_User_UserId", table: "PackageLogs");
            migrationBuilder.DropForeignKey(name: "FK_Payment_Invoice_InvoiceId", table: "Payments");
            migrationBuilder.DropForeignKey(name: "FK_Payment_PaymentMethod_PaymentMethodId", table: "Payments");
            migrationBuilder.DropForeignKey(name: "FK_PaymentMethodField_PaymentMethod_PaymentMethodId", table: "PaymentMethodFields");
            migrationBuilder.DropForeignKey(name: "FK_PaymentMethodFieldValue_PaymentMethodField_PaymentMethodFieldId", table: "PaymentMethodFieldValues");
            migrationBuilder.DropForeignKey(name: "FK_RolePermission_Permission_PermissionId", table: "RolePermissions");
            migrationBuilder.DropForeignKey(name: "FK_RolePermission_Role_RoleId", table: "RolePermissions");
            migrationBuilder.DropForeignKey(name: "FK_UserPermission_Permission_PermissionId", table: "UserPermissions");
            migrationBuilder.DropForeignKey(name: "FK_UserPermission_User_UserId", table: "UserPermissions");
            migrationBuilder.DropColumn(name: "ConsigneeAddress", table: "Bookings");
            migrationBuilder.DropColumn(name: "ConsigneeContactNumber", table: "Bookings");
            migrationBuilder.DropColumn(name: "ConsigneeName", table: "Bookings");
            migrationBuilder.DropColumn(name: "DeliveryStatus", table: "Bookings");
            migrationBuilder.DropColumn(name: "PickupAddress", table: "Bookings");
            migrationBuilder.DropColumn(name: "PickupContactNumber", table: "Bookings");
            migrationBuilder.DropColumn(name: "PickupStatus", table: "Bookings");
            migrationBuilder.DropColumn(name: "ServiceId", table: "Bookings");
            migrationBuilder.CreateTable(
                name: "Deliveries",
                columns: table => new
                {
                    BookingId = table.Column<int>(nullable: false),
                    ConsigneeAddress = table.Column<string>(type: "text", nullable: false),
                    ConsigneeContactNumber = table.Column<string>(nullable: false),
                    ConsigneeName = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Delivery", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Delivery_Booking_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.CreateTable(
                name: "Pickups",
                columns: table => new
                {
                    BookingId = table.Column<int>(nullable: false),
                    Address = table.Column<string>(type: "text", nullable: false),
                    ContactNumber = table.Column<string>(nullable: false),
                    Status = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Pickup", x => x.BookingId);
                    table.ForeignKey(
                        name: "FK_Pickup_Booking_BookingId",
                        column: x => x.BookingId,
                        principalTable: "Bookings",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityRoleClaim<int>_Role_RoleId",
                table: "RoleClaims",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserClaim<int>_User_UserId",
                table: "UserClaims",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserLogin<int>_User_UserId",
                table: "UserLogins",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<int>_Role_RoleId",
                table: "UserRoles",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_IdentityUserRole<int>_User_UserId",
                table: "UserRoles",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Booking_Customer_CustomerId",
                table: "Bookings",
                column: "CustomerId",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Invoice_Booking_BookingId",
                table: "Invoices",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Package_Booking_BookingId",
                table: "Packages",
                column: "BookingId",
                principalTable: "Bookings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_PackageLog_Package_PackageId",
                table: "PackageLogs",
                column: "PackageId",
                principalTable: "Packages",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_PackageLog_User_UserId",
                table: "PackageLogs",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Payment_Invoice_InvoiceId",
                table: "Payments",
                column: "InvoiceId",
                principalTable: "Invoices",
                principalColumn: "BookingId",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_Payment_PaymentMethod_PaymentMethodId",
                table: "Payments",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethodField_PaymentMethod_PaymentMethodId",
                table: "PaymentMethodFields",
                column: "PaymentMethodId",
                principalTable: "PaymentMethods",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_PaymentMethodFieldValue_PaymentMethodField_PaymentMethodFieldId",
                table: "PaymentMethodFieldValues",
                column: "PaymentMethodFieldId",
                principalTable: "PaymentMethodFields",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Permission_PermissionId",
                table: "RolePermissions",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_RolePermission_Role_RoleId",
                table: "RolePermissions",
                column: "RoleId",
                principalTable: "Roles",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_UserPermission_Permission_PermissionId",
                table: "UserPermissions",
                column: "PermissionId",
                principalTable: "Permissions",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
            migrationBuilder.AddForeignKey(
                name: "FK_UserPermission_User_UserId",
                table: "UserPermissions",
                column: "UserId",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
