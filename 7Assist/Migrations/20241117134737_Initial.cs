using Microsoft.EntityFrameworkCore.Migrations;
using MySql.EntityFrameworkCore.Metadata;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _7Assist.Migrations
{
    /// <inheritdoc />
    public partial class Initial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterDatabase()
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "users",
                columns: table => new
                {
                    id_user = table.Column<int>(type: "int", nullable: false)
                        .Annotation("MySQL:ValueGenerationStrategy", MySQLValueGenerationStrategy.IdentityColumn),
                    login = table.Column<string>(type: "varchar(20)", maxLength: 20, nullable: false),
                    password = table.Column<string>(type: "varchar(255)", maxLength: 255, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_user);
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "admins",
                columns: table => new
                {
                    id_user = table.Column<int>(type: "int", nullable: false),
                    surname = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    name = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false),
                    patronymic = table.Column<string>(type: "varchar(45)", maxLength: 45, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_user);
                    table.ForeignKey(
                        name: "fk_admins_users",
                        column: x => x.id_user,
                        principalTable: "users",
                        principalColumn: "id_user");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.CreateTable(
                name: "terminals",
                columns: table => new
                {
                    id_user = table.Column<int>(type: "int", nullable: false),
                    address = table.Column<string>(type: "varchar(100)", maxLength: 100, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.id_user);
                    table.ForeignKey(
                        name: "fk_terminals_users1",
                        column: x => x.id_user,
                        principalTable: "users",
                        principalColumn: "id_user");
                })
                .Annotation("MySQL:Charset", "utf8mb4");

            migrationBuilder.InsertData(
                table: "users",
                columns: new[] { "id_user", "login", "password" },
                values: new object[,]
                {
                    { 1, "arhterminal", "$2a$11$Ded9whnU846IfIHHyDcXAeDxaZnHop2aGb2JJBqx.teF4UJw8uFKG" },
                    { 2, "ekbterminal", "$2a$11$EUKQ7Ybqn9Y.1d1k1OxYi.volMA5Q72xLALR11WxpWGf9c3o3nidy" },
                    { 3, "Ivan", "$2a$11$TDEJ1bx/lHpssa4sPRyOBusuXyC6rn6VuOE8QXkTY2PCVwzf5Sw0a" }
                });

            migrationBuilder.CreateIndex(
                name: "fk_admins_users_idx",
                table: "admins",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "fk_terminals_users1_idx",
                table: "terminals",
                column: "id_user");

            migrationBuilder.CreateIndex(
                name: "login_UNIQUE",
                table: "users",
                column: "login",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "admins");

            migrationBuilder.DropTable(
                name: "terminals");

            migrationBuilder.DropTable(
                name: "users");
        }
    }
}
