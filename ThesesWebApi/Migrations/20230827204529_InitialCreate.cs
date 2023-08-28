using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace ThesesWebApi.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "dataTableResults",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    totalItems = table.Column<int>(type: "integer", nullable: false),
                    page = table.Column<int>(type: "integer", nullable: false),
                    pageSize = table.Column<int>(type: "integer", nullable: false),
                    totalPages = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_dataTableResults", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "tableItemResource",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mainAuthor = table.Column<string>(type: "text", nullable: true),
                    contactEmail = table.Column<string>(type: "text", nullable: true),
                    topic = table.Column<string>(type: "text", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: false),
                    ThesisTableItemResourceDataTableResultId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_tableItemResource", x => x.Id);
                    table.ForeignKey(
                        name: "FK_tableItemResource_dataTableResults_ThesisTableItemResourceD~",
                        column: x => x.ThesisTableItemResourceDataTableResultId,
                        principalTable: "dataTableResults",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "form",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mainAuthorId = table.Column<int>(type: "integer", nullable: false),
                    contactEmail = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    topic = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    content = table.Column<string>(type: "character varying(5000)", maxLength: 5000, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_form", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "person",
                columns: table => new
                {
                    Id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    FirstName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    LastName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: false),
                    MiddleName = table.Column<string>(type: "character varying(255)", maxLength: 255, nullable: true),
                    Workplace = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    ThesisFormId = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_person", x => x.Id);
                    table.ForeignKey(
                        name: "FK_person_form_ThesisFormId",
                        column: x => x.ThesisFormId,
                        principalTable: "form",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateTable(
                name: "theses",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    mainAuthorId = table.Column<int>(type: "integer", nullable: true),
                    contactEmail = table.Column<string>(type: "text", nullable: true),
                    otherAuthorsId = table.Column<int>(type: "integer", nullable: true),
                    topic = table.Column<string>(type: "text", nullable: true),
                    content = table.Column<string>(type: "text", nullable: true),
                    created = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated = table.Column<DateTime>(type: "timestamp with time zone", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_theses", x => x.id);
                    table.ForeignKey(
                        name: "FK_theses_person_mainAuthorId",
                        column: x => x.mainAuthorId,
                        principalTable: "person",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_theses_person_otherAuthorsId",
                        column: x => x.otherAuthorsId,
                        principalTable: "person",
                        principalColumn: "Id");
                });

            migrationBuilder.CreateIndex(
                name: "IX_form_mainAuthorId",
                table: "form",
                column: "mainAuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_person_ThesisFormId",
                table: "person",
                column: "ThesisFormId");

            migrationBuilder.CreateIndex(
                name: "IX_tableItemResource_ThesisTableItemResourceDataTableResultId",
                table: "tableItemResource",
                column: "ThesisTableItemResourceDataTableResultId");

            migrationBuilder.CreateIndex(
                name: "IX_theses_mainAuthorId",
                table: "theses",
                column: "mainAuthorId");

            migrationBuilder.CreateIndex(
                name: "IX_theses_otherAuthorsId",
                table: "theses",
                column: "otherAuthorsId");

            migrationBuilder.AddForeignKey(
                name: "FK_form_person_mainAuthorId",
                table: "form",
                column: "mainAuthorId",
                principalTable: "person",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_form_person_mainAuthorId",
                table: "form");

            migrationBuilder.DropTable(
                name: "tableItemResource");

            migrationBuilder.DropTable(
                name: "theses");

            migrationBuilder.DropTable(
                name: "dataTableResults");

            migrationBuilder.DropTable(
                name: "person");

            migrationBuilder.DropTable(
                name: "form");
        }
    }
}
