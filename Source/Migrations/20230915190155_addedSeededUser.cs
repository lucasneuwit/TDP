using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TDP.Migrations
{
    /// <inheritdoc />
    public partial class addedSeededUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: new Guid("116bcae8-7aed-4e95-9937-63158b9f302e"));

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "Country", "Discriminator", "ImdbRating", "Plot", "PosterUrl", "Released", "Runtime", "Title", "Type", "UserId" },
                values: new object[] { new Guid("02457466-821e-4376-985b-2f01bb827662"), "United States", "Movie", 9.3m, "Some not really important plot", "", new DateTime(2012, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 250L, "Avengers", "Movie", null });

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "Country", "Discriminator", "ImdbRating", "Plot", "PosterUrl", "Released", "Runtime", "Title", "Type", "UserId" },
                values: new object[] { new Guid("5cdf2f89-c1ba-4c52-b6ab-0b4842889203"), "Somalia", "Movie", 8.3m, "Some not really important plot", "", new DateTime(2013, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 220L, "Batman", "Movie", null });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "BirthDay", "EmailAddress", "IsAdministrator", "LastName", "Name", "PasswordHash", "Username" },
                values: new object[] { new Guid("51f3d4e5-eb9f-4af9-98ee-0cc02edf84c3"), new DateTime(1945, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "elYusty@bokita.com", false, "Fabra", "Yusty", "boca", "elyusty" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: new Guid("02457466-821e-4376-985b-2f01bb827662"));

            migrationBuilder.DeleteData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: new Guid("5cdf2f89-c1ba-4c52-b6ab-0b4842889203"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("51f3d4e5-eb9f-4af9-98ee-0cc02edf84c3"));

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "Country", "Discriminator", "ImdbRating", "Plot", "PosterUrl", "Released", "Runtime", "Title", "Type", "UserId" },
                values: new object[] { new Guid("116bcae8-7aed-4e95-9937-63158b9f302e"), "United States", "Movie", 9.3m, "Some not really important plot", "", new DateTime(2012, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 250L, "Avengers", "Movie", null });
        }
    }
}
