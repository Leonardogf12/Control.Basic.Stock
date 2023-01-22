using Microsoft.EntityFrameworkCore.Migrations;

namespace Controle_Estoque_Basico.Migrations
{
    public partial class novaPropriedadeImagemProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImagemProdutoModel",
                table: "Produto",
                nullable: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImagemProdutoModel",
                table: "Produto");
        }
    }
}
