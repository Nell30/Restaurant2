using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace WeeklyTask.Migrations.FoodDb
{
    public partial class OrderSer : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "date",
                table: "Reviews");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                table: "Orders",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Orders");

            migrationBuilder.AddColumn<DateTime>(
                name: "date",
                table: "Reviews",
                type: "datetime2",
                nullable: true);
        }
    }
}
