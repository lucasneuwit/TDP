using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TDP.Migrations
{
    /// <inheritdoc />
    public partial class AddFollowers : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Movie_User_UserId",
                table: "Movie");

            migrationBuilder.DropIndex(
                name: "IX_Movie_UserId",
                table: "Movie");

            migrationBuilder.DeleteData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: new Guid("d660c3d0-b00c-4f52-b869-1b112ef5a1d6"));

            migrationBuilder.DeleteData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: new Guid("d73c6774-893c-4246-8d60-d80471d46997"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("9c1182c4-59ed-4603-88a8-5d22861af689"));

            migrationBuilder.DropColumn(
                name: "UserId",
                table: "Movie");

            migrationBuilder.CreateTable(
                name: "MovieUser",
                columns: table => new
                {
                    FollowedMoviesId = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    FollowersId = table.Column<Guid>(type: "uniqueidentifier", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovieUser", x => new { x.FollowedMoviesId, x.FollowersId });
                    table.ForeignKey(
                        name: "FK_MovieUser_Movie_FollowedMoviesId",
                        column: x => x.FollowedMoviesId,
                        principalTable: "Movie",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_MovieUser_User_FollowersId",
                        column: x => x.FollowersId,
                        principalTable: "User",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "Country", "Discriminator", "ImdbId", "ImdbRating", "Plot", "PosterUrl", "Released", "Runtime", "Title", "Type" },
                values: new object[] { new Guid("0de8d323-53d4-48ad-a1ff-8a91dd141595"), "Somalia", "Movie", "", 8.3m, "Some not really important plot", "", new DateTime(2013, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 220L, "Batman", "Movie" });

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "Country", "Discriminator", "ImdbId", "ImdbRating", "Plot", "PosterUrl", "Released", "Runtime", "Title", "Type" },
                values: new object[] { new Guid("8f38966e-dfa8-45d9-8d4f-b86b330786cd"), "United States", "Movie", "", 9.3m, "Some not really important plot", "", new DateTime(2012, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 250L, "Avengers", "Movie" });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "BirthDay", "EmailAddress", "IsAdministrator", "LastName", "Name", "PasswordHash", "Username" },
                values: new object[] { new Guid("c06d24c3-2303-47ff-85c2-64def885d63d"), new DateTime(1945, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "elYusty@bokita.com", false, "Fabra", "Yusty", "boca", "elyusty" });

            migrationBuilder.CreateIndex(
                name: "IX_MovieUser_FollowersId",
                table: "MovieUser",
                column: "FollowersId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "MovieUser");

            migrationBuilder.DeleteData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: new Guid("0de8d323-53d4-48ad-a1ff-8a91dd141595"));

            migrationBuilder.DeleteData(
                table: "Movie",
                keyColumn: "Id",
                keyValue: new Guid("8f38966e-dfa8-45d9-8d4f-b86b330786cd"));

            migrationBuilder.DeleteData(
                table: "User",
                keyColumn: "Id",
                keyValue: new Guid("c06d24c3-2303-47ff-85c2-64def885d63d"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                table: "Movie",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "Country", "Discriminator", "ImdbId", "ImdbRating", "Plot", "PosterUrl", "Released", "Runtime", "Title", "Type", "UserId" },
                values: new object[] { new Guid("d660c3d0-b00c-4f52-b869-1b112ef5a1d6"), "United States", "Movie", "", 9.3m, "Some not really important plot", "", new DateTime(2012, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 250L, "Avengers", "Movie", null });

            migrationBuilder.InsertData(
                table: "Movie",
                columns: new[] { "Id", "Country", "Discriminator", "ImdbId", "ImdbRating", "Plot", "PosterUrl", "Released", "Runtime", "Title", "Type", "UserId" },
                values: new object[] { new Guid("d73c6774-893c-4246-8d60-d80471d46997"), "Somalia", "Movie", "", 8.3m, "Some not really important plot", "", new DateTime(2013, 1, 14, 0, 0, 0, 0, DateTimeKind.Unspecified), 220L, "Batman", "Movie", null });

            migrationBuilder.InsertData(
                table: "User",
                columns: new[] { "Id", "BirthDay", "EmailAddress", "IsAdministrator", "LastName", "Name", "PasswordHash", "Username" },
                values: new object[] { new Guid("9c1182c4-59ed-4603-88a8-5d22861af689"), new DateTime(1945, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), "elYusty@bokita.com", false, "Fabra", "Yusty", "boca", "elyusty" });

            migrationBuilder.CreateIndex(
                name: "IX_Movie_UserId",
                table: "Movie",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Movie_User_UserId",
                table: "Movie",
                column: "UserId",
                principalTable: "User",
                principalColumn: "Id");
        }
    }
}
