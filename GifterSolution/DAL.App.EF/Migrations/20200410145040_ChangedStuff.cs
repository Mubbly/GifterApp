using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.App.EF.Migrations
{
    public partial class ChangedStuff : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Wishlists_Gifts_GiftId",
                "Wishlists");

            migrationBuilder.DropIndex(
                "IX_Wishlists_GiftId",
                "Wishlists");

            migrationBuilder.DropColumn(
                "GiftId",
                "Wishlists");

            migrationBuilder.AddColumn<Guid>(
                "AppUserId",
                "Wishlists",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                "WishlistId",
                "Gifts",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                "IX_Wishlists_AppUserId",
                "Wishlists",
                "AppUserId");

            migrationBuilder.CreateIndex(
                "IX_Gifts_WishlistId",
                "Gifts",
                "WishlistId");

            migrationBuilder.AddForeignKey(
                "FK_Gifts_Wishlists_WishlistId",
                "Gifts",
                "WishlistId",
                "Wishlists",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                "FK_Wishlists_AspNetUsers_AppUserId",
                "Wishlists",
                "AppUserId",
                "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                "FK_Gifts_Wishlists_WishlistId",
                "Gifts");

            migrationBuilder.DropForeignKey(
                "FK_Wishlists_AspNetUsers_AppUserId",
                "Wishlists");

            migrationBuilder.DropIndex(
                "IX_Wishlists_AppUserId",
                "Wishlists");

            migrationBuilder.DropIndex(
                "IX_Gifts_WishlistId",
                "Gifts");

            migrationBuilder.DropColumn(
                "AppUserId",
                "Wishlists");

            migrationBuilder.DropColumn(
                "WishlistId",
                "Gifts");

            migrationBuilder.AddColumn<string>(
                "GiftId",
                "Wishlists",
                "char(36)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                "IX_Wishlists_GiftId",
                "Wishlists",
                "GiftId");

            migrationBuilder.AddForeignKey(
                "FK_Wishlists_Gifts_GiftId",
                "Wishlists",
                "GiftId",
                "Gifts",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}