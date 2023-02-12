using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace supermercadoAPI.Migrations
{
    /// <inheritdoc />
    public partial class Bancodedados2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Deposito",
                columns: table => new
                {
                    DepositoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Deposito", x => x.DepositoId);
                });

            migrationBuilder.CreateTable(
                name: "ItemDeposito",
                columns: table => new
                {
                    ItemDepositoId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Quantidade = table.Column<int>(type: "int", nullable: false),
                    ProdutoId = table.Column<int>(type: "int", nullable: false),
                    DepositoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ItemDeposito", x => x.ItemDepositoId);
                    table.ForeignKey(
                        name: "FK_ItemDeposito_Deposito_DepositoId",
                        column: x => x.DepositoId,
                        principalTable: "Deposito",
                        principalColumn: "DepositoId");
                    table.ForeignKey(
                        name: "FK_ItemDeposito_Produtos_ProdutoId",
                        column: x => x.ProdutoId,
                        principalTable: "Produtos",
                        principalColumn: "ProdutoId",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ItemDeposito_DepositoId",
                table: "ItemDeposito",
                column: "DepositoId");

            migrationBuilder.CreateIndex(
                name: "IX_ItemDeposito_ProdutoId",
                table: "ItemDeposito",
                column: "ProdutoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ItemDeposito");

            migrationBuilder.DropTable(
                name: "Deposito");
        }
    }
}
