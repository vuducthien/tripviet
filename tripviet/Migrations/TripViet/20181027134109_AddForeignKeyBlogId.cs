using Microsoft.EntityFrameworkCore.Migrations;
using System;
using System.Collections.Generic;

namespace TripViet.Migrations.TripViet
{
    public partial class AddForeignKeyBlogId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Places_Blogs_BlogId",
                table: "Places");

            migrationBuilder.AlterColumn<Guid>(
                name: "BlogId",
                table: "Places",
                nullable: false,
                oldClrType: typeof(Guid),
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_Places_Blogs_BlogId",
                table: "Places",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Places_Blogs_BlogId",
                table: "Places");

            migrationBuilder.AlterColumn<Guid>(
                name: "BlogId",
                table: "Places",
                nullable: true,
                oldClrType: typeof(Guid));

            migrationBuilder.AddForeignKey(
                name: "FK_Places_Blogs_BlogId",
                table: "Places",
                column: "BlogId",
                principalTable: "Blogs",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
