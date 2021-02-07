using Microsoft.EntityFrameworkCore.Migrations;

namespace Funda.Migrations
{
    public partial class intialDbMigration : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Objects",
                columns: table => new
                {
                    Id = table.Column<string>(nullable: false),
                    Adres = table.Column<string>(nullable: true),
                    BronCode = table.Column<string>(nullable: true),
                    Foto = table.Column<string>(nullable: true),
                    Huurprijs = table.Column<double>(nullable: true),
                    MakelaarId = table.Column<int>(nullable: false),
                    MakelaarNaam = table.Column<string>(nullable: true),
                    ProjectNaam = table.Column<string>(nullable: true),
                    HasTuin = table.Column<bool>(nullable: false),
                    MigrationVersion = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Objects", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Objects");
        }
    }
}
