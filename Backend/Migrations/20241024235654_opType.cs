using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace DDDNetCore.Migrations
{
    /// <inheritdoc />
    public partial class opType : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Families");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "RequiredStaffBySpecialization");

            migrationBuilder.RenameColumn(
                name: "Allergy",
                table: "PendingChanges",
                newName: "MedicalHistory");

            migrationBuilder.RenameColumn(
                name: "EmergencyContactName",
                table: "Patients",
                newName: "EmergencyContactInfo");

            migrationBuilder.RenameColumn(
                name: "Allergy",
                table: "Patients",
                newName: "MedicalHistory");

            migrationBuilder.RenameColumn(
                name: "OperationLastName",
                table: "OperationTypes",
                newName: "Name_LastName");

            migrationBuilder.RenameColumn(
                name: "OperationFirstName",
                table: "OperationTypes",
                newName: "OperationName");

            migrationBuilder.AddColumn<int>(
                name: "RequiredNumber",
                table: "OperationTypes",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "SpecializationId",
                table: "OperationTypes",
                type: "longtext",
                nullable: false)
                .Annotation("MySql:CharSet", "utf8mb4");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RequiredNumber",
                table: "OperationTypes");

            migrationBuilder.DropColumn(
                name: "SpecializationId",
                table: "OperationTypes");

            migrationBuilder.RenameColumn(
                name: "MedicalHistory",
                table: "PendingChanges",
                newName: "Allergy");

            migrationBuilder.RenameColumn(
                name: "EmergencyContactInfo",
                table: "Patients",
                newName: "EmergencyContactName");

            migrationBuilder.RenameColumn(
                name: "MedicalHistory",
                table: "Patients",
                newName: "Allergy");

            migrationBuilder.RenameColumn(
                name: "OperationName",
                table: "OperationTypes",
                newName: "OperationFirstName");

            migrationBuilder.RenameColumn(
                name: "Name_LastName",
                table: "OperationTypes",
                newName: "OperationLastName");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Families",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Families", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Active = table.Column<bool>(type: "tinyint(1)", nullable: false),
                    CategoryId = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Description = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "RequiredStaffBySpecialization",
                columns: table => new
                {
                    OperationTypeId = table.Column<string>(type: "varchar(255)", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4"),
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RequiredNumber = table.Column<int>(type: "int", nullable: false),
                    Specialization = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RequiredStaffBySpecialization", x => new { x.OperationTypeId, x.Id });
                    table.ForeignKey(
                        name: "FK_RequiredStaffBySpecialization_OperationTypes_OperationTypeId",
                        column: x => x.OperationTypeId,
                        principalTable: "OperationTypes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");
        }
    }
}
