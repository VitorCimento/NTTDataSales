using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace NTTDataSales.Domain.Migrations
{
    /// <inheritdoc />
    public partial class AddSaleItems : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SaleItem",
                columns: table => new
                {
                    ID = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    QUATITY = table.Column<int>(type: "integer", nullable: false),
                    UNITARYPRICE = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    TOTALPRICE = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    DISCOUNT = table.Column<decimal>(type: "numeric(12,2)", precision: 12, scale: 2, nullable: false),
                    ITEMCANCELLED = table.Column<bool>(type: "boolean", nullable: false),
                    PRODUCTID = table.Column<int>(type: "integer", nullable: false),
                    SaleID = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaleItem", x => x.ID);
                    table.ForeignKey(
                        name: "FK_SaleItem_Products_PRODUCTID",
                        column: x => x.PRODUCTID,
                        principalTable: "Products",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_SaleItem_Sales_SaleID",
                        column: x => x.SaleID,
                        principalTable: "Sales",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaleItem_PRODUCTID",
                table: "SaleItem",
                column: "PRODUCTID");

            migrationBuilder.CreateIndex(
                name: "IX_SaleItem_SaleID",
                table: "SaleItem",
                column: "SaleID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaleItem");
        }
    }
}
