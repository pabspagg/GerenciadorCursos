using Microsoft.EntityFrameworkCore.Migrations;

namespace GerenciadorCursos.Migrations
{
    public partial class InitialCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Cursos",
                columns: table => new {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Titulo = table.Column<string>(type: "varchar(30)", nullable: false),
                    Duracao = table.Column<int>(type: "int", nullable: false),
                    Status = table.Column<string>(type: "varchar(30)", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Cursos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Usuarios",
                columns: table => new {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Username = table.Column<string>(type: "varchar(15)", nullable: false),
                    Password = table.Column<string>(type: "varchar(15)", nullable: false),
                    Role = table.Column<string>(type: "varchar(15)", nullable: false)
                },
                constraints: table => {
                    table.PrimaryKey("PK_Usuarios", x => x.Id);
                });

            migrationBuilder.InsertData(
                table: "Cursos",
                columns: new[] { "Id", "Duracao", "Status", "Titulo" },
                values: new object[,]
                {
                    { 1, 2, "Previsto", "Curso programação backend" },
                    { 2, 2, "EmAndamento", "Curso programação frontend" },
                    { 3, 1, "Concluido", "Curso devops" },
                    { 4, 5, "Previsto", "Curso mobile" },
                    { 5, 10, "Previsto", "Curso inovação" }
                });

            migrationBuilder.InsertData(
                table: "Usuarios",
                columns: new[] { "Id", "Password", "Role", "Username" },
                values: new object[,]
                {
                    { 1, "123456", "Aluno", "pablo" },
                    { 2, "123456", "Aluno", "joao" },
                    { 3, "123456", "Aluno", "lucas" },
                    { 4, "123456", "Aluno", "diego" },
                    { 5, "123456", "Aluno", "victor" },
                    { 6, "123456", "Aluno", "joao" },
                    { 7, "123456", "Secretaria", "maria" },
                    { 8, "123456", "Gerencia", "jose" }
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Cursos");

            migrationBuilder.DropTable(
                name: "Usuarios");
        }
    }
}
