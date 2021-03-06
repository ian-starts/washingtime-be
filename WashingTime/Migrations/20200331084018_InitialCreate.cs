﻿using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace WashingTime.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "WashingTimes",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    StartDateTime = table.Column<DateTime>(nullable: false),
                    EndDateTime = table.Column<DateTime>(nullable: false),
                    WasherType = table.Column<int>(nullable: false),
                    UserId = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_WashingTimes", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_WashingTimes_StartDateTime_EndDateTime_WasherType",
                table: "WashingTimes",
                columns: new[] { "StartDateTime", "EndDateTime", "WasherType" },
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "WashingTimes");
        }
    }
}
