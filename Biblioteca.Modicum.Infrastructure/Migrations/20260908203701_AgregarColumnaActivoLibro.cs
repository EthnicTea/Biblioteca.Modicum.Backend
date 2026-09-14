using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca.Modicum.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarColumnaActivoLibro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "activo",
                table: "libro",
                type: "boolean",
                nullable: false,
                defaultValue: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "activo",
                table: "libro");
        }
    }
}
