using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using Microsoft.EntityFrameworkCore.Metadata;

namespace CheckBook.Data.Migrations
{
    public partial class Account : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Account",
                columns: table => new
                {
                    ID = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    accountName = table.Column<string>(maxLength: 60, nullable: true),
                    active = table.Column<bool>(nullable: false),
                    desciption = table.Column<string>(maxLength: 25, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Account", x => x.ID);
                });

            migrationBuilder.AlterColumn<string>(
                name: "paymentMethod",
                table: "Debit",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "Debit",
                maxLength: 60,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "paymentForm",
                table: "Credit",
                maxLength: 25,
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "Credit",
                maxLength: 60,
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Account");

            migrationBuilder.AlterColumn<string>(
                name: "paymentMethod",
                table: "Debit",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "Debit",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "paymentForm",
                table: "Credit",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "description",
                table: "Credit",
                nullable: true);
        }
    }
}
