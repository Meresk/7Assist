using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace _7Assist.Migrations
{
    /// <inheritdoc />
    public partial class AddMoreData : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.InsertData(
                table: "admins",
                columns: new[] { "id_user", "name", "patronymic", "surname" },
                values: new object[] { 3, "Иван", "Иванович", "Иванов" });

            migrationBuilder.InsertData(
                table: "terminals",
                columns: new[] { "id_user", "address" },
                values: new object[,]
                {
                    { 1, "г. Архангельск" },
                    { 2, "г. Екатеринбург" }
                });

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id_user",
                keyValue: 1,
                column: "password",
                value: "$2a$11$DBc1a.5BMD3qZx4BFN1.mOv5I0BRkn7YGYadhhVdUvv546mdclqZW");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id_user",
                keyValue: 2,
                column: "password",
                value: "$2a$11$2MXBIphNzvNHuI8lekHKE.dyAVLEeTxQKVUVH2vb5/gNrTjuaivWa");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id_user",
                keyValue: 3,
                column: "password",
                value: "$2a$11$hbe8EP7TQZG6.kYzmd/QQOunf.yuHv38390O0Qneq4Q4l37MVywvS");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "admins",
                keyColumn: "id_user",
                keyValue: 3);

            migrationBuilder.DeleteData(
                table: "terminals",
                keyColumn: "id_user",
                keyValue: 1);

            migrationBuilder.DeleteData(
                table: "terminals",
                keyColumn: "id_user",
                keyValue: 2);

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id_user",
                keyValue: 1,
                column: "password",
                value: "$2a$11$Ded9whnU846IfIHHyDcXAeDxaZnHop2aGb2JJBqx.teF4UJw8uFKG");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id_user",
                keyValue: 2,
                column: "password",
                value: "$2a$11$EUKQ7Ybqn9Y.1d1k1OxYi.volMA5Q72xLALR11WxpWGf9c3o3nidy");

            migrationBuilder.UpdateData(
                table: "users",
                keyColumn: "id_user",
                keyValue: 3,
                column: "password",
                value: "$2a$11$TDEJ1bx/lHpssa4sPRyOBusuXyC6rn6VuOE8QXkTY2PCVwzf5Sw0a");
        }
    }
}
