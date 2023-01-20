using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Controle_Estoque_Basico.Migrations
{
    public partial class NovaClassSaidaProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SaidaProduto",
                columns: table => new
                {
                    SPRO_ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    SPRO_NOME = table.Column<string>(maxLength: 50, nullable: false),
                    SPRO_DESCRICAO = table.Column<string>(nullable: true),
                    SPRO_QUANTIDADE = table.Column<decimal>(type: "decimal(18, 3)", nullable: false),
                    SPRO_DATASAIDA = table.Column<DateTime>(nullable: false),
                    SPRO_IDPRODUTO = table.Column<int>(nullable: false),
                    SPRO_IDCATEGORIA = table.Column<int>(nullable: false),
                    SPRO_ISDELETED = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SaidaProduto", x => x.SPRO_ID);
                    table.ForeignKey(
                        name: "FK_SaidaProduto_Categoria_SPRO_IDCATEGORIA",
                        column: x => x.SPRO_IDCATEGORIA,
                        principalTable: "Categoria",
                        principalColumn: "CAT_ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_SaidaProduto_Produto_SPRO_IDPRODUTO",
                        column: x => x.SPRO_IDPRODUTO,
                        principalTable: "Produto",
                        principalColumn: "PRO_ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_SaidaProduto_SPRO_IDCATEGORIA",
                table: "SaidaProduto",
                column: "SPRO_IDCATEGORIA");

            migrationBuilder.CreateIndex(
                name: "IX_SaidaProduto_SPRO_IDPRODUTO",
                table: "SaidaProduto",
                column: "SPRO_IDPRODUTO");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SaidaProduto");
        }
    }
}
