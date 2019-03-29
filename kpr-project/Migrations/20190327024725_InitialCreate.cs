using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace kpr_project.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "SkemaDetail",
                columns: table => new
                {
                    Iddetail = table.Column<Guid>(nullable: false),
                    Bulan = table.Column<int>(nullable: false),
                    Pokok = table.Column<decimal>(nullable: false),
                    Bunga = table.Column<decimal>(nullable: false),
                    PelunasanPokok = table.Column<decimal>(nullable: false),
                    Tagihan = table.Column<decimal>(nullable: false),
                    SisaPokok = table.Column<decimal>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkemaDetail", x => x.Iddetail);
                });

            migrationBuilder.CreateTable(
                name: "SkemaKpr",
                columns: table => new
                {
                    Idskema = table.Column<Guid>(nullable: false),
                    Harga = table.Column<decimal>(nullable: false),
                    Dp = table.Column<decimal>(nullable: false),
                    Bunga = table.Column<decimal>(nullable: false),
                    Tenor = table.Column<int>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SkemaKpr", x => x.Idskema);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "SkemaDetail");

            migrationBuilder.DropTable(
                name: "SkemaKpr");
        }
    }
}
