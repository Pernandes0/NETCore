using Microsoft.EntityFrameworkCore.Migrations;

namespace NETCore.Migrations
{
    public partial class typo_account : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_Account_tb_m_person_NIK",
                table: "tb_m_Account");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_profilling_tb_m_Account_NIK",
                table: "tb_m_profilling");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_Account",
                table: "tb_m_Account");

            migrationBuilder.RenameTable(
                name: "tb_m_Account",
                newName: "tb_m_account");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_account",
                table: "tb_m_account",
                column: "NIK");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_account_tb_m_person_NIK",
                table: "tb_m_account",
                column: "NIK",
                principalTable: "tb_m_person",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_profilling_tb_m_account_NIK",
                table: "tb_m_profilling",
                column: "NIK",
                principalTable: "tb_m_account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_account_tb_m_person_NIK",
                table: "tb_m_account");

            migrationBuilder.DropForeignKey(
                name: "FK_tb_m_profilling_tb_m_account_NIK",
                table: "tb_m_profilling");

            migrationBuilder.DropPrimaryKey(
                name: "PK_tb_m_account",
                table: "tb_m_account");

            migrationBuilder.RenameTable(
                name: "tb_m_account",
                newName: "tb_m_Account");

            migrationBuilder.AddPrimaryKey(
                name: "PK_tb_m_Account",
                table: "tb_m_Account",
                column: "NIK");

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_Account_tb_m_person_NIK",
                table: "tb_m_Account",
                column: "NIK",
                principalTable: "tb_m_person",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_tb_m_profilling_tb_m_Account_NIK",
                table: "tb_m_profilling",
                column: "NIK",
                principalTable: "tb_m_Account",
                principalColumn: "NIK",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
