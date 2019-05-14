using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaShare.Migrations
{
    public partial class UpdatePicModel : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<long>(
                name: "Size",
                table: "Picture",
                nullable: false,
                oldClrType: typeof(int));

            migrationBuilder.AddColumn<string>(
                name: "RootDir",
                table: "Picture",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "RootDir",
                table: "Picture");

            migrationBuilder.AlterColumn<int>(
                name: "Size",
                table: "Picture",
                nullable: false,
                oldClrType: typeof(long));
        }
    }
}
