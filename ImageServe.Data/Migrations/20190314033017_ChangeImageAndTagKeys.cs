using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ImageServe.Data.Migrations
{
    public partial class ChangeImageAndTagKeys : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageTags_Images_ImageId",
                table: "ImageTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageTags",
                table: "ImageTags");

            migrationBuilder.DropIndex(
                name: "IX_ImageTags_ImageId",
                table: "ImageTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.DropIndex(
                name: "IX_Images_Name",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "ImageTags");

            migrationBuilder.DropColumn(
                name: "ImageId",
                table: "ImageTags");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "Url",
                table: "Images");

            migrationBuilder.AddColumn<string>(
                name: "ImageName",
                table: "ImageTags",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Images",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageTags",
                table: "ImageTags",
                columns: new[] { "Name", "ImageName" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Name");

            migrationBuilder.CreateIndex(
                name: "IX_ImageTags_ImageName",
                table: "ImageTags",
                column: "ImageName");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageTags_Images_ImageName",
                table: "ImageTags",
                column: "ImageName",
                principalTable: "Images",
                principalColumn: "Name",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ImageTags_Images_ImageName",
                table: "ImageTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_ImageTags",
                table: "ImageTags");

            migrationBuilder.DropIndex(
                name: "IX_ImageTags_ImageName",
                table: "ImageTags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Images",
                table: "Images");

            migrationBuilder.DropColumn(
                name: "ImageName",
                table: "ImageTags");

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "ImageTags",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<int>(
                name: "ImageId",
                table: "ImageTags",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AlterColumn<string>(
                name: "Name",
                table: "Images",
                nullable: true,
                oldClrType: typeof(string));

            migrationBuilder.AddColumn<int>(
                name: "Id",
                table: "Images",
                nullable: false,
                defaultValue: 0)
                .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Images",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddPrimaryKey(
                name: "PK_ImageTags",
                table: "ImageTags",
                columns: new[] { "Id", "Name" });

            migrationBuilder.AddPrimaryKey(
                name: "PK_Images",
                table: "Images",
                column: "Id");

            migrationBuilder.CreateIndex(
                name: "IX_ImageTags_ImageId",
                table: "ImageTags",
                column: "ImageId");

            migrationBuilder.CreateIndex(
                name: "IX_Images_Name",
                table: "Images",
                column: "Name");

            migrationBuilder.AddForeignKey(
                name: "FK_ImageTags_Images_ImageId",
                table: "ImageTags",
                column: "ImageId",
                principalTable: "Images",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
