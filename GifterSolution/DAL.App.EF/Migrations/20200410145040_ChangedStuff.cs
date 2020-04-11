using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.App.EF.Migrations
{
    public partial class ChangedStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_Gifts_GiftId",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_Wishlists_GiftId",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "GiftId",
                table: "Wishlists");

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId",
                table: "Wishlists",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "WishlistId",
                table: "Gifts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_AppUserId",
                table: "Wishlists",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Gifts_WishlistId",
                table: "Gifts",
                column: "WishlistId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gifts_Wishlists_WishlistId",
                table: "Gifts",
                column: "WishlistId",
                principalTable: "Wishlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_AspNetUsers_AppUserId",
                table: "Wishlists",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gifts_Wishlists_WishlistId",
                table: "Gifts");

            migrationBuilder.DropForeignKey(
                name: "FK_Wishlists_AspNetUsers_AppUserId",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_Wishlists_AppUserId",
                table: "Wishlists");

            migrationBuilder.DropIndex(
                name: "IX_Gifts_WishlistId",
                table: "Gifts");

            migrationBuilder.DropColumn(
                name: "AppUserId",
                table: "Wishlists");

            migrationBuilder.DropColumn(
                name: "WishlistId",
                table: "Gifts");

            migrationBuilder.AddColumn<string>(
                name: "GiftId",
                table: "Wishlists",
                type: "char(36)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Wishlists_GiftId",
                table: "Wishlists",
                column: "GiftId");

            migrationBuilder.AddForeignKey(
                name: "FK_Wishlists_Gifts_GiftId",
                table: "Wishlists",
                column: "GiftId",
                principalTable: "Gifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
