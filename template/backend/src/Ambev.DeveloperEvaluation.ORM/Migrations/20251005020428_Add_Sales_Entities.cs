﻿using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ambev.DeveloperEvaluation.ORM.Migrations;

/// <inheritdoc />
public partial class Add_Sales_Entities : Migration
{
    /// <inheritdoc />
    protected override void Up(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.CreateTable(
            name: "Sales",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                SaleDate = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                CustomerName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                TotalValue = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                BranchName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                IsCancelled = table.Column<bool>(type: "boolean", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_Sales", x => x.Id);
            });

        migrationBuilder.CreateTable(
            name: "SaleItems",
            columns: table => new
            {
                Id = table.Column<Guid>(type: "uuid", nullable: false),
                ProductId = table.Column<Guid>(type: "uuid", nullable: false),
                ProductName = table.Column<string>(type: "character varying(100)", maxLength: 100, nullable: false),
                Quantity = table.Column<int>(type: "integer", nullable: false),
                UnitPrice = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                Discount = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                TotalValue = table.Column<decimal>(type: "numeric(18,2)", nullable: false),
                IsCancelled = table.Column<bool>(type: "boolean", nullable: false),
                SaleId = table.Column<Guid>(type: "uuid", nullable: false)
            },
            constraints: table =>
            {
                table.PrimaryKey("PK_SaleItems", x => x.Id);
                table.ForeignKey(
                    name: "FK_SaleItems_Sales_SaleId",
                    column: x => x.SaleId,
                    principalTable: "Sales",
                    principalColumn: "Id",
                    onDelete: ReferentialAction.Cascade);
            });

        migrationBuilder.CreateIndex(
            name: "IX_SaleItems_SaleId",
            table: "SaleItems",
            column: "SaleId");
    }

    /// <inheritdoc />
    protected override void Down(MigrationBuilder migrationBuilder)
    {
        migrationBuilder.DropTable(
            name: "SaleItems");

        migrationBuilder.DropTable(
            name: "Sales");
    }
}
