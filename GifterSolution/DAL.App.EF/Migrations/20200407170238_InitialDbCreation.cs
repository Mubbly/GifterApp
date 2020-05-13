using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace DAL.App.EF.Migrations
{
    public partial class InitialDbCreation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                "ActionTypes",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<string>(nullable: true),
                    EditedAt = table.Column<DateTime>(nullable: true),
                    ActionTypeValue = table.Column<string>(maxLength: 64, nullable: false),
                    Comment = table.Column<string>(maxLength: 2048, nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_ActionTypes", x => x.Id); });

            migrationBuilder.CreateTable(
                "AspNetRoles",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Name = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedName = table.Column<string>(maxLength: 256, nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_AspNetRoles", x => x.Id); });

            migrationBuilder.CreateTable(
                "AspNetUsers",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserName = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedUserName = table.Column<string>(maxLength: 256, nullable: true),
                    Email = table.Column<string>(maxLength: 256, nullable: true),
                    NormalizedEmail = table.Column<string>(maxLength: 256, nullable: true),
                    EmailConfirmed = table.Column<bool>(nullable: false),
                    PasswordHash = table.Column<string>(nullable: true),
                    SecurityStamp = table.Column<string>(nullable: true),
                    ConcurrencyStamp = table.Column<string>(nullable: true),
                    PhoneNumber = table.Column<string>(nullable: true),
                    PhoneNumberConfirmed = table.Column<bool>(nullable: false),
                    TwoFactorEnabled = table.Column<bool>(nullable: false),
                    LockoutEnd = table.Column<DateTimeOffset>(nullable: true),
                    LockoutEnabled = table.Column<bool>(nullable: false),
                    AccessFailedCount = table.Column<int>(nullable: false),
                    FirstName = table.Column<string>(maxLength: 256, nullable: false),
                    LastName = table.Column<string>(maxLength: 256, nullable: false),
                    IsCampaignManager = table.Column<bool>(nullable: true),
                    IsActive = table.Column<bool>(nullable: true),
                    LastActive = table.Column<DateTime>(nullable: true),
                    DateJoined = table.Column<DateTime>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_AspNetUsers", x => x.Id); });

            migrationBuilder.CreateTable(
                "Campaigns",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<string>(nullable: true),
                    EditedAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 512, nullable: false),
                    Description = table.Column<string>(maxLength: 4096, nullable: true),
                    AdImage = table.Column<string>(maxLength: 2048, nullable: true),
                    Institution = table.Column<string>(maxLength: 512, nullable: true),
                    ActiveFromDate = table.Column<DateTime>(nullable: false),
                    ActiveToDate = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false)
                },
                constraints: table => { table.PrimaryKey("PK_Campaigns", x => x.Id); });

            migrationBuilder.CreateTable(
                "NotificationTypes",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<string>(nullable: true),
                    EditedAt = table.Column<DateTime>(nullable: true),
                    NotificationTypeValue = table.Column<string>(maxLength: 64, nullable: false),
                    Comment = table.Column<string>(maxLength: 2048, nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_NotificationTypes", x => x.Id); });

            migrationBuilder.CreateTable(
                "Permissions",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<string>(nullable: true),
                    EditedAt = table.Column<DateTime>(nullable: true),
                    PermissionValue = table.Column<string>(maxLength: 1024, nullable: false),
                    Comment = table.Column<string>(maxLength: 2048, nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Permissions", x => x.Id); });

            migrationBuilder.CreateTable(
                "Statuses",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<string>(nullable: true),
                    EditedAt = table.Column<DateTime>(nullable: true),
                    StatusValue = table.Column<string>(maxLength: 64, nullable: false),
                    Comment = table.Column<string>(maxLength: 2048, nullable: true)
                },
                constraints: table => { table.PrimaryKey("PK_Statuses", x => x.Id); });

            migrationBuilder.CreateTable(
                "AspNetRoleClaims",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    RoleId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetRoleClaims", x => x.Id);
                    table.ForeignKey(
                        "FK_AspNetRoleClaims_AspNetRoles_RoleId",
                        x => x.RoleId,
                        "AspNetRoles",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "AspNetUserClaims",
                table => new
                {
                    Id = table.Column<int>(nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<Guid>(nullable: false),
                    ClaimType = table.Column<string>(nullable: true),
                    ClaimValue = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserClaims", x => x.Id);
                    table.ForeignKey(
                        "FK_AspNetUserClaims_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "AspNetUserLogins",
                table => new
                {
                    LoginProvider = table.Column<string>(nullable: false),
                    ProviderKey = table.Column<string>(nullable: false),
                    ProviderDisplayName = table.Column<string>(nullable: true),
                    UserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserLogins", x => new {x.LoginProvider, x.ProviderKey});
                    table.ForeignKey(
                        "FK_AspNetUserLogins_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "AspNetUserRoles",
                table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    RoleId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserRoles", x => new {x.UserId, x.RoleId});
                    table.ForeignKey(
                        "FK_AspNetUserRoles_AspNetRoles_RoleId",
                        x => x.RoleId,
                        "AspNetRoles",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_AspNetUserRoles_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "AspNetUserTokens",
                table => new
                {
                    UserId = table.Column<Guid>(nullable: false),
                    LoginProvider = table.Column<string>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    Value = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AspNetUserTokens", x => new {x.UserId, x.LoginProvider, x.Name});
                    table.ForeignKey(
                        "FK_AspNetUserTokens_AspNetUsers_UserId",
                        x => x.UserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Friendships",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<string>(nullable: true),
                    EditedAt = table.Column<DateTime>(nullable: true),
                    IsConfirmed = table.Column<bool>(nullable: false),
                    Comment = table.Column<string>(maxLength: 2048, nullable: true),
                    AppUser1Id = table.Column<Guid>(nullable: false),
                    AppUser2Id = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Friendships", x => x.Id);
                    table.ForeignKey(
                        "FK_Friendships_AspNetUsers_AppUser1Id",
                        x => x.AppUser1Id,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Friendships_AspNetUsers_AppUser2Id",
                        x => x.AppUser2Id,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "InvitedUsers",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<string>(nullable: true),
                    EditedAt = table.Column<DateTime>(nullable: true),
                    Email = table.Column<string>(maxLength: 128, nullable: false),
                    PhoneNumber = table.Column<string>(maxLength: 32, nullable: true),
                    Message = table.Column<string>(maxLength: 1024, nullable: true),
                    DateInvited = table.Column<DateTime>(nullable: false),
                    HasJoined = table.Column<bool>(nullable: false),
                    InvitorUserId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_InvitedUsers", x => x.Id);
                    table.ForeignKey(
                        "FK_InvitedUsers_AspNetUsers_InvitorUserId",
                        x => x.InvitorUserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "PrivateMessages",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<string>(nullable: true),
                    EditedAt = table.Column<DateTime>(nullable: true),
                    Message = table.Column<string>(maxLength: 4096, nullable: false),
                    SentAt = table.Column<DateTime>(nullable: false),
                    IsSeen = table.Column<bool>(nullable: false),
                    UserSenderId = table.Column<Guid>(nullable: false),
                    UserReceiverId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_PrivateMessages", x => x.Id);
                    table.ForeignKey(
                        "FK_PrivateMessages_AspNetUsers_UserReceiverId",
                        x => x.UserReceiverId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_PrivateMessages_AspNetUsers_UserSenderId",
                        x => x.UserSenderId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "UserCampaigns",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<string>(nullable: true),
                    EditedAt = table.Column<DateTime>(nullable: true),
                    Comment = table.Column<string>(maxLength: 2048, nullable: true),
                    AppUserId = table.Column<Guid>(nullable: false),
                    CampaignId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserCampaigns", x => x.Id);
                    table.ForeignKey(
                        "FK_UserCampaigns_AspNetUsers_AppUserId",
                        x => x.AppUserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_UserCampaigns_Campaigns_CampaignId",
                        x => x.CampaignId,
                        "Campaigns",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Notifications",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<string>(nullable: true),
                    EditedAt = table.Column<DateTime>(nullable: true),
                    NotificationValue = table.Column<string>(maxLength: 1024, nullable: false),
                    Comment = table.Column<string>(maxLength: 2048, nullable: true),
                    NotificationTypeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                    table.ForeignKey(
                        "FK_Notifications_NotificationTypes_NotificationTypeId",
                        x => x.NotificationTypeId,
                        "NotificationTypes",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "UserPermissions",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<string>(nullable: true),
                    EditedAt = table.Column<DateTime>(nullable: true),
                    From = table.Column<DateTime>(nullable: false),
                    To = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(maxLength: 2048, nullable: true),
                    AppUserId = table.Column<Guid>(nullable: false),
                    PermissionId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserPermissions", x => x.Id);
                    table.ForeignKey(
                        "FK_UserPermissions_AspNetUsers_AppUserId",
                        x => x.AppUserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_UserPermissions_Permissions_PermissionId",
                        x => x.PermissionId,
                        "Permissions",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Donatees",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<string>(nullable: true),
                    EditedAt = table.Column<DateTime>(nullable: true),
                    FirstName = table.Column<string>(maxLength: 256, nullable: false),
                    LastName = table.Column<string>(maxLength: 256, nullable: true),
                    Gender = table.Column<string>(maxLength: 256, nullable: true),
                    Age = table.Column<int>(nullable: true),
                    Bio = table.Column<string>(maxLength: 4096, nullable: true),
                    GiftName = table.Column<string>(maxLength: 256, nullable: false),
                    GiftDescription = table.Column<string>(maxLength: 1024, nullable: true),
                    GiftImage = table.Column<string>(maxLength: 2048, nullable: true),
                    GiftUrl = table.Column<string>(maxLength: 2048, nullable: true),
                    GiftReservedFrom = table.Column<DateTime>(nullable: true),
                    ActiveFrom = table.Column<DateTime>(nullable: false),
                    ActiveTo = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    ActionTypeId = table.Column<Guid>(nullable: false),
                    StatusId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Donatees", x => x.Id);
                    table.ForeignKey(
                        "FK_Donatees_ActionTypes_ActionTypeId",
                        x => x.ActionTypeId,
                        "ActionTypes",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Donatees_Statuses_StatusId",
                        x => x.StatusId,
                        "Statuses",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Gifts",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<string>(nullable: true),
                    EditedAt = table.Column<DateTime>(nullable: true),
                    Name = table.Column<string>(maxLength: 256, nullable: false),
                    Description = table.Column<string>(maxLength: 1024, nullable: true),
                    Image = table.Column<string>(maxLength: 2048, nullable: true),
                    Url = table.Column<string>(maxLength: 2048, nullable: true),
                    PartnerUrl = table.Column<string>(maxLength: 2048, nullable: true),
                    IsPartnered = table.Column<bool>(nullable: false),
                    IsPinned = table.Column<bool>(nullable: false),
                    ActionTypeId = table.Column<Guid>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false),
                    StatusId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Gifts", x => x.Id);
                    table.ForeignKey(
                        "FK_Gifts_ActionTypes_ActionTypeId",
                        x => x.ActionTypeId,
                        "ActionTypes",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Gifts_AspNetUsers_AppUserId",
                        x => x.AppUserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Gifts_Statuses_StatusId",
                        x => x.StatusId,
                        "Statuses",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "UserNotifications",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<string>(nullable: true),
                    EditedAt = table.Column<DateTime>(nullable: true),
                    LastNotified = table.Column<DateTime>(nullable: false),
                    RenotifyAt = table.Column<DateTime>(nullable: false),
                    IsActive = table.Column<bool>(nullable: false),
                    IsDisabled = table.Column<bool>(nullable: false),
                    Comment = table.Column<string>(maxLength: 2048, nullable: true),
                    AppUserId = table.Column<Guid>(nullable: false),
                    NotificationId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserNotifications", x => x.Id);
                    table.ForeignKey(
                        "FK_UserNotifications_AspNetUsers_AppUserId",
                        x => x.AppUserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_UserNotifications_Notifications_NotificationId",
                        x => x.NotificationId,
                        "Notifications",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "CampaignDonatees",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<string>(nullable: true),
                    EditedAt = table.Column<DateTime>(nullable: true),
                    IsActive = table.Column<bool>(nullable: false),
                    Comment = table.Column<string>(maxLength: 1024, nullable: true),
                    CampaignId = table.Column<Guid>(nullable: false),
                    DonateeId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CampaignDonatees", x => x.Id);
                    table.ForeignKey(
                        "FK_CampaignDonatees_Campaigns_CampaignId",
                        x => x.CampaignId,
                        "Campaigns",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_CampaignDonatees_Donatees_DonateeId",
                        x => x.DonateeId,
                        "Donatees",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "ArchivedGifts",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<string>(nullable: true),
                    EditedAt = table.Column<DateTime>(nullable: true),
                    DateArchived = table.Column<DateTime>(nullable: false),
                    IsConfirmed = table.Column<bool>(nullable: false),
                    Comment = table.Column<string>(maxLength: 2048, nullable: true),
                    ActionTypeId = table.Column<Guid>(nullable: false),
                    GiftId = table.Column<Guid>(nullable: false),
                    StatusId = table.Column<Guid>(nullable: false),
                    UserGiverId = table.Column<Guid>(nullable: false),
                    UserReceiverId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ArchivedGifts", x => x.Id);
                    table.ForeignKey(
                        "FK_ArchivedGifts_ActionTypes_ActionTypeId",
                        x => x.ActionTypeId,
                        "ActionTypes",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_ArchivedGifts_Gifts_GiftId",
                        x => x.GiftId,
                        "Gifts",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_ArchivedGifts_Statuses_StatusId",
                        x => x.StatusId,
                        "Statuses",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_ArchivedGifts_AspNetUsers_UserGiverId",
                        x => x.UserGiverId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_ArchivedGifts_AspNetUsers_UserReceiverId",
                        x => x.UserReceiverId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "ReservedGifts",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<string>(nullable: true),
                    EditedAt = table.Column<DateTime>(nullable: true),
                    ReservedFrom = table.Column<DateTime>(nullable: false),
                    Comment = table.Column<string>(maxLength: 2048, nullable: true),
                    GiftId = table.Column<Guid>(nullable: false),
                    ActionTypeId = table.Column<Guid>(nullable: false),
                    StatusId = table.Column<Guid>(nullable: false),
                    UserGiverId = table.Column<Guid>(nullable: false),
                    UserReceiverId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReservedGifts", x => x.Id);
                    table.ForeignKey(
                        "FK_ReservedGifts_ActionTypes_ActionTypeId",
                        x => x.ActionTypeId,
                        "ActionTypes",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_ReservedGifts_Gifts_GiftId",
                        x => x.GiftId,
                        "Gifts",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_ReservedGifts_Statuses_StatusId",
                        x => x.StatusId,
                        "Statuses",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_ReservedGifts_AspNetUsers_UserGiverId",
                        x => x.UserGiverId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_ReservedGifts_AspNetUsers_UserReceiverId",
                        x => x.UserReceiverId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Wishlists",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<string>(nullable: true),
                    EditedAt = table.Column<DateTime>(nullable: true),
                    Comment = table.Column<string>(maxLength: 2048, nullable: true),
                    GiftId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Wishlists", x => x.Id);
                    table.ForeignKey(
                        "FK_Wishlists_Gifts_GiftId",
                        x => x.GiftId,
                        "Gifts",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "Profiles",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<string>(nullable: true),
                    EditedAt = table.Column<DateTime>(nullable: true),
                    ProfilePicture = table.Column<string>(maxLength: 2048, nullable: true),
                    Gender = table.Column<string>(maxLength: 256, nullable: true),
                    Bio = table.Column<string>(maxLength: 512, nullable: true),
                    Age = table.Column<int>(nullable: true),
                    IsPrivate = table.Column<bool>(nullable: false),
                    AppUserId = table.Column<Guid>(nullable: false),
                    WishlistId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Profiles", x => x.Id);
                    table.ForeignKey(
                        "FK_Profiles_AspNetUsers_AppUserId",
                        x => x.AppUserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_Profiles_Wishlists_WishlistId",
                        x => x.WishlistId,
                        "Wishlists",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                "UserProfiles",
                table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    CreatedBy = table.Column<string>(nullable: true),
                    CreatedAt = table.Column<DateTime>(nullable: false),
                    EditedBy = table.Column<string>(nullable: true),
                    EditedAt = table.Column<DateTime>(nullable: true),
                    Comment = table.Column<string>(maxLength: 2048, nullable: true),
                    AppUserId = table.Column<Guid>(nullable: false),
                    ProfileId = table.Column<Guid>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserProfiles", x => x.Id);
                    table.ForeignKey(
                        "FK_UserProfiles_AspNetUsers_AppUserId",
                        x => x.AppUserId,
                        "AspNetUsers",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        "FK_UserProfiles_Profiles_ProfileId",
                        x => x.ProfileId,
                        "Profiles",
                        "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                "IX_ArchivedGifts_ActionTypeId",
                "ArchivedGifts",
                "ActionTypeId");

            migrationBuilder.CreateIndex(
                "IX_ArchivedGifts_GiftId",
                "ArchivedGifts",
                "GiftId");

            migrationBuilder.CreateIndex(
                "IX_ArchivedGifts_StatusId",
                "ArchivedGifts",
                "StatusId");

            migrationBuilder.CreateIndex(
                "IX_ArchivedGifts_UserGiverId",
                "ArchivedGifts",
                "UserGiverId");

            migrationBuilder.CreateIndex(
                "IX_ArchivedGifts_UserReceiverId",
                "ArchivedGifts",
                "UserReceiverId");

            migrationBuilder.CreateIndex(
                "IX_AspNetRoleClaims_RoleId",
                "AspNetRoleClaims",
                "RoleId");

            migrationBuilder.CreateIndex(
                "RoleNameIndex",
                "AspNetRoles",
                "NormalizedName",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_AspNetUserClaims_UserId",
                "AspNetUserClaims",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_AspNetUserLogins_UserId",
                "AspNetUserLogins",
                "UserId");

            migrationBuilder.CreateIndex(
                "IX_AspNetUserRoles_RoleId",
                "AspNetUserRoles",
                "RoleId");

            migrationBuilder.CreateIndex(
                "EmailIndex",
                "AspNetUsers",
                "NormalizedEmail");

            migrationBuilder.CreateIndex(
                "UserNameIndex",
                "AspNetUsers",
                "NormalizedUserName",
                unique: true);

            migrationBuilder.CreateIndex(
                "IX_CampaignDonatees_CampaignId",
                "CampaignDonatees",
                "CampaignId");

            migrationBuilder.CreateIndex(
                "IX_CampaignDonatees_DonateeId",
                "CampaignDonatees",
                "DonateeId");

            migrationBuilder.CreateIndex(
                "IX_Donatees_ActionTypeId",
                "Donatees",
                "ActionTypeId");

            migrationBuilder.CreateIndex(
                "IX_Donatees_StatusId",
                "Donatees",
                "StatusId");

            migrationBuilder.CreateIndex(
                "IX_Friendships_AppUser1Id",
                "Friendships",
                "AppUser1Id");

            migrationBuilder.CreateIndex(
                "IX_Friendships_AppUser2Id",
                "Friendships",
                "AppUser2Id");

            migrationBuilder.CreateIndex(
                "IX_Gifts_ActionTypeId",
                "Gifts",
                "ActionTypeId");

            migrationBuilder.CreateIndex(
                "IX_Gifts_AppUserId",
                "Gifts",
                "AppUserId");

            migrationBuilder.CreateIndex(
                "IX_Gifts_StatusId",
                "Gifts",
                "StatusId");

            migrationBuilder.CreateIndex(
                "IX_InvitedUsers_InvitorUserId",
                "InvitedUsers",
                "InvitorUserId");

            migrationBuilder.CreateIndex(
                "IX_Notifications_NotificationTypeId",
                "Notifications",
                "NotificationTypeId");

            migrationBuilder.CreateIndex(
                "IX_PrivateMessages_UserReceiverId",
                "PrivateMessages",
                "UserReceiverId");

            migrationBuilder.CreateIndex(
                "IX_PrivateMessages_UserSenderId",
                "PrivateMessages",
                "UserSenderId");

            migrationBuilder.CreateIndex(
                "IX_Profiles_AppUserId",
                "Profiles",
                "AppUserId");

            migrationBuilder.CreateIndex(
                "IX_Profiles_WishlistId",
                "Profiles",
                "WishlistId");

            migrationBuilder.CreateIndex(
                "IX_ReservedGifts_ActionTypeId",
                "ReservedGifts",
                "ActionTypeId");

            migrationBuilder.CreateIndex(
                "IX_ReservedGifts_GiftId",
                "ReservedGifts",
                "GiftId");

            migrationBuilder.CreateIndex(
                "IX_ReservedGifts_StatusId",
                "ReservedGifts",
                "StatusId");

            migrationBuilder.CreateIndex(
                "IX_ReservedGifts_UserGiverId",
                "ReservedGifts",
                "UserGiverId");

            migrationBuilder.CreateIndex(
                "IX_ReservedGifts_UserReceiverId",
                "ReservedGifts",
                "UserReceiverId");

            migrationBuilder.CreateIndex(
                "IX_UserCampaigns_AppUserId",
                "UserCampaigns",
                "AppUserId");

            migrationBuilder.CreateIndex(
                "IX_UserCampaigns_CampaignId",
                "UserCampaigns",
                "CampaignId");

            migrationBuilder.CreateIndex(
                "IX_UserNotifications_AppUserId",
                "UserNotifications",
                "AppUserId");

            migrationBuilder.CreateIndex(
                "IX_UserNotifications_NotificationId",
                "UserNotifications",
                "NotificationId");

            migrationBuilder.CreateIndex(
                "IX_UserPermissions_AppUserId",
                "UserPermissions",
                "AppUserId");

            migrationBuilder.CreateIndex(
                "IX_UserPermissions_PermissionId",
                "UserPermissions",
                "PermissionId");

            migrationBuilder.CreateIndex(
                "IX_UserProfiles_AppUserId",
                "UserProfiles",
                "AppUserId");

            migrationBuilder.CreateIndex(
                "IX_UserProfiles_ProfileId",
                "UserProfiles",
                "ProfileId");

            migrationBuilder.CreateIndex(
                "IX_Wishlists_GiftId",
                "Wishlists",
                "GiftId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                "ArchivedGifts");

            migrationBuilder.DropTable(
                "AspNetRoleClaims");

            migrationBuilder.DropTable(
                "AspNetUserClaims");

            migrationBuilder.DropTable(
                "AspNetUserLogins");

            migrationBuilder.DropTable(
                "AspNetUserRoles");

            migrationBuilder.DropTable(
                "AspNetUserTokens");

            migrationBuilder.DropTable(
                "CampaignDonatees");

            migrationBuilder.DropTable(
                "Friendships");

            migrationBuilder.DropTable(
                "InvitedUsers");

            migrationBuilder.DropTable(
                "PrivateMessages");

            migrationBuilder.DropTable(
                "ReservedGifts");

            migrationBuilder.DropTable(
                "UserCampaigns");

            migrationBuilder.DropTable(
                "UserNotifications");

            migrationBuilder.DropTable(
                "UserPermissions");

            migrationBuilder.DropTable(
                "UserProfiles");

            migrationBuilder.DropTable(
                "AspNetRoles");

            migrationBuilder.DropTable(
                "Donatees");

            migrationBuilder.DropTable(
                "Campaigns");

            migrationBuilder.DropTable(
                "Notifications");

            migrationBuilder.DropTable(
                "Permissions");

            migrationBuilder.DropTable(
                "Profiles");

            migrationBuilder.DropTable(
                "NotificationTypes");

            migrationBuilder.DropTable(
                "Wishlists");

            migrationBuilder.DropTable(
                "Gifts");

            migrationBuilder.DropTable(
                "ActionTypes");

            migrationBuilder.DropTable(
                "AspNetUsers");

            migrationBuilder.DropTable(
                "Statuses");
        }
    }
}