using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
namespace Cartera_Cripto.Migrations
{
    /// <inheritdoc />
    public partial class CambiarNombreClienteId : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacciones_Clientes_client_dataid",
                table: "Transacciones");

            migrationBuilder.DropIndex(
                name: "IX_Transacciones_client_dataid",
                table: "Transacciones");

            migrationBuilder.DropColumn(
                name: "client_dataid",
                table: "Transacciones");

            migrationBuilder.RenameColumn(
                name: "client_id",
                table: "Transacciones",
                newName: "ClienteId");

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_ClienteId",
                table: "Transacciones",
                column: "ClienteId");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacciones_Clientes_ClienteId",
                table: "Transacciones",
                column: "ClienteId",
                principalTable: "Clientes",
                principalColumn: "id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transacciones_Clientes_ClienteId",
                table: "Transacciones");

            migrationBuilder.DropIndex(
                name: "IX_Transacciones_ClienteId",
                table: "Transacciones");

            migrationBuilder.RenameColumn(
                name: "ClienteId",
                table: "Transacciones",
                newName: "client_id");

            migrationBuilder.AddColumn<int>(
                name: "client_dataid",
                table: "Transacciones",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_client_dataid",
                table: "Transacciones",
                column: "client_dataid");

            migrationBuilder.AddForeignKey(
                name: "FK_Transacciones_Clientes_client_dataid",
                table: "Transacciones",
                column: "client_dataid",
                principalTable: "Clientes",
                principalColumn: "id");
        }
    }
}
