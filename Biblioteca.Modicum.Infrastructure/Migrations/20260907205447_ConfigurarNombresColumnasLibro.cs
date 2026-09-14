using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Biblioteca.Modicum.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class ConfigurarNombresColumnasLibro : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Libros",
                table: "Libros");

            migrationBuilder.RenameTable(
                name: "Libros",
                newName: "libro");

            migrationBuilder.RenameColumn(
                name: "Titulo",
                table: "libro",
                newName: "titulo");

            migrationBuilder.RenameColumn(
                name: "Estado",
                table: "libro",
                newName: "estado");

            migrationBuilder.RenameColumn(
                name: "Calidad",
                table: "libro",
                newName: "calidad");

            migrationBuilder.RenameColumn(
                name: "Autor",
                table: "libro",
                newName: "autor");

            migrationBuilder.RenameColumn(
                name: "Id",
                table: "libro",
                newName: "id");

            migrationBuilder.AlterColumn<string>(
                name: "estado",
                table: "libro",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "calidad",
                table: "libro",
                type: "character varying(100)",
                maxLength: 100,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(20)",
                oldMaxLength: 20);

            migrationBuilder.AddPrimaryKey(
                name: "PK_libro",
                table: "libro",
                column: "id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_libro",
                table: "libro");

            migrationBuilder.RenameTable(
                name: "libro",
                newName: "Libros");

            migrationBuilder.RenameColumn(
                name: "titulo",
                table: "Libros",
                newName: "Titulo");

            migrationBuilder.RenameColumn(
                name: "estado",
                table: "Libros",
                newName: "Estado");

            migrationBuilder.RenameColumn(
                name: "calidad",
                table: "Libros",
                newName: "Calidad");

            migrationBuilder.RenameColumn(
                name: "autor",
                table: "Libros",
                newName: "Autor");

            migrationBuilder.RenameColumn(
                name: "id",
                table: "Libros",
                newName: "Id");

            migrationBuilder.AlterColumn<string>(
                name: "Estado",
                table: "Libros",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "Calidad",
                table: "Libros",
                type: "character varying(20)",
                maxLength: 20,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "character varying(100)",
                oldMaxLength: 100);

            migrationBuilder.AddPrimaryKey(
                name: "PK_Libros",
                table: "Libros",
                column: "Id");
        }
    }
}
