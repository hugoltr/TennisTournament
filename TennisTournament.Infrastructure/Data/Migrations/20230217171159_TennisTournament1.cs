using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TennisTournament.Data.Migrations
{
    public partial class TennisTournament1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Admins",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Admins", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Courts",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Number = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Courts", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Players",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nationality = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Sexe = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Players", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Presses",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Password = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Presses", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Referees",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nationality = table.Column<int>(type: "int", maxLength: 50, nullable: false),
                    Sexe = table.Column<int>(type: "int", maxLength: 20, nullable: false),
                    FirstName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    LastName = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Referees", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Tournaments",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Name = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tournaments", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "Matchs",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StartingDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    FirstPlayerID = table.Column<int>(type: "int", nullable: false),
                    SecondPlayerID = table.Column<int>(type: "int", nullable: false),
                    RefereeID = table.Column<int>(type: "int", nullable: false),
                    CourtID = table.Column<int>(type: "int", nullable: false),
                    TournamentID = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Matchs", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Matchs_Courts_CourtID",
                        column: x => x.CourtID,
                        principalTable: "Courts",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matchs_Players_FirstPlayerID",
                        column: x => x.FirstPlayerID,
                        principalTable: "Players",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Matchs_Players_SecondPlayerID",
                        column: x => x.SecondPlayerID,
                        principalTable: "Players",
                        principalColumn: "ID");
                    table.ForeignKey(
                        name: "FK_Matchs_Referees_RefereeID",
                        column: x => x.RefereeID,
                        principalTable: "Referees",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Matchs_Tournaments_TournamentID",
                        column: x => x.TournamentID,
                        principalTable: "Tournaments",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Results",
                columns: table => new
                {
                    ID = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    EndingDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "getdate()"),
                    PlayerID = table.Column<int>(type: "int", nullable: false),
                    MatchID = table.Column<int>(type: "int", nullable: false),
                    Scores = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Results", x => x.ID);
                    table.ForeignKey(
                        name: "FK_Results_Matchs_MatchID",
                        column: x => x.MatchID,
                        principalTable: "Matchs",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Results_Players_PlayerID",
                        column: x => x.PlayerID,
                        principalTable: "Players",
                        principalColumn: "ID",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Admins",
                columns: new[] { "ID", "FirstName", "LastName", "Password" },
                values: new object[] { 1, "AdminF", "AdminL", "Password" });

            migrationBuilder.InsertData(
                table: "Courts",
                columns: new[] { "ID", "Name", "Number" },
                values: new object[] { 1, "CourtTest", 111 });

            migrationBuilder.InsertData(
                table: "Players",
                columns: new[] { "ID", "FirstName", "LastName", "Nationality", "Sexe" },
                values: new object[,]
                {
                    { 1, "Quentin", "CLAVIER", 67, 0 },
                    { 2, "Hugo", "LETOURNEUR", 67, 0 }
                });

            migrationBuilder.InsertData(
                table: "Presses",
                columns: new[] { "ID", "FirstName", "LastName", "Password" },
                values: new object[] { 1, "PressF", "PressL", "Password" });

            migrationBuilder.InsertData(
                table: "Referees",
                columns: new[] { "ID", "FirstName", "LastName", "Nationality", "Sexe" },
                values: new object[] { 1, "ArbitreF", "ArbitreL", 176, 0 });

            migrationBuilder.InsertData(
                table: "Tournaments",
                columns: new[] { "ID", "Name" },
                values: new object[] { 1, "Test" });

            migrationBuilder.InsertData(
                table: "Matchs",
                columns: new[] { "ID", "CourtID", "FirstPlayerID", "RefereeID", "SecondPlayerID", "TournamentID" },
                values: new object[] { 1, 1, 1, 1, 2, 1 });

            migrationBuilder.InsertData(
                table: "Results",
                columns: new[] { "ID", "MatchID", "PlayerID", "Scores" },
                values: new object[] { 1, 1, 1, "6-2, 6-0, 6-1" });

            migrationBuilder.CreateIndex(
                name: "IX_Matchs_CourtID",
                table: "Matchs",
                column: "CourtID");

            migrationBuilder.CreateIndex(
                name: "IX_Matchs_FirstPlayerID",
                table: "Matchs",
                column: "FirstPlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_Matchs_RefereeID",
                table: "Matchs",
                column: "RefereeID");

            migrationBuilder.CreateIndex(
                name: "IX_Matchs_SecondPlayerID",
                table: "Matchs",
                column: "SecondPlayerID");

            migrationBuilder.CreateIndex(
                name: "IX_Matchs_TournamentID",
                table: "Matchs",
                column: "TournamentID");

            migrationBuilder.CreateIndex(
                name: "IX_Results_MatchID",
                table: "Results",
                column: "MatchID");

            migrationBuilder.CreateIndex(
                name: "IX_Results_PlayerID",
                table: "Results",
                column: "PlayerID");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Admins");

            migrationBuilder.DropTable(
                name: "Presses");

            migrationBuilder.DropTable(
                name: "Results");

            migrationBuilder.DropTable(
                name: "Matchs");

            migrationBuilder.DropTable(
                name: "Courts");

            migrationBuilder.DropTable(
                name: "Players");

            migrationBuilder.DropTable(
                name: "Referees");

            migrationBuilder.DropTable(
                name: "Tournaments");
        }
    }
}
