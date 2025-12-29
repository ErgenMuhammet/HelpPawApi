using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpPaw.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class CreateNotificationsTableFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Notifications",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    UserId = table.Column<string>(nullable: false),
                    Title = table.Column<string>(nullable: false),
                    Message = table.Column<string>(nullable: false),
                    Type = table.Column<int>(nullable: false),
                    IsRead = table.Column<bool>(nullable: false),
                    CreatedTime = table.Column<DateTime>(nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Notifications", x => x.Id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
       name: "Notifications");
        }
    }
}
