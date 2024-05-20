using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGDP.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class RenameClaveResultColumn : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<int>(
                name: "clave",
                table: "Result",
                type: "int",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "clave",
                table: "Result");

            migrationBuilder.RenameColumn(
                name: "Patiente",
                table: "Appointment",
                newName: "PatienteName");
        }
    }
}
