using Microsoft.EntityFrameworkCore.Migrations;

namespace Repository.Migrations
{
    public partial class RemoveRemetenteEmail : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "REMETENTE",
                table: "EMAIL",
                newName: "Remetente");

            migrationBuilder.AlterColumn<string>(
                name: "Remetente",
                table: "EMAIL",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "verchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "DESTINATARIO",
                table: "EMAIL",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "verchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "ASSUNTO",
                table: "EMAIL",
                type: "varchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "verchar(100)");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Remetente",
                table: "EMAIL",
                newName: "REMETENTE");

            migrationBuilder.AlterColumn<string>(
                name: "REMETENTE",
                table: "EMAIL",
                type: "verchar(100)",
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "DESTINATARIO",
                table: "EMAIL",
                type: "verchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");

            migrationBuilder.AlterColumn<string>(
                name: "ASSUNTO",
                table: "EMAIL",
                type: "verchar(100)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(100)");
        }
    }
}
