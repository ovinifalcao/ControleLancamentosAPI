using System;
using ControleLancamentosAPI.Data.Models;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ControleLancamentosAPI.data.migracoes
{
    /// <inheritdoc />
    public partial class inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("Npgsql:Enum:natureza", "credito,credito_cancelamento,debito,debito_cancelamento");

            migrationBuilder.CreateSequence<int>(
                name: "SeqNumeroLancamento",
                startValue: 10000000L);

            migrationBuilder.CreateTable(
                name: "Lancamento",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false),
                    Data = table.Column<DateTime>(type: "timestamp without time zone", nullable: false),
                    Natureza = table.Column<NaturezaEnum>(type: "natureza", nullable: false),
                    Operador = table.Column<string>(type: "text", nullable: false),
                    Descricao = table.Column<string>(type: "text", nullable: false),
                    Valor = table.Column<double>(type: "double precision", nullable: false),
                    NumeroLancamento = table.Column<long>(type: "bigint", nullable: false, defaultValueSql: "nextval('\"SeqNumeroLancamento\"')")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Lancamento", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Lancamento");

            migrationBuilder.DropSequence(
                name: "SeqNumeroLancamento");
        }
    }
}
