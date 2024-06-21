using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.web.Migrations.ImsDb
{
    /// <inheritdoc />
    public partial class updatedstoreinfo : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInvoiceInfo_StoreInfo_StoreInfoId",
                table: "ProductInvoiceInfo");

            migrationBuilder.RenameColumn(
                name: "StoreInfoId",
                table: "ProductInvoiceInfo",
                newName: "StoreinfoId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductInvoiceInfo_StoreInfoId",
                table: "ProductInvoiceInfo",
                newName: "IX_ProductInvoiceInfo_StoreinfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInvoiceInfo_StoreInfo_StoreinfoId",
                table: "ProductInvoiceInfo",
                column: "StoreinfoId",
                principalTable: "StoreInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductInvoiceInfo_StoreInfo_StoreinfoId",
                table: "ProductInvoiceInfo");

            migrationBuilder.RenameColumn(
                name: "StoreinfoId",
                table: "ProductInvoiceInfo",
                newName: "StoreInfoId");

            migrationBuilder.RenameIndex(
                name: "IX_ProductInvoiceInfo_StoreinfoId",
                table: "ProductInvoiceInfo",
                newName: "IX_ProductInvoiceInfo_StoreInfoId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductInvoiceInfo_StoreInfo_StoreInfoId",
                table: "ProductInvoiceInfo",
                column: "StoreInfoId",
                principalTable: "StoreInfo",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
