using Microsoft.EntityFrameworkCore.Migrations;

namespace BookStore.Migrations
{
    public partial class add_account : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "account",
                columns: table => new
                {
                    username = table.Column<string>(unicode: false, maxLength: 100, nullable: false),
                    password = table.Column<string>(unicode: false, maxLength: 100, nullable: true),
                    fullname = table.Column<string>(maxLength: 100, nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PRIMARY", x => x.username);
                });
              migrationBuilder.InsertData("account",
               new[] { "username", "password", "fullname"},
               new object[,]
               {
                   {"hoang@gmail.com","123","Ho Thanh Hoang" },
                   {"hoang123@gmail.com","123","Hoang 123" }
               });
       }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "account");
        }
    }
}
