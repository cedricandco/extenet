using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExtenetDemo.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Client",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    EnrollmentDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Client", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Vendor",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    HireDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Vendor", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Departments",
                columns: table => new
                {
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Budget = table.Column<decimal>(type: "money", nullable: false),
                    StartDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    InstructorID = table.Column<int>(type: "int", nullable: true),
                    ConcurrencyToken = table.Column<byte[]>(type: "rowversion", rowVersion: true, nullable: true),
                    AdministratorID = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Departments", x => x.DepartmentID);
                    table.ForeignKey(
                        name: "FK_Departments_Vendor_AdministratorID",
                        column: x => x.AdministratorID,
                        principalTable: "Vendor",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateTable(
                name: "OfficeAssignments",
                columns: table => new
                {
                    VendorID = table.Column<int>(type: "int", nullable: false),
                    Location = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OfficeAssignments", x => x.VendorID);
                    table.ForeignKey(
                        name: "FK_OfficeAssignments_Vendor_VendorID",
                        column: x => x.VendorID,
                        principalTable: "Vendor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Item",
                columns: table => new
                {
                    ItemID = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    Price = table.Column<int>(type: "int", nullable: false),
                    DepartmentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Item", x => x.ItemID);
                    table.ForeignKey(
                        name: "FK_Item_Departments_DepartmentID",
                        column: x => x.DepartmentID,
                        principalTable: "Departments",
                        principalColumn: "DepartmentID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Enrollments",
                columns: table => new
                {
                    SaleID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ItemID = table.Column<int>(type: "int", nullable: false),
                    ClientID = table.Column<int>(type: "int", nullable: false),
                    Grade = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Enrollments", x => x.SaleID);
                    table.ForeignKey(
                        name: "FK_Enrollments_Client_ClientID",
                        column: x => x.ClientID,
                        principalTable: "Client",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Enrollments_Item_ItemID",
                        column: x => x.ItemID,
                        principalTable: "Item",
                        principalColumn: "ItemID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ItemVendor",
                columns: table => new
                {
                    CoursesItemID = table.Column<int>(type: "int", nullable: false),
                    VendorsID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemVendor", x => new { x.CoursesItemID, x.VendorsID });
                    table.ForeignKey(
                        name: "FK_ItemVendor_Item_CoursesItemID",
                        column: x => x.CoursesItemID,
                        principalTable: "Item",
                        principalColumn: "ItemID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ItemVendor_Vendor_VendorsID",
                        column: x => x.VendorsID,
                        principalTable: "Vendor",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Departments_AdministratorID",
                table: "Departments",
                column: "AdministratorID");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_ClientID",
                table: "Enrollments",
                column: "ClientID");

            migrationBuilder.CreateIndex(
                name: "IX_Enrollments_ItemID",
                table: "Enrollments",
                column: "ItemID");

            migrationBuilder.CreateIndex(
                name: "IX_Item_DepartmentID",
                table: "Item",
                column: "DepartmentID");

            migrationBuilder.CreateIndex(
                name: "IX_ItemVendor_VendorsID",
                table: "ItemVendor",
                column: "VendorsID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Enrollments");

            migrationBuilder.DropTable(
                name: "ItemVendor");

            migrationBuilder.DropTable(
                name: "OfficeAssignments");

            migrationBuilder.DropTable(
                name: "Client");

            migrationBuilder.DropTable(
                name: "Item");

            migrationBuilder.DropTable(
                name: "Departments");

            migrationBuilder.DropTable(
                name: "Vendor");
        }
    }
}
