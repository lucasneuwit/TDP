using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TDP.Migrations
{
    /// <inheritdoc />
    public partial class AddAdminUser : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: new Guid("6342aa21-db4a-44d4-a1c9-80dc22a22582"));

            migrationBuilder.DeleteData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: new Guid("7e90b67c-364f-440d-96fd-f344c83c4a5a"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("94cf6d33-3128-4cb2-b656-44381a59fe54"));

            migrationBuilder.AddColumn<string>(
                name: "user_type",
                table: "User",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "Country", "Discriminator", "ImdbId", "ImdbRating", "Plot", "PosterUrl", "Released", "Runtime", "Title", "Type" },
                values: new object[] { new Guid("5b8dc005-92be-46e0-9586-c9996ae60128"), "Somalia", "Movie", "", 8.3m, "Some not really important plot", "", new DateTime(2013, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 220L, "Batman", "Movie" });

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "Country", "Discriminator", "ImdbId", "ImdbRating", "Plot", "PosterUrl", "Released", "Runtime", "Title", "Type" },
                values: new object[] { new Guid("b4915b32-4123-45b2-a11e-2d1d7f508876"), "United States", "Movie", "", 9.3m, "Some not really important plot", "", new DateTime(2012, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 250L, "Avengers", "Movie" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "BirthDay", "EmailAddress", "IsAdministrator", "LastName", "Name", "PasswordHash", "Username", "user_type" },
                values: new object[] { new Guid("e2881293-fd8d-466a-9a0a-ab5b1193531a"), new DateTime(1945, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "elYusty@bokita.com", false, "Fabra", "Yusty", "boca", "elyusty", "User" });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
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

            migrationBuilder.DropColumn(
                name: "user_type",
                table: "User");

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "Country", "Discriminator", "ImdbId", "ImdbRating", "Plot", "PosterUrl", "Released", "Runtime", "Title", "Type" },
                values: new object[] { new Guid("6342aa21-db4a-44d4-a1c9-80dc22a22582"), "United States", "Movie", "", 9.3m, "Some not really important plot", "", new DateTime(2012, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 250L, "Avengers", "Movie" });

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "Country", "Discriminator", "ImdbId", "ImdbRating", "Plot", "PosterUrl", "Released", "Runtime", "Title", "Type" },
                values: new object[] { new Guid("7e90b67c-364f-440d-96fd-f344c83c4a5a"), "Somalia", "Movie", "", 8.3m, "Some not really important plot", "", new DateTime(2013, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 220L, "Batman", "Movie" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "BirthDay", "EmailAddress", "IsAdministrator", "LastName", "Name", "PasswordHash", "Username" },
                values: new object[] { new Guid("94cf6d33-3128-4cb2-b656-44381a59fe54"), new DateTime(1945, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "elYusty@bokita.com", false, "Fabra", "Yusty", "boca", "elyusty" });
        }
    }
}
