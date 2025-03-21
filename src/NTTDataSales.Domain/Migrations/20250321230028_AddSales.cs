using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NTTDataSales.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddSales : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Sales",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    DATE = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    TOTALVALUE = table.Column<decimal>(type: "numeric", nullable: false),
                    TOTALDISCOUNT = table.Column<decimal>(type: "numeric", nullable: false),
                    SALECANCELLED = table.Column<bool>(type: "boolean", nullable: false),
                    CUSTOMERID = table.Column<int>(type: "integer", nullable: false),
                    BRANCHID = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Sales", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Sales_Branches_BRANCHID",
                        column: x => x.BRANCHID,
                        principalTable: "Branches",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Sales_Customers_CUSTOMERID",
                        column: x => x.CUSTOMERID,
                        principalTable: "Customers",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Sales_BRANCHID",
                table: "Sales",
                column: "BRANCHID");

            migrationBuilder.CreateIndex(
                name: "IX_Sales_CUSTOMERID",
                table: "Sales",
                column: "CUSTOMERID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Sales");
        }
    }
}
