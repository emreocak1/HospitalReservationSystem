using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace itec420.Migrations
{
    /// <inheritdoc />
    public partial class FirstCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Department",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DepartmentName = table.Column<string>(type: "TEXT", nullable: false),
                    DepartmentDescription = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Department", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Doctors",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    DoctorName = table.Column<string>(type: "TEXT", nullable: false),
                    DoctorDepartment = table.Column<string>(type: "TEXT", nullable: false),
                    PicOfDoc = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Doctors", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Department",
                columns: new[] { "Id", "DepartmentDescription", "DepartmentName" },
                values: new object[,]
                {
                    { 1, "A", "Cardiology" },
                    { 2, "B", "Neurology" },
                    { 3, "C", "Oncology" },
                    { 4, "D", "Dermatology" }
                });

            migrationBuilder.InsertData(
                table: "Doctors",
                columns: new[] { "Id", "DoctorDepartment", "DoctorName", "PicOfDoc" },
                values: new object[,]
                {
                    { 1, "Cardiology", "Michael Jordan", "/img/1.png" },
                    { 2, "Neurology", "Jalen Brunson", "/img/2.png" },
                    { 3, "Cardiology", "Lebron James", "/img/3.png" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Department");

            migrationBuilder.DropTable(
                name: "Doctors");
        }
    }
}
