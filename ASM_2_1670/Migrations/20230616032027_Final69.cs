using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASM_2_1670.Migrations
{
    /// <inheritdoc />
    public partial class Final69 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ViewCount",
                table: "Product",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ViewCount",
                table: "Product");
        }
    }
}
