using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGDP.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class Initia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Appointment_Patient_patientId",
                table: "Appointment");

            migrationBuilder.DropIndex(
                name: "IX_Appointment_patientId",
                table: "Appointment");

            migrationBuilder.DropColumn(
                name: "patientId",
                table: "Appointment");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "patientId",
                table: "Appointment",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_patientId",
                table: "Appointment",
                column: "patientId");

            migrationBuilder.AddForeignKey(
                name: "FK_Appointment_Patient_patientId",
                table: "Appointment",
                column: "patientId",
                principalTable: "Patient",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
