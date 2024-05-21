using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IMS.web.Migrations.ImsDb
{
    /// <inheritdoc />
    public partial class UpdatedMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "StoreName",
                table: "StoreInfo",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "RegistrationNumber",
                table: "StoreInfo",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "StoreInfo",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20,
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "PanNo",
                table: "StoreInfo",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50,
                oldNullable: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "StoreInfo",
                type: "datetime2",
                nullable: true,
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "StoreInfo",
                type: "nvarchar(max)",
                nullable: true,
                oldClrType: typeof(string),
                oldType: "varchar(max)",
                oldUnicode: false,
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "StoreInfo",
                type: "bit",
                nullable: false,
                defaultValue: true,
                oldClrType: typeof(bool),
                oldType: "bit");

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "StoreInfo",
                type: "datetime2",
                nullable: false,
                defaultValueSql: "GETDATE()",
                oldClrType: typeof(DateTime),
                oldType: "datetime2");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "StoreInfo",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: false,
                defaultValue: "",
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100,
                oldNullable: true);

            migrationBuilder.CreateTable(
                name: "CategoryInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CategoryDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    StoreInfoId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CategoryInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CategoryInfo_StoreInfo_StoreInfoId",
                        column: x => x.StoreInfoId,
                        principalTable: "StoreInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "CustomerInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    StoreInfoId = table.Column<int>(type: "int", nullable: false),
                    CustomerName = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(100)", maxLength: 100, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    Address = table.Column<string>(type: "nvarchar(250)", maxLength: 250, nullable: true),
                    PanNo = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_CustomerInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_CustomerInfo_StoreInfo_StoreInfoId",
                        column: x => x.StoreInfoId,
                        principalTable: "StoreInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "RackInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    RackName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    StoreInfoId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_RackInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_RackInfo_StoreInfo_StoreInfoId",
                        column: x => x.StoreInfoId,
                        principalTable: "StoreInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "SupplierInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    SupplierName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ContactPerson = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PhoneNumber = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Address = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    Email = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    StoreInfoId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_SupplierInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_SupplierInfo_StoreInfo_StoreInfoId",
                        column: x => x.StoreInfoId,
                        principalTable: "StoreInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "UnitInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UnitName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UnitInfo", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryInfoId = table.Column<int>(type: "int", nullable: false),
                    ProductName = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    ProductDescription = table.Column<string>(type: "nvarchar(500)", maxLength: 500, nullable: true),
                    UnitInfoId = table.Column<int>(type: "int", nullable: false),
                    StoreInfoId = table.Column<int>(type: "int", nullable: false),
                    ImageUrl = table.Column<string>(type: "nvarchar(300)", maxLength: 300, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductInfo_CategoryInfo_CategoryInfoId",
                        column: x => x.CategoryInfoId,
                        principalTable: "CategoryInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductInfo_StoreInfo_StoreInfoId",
                        column: x => x.StoreInfoId,
                        principalTable: "StoreInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductInfo_UnitInfo_UnitInfoId",
                        column: x => x.UnitInfoId,
                        principalTable: "UnitInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductRateInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategroyInfoId = table.Column<int>(type: "int", nullable: false),
                    ProductInfoId = table.Column<int>(type: "int", nullable: false),
                    StoreInfoId = table.Column<int>(type: "int", nullable: false),
                    CostPrice = table.Column<double>(type: "float", nullable: false),
                    SellingPrice = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    SoldQuantity = table.Column<double>(type: "float", nullable: false),
                    RemainingQuantity = table.Column<double>(type: "float", nullable: false),
                    BatchNo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    PurchaseDate = table.Column<DateTime>(type: "datetime2", maxLength: 200, nullable: false),
                    ExpiryDate = table.Column<DateTime>(type: "datetime2", maxLength: 200, nullable: false),
                    SupplierInfoId = table.Column<int>(type: "int", nullable: false),
                    RackInfoId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductRateInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductRateInfo_CategoryInfo_CategroyInfoId",
                        column: x => x.CategroyInfoId,
                        principalTable: "CategoryInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductRateInfo_ProductInfo_ProductInfoId",
                        column: x => x.ProductInfoId,
                        principalTable: "ProductInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductRateInfo_RackInfo_RackInfoId",
                        column: x => x.RackInfoId,
                        principalTable: "RackInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductRateInfo_StoreInfo_StoreInfoId",
                        column: x => x.StoreInfoId,
                        principalTable: "StoreInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductRateInfo_SupplierInfo_SupplierInfoId",
                        column: x => x.SupplierInfoId,
                        principalTable: "SupplierInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductInvoiceInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    PaymentMethod = table.Column<int>(type: "int", maxLength: 200, nullable: false),
                    InvoiceNo = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    StoreinfoId = table.Column<int>(type: "int", nullable: false),
                    CustomerInfoId = table.Column<int>(type: "int", nullable: false),
                    TransactionDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    NetAmount = table.Column<double>(type: "float", nullable: false),
                    DiscountAmount = table.Column<double>(type: "float", nullable: false),
                    TotalAmount = table.Column<double>(type: "float", nullable: false),
                    Remarks = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    BillStatus = table.Column<int>(type: "int", maxLength: 200, nullable: false),
                    CancellationRemarks = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductRateInfoId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInvoiceInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductInvoiceInfo_CustomerInfo_CustomerInfoId",
                        column: x => x.CustomerInfoId,
                        principalTable: "CustomerInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductInvoiceInfo_ProductRateInfo_ProductRateInfoId",
                        column: x => x.ProductRateInfoId,
                        principalTable: "ProductRateInfo",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ProductInvoiceInfo_StoreInfo_StoreinfoId",
                        column: x => x.StoreinfoId,
                        principalTable: "StoreInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "StockInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    CategoryInfoId = table.Column<int>(type: "int", nullable: false),
                    ProductInfoId = table.Column<int>(type: "int", nullable: false),
                    ProductRateInfoId = table.Column<int>(type: "int", nullable: false),
                    Quantity = table.Column<float>(type: "real", maxLength: 200, nullable: false),
                    StoreInfoId = table.Column<int>(type: "int", nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_StockInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_StockInfo_CategoryInfo_CategoryInfoId",
                        column: x => x.CategoryInfoId,
                        principalTable: "CategoryInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockInfo_ProductInfo_ProductInfoId",
                        column: x => x.ProductInfoId,
                        principalTable: "ProductInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_StockInfo_ProductRateInfo_ProductRateInfoId",
                        column: x => x.ProductRateInfoId,
                        principalTable: "ProductRateInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_StockInfo_StoreInfo_StoreInfoId",
                        column: x => x.StoreInfoId,
                        principalTable: "StoreInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "TransationInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    TransactionType = table.Column<string>(type: "nvarchar(200)", maxLength: 200, nullable: true),
                    CategoryInfoId = table.Column<int>(type: "int", nullable: false),
                    ProductInfoId = table.Column<int>(type: "int", nullable: false),
                    UnitInfoId = table.Column<int>(type: "int", nullable: false),
                    ProductRateInfoId = table.Column<int>(type: "int", nullable: false),
                    StoreInfoId = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<float>(type: "real", maxLength: 200, nullable: false),
                    IsActive = table.Column<bool>(type: "bit", nullable: false, defaultValue: true),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ModifiedDate = table.Column<DateTime>(type: "datetime", nullable: true),
                    ModifiedBy = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TransationInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_TransationInfo_CategoryInfo_CategoryInfoId",
                        column: x => x.CategoryInfoId,
                        principalTable: "CategoryInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransationInfo_ProductInfo_ProductInfoId",
                        column: x => x.ProductInfoId,
                        principalTable: "ProductInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransationInfo_ProductRateInfo_ProductRateInfoId",
                        column: x => x.ProductRateInfoId,
                        principalTable: "ProductRateInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransationInfo_StoreInfo_StoreInfoId",
                        column: x => x.StoreInfoId,
                        principalTable: "StoreInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_TransationInfo_UnitInfo_UnitInfoId",
                        column: x => x.UnitInfoId,
                        principalTable: "UnitInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "ProductInvoiceDetailInfo",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    ProductInvoiceInfoId = table.Column<int>(type: "int", nullable: false),
                    ProductRateInfoId = table.Column<int>(type: "int", nullable: false),
                    Rate = table.Column<double>(type: "float", nullable: false),
                    Quantity = table.Column<double>(type: "float", nullable: false),
                    Amount = table.Column<double>(type: "float", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false, defaultValueSql: "GETDATE()"),
                    CreatedBy = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductInvoiceDetailInfo", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ProductInvoiceDetailInfo_ProductInvoiceInfo_ProductInvoiceInfoId",
                        column: x => x.ProductInvoiceInfoId,
                        principalTable: "ProductInvoiceInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                    table.ForeignKey(
                        name: "FK_ProductInvoiceDetailInfo_ProductRateInfo_ProductRateInfoId",
                        column: x => x.ProductRateInfoId,
                        principalTable: "ProductRateInfo",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateIndex(
                name: "IX_CategoryInfo_StoreInfoId",
                table: "CategoryInfo",
                column: "StoreInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_CustomerInfo_StoreInfoId",
                table: "CustomerInfo",
                column: "StoreInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInfo_CategoryInfoId",
                table: "ProductInfo",
                column: "CategoryInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInfo_StoreInfoId",
                table: "ProductInfo",
                column: "StoreInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInfo_UnitInfoId",
                table: "ProductInfo",
                column: "UnitInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInvoiceDetailInfo_ProductInvoiceInfoId",
                table: "ProductInvoiceDetailInfo",
                column: "ProductInvoiceInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInvoiceDetailInfo_ProductRateInfoId",
                table: "ProductInvoiceDetailInfo",
                column: "ProductRateInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInvoiceInfo_CustomerInfoId",
                table: "ProductInvoiceInfo",
                column: "CustomerInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInvoiceInfo_ProductRateInfoId",
                table: "ProductInvoiceInfo",
                column: "ProductRateInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductInvoiceInfo_StoreinfoId",
                table: "ProductInvoiceInfo",
                column: "StoreinfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRateInfo_CategroyInfoId",
                table: "ProductRateInfo",
                column: "CategroyInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRateInfo_ProductInfoId",
                table: "ProductRateInfo",
                column: "ProductInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRateInfo_RackInfoId",
                table: "ProductRateInfo",
                column: "RackInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRateInfo_StoreInfoId",
                table: "ProductRateInfo",
                column: "StoreInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductRateInfo_SupplierInfoId",
                table: "ProductRateInfo",
                column: "SupplierInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_RackInfo_StoreInfoId",
                table: "RackInfo",
                column: "StoreInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_StockInfo_CategoryInfoId",
                table: "StockInfo",
                column: "CategoryInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_StockInfo_ProductInfoId",
                table: "StockInfo",
                column: "ProductInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_StockInfo_ProductRateInfoId",
                table: "StockInfo",
                column: "ProductRateInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_StockInfo_StoreInfoId",
                table: "StockInfo",
                column: "StoreInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_SupplierInfo_StoreInfoId",
                table: "SupplierInfo",
                column: "StoreInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_TransationInfo_CategoryInfoId",
                table: "TransationInfo",
                column: "CategoryInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_TransationInfo_ProductInfoId",
                table: "TransationInfo",
                column: "ProductInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_TransationInfo_ProductRateInfoId",
                table: "TransationInfo",
                column: "ProductRateInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_TransationInfo_StoreInfoId",
                table: "TransationInfo",
                column: "StoreInfoId");

            migrationBuilder.CreateIndex(
                name: "IX_TransationInfo_UnitInfoId",
                table: "TransationInfo",
                column: "UnitInfoId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ProductInvoiceDetailInfo");

            migrationBuilder.DropTable(
                name: "StockInfo");

            migrationBuilder.DropTable(
                name: "TransationInfo");

            migrationBuilder.DropTable(
                name: "ProductInvoiceInfo");

            migrationBuilder.DropTable(
                name: "CustomerInfo");

            migrationBuilder.DropTable(
                name: "ProductRateInfo");

            migrationBuilder.DropTable(
                name: "ProductInfo");

            migrationBuilder.DropTable(
                name: "RackInfo");

            migrationBuilder.DropTable(
                name: "SupplierInfo");

            migrationBuilder.DropTable(
                name: "CategoryInfo");

            migrationBuilder.DropTable(
                name: "UnitInfo");

            migrationBuilder.AlterColumn<string>(
                name: "StoreName",
                table: "StoreInfo",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);

            migrationBuilder.AlterColumn<string>(
                name: "RegistrationNumber",
                table: "StoreInfo",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<string>(
                name: "PhoneNumber",
                table: "StoreInfo",
                type: "nvarchar(20)",
                maxLength: 20,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(20)",
                oldMaxLength: 20);

            migrationBuilder.AlterColumn<string>(
                name: "PanNo",
                table: "StoreInfo",
                type: "nvarchar(50)",
                maxLength: 50,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(50)",
                oldMaxLength: 50);

            migrationBuilder.AlterColumn<DateTime>(
                name: "ModifiedDate",
                table: "StoreInfo",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified),
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldNullable: true);

            migrationBuilder.AlterColumn<string>(
                name: "ModifiedBy",
                table: "StoreInfo",
                type: "varchar(max)",
                unicode: false,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(max)",
                oldNullable: true);

            migrationBuilder.AlterColumn<bool>(
                name: "IsActive",
                table: "StoreInfo",
                type: "bit",
                nullable: false,
                oldClrType: typeof(bool),
                oldType: "bit",
                oldDefaultValue: true);

            migrationBuilder.AlterColumn<DateTime>(
                name: "CreatedDate",
                table: "StoreInfo",
                type: "datetime2",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "datetime2",
                oldDefaultValueSql: "GETDATE()");

            migrationBuilder.AlterColumn<string>(
                name: "Address",
                table: "StoreInfo",
                type: "nvarchar(100)",
                maxLength: 100,
                nullable: true,
                oldClrType: typeof(string),
                oldType: "nvarchar(100)",
                oldMaxLength: 100);
        }
    }
}
