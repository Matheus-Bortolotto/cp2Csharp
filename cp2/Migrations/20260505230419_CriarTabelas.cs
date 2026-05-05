using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace cp2.Migrations
{
    /// <inheritdoc />
    public partial class CriarTabelas : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Agencias",
                columns: table => new
                {
                    IdAgencia = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NmEndereco = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Cep = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Agencias", x => x.IdAgencia);
                });

            migrationBuilder.CreateTable(
                name: "Produtos",
                columns: table => new
                {
                    IdProduto = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NmProduto = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    TipoProduto = table.Column<string>(type: "NVARCHAR2(13)", maxLength: 13, nullable: false),
                    ValorSolicitado = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: true),
                    PrazoMeses = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    TaxaJuros = table.Column<decimal>(type: "DECIMAL(18, 2)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Produtos", x => x.IdProduto);
                });

            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    IdCliente = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    NmCliente = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    Email = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    TipoCliente = table.Column<string>(type: "NVARCHAR2(8)", maxLength: 8, nullable: false),
                    IdAgencia = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    AgenciaIdAgencia = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    Cpf = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    DataNascimento = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: true),
                    Cnpj = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    RazaoSocial = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.IdCliente);
                    table.ForeignKey(
                        name: "FK_Clientes_Agencias_AgenciaIdAgencia",
                        column: x => x.AgenciaIdAgencia,
                        principalTable: "Agencias",
                        principalColumn: "IdAgencia");
                });

            migrationBuilder.CreateTable(
                name: "Contratacoes",
                columns: table => new
                {
                    IdContratacao = table.Column<int>(type: "NUMBER(10)", nullable: false)
                        .Annotation("Oracle:Identity", "START WITH 1 INCREMENT BY 1"),
                    IdCliente = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ClienteIdCliente = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    IdProduto = table.Column<int>(type: "NUMBER(10)", nullable: false),
                    ProdutoIdProduto = table.Column<int>(type: "NUMBER(10)", nullable: true),
                    Status = table.Column<string>(type: "NVARCHAR2(2000)", nullable: true),
                    DtSolicitacao = table.Column<DateTime>(type: "TIMESTAMP(7)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contratacoes", x => x.IdContratacao);
                    table.ForeignKey(
                        name: "FK_Contratacoes_Clientes_ClienteIdCliente",
                        column: x => x.ClienteIdCliente,
                        principalTable: "Clientes",
                        principalColumn: "IdCliente");
                    table.ForeignKey(
                        name: "FK_Contratacoes_Produtos_ProdutoIdProduto",
                        column: x => x.ProdutoIdProduto,
                        principalTable: "Produtos",
                        principalColumn: "IdProduto");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Clientes_AgenciaIdAgencia",
                table: "Clientes",
                column: "AgenciaIdAgencia");

            migrationBuilder.CreateIndex(
                name: "IX_Contratacoes_ClienteIdCliente",
                table: "Contratacoes",
                column: "ClienteIdCliente");

            migrationBuilder.CreateIndex(
                name: "IX_Contratacoes_ProdutoIdProduto",
                table: "Contratacoes",
                column: "ProdutoIdProduto");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contratacoes");

            migrationBuilder.DropTable(
                name: "Clientes");

            migrationBuilder.DropTable(
                name: "Produtos");

            migrationBuilder.DropTable(
                name: "Agencias");
        }
    }
}
