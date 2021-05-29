using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace ApiCartaoDeCredito.Migrations
{
    public partial class NovasSeeds : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "CreditCards",
                keyColumn: "CreditCardId",
                keyValue: 2,
                column: "DateOfCreation",
                value: new DateTime(2019, 5, 28, 22, 9, 30, 820, DateTimeKind.Local).AddTicks(1672));

            migrationBuilder.InsertData(
                table: "CreditCards",
                columns: new[] { "CreditCardId", "CVV", "DateOfCreation", "ExpiryDate", "IsActive", "Number", "PersonId" },
                values: new object[] { 1, "696", new DateTime(2021, 5, 28, 22, 9, 30, 818, DateTimeKind.Local).AddTicks(4730), "05/26", false, "4260550142035162", 1 });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "CreditCards",
                keyColumn: "CreditCardId",
                keyValue: 1);

            migrationBuilder.UpdateData(
                table: "CreditCards",
                keyColumn: "CreditCardId",
                keyValue: 2,
                column: "DateOfCreation",
                value: new DateTime(2019, 5, 28, 20, 4, 42, 442, DateTimeKind.Local).AddTicks(378));
        }
    }
}
