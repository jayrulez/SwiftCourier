using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SwiftCourier.Migrations
{
    public partial class CustomerAllowEmptyEmailAddress : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "UK_Customer_Email",
                table: "Customers");

            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "Customers",
                maxLength: 50,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "EmailAddress",
                table: "Customers",
                maxLength: 50,
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "UK_Customer_Email",
                table: "Customers",
                column: "EmailAddress",
                unique: true);
        }
    }
}
