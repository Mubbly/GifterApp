using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.App.EF.Migrations
{
    public partial class DbFixes : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gifts_ActionTypes_ActionTypeId1",
                table: "Gifts");

            migrationBuilder.DropForeignKey(
                name: "FK_Gifts_AspNetUsers_AppUserId1",
                table: "Gifts");

            migrationBuilder.DropForeignKey(
                name: "FK_Gifts_Statuses_StatusId1",
                table: "Gifts");

            migrationBuilder.DropIndex(
                name: "IX_Gifts_ActionTypeId1",
                table: "Gifts");

            migrationBuilder.DropIndex(
                name: "IX_Gifts_AppUserId1",
                table: "Gifts");

            migrationBuilder.DropIndex(
                name: "IX_Gifts_StatusId1",
                table: "Gifts");

            migrationBuilder.DropColumn(
                name: "ActionTypeId1",
                table: "Gifts");

            migrationBuilder.DropColumn(
                name: "AppUserId1",
                table: "Gifts");

            migrationBuilder.DropColumn(
                name: "StatusId1",
                table: "Gifts");

            migrationBuilder.AlterColumn<Guid>(
                name: "StatusId",
                table: "Gifts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(36) CHARACTER SET utf8mb4",
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<Guid>(
                name: "AppUserId",
                table: "Gifts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(36) CHARACTER SET utf8mb4",
                oldMaxLength: 36);

            migrationBuilder.AlterColumn<Guid>(
                name: "ActionTypeId",
                table: "Gifts",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "varchar(36) CHARACTER SET utf8mb4",
                oldMaxLength: 36);

            migrationBuilder.CreateIndex(
                name: "IX_Gifts_ActionTypeId",
                table: "Gifts",
                column: "ActionTypeId");

            migrationBuilder.CreateIndex(
                name: "IX_Gifts_AppUserId",
                table: "Gifts",
                column: "AppUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Gifts_StatusId",
                table: "Gifts",
                column: "StatusId");

            migrationBuilder.AddForeignKey(
                name: "FK_Gifts_ActionTypes_ActionTypeId",
                table: "Gifts",
                column: "ActionTypeId",
                principalTable: "ActionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gifts_AspNetUsers_AppUserId",
                table: "Gifts",
                column: "AppUserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Gifts_Statuses_StatusId",
                table: "Gifts",
                column: "StatusId",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Gifts_ActionTypes_ActionTypeId",
                table: "Gifts");

            migrationBuilder.DropForeignKey(
                name: "FK_Gifts_AspNetUsers_AppUserId",
                table: "Gifts");

            migrationBuilder.DropForeignKey(
                name: "FK_Gifts_Statuses_StatusId",
                table: "Gifts");

            migrationBuilder.DropIndex(
                name: "IX_Gifts_ActionTypeId",
                table: "Gifts");

            migrationBuilder.DropIndex(
                name: "IX_Gifts_AppUserId",
                table: "Gifts");

            migrationBuilder.DropIndex(
                name: "IX_Gifts_StatusId",
                table: "Gifts");

            migrationBuilder.AlterColumn<string>(
                name: "StatusId",
                table: "Gifts",
                type: "varchar(36) CHARACTER SET utf8mb4",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<string>(
                name: "AppUserId",
                table: "Gifts",
                type: "varchar(36) CHARACTER SET utf8mb4",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AlterColumn<string>(
                name: "ActionTypeId",
                table: "Gifts",
                type: "varchar(36) CHARACTER SET utf8mb4",
                maxLength: 36,
                nullable: false,
                oldClrType: typeof(Guid));

            migrationBuilder.AddColumn<Guid>(
                name: "ActionTypeId1",
                table: "Gifts",
                type: "char(36)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "AppUserId1",
                table: "Gifts",
                type: "char(36)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "StatusId1",
                table: "Gifts",
                type: "char(36)",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Gifts_ActionTypeId1",
                table: "Gifts",
                column: "ActionTypeId1");

            migrationBuilder.CreateIndex(
                name: "IX_Gifts_AppUserId1",
                table: "Gifts",
                column: "AppUserId1");

            migrationBuilder.CreateIndex(
                name: "IX_Gifts_StatusId1",
                table: "Gifts",
                column: "StatusId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Gifts_ActionTypes_ActionTypeId1",
                table: "Gifts",
                column: "ActionTypeId1",
                principalTable: "ActionTypes",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gifts_AspNetUsers_AppUserId1",
                table: "Gifts",
                column: "AppUserId1",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Gifts_Statuses_StatusId1",
                table: "Gifts",
                column: "StatusId1",
                principalTable: "Statuses",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
