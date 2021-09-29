using Microsoft.EntityFrameworkCore.Migrations;

namespace Galicia.Test.Infrastructure.Migrations
{
    public partial class InitialMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Persona",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nombre = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    Apellido = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    DNI = table.Column<int>(type: "int", nullable: false),
                    Telefono = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Persona", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Domicilio",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Calle = table.Column<string>(type: "nvarchar(70)", maxLength: 70, nullable: false),
                    Altura = table.Column<int>(type: "int", nullable: false),
                    Departamento = table.Column<string>(type: "nvarchar(12)", maxLength: 12, nullable: false),
                    CodigoPostal = table.Column<int>(type: "int", nullable: false),
                    IdPersona = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Domicilio", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Domicilio_Persona_IdPersona",
                        column: x => x.IdPersona,
                        principalTable: "Persona",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Domicilio_IdPersona",
                table: "Domicilio",
                column: "IdPersona",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Persona_DNI_Telefono",
                table: "Persona",
                columns: new[] { "DNI", "Telefono" },
                unique: true,
                filter: "[Telefono] IS NOT NULL");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Domicilio");

            migrationBuilder.DropTable(
                name: "Persona");
        }
    }
}
