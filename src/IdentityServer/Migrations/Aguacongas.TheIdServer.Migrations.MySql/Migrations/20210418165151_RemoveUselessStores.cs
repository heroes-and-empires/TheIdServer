﻿// Project: Aguafrommars/TheIdServer
// Copyright (c) 2022 @Olivier Lefebvre
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

namespace Aguacongas.TheIdServer.MySql.Migrations
{
    public partial class RemoveUselessStores : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "AspNetUserTokens",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "AspNetUserRoles",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Id",
                table: "AspNetUserLogins",
                type: "longtext CHARACTER SET utf8mb4",
                nullable: true);

        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Id",
                table: "AspNetUserTokens");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AspNetUserRoles");

            migrationBuilder.DropColumn(
                name: "Id",
                table: "AspNetUserLogins");

        }
    }
}
