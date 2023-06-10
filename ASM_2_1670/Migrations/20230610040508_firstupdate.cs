using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ASM_2_1670.Migrations
{
    /// <inheritdoc />
    public partial class firstupdate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "User",
                columns: table => new
                {
                    UserId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    UserEmail = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    UserPassword = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    UserPhone = table.Column<string>(type: "nvarchar(50)", nullable: true),
                    UserAddress = table.Column<string>(type: "nvarchar(100)", nullable: true),
                    UserRole = table.Column<string>(type: "nvarchar(50)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_User", x => x.UserId);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "User");
        }
    }
}
