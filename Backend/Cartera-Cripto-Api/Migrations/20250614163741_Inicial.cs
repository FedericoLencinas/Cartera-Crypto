using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable
namespace Cartera_Cripto.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Clientes",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    name = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    email = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Clientes", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "Transacciones",
                columns: table => new
                {
                    id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    action = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    crypto_code = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    client_id = table.Column<int>(type: "int", nullable: false),
                    client_dataid = table.Column<int>(type: "int", nullable: true),
                    crypto_amount = table.Column<float>(type: "real", nullable: false),
                    money = table.Column<float>(type: "real", nullable: false),
                    datetime = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Transacciones", x => x.id);
                    table.ForeignKey(
                        name: "FK_Transacciones_Clientes_client_dataid",
                        column: x => x.client_dataid,
                        principalTable: "Clientes",
                        principalColumn: "id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transacciones_client_dataid",
                table: "Transacciones",
                column: "client_dataid");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Transacciones");

            migrationBuilder.DropTable(
                name: "Clientes");
        }
    }
}
