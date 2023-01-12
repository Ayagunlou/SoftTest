using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class addv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Unit",
                table: "preOrders");

            migrationBuilder.AddColumn<long>(
                name: "Total",
                table: "preOrders",
                type: "bigint",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Total",
                table: "preOrders");

            migrationBuilder.AddColumn<string>(
                name: "Unit",
                table: "preOrders",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
