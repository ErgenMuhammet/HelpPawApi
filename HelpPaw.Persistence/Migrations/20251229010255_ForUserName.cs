using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace HelpPaw.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ForUserName : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
       
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsDeleted",
                table: "Notifications",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "MyProperty",
                table: "Notifications",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }
    }
}
