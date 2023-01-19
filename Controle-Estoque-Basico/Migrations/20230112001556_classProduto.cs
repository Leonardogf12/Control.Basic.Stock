using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Controle_Estoque_Basico.Migrations
{
    public partial class classProduto : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Produto",
                columns: table => new
                {
                    PRO_ID = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    PRO_NOME = table.Column<string>(maxLength: 50, nullable: false),
                    PRO_DESCRICAO = table.Column<string>(nullable: true),
                    PRO_QUANTIDADE = table.Column<decimal>(type: "decimal(18, 3)", nullable: false),
                    PRO_DATAENTRADA = table.Column<DateTime>(nullable: false),
                    PRO_VALIDADE = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produto", x => x.PRO_ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Produto");
        }
    }
}
