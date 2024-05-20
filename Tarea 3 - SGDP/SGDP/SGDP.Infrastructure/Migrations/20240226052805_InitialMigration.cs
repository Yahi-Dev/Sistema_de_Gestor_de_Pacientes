using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SGDP.Infrastructure.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class InitialMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "LabTests",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TestName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_LabTests", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Medicos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Phone = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Specialization = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    IdCedula = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePathMedico = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Medicos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Patient",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ImagePathMedico = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Phone = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Direccion = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Cedula = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Sex = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    FechaDeNacimiento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    EsAlergico = table.Column<string>(name: "¿Es Alergico?", type: "nvarchar(200)", maxLength: 200, nullable: false),
                    EsFumador = table.Column<string>(name: "¿Es Fumador?", type: "nvarchar(200)", maxLength: 200, nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Patient", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Users",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    TypeUser = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreateBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Created = table.Column<DateTime>(type: "datetime2", nullable: false),
                    LastModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModified = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(150)", maxLength: 150, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Email = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ImagePathMedico = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Users", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Appointment",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Patiente = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    PacienteId = table.Column<int>(type: "int", nullable: true),
                    patientId = table.Column<int>(type: "int", nullable: false),
                    Doctor = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    Date = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Time = table.Column<TimeSpan>(type: "time", nullable: false),
                    why = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    NameTest = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    clave = table.Column<int>(type: "int", nullable: true),
                    medicoID = table.Column<int>(type: "int", nullable: true),
                    LabTestId = table.Column<int>(type: "int", nullable: true),
                    LabTestsId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Appointment", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Appointment_LabTests_LabTestId",
                        column: x => x.LabTestId,
                        principalTable: "LabTests",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointment_LabTests_LabTestsId",
                        column: x => x.LabTestsId,
                        principalTable: "LabTests",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Appointment_Medicos_medicoID",
                        column: x => x.medicoID,
                        principalTable: "Medicos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_Appointment_Patient_patientId",
                        column: x => x.patientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Result",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    NamePatient = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    LastNamePatient = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    cedulapatient = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    patientId = table.Column<int>(type: "int", nullable: true),
                    AppointmentId = table.Column<int>(type: "int", nullable: true),
                    NameLabTest = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: false),
                    ResultTest = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Result", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Result_Patient_patientId",
                        column: x => x.patientId,
                        principalTable: "Patient",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_LabTestId",
                table: "Appointment",
                column: "LabTestId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_LabTestsId",
                table: "Appointment",
                column: "LabTestsId");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_medicoID",
                table: "Appointment",
                column: "medicoID");

            migrationBuilder.CreateIndex(
                name: "IX_Appointment_patientId",
                table: "Appointment",
                column: "patientId");

            migrationBuilder.CreateIndex(
                name: "IX_Result_patientId",
                table: "Result",
                column: "patientId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Appointment");

            migrationBuilder.DropTable(
                name: "Result");

            migrationBuilder.DropTable(
                name: "Users");

            migrationBuilder.DropTable(
                name: "LabTests");

            migrationBuilder.DropTable(
                name: "Medicos");

            migrationBuilder.DropTable(
                name: "Patient");
        }
    }
}
