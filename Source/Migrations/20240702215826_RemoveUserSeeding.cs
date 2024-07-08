using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace TDP.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUserSeeding : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
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

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "Country", "Discriminator", "ImdbId", "ImdbRating", "Plot", "PosterUrl", "Released", "Runtime", "Title", "Type" },
                values: new object[,]
                {
                    { new Guid("451e89b9-f5af-40a1-b70d-1bd4b465b408"), "United States", "Movie", "", 9.3m, "Some not really important plot", "", new DateTime(2012, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 250L, "Avengers", "Movie" },
                    { new Guid("6e7cac96-8f43-4888-ab94-1e8eddaca289"), "Somalia", "Movie", "", 8.3m, "Some not really important plot", "", new DateTime(2013, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 220L, "Batman", "Movie" }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: new Guid("451e89b9-f5af-40a1-b70d-1bd4b465b408"));

            migrationBuilder.DeleteData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: new Guid("6e7cac96-8f43-4888-ab94-1e8eddaca289"));

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
    }
}
