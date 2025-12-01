using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MyApi.Migrations
{
    /// <inheritdoc />
    public partial class RemoveUnexpectedTables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop unexpected tables in order to avoid foreign key constraint issues
            migrationBuilder.Sql(@"
                DROP TABLE IF EXISTS ""UserToUserMessages"" CASCADE;
                DROP TABLE IF EXISTS ""UserToUserChats"" CASCADE;
                DROP TABLE IF EXISTS ""UserTokens"" CASCADE;
                DROP TABLE IF EXISTS ""UserLogins"" CASCADE;
                DROP TABLE IF EXISTS ""UserConnectedDevices"" CASCADE;
                DROP TABLE IF EXISTS ""UserClaims"" CASCADE;
                DROP TABLE IF EXISTS ""UiPages"" CASCADE;
                DROP TABLE IF EXISTS ""SpareParts"" CASCADE;
                DROP TABLE IF EXISTS ""ServiceRequests"" CASCADE;
                DROP TABLE IF EXISTS ""ServiceRequestImages"" CASCADE;
                DROP TABLE IF EXISTS ""RoleClaims"" CASCADE;
                DROP TABLE IF EXISTS ""RefreshTokens"" CASCADE;
                DROP TABLE IF EXISTS ""Orders"" CASCADE;
                DROP TABLE IF EXISTS ""OrderItems"" CASCADE;
                DROP TABLE IF EXISTS ""Notifications"" CASCADE;
                DROP TABLE IF EXISTS ""Invoices"" CASCADE;
                DROP TABLE IF EXISTS ""InvoiceItems"" CASCADE;
                DROP TABLE IF EXISTS ""InstallationRequests"" CASCADE;
                DROP TABLE IF EXISTS ""Feedbacks"" CASCADE;
                DROP TABLE IF EXISTS ""Devices"" CASCADE;
                DROP TABLE IF EXISTS ""CustomerDevices"" CASCADE;
                DROP TABLE IF EXISTS ""ClientOrganizations"" CASCADE;
                DROP TABLE IF EXISTS ""ChatMessages"" CASCADE;
                DROP TABLE IF EXISTS ""Addresses"" CASCADE;
            ");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // No down migration - these tables are not part of the expected schema
            // If you need to restore them, use a database backup
        }
    }
}
