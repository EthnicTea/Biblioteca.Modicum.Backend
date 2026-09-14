using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Biblioteca.Modicum.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AgregarEntidadPrestamo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "prestamos",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    id_libro = table.Column<int>(type: "integer", nullable: false),
                    id_usuario = table.Column<string>(type: "character varying(450)", maxLength: 450, nullable: false),
                    fecha_prestamo = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_devolucion_esperada = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    fecha_devolucion_real = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    estado_prestamo = table.Column<string>(type: "character varying(50)", maxLength: 50, nullable: false),
                    observaciones = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_prestamos", x => x.id);
                    table.ForeignKey(
                        name: "fk_prestamos_aspnetusers_id_usuario",
                        column: x => x.id_usuario,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "fk_prestamos_libros_id_libro",
                        column: x => x.id_libro,
                        principalTable: "libro",
                        principalColumn: "id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_prestamos_id_libro",
                table: "prestamos",
                column: "id_libro");

            migrationBuilder.CreateIndex(
                name: "IX_prestamos_id_usuario",
                table: "prestamos",
                column: "id_usuario");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "prestamos");
        }
    }
}
