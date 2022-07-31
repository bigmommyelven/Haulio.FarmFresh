using Haulio.FarmFresh.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace Haulio.FarmFresh.Persistence.Seeds
{
    public static class ApplicationSeed
    {
        public static void SeedApplicationData(this ModelBuilder builder)
        {
            builder
                .Entity<Tag>()
                .HasData(
                    new List<Tag>
                    {
                        new Tag { Id = "Fresh" },
                        new Tag { Id = "Healthy" },
                        new Tag { Id = "New" }
                    }
                );

            builder
                .Entity<Customer>()
                .HasData(
                    new List<Customer>()
                    {
                        new Customer
                        {
                            Id = 1,
                            CustomerName = "Hafizhan Al Wafi",
                            ContactName = "Hafizhan Al Wafi",
                            ContactTitle = "Hafiz"
                        }
                    }
                );

            builder
                .Entity<Category>()
                .HasData(
                    new List<Category>()
                    {
                        new Category { Id = 1, Name = "Fruit", Description = "Fruit" },
                        new Category { Id = 2, Name = "Vegetable", Description = "Vegetable" },
                        new Category { Id = 3, Name = "Bakery", Description = "Bakery" }
                    }
                );

            builder
                .Entity<Product>()
                .HasData(
                    new List<Product>()
                    {
                        new Product
                        {
                            Id = 1,
                            CategoryId = 1,
                            Name = "Ripe Blue Grape",
                            Strategy = "Packet",
                            Price = 10000,
                            Description =
                                "Ripe blue grape bunch among grapevine leaves at vineyard in warm sunset sunlight. Beautiful clusters of ripening grapes."
                        },
                        new Product
                        {
                            Id = 2,
                            CategoryId = 1,
                            Strategy = "Packet",
                            Name = "Spinach",
                            Price = 10000,
                            Description =
                                "Spinach (Spinacia oleracea) is a leafy green flowering plant native to central and western Asia. It is of the order Caryophyllales, family Amaranthaceae, subfamily Chenopodioideae. Its leaves are a common edible vegetable consumed either fresh, or after storage using preservation techniques by canning, freezing, or dehydration. It may be eaten cooked or raw, and the taste differs considerably; the high oxalate content may be reduced by steaming."
                        },
                        new Product
                        {
                            Id = 3,
                            CategoryId = 2,
                            Strategy = "Packet",
                            Name = "Capsicum",
                            Price = 12000,
                            Description =
                                "Capsicum ˈkæpsɪkəm' is a genus of flowering plants in the nightshade family Solanaceae, native to the Americas, cultivated worldwide for their chili pepper or bell pepper fruit."
                        },
                        new Product
                        {
                            Id = 4,
                            CategoryId = 2,
                            Strategy = "Packet",
                            Name = "Tomato",
                            Price = 5000,
                            Description =
                                "The tomato is the edible berry of the plant Solanum lycopersicum,[1][2] commonly known as the tomato plant. The species originated in western South America, Mexico, and Central America.[2][3] The Mexican Nahuatl word tomatl gave rise to the Spanish word tomate, from which the English word tomato derived.[3][4] Its domestication and use as a cultivated food may have originated with the indigenous peoples of Mexico.[2][5] The Aztecs used tomatoes in their cooking at the time of the Spanish conquest of the Aztec Empire, and after the Spanish encountered the tomato for the first time after their contact with the Aztecs, they brought the plant to Europe, in a widespread transfer of plants known as the Columbian exchange. From there, the tomato was introduced to other parts of the European-colonized world during the 16th century.[2]"
                        },
                        new Product
                        {
                            Id = 5,
                            Strategy = "Packet",
                            CategoryId = 3,
                            Name = "Biscuit",
                            Price = 10000,
                            Description =
                                "a term used for a variety of baked, commonly flour-based food products.[2] The term is applied to two distinct products in North America and the United Kingdom,[3] and is also distinguished from U.S. versions in the Commonwealth of Nations and Europe."
                        },
                        new Product
                        {
                            Id = 6,
                            Strategy = "Packet",
                            CategoryId = 3,
                            Name = "Bread",
                            Price = 10000,
                            Description =
                                "a staple food prepared from a dough of flour and water, usually by baking."
                        },
                         new Product
                        {
                            Id = 7,
                            CategoryId = 1,
                            Name = "Ripe Blue Grape",
                            Strategy = "Packet of 1 Bundles",
                            Price = 50000,
                            Description =
                                "Ripe blue grape bunch among grapevine leaves at vineyard in warm sunset sunlight. Beautiful clusters of ripening grapes.",
                        },
                    }
                );

            builder.Entity<ProductMenu>().HasData(
                new List<ProductMenu>()
                {
                    new ProductMenu
                    {
                        Id = 1,
                        Position = 1,
                        DisplayText = "On Sales!",
                        IsActive = true
                    },
                    new ProductMenu
                    {
                        Id = 2,
                        Position = 2,
                        DisplayText = "New",
                        IsActive = true
                    },
                    new ProductMenu
                    {
                        Id = 3,
                        Position = 3,
                        DisplayText = "Shop by Store",
                        IsActive = true
                    },
                    new ProductMenu
                    {
                        Id = 4,
                        Position = 4,
                        DisplayText = "Fruit & Veg",
                        IsActive = true
                    },
                    new ProductMenu
                    {
                        Id = 5,
                        Position = 5,
                        DisplayText = "Meat & Seafood",
                        IsActive = true
                    },
                    new ProductMenu
                    {
                        Id = 6,
                        Position = 6,
                        DisplayText = "Dairy and Chilled",
                        IsActive = true
                    },
                    new ProductMenu
                    {
                        Id = 7,
                        Position = 7,
                        DisplayText = "Bakery",
                        IsActive = true
                    },
                    new ProductMenu
                    {
                        Id = 8,
                        Position = 8,
                        DisplayText = "Beverages",
                        IsActive = true
                    }
                });

            builder.Entity("ProductProductMenu").HasData(
                new List<object>()
                {
                    new
                    {
                        ProductMenusId = 1,
                        ProductsId = 1,
                    },
                    new
                    {
                        ProductMenusId = 1,
                        ProductsId = 2,
                    },
                    new
                    {
                        ProductMenusId = 2,
                        ProductsId = 1,
                    },
                    new
                    {
                        ProductMenusId = 3,
                        ProductsId = 3,
                    },
                    new
                    {
                        ProductMenusId = 4,
                        ProductsId = 8,
                    },
                    new
                    {
                        ProductMenusId = 4,
                        ProductsId = 1,
                    },
                });

            builder.Entity<ProductImage>()
                .HasData(
                new List<ProductImage>
                {
                    new ProductImage
                    {
                        ProductID = 1,
                        ImageUrl = "/src/assets/res/Screen%203/Untitled-1.png"
                    },
                    new ProductImage
                    {
                        ProductID = 1,
                        ImageUrl = "/src/assets/res/Screen%203/Untitled-3.png"
                    },
                    new ProductImage
                    {
                        ProductID = 1,
                        ImageUrl = "/src/assets/res/Screen%203/Untitled-9.jpg"
                    },
                    new ProductImage
                    {
                        ProductID = 2,
                        ImageUrl = "/src/assets/res/Screen%203/Untitled-5.png"
                    }
                });

        }
    }
}
