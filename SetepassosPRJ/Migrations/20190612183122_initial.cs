using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SetepassosPRJ.Migrations
{
    public partial class initial : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Scores",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Nome = table.Column<string>(nullable: true),
                    NumFugas = table.Column<int>(nullable: false),
                    NumInimigosDerrotados = table.Column<int>(nullable: false),
                    NumItensEncontrados = table.Column<int>(nullable: false),
                    PocoesObtidas = table.Column<int>(nullable: false),
                    PocoesUsadas = table.Column<int>(nullable: false),
                    GameID = table.Column<int>(nullable: false),
                    MoedasOuroTotal = table.Column<int>(nullable: false),
                    ResultadoJogo = table.Column<int>(nullable: false),
                    TotalAreasExaminadas = table.Column<int>(nullable: false),
                    Chave = table.Column<bool>(nullable: false),
                    Terminado = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Scores", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Scores");
        }
    }
}
