using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ExpoCenter.Repositorios.SqlServer.Migrations
{
    public partial class AdicaoColunaPagamentoStatus : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Status",
                table: "Pagamento",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Status",
                table: "Pagamento");
        }
    }
}
