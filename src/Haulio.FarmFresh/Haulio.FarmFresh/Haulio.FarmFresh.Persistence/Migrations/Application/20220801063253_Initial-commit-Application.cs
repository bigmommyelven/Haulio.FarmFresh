using Microsoft.EntityFrameworkCore.Migrations;
using System;

namespace Haulio.FarmFresh.Persistence.Migrations.Application
{
    public partial class InitialcommitApplication : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    Description = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Customers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    CustomerName = table.Column<string>(type: "TEXT", nullable: true),
                    ContactName = table.Column<string>(type: "TEXT", nullable: true),
                    ContactTitle = table.Column<string>(type: "TEXT", nullable: true),
                    Address = table.Column<string>(type: "TEXT", nullable: true),
                    City = table.Column<string>(type: "TEXT", nullable: true),
                    Region = table.Column<string>(type: "TEXT", nullable: true),
                    PostalCode = table.Column<string>(type: "TEXT", nullable: true),
                    Country = table.Column<string>(type: "TEXT", nullable: true),
                    Phone = table.Column<string>(type: "TEXT", nullable: true),
                    Fax = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Customers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "ProductMenus",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Position = table.Column<int>(type: "INTEGER", nullable: false),
                    DisplayText = table.Column<string>(type: "TEXT", nullable: true),
                    IsActive = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductMenus", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Suppliers",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    SupplierName = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Suppliers", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Tags",
                columns: table => new
                {
                    Id = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Tags", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Orders",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "TEXT", nullable: false),
                    CustomerId = table.Column<int>(type: "INTEGER", nullable: false),
                    EmployeeId = table.Column<int>(type: "INTEGER", nullable: false),
                    Total = table.Column<decimal>(type: "TEXT", nullable: false),
                    OrderDate = table.Column<DateTime>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Orders", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Orders_Customers_CustomerId",
                        column: x => x.CustomerId,
                        principalTable: "Customers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "Products",
                columns: table => new
                {
                    Id = table.Column<int>(type: "INTEGER", nullable: false)
                        .Annotation("Sqlite:Autoincrement", true),
                    Name = table.Column<string>(type: "TEXT", nullable: true),
                    CategoryId = table.Column<int>(type: "INTEGER", nullable: false),
                    Strategy = table.Column<string>(type: "TEXT", nullable: true),
                    Price = table.Column<decimal>(type: "money", nullable: false),
                    Description = table.Column<string>(type: "TEXT", nullable: true),
                    SupplierId = table.Column<int>(type: "INTEGER", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Products", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Products_Categories_CategoryId",
                        column: x => x.CategoryId,
                        principalTable: "Categories",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Products_Suppliers_SupplierId",
                        column: x => x.SupplierId,
                        principalTable: "Suppliers",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Restrict);
                });

            migrationBuilder.CreateTable(
                name: "OrderDetails",
                columns: table => new
                {
                    OrderId = table.Column<Guid>(type: "TEXT", nullable: false),
                    ProductId = table.Column<int>(type: "INTEGER", nullable: false),
                    Price = table.Column<decimal>(type: "TEXT", nullable: false),
                    Quantity = table.Column<int>(type: "INTEGER", nullable: false),
                    Total = table.Column<decimal>(type: "TEXT", nullable: false),
                    Cancelled = table.Column<bool>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_OrderDetails", x => new { x.OrderId, x.ProductId });
                    table.ForeignKey(
                        name: "FK_OrderDetails_Orders_OrderId",
                        column: x => x.OrderId,
                        principalTable: "Orders",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_OrderDetails_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductImages",
                columns: table => new
                {
                    ProductID = table.Column<int>(type: "INTEGER", nullable: false),
                    ImageUrl = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductImages", x => new { x.ProductID, x.ImageUrl });
                    table.ForeignKey(
                        name: "FK_ProductImages_Product_ProductId",
                        column: x => x.ProductID,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductProductMenu",
                columns: table => new
                {
                    ProductMenusId = table.Column<int>(type: "INTEGER", nullable: false),
                    ProductsId = table.Column<int>(type: "INTEGER", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductProductMenu", x => new { x.ProductMenusId, x.ProductsId });
                    table.ForeignKey(
                        name: "FK_ProductProductMenu_ProductMenus_ProductMenusId",
                        column: x => x.ProductMenusId,
                        principalTable: "ProductMenus",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductProductMenu_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "ProductTag",
                columns: table => new
                {
                    ProductsId = table.Column<int>(type: "INTEGER", nullable: false),
                    TagsId = table.Column<string>(type: "TEXT", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ProductTag", x => new { x.ProductsId, x.TagsId });
                    table.ForeignKey(
                        name: "FK_ProductTag_Products_ProductsId",
                        column: x => x.ProductsId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_ProductTag_Tags_TagsId",
                        column: x => x.TagsId,
                        principalTable: "Tags",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 1, "Fruit", "Fruit" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 2, "Vegetable", "Vegetable" });

            migrationBuilder.InsertData(
                table: "Categories",
                columns: new[] { "Id", "Description", "Name" },
                values: new object[] { 3, "Bakery", "Bakery" });

            migrationBuilder.InsertData(
                table: "Customers",
                columns: new[] { "Id", "Address", "City", "ContactName", "ContactTitle", "Country", "CustomerName", "Fax", "Phone", "PostalCode", "Region" },
                values: new object[] { 1, null, null, "Hafizhan Al Wafi", "Hafiz", null, "Hafizhan Al Wafi", null, null, null, null });

            migrationBuilder.InsertData(
                table: "ProductMenus",
                columns: new[] { "Id", "DisplayText", "IsActive", "Position" },
                values: new object[] { 1, "On Sales!", true, 1 });

            migrationBuilder.InsertData(
                table: "ProductMenus",
                columns: new[] { "Id", "DisplayText", "IsActive", "Position" },
                values: new object[] { 2, "New", true, 2 });

            migrationBuilder.InsertData(
                table: "ProductMenus",
                columns: new[] { "Id", "DisplayText", "IsActive", "Position" },
                values: new object[] { 3, "Shop by Store", true, 3 });

            migrationBuilder.InsertData(
                table: "ProductMenus",
                columns: new[] { "Id", "DisplayText", "IsActive", "Position" },
                values: new object[] { 4, "Fruit & Veg", true, 4 });

            migrationBuilder.InsertData(
                table: "ProductMenus",
                columns: new[] { "Id", "DisplayText", "IsActive", "Position" },
                values: new object[] { 5, "Meat & Seafood", true, 5 });

            migrationBuilder.InsertData(
                table: "ProductMenus",
                columns: new[] { "Id", "DisplayText", "IsActive", "Position" },
                values: new object[] { 6, "Dairy and Chilled", true, 6 });

            migrationBuilder.InsertData(
                table: "ProductMenus",
                columns: new[] { "Id", "DisplayText", "IsActive", "Position" },
                values: new object[] { 7, "Bakery", true, 7 });

            migrationBuilder.InsertData(
                table: "ProductMenus",
                columns: new[] { "Id", "DisplayText", "IsActive", "Position" },
                values: new object[] { 8, "Beverages", true, 8 });

            migrationBuilder.InsertData(
                table: "Tags",
                column: "Id",
                value: "Fresh");

            migrationBuilder.InsertData(
                table: "Tags",
                column: "Id",
                value: "Healthy");

            migrationBuilder.InsertData(
                table: "Tags",
                column: "Id",
                value: "New");

            migrationBuilder.InsertData(
                table: "ProductProductMenu",
                columns: new[] { "ProductMenusId", "ProductsId" },
                values: new object[] { 4, 8 });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price", "Strategy", "SupplierId" },
                values: new object[] { 1, 1, "Ripe blue grape bunch among grapevine leaves at vineyard in warm sunset sunlight. Beautiful clusters of ripening grapes.", "Ripe Blue Grape", 10000m, "Packet", null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price", "Strategy", "SupplierId" },
                values: new object[] { 2, 1, "Spinach (Spinacia oleracea) is a leafy green flowering plant native to central and western Asia. It is of the order Caryophyllales, family Amaranthaceae, subfamily Chenopodioideae. Its leaves are a common edible vegetable consumed either fresh, or after storage using preservation techniques by canning, freezing, or dehydration. It may be eaten cooked or raw, and the taste differs considerably; the high oxalate content may be reduced by steaming.", "Spinach", 10000m, "Packet", null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price", "Strategy", "SupplierId" },
                values: new object[] { 7, 1, "Ripe blue grape bunch among grapevine leaves at vineyard in warm sunset sunlight. Beautiful clusters of ripening grapes.", "Ripe Blue Grape", 50000m, "Packet of 1 Bundles", null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price", "Strategy", "SupplierId" },
                values: new object[] { 3, 2, "Capsicum ˈkæpsɪkəm' is a genus of flowering plants in the nightshade family Solanaceae, native to the Americas, cultivated worldwide for their chili pepper or bell pepper fruit.", "Capsicum", 12000m, "Packet", null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price", "Strategy", "SupplierId" },
                values: new object[] { 4, 2, "The tomato is the edible berry of the plant Solanum lycopersicum,[1][2] commonly known as the tomato plant. The species originated in western South America, Mexico, and Central America.[2][3] The Mexican Nahuatl word tomatl gave rise to the Spanish word tomate, from which the English word tomato derived.[3][4] Its domestication and use as a cultivated food may have originated with the indigenous peoples of Mexico.[2][5] The Aztecs used tomatoes in their cooking at the time of the Spanish conquest of the Aztec Empire, and after the Spanish encountered the tomato for the first time after their contact with the Aztecs, they brought the plant to Europe, in a widespread transfer of plants known as the Columbian exchange. From there, the tomato was introduced to other parts of the European-colonized world during the 16th century.[2]", "Tomato", 5000m, "Packet", null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price", "Strategy", "SupplierId" },
                values: new object[] { 5, 3, "a term used for a variety of baked, commonly flour-based food products.[2] The term is applied to two distinct products in North America and the United Kingdom,[3] and is also distinguished from U.S. versions in the Commonwealth of Nations and Europe.", "Biscuit", 10000m, "Packet", null });

            migrationBuilder.InsertData(
                table: "Products",
                columns: new[] { "Id", "CategoryId", "Description", "Name", "Price", "Strategy", "SupplierId" },
                values: new object[] { 6, 3, "a staple food prepared from a dough of flour and water, usually by baking.", "Bread", 10000m, "Packet", null });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "ImageUrl", "ProductID" },
                values: new object[] { "/src/assets/res/Screen%203/Untitled-1.png", 1 });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "ImageUrl", "ProductID" },
                values: new object[] { "/src/assets/res/Screen%203/Untitled-3.png", 1 });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "ImageUrl", "ProductID" },
                values: new object[] { "/src/assets/res/Screen%203/Untitled-9.jpg", 1 });

            migrationBuilder.InsertData(
                table: "ProductImages",
                columns: new[] { "ImageUrl", "ProductID" },
                values: new object[] { "/src/assets/res/Screen%203/Untitled-5.png", 2 });

            migrationBuilder.InsertData(
                table: "ProductProductMenu",
                columns: new[] { "ProductMenusId", "ProductsId" },
                values: new object[] { 1, 1 });

            migrationBuilder.InsertData(
                table: "ProductProductMenu",
                columns: new[] { "ProductMenusId", "ProductsId" },
                values: new object[] { 2, 1 });

            migrationBuilder.InsertData(
                table: "ProductProductMenu",
                columns: new[] { "ProductMenusId", "ProductsId" },
                values: new object[] { 4, 1 });

            migrationBuilder.InsertData(
                table: "ProductProductMenu",
                columns: new[] { "ProductMenusId", "ProductsId" },
                values: new object[] { 1, 2 });

            migrationBuilder.InsertData(
                table: "ProductProductMenu",
                columns: new[] { "ProductMenusId", "ProductsId" },
                values: new object[] { 3, 3 });

            migrationBuilder.CreateIndex(
                name: "IX_OrderDetails_ProductId",
                table: "OrderDetails",
                column: "ProductId");

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CustomerId",
                table: "Orders",
                column: "CustomerId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductProductMenu_ProductsId",
                table: "ProductProductMenu",
                column: "ProductsId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_CategoryId",
                table: "Products",
                column: "CategoryId");

            migrationBuilder.CreateIndex(
                name: "IX_Products_SupplierId",
                table: "Products",
                column: "SupplierId");

            migrationBuilder.CreateIndex(
                name: "IX_ProductTag_TagsId",
                table: "ProductTag",
                column: "TagsId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "OrderDetails");

            migrationBuilder.DropTable(
                name: "ProductImages");

            migrationBuilder.DropTable(
                name: "ProductProductMenu");

            migrationBuilder.DropTable(
                name: "ProductTag");

            migrationBuilder.DropTable(
                name: "Orders");

            migrationBuilder.DropTable(
                name: "ProductMenus");

            migrationBuilder.DropTable(
                name: "Products");

            migrationBuilder.DropTable(
                name: "Tags");

            migrationBuilder.DropTable(
                name: "Customers");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropTable(
                name: "Suppliers");
        }
    }
}
