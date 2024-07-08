using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TDP.Migrations
{
    /// <inheritdoc />
    public partial class ProfilePicture : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: new Guid("5b8dc005-92be-46e0-9586-c9996ae60128"));

            migrationBuilder.DeleteData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: new Guid("b4915b32-4123-45b2-a11e-2d1d7f508876"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("e2881293-fd8d-466a-9a0a-ab5b1193531a"));

            migrationBuilder.AlterColumn<string>(
                name: "user_type",
                table: "User",
                type: "nvarchar(13)",
                maxLength: 13,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.AddColumn<byte[]>(
                name: "ProfilePicture",
                table: "User",
                type: "varbinary(max)",
                nullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Movie",
                type: "nvarchar(8)",
                maxLength: 8,
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)");

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "Country", "Discriminator", "ImdbId", "ImdbRating", "Plot", "PosterUrl", "Released", "Runtime", "Title", "Type" },
                values: new object[,]
                {
                    { new Guid("435f6d85-776d-434c-9d51-18a2cfcf2432"), "United States", "Movie", "", 9.3m, "Some not really important plot", "", new DateTime(2012, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 250L, "Avengers", "Movie" },
                    { new Guid("9e4e82eb-9426-4d0a-9cf2-ab552c4cbcfb"), "Somalia", "Movie", "", 8.3m, "Some not really important plot", "", new DateTime(2013, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 220L, "Batman", "Movie" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "BirthDay", "EmailAddress", "IsAdministrator", "LastName", "Name", "PasswordHash", "ProfilePicture", "Username", "user_type" },
                values: new object[] { new Guid("1e97e75d-af4b-431d-bd55-98e954fe4427"), new DateTime(1945, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "elYusty@bokita.com", false, "Fabra", "Yusty", "boca", null, "elyusty", "User" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: new Guid("435f6d85-776d-434c-9d51-18a2cfcf2432"));

            migrationBuilder.DeleteData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: new Guid("9e4e82eb-9426-4d0a-9cf2-ab552c4cbcfb"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("1e97e75d-af4b-431d-bd55-98e954fe4427"));

            migrationBuilder.DropColumn(
                name: "ProfilePicture",
                table: "User");

            migrationBuilder.AlterColumn<string>(
                name: "user_type",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(13)",
                oldMaxLength: 13);

            migrationBuilder.AlterColumn<string>(
                name: "Discriminator",
                table: "Movie",
                type: "nvarchar(max)",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(8)",
                oldMaxLength: 8);

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "Country", "Discriminator", "ImdbId", "ImdbRating", "Plot", "PosterUrl", "Released", "Runtime", "Title", "Type" },
                values: new object[,]
                {
                    { new Guid("5b8dc005-92be-46e0-9586-c9996ae60128"), "Somalia", "Movie", "", 8.3m, "Some not really important plot", "", new DateTime(2013, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 220L, "Batman", "Movie" },
                    { new Guid("b4915b32-4123-45b2-a11e-2d1d7f508876"), "United States", "Movie", "", 9.3m, "Some not really important plot", "", new DateTime(2012, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 250L, "Avengers", "Movie" }
                });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "BirthDay", "EmailAddress", "IsAdministrator", "LastName", "Name", "PasswordHash", "Username", "user_type" },
                values: new object[] { new Guid("e2881293-fd8d-466a-9a0a-ab5b1193531a"), new DateTime(1945, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "elYusty@bokita.com", false, "Fabra", "Yusty", "boca", "elyusty", "User" });
        }
    }
}
