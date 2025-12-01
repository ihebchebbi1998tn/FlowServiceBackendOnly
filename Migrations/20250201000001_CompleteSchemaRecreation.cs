using Microsoft.EntityFrameworkCore.Migrations;
using System;

#nullable disable

namespace MyApi.Migrations
{
    /// <inheritdoc />
    public partial class CompleteSchemaRecreation : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            // Drop all existing tables in reverse dependency order
            migrationBuilder.Sql(@"
                DROP TABLE IF EXISTS ""dispatch_history"" CASCADE;
                DROP TABLE IF EXISTS ""technician_status_history"" CASCADE;
                DROP TABLE IF EXISTS ""technician_leaves"" CASCADE;
                DROP TABLE IF EXISTS ""technician_working_hours"" CASCADE;
                DROP TABLE IF EXISTS ""dispatch_notes"" CASCADE;
                DROP TABLE IF EXISTS ""dispatch_attachments"" CASCADE;
                DROP TABLE IF EXISTS ""dispatch_materials"" CASCADE;
                DROP TABLE IF EXISTS ""dispatch_expenses"" CASCADE;
                DROP TABLE IF EXISTS ""dispatch_time_entries"" CASCADE;
                DROP TABLE IF EXISTS ""dispatch_technicians"" CASCADE;
                DROP TABLE IF EXISTS ""dispatches"" CASCADE;
                DROP TABLE IF EXISTS ""dailytasks"" CASCADE;
                DROP TABLE IF EXISTS ""taskattachments"" CASCADE;
                DROP TABLE IF EXISTS ""taskcomments"" CASCADE;
                DROP TABLE IF EXISTS ""projecttasks"" CASCADE;
                DROP TABLE IF EXISTS ""projectcolumns"" CASCADE;
                DROP TABLE IF EXISTS ""projects"" CASCADE;
                DROP TABLE IF EXISTS ""service_order_jobs"" CASCADE;
                DROP TABLE IF EXISTS ""service_orders"" CASCADE;
                DROP TABLE IF EXISTS ""maintenance_histories"" CASCADE;
                DROP TABLE IF EXISTS ""installations"" CASCADE;
                DROP TABLE IF EXISTS ""sale_activities"" CASCADE;
                DROP TABLE IF EXISTS ""sale_items"" CASCADE;
                DROP TABLE IF EXISTS ""sales"" CASCADE;
                DROP TABLE IF EXISTS ""offer_activities"" CASCADE;
                DROP TABLE IF EXISTS ""offer_items"" CASCADE;
                DROP TABLE IF EXISTS ""offers"" CASCADE;
                DROP TABLE IF EXISTS ""event_reminders"" CASCADE;
                DROP TABLE IF EXISTS ""event_attendees"" CASCADE;
                DROP TABLE IF EXISTS ""calendar_events"" CASCADE;
                DROP TABLE IF EXISTS ""event_types"" CASCADE;
                DROP TABLE IF EXISTS ""inventory_transactions"" CASCADE;
                DROP TABLE IF EXISTS ""locations"" CASCADE;
                DROP TABLE IF EXISTS ""article_categories"" CASCADE;
                DROP TABLE IF EXISTS ""articles"" CASCADE;
                DROP TABLE IF EXISTS ""ContactNotes"" CASCADE;
                DROP TABLE IF EXISTS ""ContactTagAssignments"" CASCADE;
                DROP TABLE IF EXISTS ""ContactTags"" CASCADE;
                DROP TABLE IF EXISTS ""Contacts"" CASCADE;
                DROP TABLE IF EXISTS ""Currencies"" CASCADE;
                DROP TABLE IF EXISTS ""LookupItems"" CASCADE;
                DROP TABLE IF EXISTS ""RoleSkills"" CASCADE;
                DROP TABLE IF EXISTS ""UserSkills"" CASCADE;
                DROP TABLE IF EXISTS ""UserRoles"" CASCADE;
                DROP TABLE IF EXISTS ""Skills"" CASCADE;
                DROP TABLE IF EXISTS ""Roles"" CASCADE;
                DROP TABLE IF EXISTS ""UserPreferences"" CASCADE;
                DROP TABLE IF EXISTS ""Users"" CASCADE;
                DROP TABLE IF EXISTS ""MainAdminUsers"" CASCADE;
            ");

            // Create MainAdminUsers table
            migrationBuilder.Sql(@"
                CREATE TABLE ""MainAdminUsers"" (
                    ""Id"" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                    ""Username"" VARCHAR(100) NOT NULL UNIQUE,
                    ""Email"" VARCHAR(255) NOT NULL UNIQUE,
                    ""PasswordHash"" VARCHAR(500) NOT NULL,
                    ""FirstName"" VARCHAR(100),
                    ""LastName"" VARCHAR(100),
                    ""Avatar"" VARCHAR(500),
                    ""IsActive"" BOOLEAN NOT NULL DEFAULT TRUE,
                    ""OnboardingCompleted"" BOOLEAN NOT NULL DEFAULT FALSE,
                    ""CreatedAt"" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    ""UpdatedAt"" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP
                );
            ");

            // Create Users table
            migrationBuilder.Sql(@"
                CREATE TABLE ""Users"" (
                    ""Id"" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                    ""Username"" VARCHAR(100) NOT NULL UNIQUE,
                    ""Email"" VARCHAR(255) NOT NULL UNIQUE,
                    ""PasswordHash"" VARCHAR(500) NOT NULL,
                    ""FirstName"" VARCHAR(100),
                    ""LastName"" VARCHAR(100),
                    ""Avatar"" VARCHAR(500),
                    ""Phone"" VARCHAR(50),
                    ""IsActive"" BOOLEAN NOT NULL DEFAULT TRUE,
                    ""HireDate"" DATE,
                    ""CreatedAt"" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    ""UpdatedAt"" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    ""CreatedUser"" VARCHAR(100),
                    ""ModifyUser"" VARCHAR(100),
                    ""IsDeleted"" BOOLEAN NOT NULL DEFAULT FALSE
                );
            ");

            // Create UserPreferences table
            migrationBuilder.Sql(@"
                CREATE TABLE ""UserPreferences"" (
                    ""Id"" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                    ""UserId"" UUID NOT NULL UNIQUE,
                    ""Theme"" VARCHAR(20) NOT NULL DEFAULT 'light',
                    ""Language"" VARCHAR(10) NOT NULL DEFAULT 'en',
                    ""Timezone"" VARCHAR(50) NOT NULL DEFAULT 'UTC',
                    ""PreferencesJson"" JSONB,
                    ""CreatedAt"" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    ""UpdatedAt"" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY (""UserId"") REFERENCES ""Users""(""Id"") ON DELETE CASCADE
                );
            ");

            // Create Roles table
            migrationBuilder.Sql(@"
                CREATE TABLE ""Roles"" (
                    ""Id"" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                    ""Name"" VARCHAR(100) NOT NULL UNIQUE,
                    ""Description"" VARCHAR(500),
                    ""CreatedAt"" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    ""UpdatedAt"" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    ""IsDeleted"" BOOLEAN NOT NULL DEFAULT FALSE
                );
            ");

            // Create Skills table
            migrationBuilder.Sql(@"
                CREATE TABLE ""Skills"" (
                    ""Id"" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                    ""Name"" VARCHAR(100) NOT NULL UNIQUE,
                    ""Description"" VARCHAR(500),
                    ""Category"" VARCHAR(100),
                    ""CreatedAt"" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    ""UpdatedAt"" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    ""IsDeleted"" BOOLEAN NOT NULL DEFAULT FALSE
                );
            ");

            // Create UserRoles table
            migrationBuilder.Sql(@"
                CREATE TABLE ""UserRoles"" (
                    ""Id"" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                    ""UserId"" UUID NOT NULL,
                    ""RoleId"" UUID NOT NULL,
                    ""AssignedAt"" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY (""UserId"") REFERENCES ""Users""(""Id"") ON DELETE CASCADE,
                    FOREIGN KEY (""RoleId"") REFERENCES ""Roles""(""Id"") ON DELETE CASCADE,
                    UNIQUE (""UserId"", ""RoleId"")
                );
            ");

            // Create UserSkills table
            migrationBuilder.Sql(@"
                CREATE TABLE ""UserSkills"" (
                    ""Id"" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                    ""UserId"" UUID NOT NULL,
                    ""SkillId"" UUID NOT NULL,
                    ""ProficiencyLevel"" INTEGER NOT NULL DEFAULT 1,
                    ""YearsOfExperience"" DECIMAL(4,1),
                    ""AssignedAt"" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    FOREIGN KEY (""UserId"") REFERENCES ""Users""(""Id"") ON DELETE CASCADE,
                    FOREIGN KEY (""SkillId"") REFERENCES ""Skills""(""Id"") ON DELETE CASCADE,
                    UNIQUE (""UserId"", ""SkillId"")
                );
            ");

            // Create RoleSkills table
            migrationBuilder.Sql(@"
                CREATE TABLE ""RoleSkills"" (
                    ""Id"" UUID PRIMARY KEY DEFAULT gen_random_uuid(),
                    ""RoleId"" UUID NOT NULL,
                    ""SkillId"" UUID NOT NULL,
                    ""RequiredProficiencyLevel"" INTEGER NOT NULL DEFAULT 1,
                    ""IsRequired"" BOOLEAN NOT NULL DEFAULT FALSE,
                    FOREIGN KEY (""RoleId"") REFERENCES ""Roles""(""Id"") ON DELETE CASCADE,
                    FOREIGN KEY (""SkillId"") REFERENCES ""Skills""(""Id"") ON DELETE CASCADE,
                    UNIQUE (""RoleId"", ""SkillId"")
                );
            ");

            // Create LookupItems and Currencies tables
            migrationBuilder.Sql(@"
                CREATE TABLE ""LookupItems"" (
                    ""Id"" VARCHAR(50) PRIMARY KEY,
                    ""Name"" VARCHAR(100) NOT NULL,
                    ""Description"" VARCHAR(500),
                    ""Color"" VARCHAR(20),
                    ""LookupType"" VARCHAR(50) NOT NULL,
                    ""IsActive"" BOOLEAN NOT NULL DEFAULT TRUE,
                    ""SortOrder"" INTEGER NOT NULL DEFAULT 0,
                    ""CreatedUser"" VARCHAR(100) NOT NULL,
                    ""ModifyUser"" VARCHAR(100),
                    ""CreatedAt"" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    ""UpdatedAt"" TIMESTAMP,
                    ""IsDeleted"" BOOLEAN NOT NULL DEFAULT FALSE,
                    ""Level"" INTEGER,
                    ""IsCompleted"" BOOLEAN,
                    ""DefaultDuration"" INTEGER,
                    ""IsAvailable"" BOOLEAN,
                    ""IsPaid"" BOOLEAN,
                    ""Category"" VARCHAR(100)
                );

                CREATE TABLE ""Currencies"" (
                    ""Id"" VARCHAR(3) PRIMARY KEY,
                    ""Name"" VARCHAR(100) NOT NULL,
                    ""Symbol"" VARCHAR(10) NOT NULL,
                    ""Code"" VARCHAR(3) NOT NULL,
                    ""IsActive"" BOOLEAN NOT NULL DEFAULT TRUE,
                    ""IsDefault"" BOOLEAN NOT NULL DEFAULT FALSE,
                    ""SortOrder"" INTEGER NOT NULL DEFAULT 0,
                    ""CreatedUser"" VARCHAR(100) NOT NULL,
                    ""ModifyUser"" VARCHAR(100),
                    ""CreatedAt"" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    ""UpdatedAt"" TIMESTAMP,
                    ""IsDeleted"" BOOLEAN NOT NULL DEFAULT FALSE
                );
            ");

            // Create Contacts module tables
            migrationBuilder.Sql(@"
                CREATE TABLE ""Contacts"" (
                    ""Id"" SERIAL PRIMARY KEY,
                    ""Name"" VARCHAR(255) NOT NULL,
                    ""Email"" VARCHAR(255) NOT NULL,
                    ""Phone"" VARCHAR(50),
                    ""Company"" VARCHAR(255),
                    ""Position"" VARCHAR(255),
                    ""Status"" VARCHAR(50) NOT NULL DEFAULT 'active',
                    ""Type"" VARCHAR(50) NOT NULL DEFAULT 'individual',
                    ""Address"" VARCHAR(500),
                    ""Avatar"" VARCHAR(500),
                    ""Favorite"" BOOLEAN NOT NULL DEFAULT FALSE,
                    ""LastContactDate"" TIMESTAMP,
                    ""CreatedAt"" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    ""UpdatedAt"" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    ""CreatedBy"" VARCHAR(255),
                    ""ModifiedBy"" VARCHAR(255),
                    ""IsDeleted"" BOOLEAN NOT NULL DEFAULT FALSE
                );

                CREATE TABLE ""ContactTags"" (
                    ""Id"" SERIAL PRIMARY KEY,
                    ""Name"" VARCHAR(100) NOT NULL UNIQUE,
                    ""Color"" VARCHAR(50) NOT NULL DEFAULT '#3b82f6',
                    ""Description"" VARCHAR(500),
                    ""CreatedAt"" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    ""IsDeleted"" BOOLEAN NOT NULL DEFAULT FALSE
                );

                CREATE TABLE ""ContactTagAssignments"" (
                    ""Id"" SERIAL PRIMARY KEY,
                    ""ContactId"" INTEGER NOT NULL,
                    ""TagId"" INTEGER NOT NULL,
                    ""AssignedAt"" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    ""AssignedBy"" VARCHAR(255),
                    FOREIGN KEY (""ContactId"") REFERENCES ""Contacts""(""Id"") ON DELETE CASCADE,
                    FOREIGN KEY (""TagId"") REFERENCES ""ContactTags""(""Id"") ON DELETE CASCADE,
                    UNIQUE (""ContactId"", ""TagId"")
                );

                CREATE TABLE ""ContactNotes"" (
                    ""Id"" SERIAL PRIMARY KEY,
                    ""ContactId"" INTEGER NOT NULL,
                    ""Content"" VARCHAR(2000) NOT NULL,
                    ""CreatedAt"" TIMESTAMP NOT NULL DEFAULT CURRENT_TIMESTAMP,
                    ""CreatedBy"" VARCHAR(255),
                    FOREIGN KEY (""ContactId"") REFERENCES ""Contacts""(""Id"") ON DELETE CASCADE
                );
            ");

            // Run all remaining SQL scripts from Neon folder
            migrationBuilder.Sql(System.IO.File.ReadAllText("Neon/04_articles.sql"));
            migrationBuilder.Sql(System.IO.File.ReadAllText("Neon/05_offers.sql"));
            migrationBuilder.Sql(System.IO.File.ReadAllText("Neon/06_sales.sql"));
            migrationBuilder.Sql(System.IO.File.ReadAllText("Neon/07_projects.sql"));
            migrationBuilder.Sql(System.IO.File.ReadAllText("Neon/08_calendar.sql"));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            // Drop all tables
            migrationBuilder.Sql(@"
                DROP TABLE IF EXISTS ""dispatch_history"" CASCADE;
                DROP TABLE IF EXISTS ""technician_status_history"" CASCADE;
                DROP TABLE IF EXISTS ""technician_leaves"" CASCADE;
                DROP TABLE IF EXISTS ""technician_working_hours"" CASCADE;
                DROP TABLE IF EXISTS ""dispatch_notes"" CASCADE;
                DROP TABLE IF EXISTS ""dispatch_attachments"" CASCADE;
                DROP TABLE IF EXISTS ""dispatch_materials"" CASCADE;
                DROP TABLE IF EXISTS ""dispatch_expenses"" CASCADE;
                DROP TABLE IF EXISTS ""dispatch_time_entries"" CASCADE;
                DROP TABLE IF EXISTS ""dispatch_technicians"" CASCADE;
                DROP TABLE IF EXISTS ""dispatches"" CASCADE;
                DROP TABLE IF EXISTS ""dailytasks"" CASCADE;
                DROP TABLE IF EXISTS ""taskattachments"" CASCADE;
                DROP TABLE IF EXISTS ""taskcomments"" CASCADE;
                DROP TABLE IF EXISTS ""projecttasks"" CASCADE;
                DROP TABLE IF EXISTS ""projectcolumns"" CASCADE;
                DROP TABLE IF EXISTS ""projects"" CASCADE;
                DROP TABLE IF EXISTS ""service_order_jobs"" CASCADE;
                DROP TABLE IF EXISTS ""service_orders"" CASCADE;
                DROP TABLE IF EXISTS ""maintenance_histories"" CASCADE;
                DROP TABLE IF EXISTS ""installations"" CASCADE;
                DROP TABLE IF EXISTS ""sale_activities"" CASCADE;
                DROP TABLE IF EXISTS ""sale_items"" CASCADE;
                DROP TABLE IF EXISTS ""sales"" CASCADE;
                DROP TABLE IF EXISTS ""offer_activities"" CASCADE;
                DROP TABLE IF EXISTS ""offer_items"" CASCADE;
                DROP TABLE IF EXISTS ""offers"" CASCADE;
                DROP TABLE IF EXISTS ""event_reminders"" CASCADE;
                DROP TABLE IF EXISTS ""event_attendees"" CASCADE;
                DROP TABLE IF EXISTS ""calendar_events"" CASCADE;
                DROP TABLE IF EXISTS ""event_types"" CASCADE;
                DROP TABLE IF EXISTS ""inventory_transactions"" CASCADE;
                DROP TABLE IF EXISTS ""locations"" CASCADE;
                DROP TABLE IF EXISTS ""article_categories"" CASCADE;
                DROP TABLE IF EXISTS ""articles"" CASCADE;
                DROP TABLE IF EXISTS ""ContactNotes"" CASCADE;
                DROP TABLE IF EXISTS ""ContactTagAssignments"" CASCADE;
                DROP TABLE IF EXISTS ""ContactTags"" CASCADE;
                DROP TABLE IF EXISTS ""Contacts"" CASCADE;
                DROP TABLE IF EXISTS ""Currencies"" CASCADE;
                DROP TABLE IF EXISTS ""LookupItems"" CASCADE;
                DROP TABLE IF EXISTS ""RoleSkills"" CASCADE;
                DROP TABLE IF EXISTS ""UserSkills"" CASCADE;
                DROP TABLE IF EXISTS ""UserRoles"" CASCADE;
                DROP TABLE IF EXISTS ""Skills"" CASCADE;
                DROP TABLE IF EXISTS ""Roles"" CASCADE;
                DROP TABLE IF EXISTS ""UserPreferences"" CASCADE;
                DROP TABLE IF EXISTS ""Users"" CASCADE;
                DROP TABLE IF EXISTS ""MainAdminUsers"" CASCADE;
            ");
        }
    }
}
