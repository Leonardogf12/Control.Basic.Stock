using Microsoft.EntityFrameworkCore.Migrations;

namespace Controle_Estoque_Basico.Migrations
{
    public partial class NovaPropNaClassCategoria : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "CAT_DESCRICAO",
                table: "Categoria",
                maxLength: 200,
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "CAT_DESCRICAO",
                table: "Categoria");
        }
    }
}
