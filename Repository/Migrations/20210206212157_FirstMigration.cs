using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class FirstMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "EMAIL",
                columns: table => new
                {
                    ID = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Identificador = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ASSUNTO = table.Column<string>(type: "varchar(100)", nullable: false),
                    DESTINATARIO = table.Column<string>(type: "varchar(100)", nullable: false),
                    REMETENTE = table.Column<string>(type: "varchar(100)", nullable: false),
                    MENSAGEM = table.Column<string>(type: "text", nullable: false),
                    DATACRIACAO = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_EMAIL", x => x.ID);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "EMAIL");
        }
    }
}
