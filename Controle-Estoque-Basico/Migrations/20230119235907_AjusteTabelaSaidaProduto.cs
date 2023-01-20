using Microsoft.EntityFrameworkCore.Migrations;

namespace Controle_Estoque_Basico.Migrations
{
    public partial class AjusteTabelaSaidaProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "SPRO_DESCRICAO",
                table: "SaidaProduto");

            migrationBuilder.DropColumn(
                name: "SPRO_NOME",
                table: "SaidaProduto");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "SPRO_DESCRICAO",
                table: "SaidaProduto",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "SPRO_NOME",
                table: "SaidaProduto",
                type: "varchar(50) CHARACTER SET utf8mb4",
                maxLength: 50,
                nullable: false,
                defaultValue: "");
        }
    }
}
