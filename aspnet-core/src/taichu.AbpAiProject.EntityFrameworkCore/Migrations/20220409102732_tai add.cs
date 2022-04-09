using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace taichu.AbpAiProject.Migrations
{
    public partial class taiadd : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Db_Ai_Training",
                columns: table => new
                {
                    Id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    InputString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    OutputString = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    FuncName = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Link = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Note = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Db_Ai_Training", x => x.Id);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Db_Ai_Training");
        }
    }
}
