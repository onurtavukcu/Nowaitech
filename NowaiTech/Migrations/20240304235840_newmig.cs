using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace NowaiTechAPI.Migrations
{
    /// <inheritdoc />
    public partial class newmig : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserInputs",
                keyColumn: "Username",
                keyValue: "admin",
                column: "PasswordHash",
                value: "G1j+rGO9P+43yfloKmmjFL+WhtGKfepIrq4f5r9C1JGtxEoobkgJJbO2M7btfEf4ACx7KG10uWTFLopSpYqZ/A==");

            migrationBuilder.UpdateData(
                table: "UserInputs",
                keyColumn: "Username",
                keyValue: "regular",
                column: "PasswordHash",
                value: "iPyaEItWVH8SA5bpuKVHLmnA5TrAJKp67YfD58abUdRfF/fD+1EMDXFGl+hW402RyDZyuFzTGuhtyUf4XcQtcA==");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.UpdateData(
                table: "UserInputs",
                keyColumn: "Username",
                keyValue: "admin",
                column: "PasswordHash",
                value: "t8LCKtkU9CWD4Q6JqoRkk80QPbYgIF30Ls9fTinV3WizX3nwV8RHOatMOK/cMKaMBLPtg29K89QSc0XNzY70SQ==");

            migrationBuilder.UpdateData(
                table: "UserInputs",
                keyColumn: "Username",
                keyValue: "regular",
                column: "PasswordHash",
                value: "jKgQ7I2SOIYIzU7qYCSjpc5r8rs5kkFjkqaDqHLjeCtvM5LDpvg2vP6d0tCqUDBaidfk4e2xaHNFZpxrYg32Dg==");
        }
    }
}
