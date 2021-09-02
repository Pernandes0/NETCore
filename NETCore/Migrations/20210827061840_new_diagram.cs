using Microsoft.EntityFrameworkCore.Migrations;

namespace NETCore.Migrations
{
    public partial class new_diagram : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Education_id",
                table: "tb_m_profilling");

            migrationBuilder.DropColumn(
                name: "University_id",
                table: "tb_m_education");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Education_id",
                table: "tb_m_profilling",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "University_id",
                table: "tb_m_education",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
