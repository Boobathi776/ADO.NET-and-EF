using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace EF_Core_DbFirst.Migrations
{
    /// <inheritdoc />
    public partial class AddedARegisterNumber3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "RegisterNumber",
                table: "Student",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RegisterNumber",
                table: "Student");
        }
    }
}
