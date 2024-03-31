using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

#pragma warning disable CA1814 // Prefer jagged arrays over multidimensional

namespace DemoWebApi.Migrations
{
    /// <inheritdoc />
    public partial class Init : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Shirt",
                columns: table => new
                {
                    ShirtId = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Brand = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Color = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Size = table.Column<int>(type: "int", nullable: true),
                    Gender = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    price = table.Column<double>(type: "float", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Shirt", x => x.ShirtId);
                });

            migrationBuilder.InsertData(
                table: "Shirt",
                columns: new[] { "ShirtId", "Brand", "Color", "Gender", "Size", "price" },
                values: new object[,]
                {
                    { 1, "My Brand", "Blue", "Men", 10, 30.0 },
                    { 2, "My Brand", "Black", "Men", 12, 35.0 },
                    { 3, "Your Brand", "Pink", "women", 8, 28.0 },
                    { 4, "Your Brand", "Yellow", "women", 9, 30.0 }
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Shirt");
        }
    }
}
