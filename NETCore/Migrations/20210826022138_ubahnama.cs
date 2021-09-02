using Microsoft.EntityFrameworkCore.Migrations;

namespace NETCore.Migrations
{
    public partial class ubahnama : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_Persons",
                table: "Persons");

            migrationBuilder.RenameTable(
                name: "Persons",
                newName: "tb_m_person");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_person",
                table: "tb_m_person",
                column: "NIK");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_person",
                table: "tb_m_person");

            migrationBuilder.RenameTable(
                name: "tb_m_person",
                newName: "Persons");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Persons",
                table: "Persons",
                column: "NIK");
        }
    }
}
