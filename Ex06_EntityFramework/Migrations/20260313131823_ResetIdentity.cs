using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Ex06_EntityFramework.Migrations
{
    /// <inheritdoc />
    public partial class ResetIdentity : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql("DBCC CHECKIDENT ('orders', RESEED, 0)");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {

        }
    }
}
