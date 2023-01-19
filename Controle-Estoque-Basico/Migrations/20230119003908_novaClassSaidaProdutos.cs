using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Controle_Estoque_Basico.Migrations
{
    public partial class novaClassSaidaProdutos : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "PRO_IDCATEGORIA",
                table: "Produto",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<bool>(
                name: "PRO_ISDELETED",
                table: "Produto",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "PRO_STATUS",
                table: "Produto",
                nullable: false,
                defaultValue: false);

            migrationBuilder.CreateTable(
                name: "Categoria",
                columns: table => new
                {
                    CAT_ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    CAT_NOME = table.Column<string>(maxLength: 30, nullable: false),
                    CAT_ISDELETED = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categoria", x => x.CAT_ID);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Produto_PRO_IDCATEGORIA",
                table: "Produto",
                column: "PRO_IDCATEGORIA");

            migrationBuilder.AddForeignKey(
                name: "FK_Produto_Categoria_PRO_IDCATEGORIA",
                table: "Produto",
                column: "PRO_IDCATEGORIA",
                principalTable: "Categoria",
                principalColumn: "CAT_ID",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Produto_Categoria_PRO_IDCATEGORIA",
                table: "Produto");

            migrationBuilder.DropTable(
                name: "Categoria");

            migrationBuilder.DropIndex(
                name: "IX_Produto_PRO_IDCATEGORIA",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "PRO_IDCATEGORIA",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "PRO_ISDELETED",
                table: "Produto");

            migrationBuilder.DropColumn(
                name: "PRO_STATUS",
                table: "Produto");
        }
    }
}
