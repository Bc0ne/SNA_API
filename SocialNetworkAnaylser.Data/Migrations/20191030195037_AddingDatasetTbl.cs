using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace SocialNetworkAnaylser.Data.Migrations
{
    public partial class AddingDatasetTbl : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<long>(
                name: "DatasetId",
                table: "Friendships",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.CreateTable(
                name: "Datasets",
                columns: table => new
                {
                    Id = table.Column<long>(nullable: false)
                        .Annotation("SqlServer:ValueGenerationStrategy", SqlServerValueGenerationStrategy.IdentityColumn),
                    CreationTime = table.Column<DateTime>(nullable: false),
                    DeletionTime = table.Column<DateTime>(nullable: true),
                    IsDeleted = table.Column<bool>(nullable: false),
                    Name = table.Column<string>(nullable: false),
                    IsImported = table.Column<bool>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Datasets", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Friendships_DatasetId",
                table: "Friendships",
                column: "DatasetId");

            migrationBuilder.AddForeignKey(
                name: "FK_Friendships_Datasets_DatasetId",
                table: "Friendships",
                column: "DatasetId",
                principalTable: "Datasets",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Friendships_Datasets_DatasetId",
                table: "Friendships");

            migrationBuilder.DropTable(
                name: "Datasets");

            migrationBuilder.DropIndex(
                name: "IX_Friendships_DatasetId",
                table: "Friendships");

            migrationBuilder.DropColumn(
                name: "DatasetId",
                table: "Friendships");
        }
    }
}
