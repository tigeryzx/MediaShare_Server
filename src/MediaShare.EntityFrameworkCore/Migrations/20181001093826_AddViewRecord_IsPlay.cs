using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaShare.Migrations
{
    public partial class AddViewRecord_IsPlay : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsPlay",
                table: "ViewRecord",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsPlay",
                table: "ViewRecord");
        }
    }
}
