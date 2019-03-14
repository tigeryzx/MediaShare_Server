using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace MediaShare.Migrations
{
    public partial class AddPicModule : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "PicTag",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PicTag", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Picture",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FileName = table.Column<string>(nullable: true),
                    RealPath = table.Column<string>(nullable: true),
                    Width = table.Column<int>(nullable: false),
                    Height = table.Column<int>(nullable: false),
                    Size = table.Column<int>(nullable: false),
                    Title = table.Column<string>(nullable: true),
                    CreateDate = table.Column<DateTime>(nullable: false),
                    IsHidden = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Picture", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "PicFav",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    Name = table.Column<string>(nullable: true),
                    CoverId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PicFav", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PicFav_Picture_CoverId",
                        column: x => x.CoverId,
                        principalTable: "Picture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PicFav_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PicTagRelation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    TagId = table.Column<int>(nullable: true),
                    PictureId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PicTagRelation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PicTagRelation_Picture_PictureId",
                        column: x => x.PictureId,
                        principalTable: "Picture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PicTagRelation_PicTag_TagId",
                        column: x => x.TagId,
                        principalTable: "PicTag",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PicViewRecord",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    ViewDate = table.Column<DateTime>(nullable: false),
                    PictureId = table.Column<int>(nullable: true),
                    UserId = table.Column<long>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PicViewRecord", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PicViewRecord_Picture_PictureId",
                        column: x => x.PictureId,
                        principalTable: "Picture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PicViewRecord_AbpUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AbpUsers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "PicFavRelation",
                columns: table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    FavId = table.Column<int>(nullable: true),
                    PictureId = table.Column<int>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PicFavRelation", x => x.Id);
                    table.ForeignKey(
                        name: "FK_PicFavRelation_PicFav_FavId",
                        column: x => x.FavId,
                        principalTable: "PicFav",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_PicFavRelation_Picture_PictureId",
                        column: x => x.PictureId,
                        principalTable: "Picture",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_PicFav_CoverId",
                table: "PicFav",
                column: "CoverId");

            migrationBuilder.CreateIndex(
                name: "IX_PicFav_UserId",
                table: "PicFav",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_PicFavRelation_FavId",
                table: "PicFavRelation",
                column: "FavId");

            migrationBuilder.CreateIndex(
                name: "IX_PicFavRelation_PictureId",
                table: "PicFavRelation",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_PicTagRelation_PictureId",
                table: "PicTagRelation",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_PicTagRelation_TagId",
                table: "PicTagRelation",
                column: "TagId");

            migrationBuilder.CreateIndex(
                name: "IX_PicViewRecord_PictureId",
                table: "PicViewRecord",
                column: "PictureId");

            migrationBuilder.CreateIndex(
                name: "IX_PicViewRecord_UserId",
                table: "PicViewRecord",
                column: "UserId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "PicFavRelation");

            migrationBuilder.DropTable(
                name: "PicTagRelation");

            migrationBuilder.DropTable(
                name: "PicViewRecord");

            migrationBuilder.DropTable(
                name: "PicFav");

            migrationBuilder.DropTable(
                name: "PicTag");

            migrationBuilder.DropTable(
                name: "Picture");
        }
    }
}
