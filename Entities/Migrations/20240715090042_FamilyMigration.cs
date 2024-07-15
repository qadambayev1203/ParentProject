using System;
using Microsoft.EntityFrameworkCore.Migrations;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace Entities.Migrations
{
    /// <inheritdoc />
    public partial class FamilyMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "statuses_2024parent",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    status = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_statuses_2024parent", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "files_2024parent",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    url = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: true),
                    status_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_files_2024parent", x => x.id);
                    table.ForeignKey(
                        name: "FK_files_2024parent_statuses_2024parent_status_id",
                        column: x => x.status_id,
                        principalTable: "statuses_2024parent",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "user_types_2024parent",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    type = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    status_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_user_types_2024parent", x => x.id);
                    table.ForeignKey(
                        name: "FK_user_types_2024parent_statuses_2024parent_status_id",
                        column: x => x.status_id,
                        principalTable: "statuses_2024parent",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "departament_2024parent",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    title = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    first_name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    last_name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    father_name = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: true),
                    birthday = table.Column<DateOnly>(type: "date", nullable: true),
                    died_day = table.Column<DateOnly>(type: "date", nullable: true),
                    is_died = table.Column<bool>(type: "boolean", nullable: true),
                    is_spouse = table.Column<bool>(type: "boolean", nullable: true),
                    text = table.Column<string>(type: "text", nullable: true),
                    parent_id = table.Column<int>(type: "integer", nullable: true),
                    status_id = table.Column<int>(type: "integer", nullable: true),
                    crated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    updated_at = table.Column<DateTime>(type: "timestamp with time zone", nullable: true),
                    img_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_departament_2024parent", x => x.id);
                    table.ForeignKey(
                        name: "FK_departament_2024parent_files_2024parent_img_id",
                        column: x => x.img_id,
                        principalTable: "files_2024parent",
                        principalColumn: "id");
                    table.ForeignKey(
                        name: "FK_departament_2024parent_statuses_2024parent_status_id",
                        column: x => x.status_id,
                        principalTable: "statuses_2024parent",
                        principalColumn: "id");
                });

            migrationBuilder.CreateTable(
                name: "users_2024parent",
                columns: table => new
                {
                    id = table.Column<int>(type: "integer", nullable: false)
                        .Annotation("Npgsql:ValueGenerationStrategy", NpgsqlValueGenerationStrategy.IdentityByDefaultColumn),
                    login = table.Column<string>(type: "character varying(250)", maxLength: 250, nullable: false),
                    password = table.Column<string>(type: "character varying(500)", maxLength: 500, nullable: false),
                    token = table.Column<string>(type: "text", nullable: true),
                    user_type_id = table.Column<int>(type: "integer", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_users_2024parent", x => x.id);
                    table.ForeignKey(
                        name: "FK_users_2024parent_user_types_2024parent_user_type_id",
                        column: x => x.user_type_id,
                        principalTable: "user_types_2024parent",
                        principalColumn: "id");
                });

            migrationBuilder.InsertData(
                table: "statuses_2024parent",
                columns: new[] { "id", "status" },
                values: new object[,]
                {
                    { 1, "Active" },
                    { 2, "Deleted" }
                });

            migrationBuilder.InsertData(
                table: "user_types_2024parent",
                columns: new[] { "id", "status_id", "type" },
                values: new object[] { 1, null, "Admin" });

            migrationBuilder.InsertData(
                table: "users_2024parent",
                columns: new[] { "id", "login", "password", "token", "user_type_id" },
                values: new object[] { 1, "family@admin.family", "lFoqX0fowBrEqw4CRj8D0r+x9FKY0U0KbEkxTb5wEz0=", null, 1 });

            migrationBuilder.CreateIndex(
                name: "IX_departament_2024parent_img_id",
                table: "departament_2024parent",
                column: "img_id");

            migrationBuilder.CreateIndex(
                name: "IX_departament_2024parent_status_id",
                table: "departament_2024parent",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_files_2024parent_status_id",
                table: "files_2024parent",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_files_2024parent_title",
                table: "files_2024parent",
                column: "title",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_user_types_2024parent_status_id",
                table: "user_types_2024parent",
                column: "status_id");

            migrationBuilder.CreateIndex(
                name: "IX_users_2024parent_login",
                table: "users_2024parent",
                column: "login",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_users_2024parent_user_type_id",
                table: "users_2024parent",
                column: "user_type_id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "departament_2024parent");

            migrationBuilder.DropTable(
                name: "users_2024parent");

            migrationBuilder.DropTable(
                name: "files_2024parent");

            migrationBuilder.DropTable(
                name: "user_types_2024parent");

            migrationBuilder.DropTable(
                name: "statuses_2024parent");
        }
    }
}
