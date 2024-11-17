using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GlucoSeeTracker.Migrations
{
    public partial class Models : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Landings",
                columns: table => new
                {
                    UserID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Password = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Landings", x => x.UserID);
                });

            migrationBuilder.CreateTable(
                name: "Dashboards",
                columns: table => new
                {
                    DashID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    FirstName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Age = table.Column<int>(type: "int", nullable: false),
                    LastReading = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    UserID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Dashboards", x => x.DashID);
                    table.ForeignKey(
                        name: "FK_Dashboards_Landings_UserID",
                        column: x => x.UserID,
                        principalTable: "Landings",
                        principalColumn: "UserID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "GlucoRecords",
                columns: table => new
                {
                    ReadingID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    GlucoLevel = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DateTime = table.Column<DateTime>(type: "datetime2", nullable: false),
                    PrePostMeal = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DashID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_GlucoRecords", x => x.ReadingID);
                    table.ForeignKey(
                        name: "FK_GlucoRecords_Dashboards_DashID",
                        column: x => x.DashID,
                        principalTable: "Dashboards",
                        principalColumn: "DashID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Landings",
                columns: new[] { "UserID", "Password", "Username" },
                values: new object[] { 1, "Password1", "First" });

            migrationBuilder.InsertData(
                table: "Landings",
                columns: new[] { "UserID", "Password", "Username" },
                values: new object[] { 2, "Password2", "Second" });

            migrationBuilder.InsertData(
                table: "Landings",
                columns: new[] { "UserID", "Password", "Username" },
                values: new object[] { 3, "Password3", "Third" });

            migrationBuilder.InsertData(
                table: "Dashboards",
                columns: new[] { "DashID", "Age", "FirstName", "LastName", "LastReading", "UserID" },
                values: new object[] { 1, 40, "Will", "Smith", 7m, 1 });

            migrationBuilder.InsertData(
                table: "Dashboards",
                columns: new[] { "DashID", "Age", "FirstName", "LastName", "LastReading", "UserID" },
                values: new object[] { 2, 30, "Sam", "Smith", 8.5m, 2 });

            migrationBuilder.InsertData(
                table: "Dashboards",
                columns: new[] { "DashID", "Age", "FirstName", "LastName", "LastReading", "UserID" },
                values: new object[] { 3, 30, "Taylor", "Swift", 5.6m, 3 });

            migrationBuilder.InsertData(
                table: "GlucoRecords",
                columns: new[] { "ReadingID", "DashID", "DateTime", "GlucoLevel", "PrePostMeal" },
                values: new object[] { 1, 1, new DateTime(2022, 3, 12, 8, 0, 0, 0, DateTimeKind.Unspecified), 7.5m, "Before" });

            migrationBuilder.InsertData(
                table: "GlucoRecords",
                columns: new[] { "ReadingID", "DashID", "DateTime", "GlucoLevel", "PrePostMeal" },
                values: new object[] { 2, 2, new DateTime(2023, 7, 15, 10, 0, 0, 0, DateTimeKind.Unspecified), 8.5m, "After" });

            migrationBuilder.InsertData(
                table: "GlucoRecords",
                columns: new[] { "ReadingID", "DashID", "DateTime", "GlucoLevel", "PrePostMeal" },
                values: new object[] { 3, 3, new DateTime(2023, 9, 17, 13, 0, 0, 0, DateTimeKind.Unspecified), 7m, "After" });

            migrationBuilder.CreateIndex(
                name: "IX_Dashboards_UserID",
                table: "Dashboards",
                column: "UserID");

            migrationBuilder.CreateIndex(
                name: "IX_GlucoRecords_DashID",
                table: "GlucoRecords",
                column: "DashID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "GlucoRecords");

            migrationBuilder.DropTable(
                name: "Dashboards");

            migrationBuilder.DropTable(
                name: "Landings");
        }
    }
}
