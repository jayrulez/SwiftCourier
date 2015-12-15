using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace SwiftCourier.Migrations
{
    public partial class Pending : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_IdentityRoleClaim<int>_Role_RoleId", table: "RoleClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserClaim<int>_User_UserId", table: "UserClaims");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserLogin<int>_User_UserId", table: "UserLogins");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<int>_Role_RoleId", table: "UserRoles");
            migrationBuilder.DropForeignKey(name: "FK_IdentityUserRole<int>_User_UserId", table: "UserRoles");
            migrationBuilder.DropForeignKey(name: "FK_Booking_Customer_CustomerId", table: "Bookings");
            migrationBuilder.DropForeignKey(name: "FK_Delivery_Booking_BookingId", table: "Deliveries");
            migrationBuilder.DropForeignKey(name: "FK_Invoice_Booking_BookingId", table: "Invoices");
            migrationBuilder.DropForeignKey(name: "FK_Package_Booking_BookingId", table: "Packages");
            migrationBuilder.DropForeignKey(name: "FK_PackageLog_Package_PackageId", table: "PackageLogs");
            migrationBuilder.DropForeignKey(name: "FK_PackageLog_User_UserId", table: "PackageLogs");
            migrationBuilder.DropForeignKey(name: "FK_Payment_Invoice_InvoiceId", table: "Payments");
            migrationBuilder.DropForeignKey(name: "FK_Payment_PaymentMethod_PaymentMethodId", table: "Payments");
            migrationBuilder.DropForeignKey(name: "FK_PaymentMethodField_PaymentMethod_PaymentMethodId", table: "PaymentMethodFields");
            migrationBuilder.DropForeignKey(name: "FK_PaymentMethodFieldValue_PaymentMethodField_PaymentMethodFieldId", table: "PaymentMethodFieldValues");
            migrationBuilder.DropForeignKey(name: "FK_Pickup_Booking_BookingId", table: "Pickups");
            migrationBuilder.DropForeignKey(name: "FK_UserPermission_Permission_PermissionId", table: "UserPermissions");
            migrationBuilder.DropForeignKey(name: "FK_UserPermission_User_UserId", table: "UserPermissions");
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
                name: "FK_Delivery_Booking_BookingId",
                table: "Deliveries",
                column: "BookingId",
                principalTable: "Bookings",
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
                name: "FK_Pickup_Booking_BookingId",
                table: "Pickups",
                column: "BookingId",
                principalTable: "Bookings",
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
            migrationBuilder.DropForeignKey(name: "FK_Delivery_Booking_BookingId", table: "Deliveries");
            migrationBuilder.DropForeignKey(name: "FK_Invoice_Booking_BookingId", table: "Invoices");
            migrationBuilder.DropForeignKey(name: "FK_Package_Booking_BookingId", table: "Packages");
            migrationBuilder.DropForeignKey(name: "FK_PackageLog_Package_PackageId", table: "PackageLogs");
            migrationBuilder.DropForeignKey(name: "FK_PackageLog_User_UserId", table: "PackageLogs");
            migrationBuilder.DropForeignKey(name: "FK_Payment_Invoice_InvoiceId", table: "Payments");
            migrationBuilder.DropForeignKey(name: "FK_Payment_PaymentMethod_PaymentMethodId", table: "Payments");
            migrationBuilder.DropForeignKey(name: "FK_PaymentMethodField_PaymentMethod_PaymentMethodId", table: "PaymentMethodFields");
            migrationBuilder.DropForeignKey(name: "FK_PaymentMethodFieldValue_PaymentMethodField_PaymentMethodFieldId", table: "PaymentMethodFieldValues");
            migrationBuilder.DropForeignKey(name: "FK_Pickup_Booking_BookingId", table: "Pickups");
            migrationBuilder.DropForeignKey(name: "FK_UserPermission_Permission_PermissionId", table: "UserPermissions");
            migrationBuilder.DropForeignKey(name: "FK_UserPermission_User_UserId", table: "UserPermissions");
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
                name: "FK_Delivery_Booking_BookingId",
                table: "Deliveries",
                column: "BookingId",
                principalTable: "Bookings",
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
                name: "FK_Pickup_Booking_BookingId",
                table: "Pickups",
                column: "BookingId",
                principalTable: "Bookings",
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
