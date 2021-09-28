using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace PolicyMicroservice.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "consumerPolicies",
                columns: table => new
                {
                    ConsumerId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    BusinessId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PolicyId = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcceptedQuotes = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PolicyStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    PaymentDetails = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AcceptanceStatus = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    EffectiveDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_consumerPolicies", x => x.ConsumerId);
                });

            migrationBuilder.CreateTable(
                name: "policyMasters",
                columns: table => new
                {
                    PolicyId = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    PropertyType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ConsumerType = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    AssuredSum = table.Column<int>(type: "int", nullable: false),
                    Tenure = table.Column<int>(type: "int", nullable: false),
                    BusinessValue = table.Column<int>(type: "int", nullable: false),
                    PropertyValue = table.Column<int>(type: "int", nullable: false),
                    BaseLocation = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Type = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_policyMasters", x => x.PolicyId);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "consumerPolicies");

            migrationBuilder.DropTable(
                name: "policyMasters");
        }
    }
}
