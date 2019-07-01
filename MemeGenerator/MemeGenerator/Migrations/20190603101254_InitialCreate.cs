using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MemeGenerator.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    IdCategory = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    NameCategory = table.Column<string>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.IdCategory);
                });

            migrationBuilder.CreateTable(
                name: "Marking",
                columns: table => new
                {
                    IdMakring = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    IdMema = table.Column<int>(nullable: false),
                    Authorr = table.Column<string>(nullable: true),
                    CountLike = table.Column<int>(nullable: true),
                    CountDislike = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Marking", x => x.IdMakring);
                });

            migrationBuilder.CreateTable(
                name: "MemeReports",
                columns: table => new
                {
                    Id_report = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    login = table.Column<string>(nullable: true),
                    Id_meme = table.Column<int>(nullable: false),
                    Description = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MemeReports", x => x.Id_report);
                });

            migrationBuilder.CreateTable(
                name: "Memy",
                columns: table => new
                {
                    Id_mema = table.Column<int>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    Autor = table.Column<string>(nullable: true),
                    Title = table.Column<string>(nullable: false),
                    coverImg = table.Column<string>(nullable: true),
                    Description = table.Column<string>(nullable: true),
                    Category = table.Column<string>(nullable: false),
                    releaseDate = table.Column<DateTime>(nullable: true),
                    modifyDate = table.Column<DateTime>(nullable: true),
                    Like = table.Column<int>(nullable: true),
                    Dislike = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Memy", x => x.Id_mema);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Marking");

            migrationBuilder.DropTable(
                name: "MemeReports");

            migrationBuilder.DropTable(
                name: "Memy");
        }
    }
}
