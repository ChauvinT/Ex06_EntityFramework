using Ex06_EntityFramework.Models;
using Ex06_EntityFramework.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq.Expressions;
using static System.Collections.Specialized.BitVector32;

namespace Ex06_EntityFramework
{
    public class Program
    {
        private static void Main(string[] args)
        {
            InsertDefaultData();

            //DisplayAllArticles();
            //Console.ReadLine();

            //var parser = new DataParser();
            //parser.Parse(@"C:\Users\thomas.chauvin\Downloads\export.csv");

            using (var context = new ApplicationDbContext())
            {
                //ArticleService articleService = new ArticleService(context);
                //foreach (var item in parser.Articles)
                //{
                //    articleService.add(item);
                //}
                //context.SaveChanges();

                //OrderService orderService = new OrderService(context);
                //foreach (var item in parser.Orders)
                //{
                //    orderService.add(item);
                //}
                //context.SaveChanges();

                //context.orderDetails.AddRange(parser.OrderDetails);
                //context.SaveChanges();

                WareHouseService wareHouseService = new WareHouseService(context);
                var allWareHouse = wareHouseService.getAllWarehouse();

                ArticleService articleService = new ArticleService(context);
                var article1 = context.articles.FirstOrDefault();
                articleService.updateArticleStock(article1.Id, 200);
                //context.SaveChanges();
                
                var articleBelowStock = articleService.getArticlesBelowStock(30);

                var articleTotalSales = articleService.getTotalSalesPerArticle();

                OrderService orderService = new OrderService(context);
                var order1 = context.orders.FirstOrDefault();
                orderService.deleteOrder(order1.Id);

                var customer1 = context.customer.FirstOrDefault();
                var allOrdersCustomer = orderService.getAllOrdersByCustomer(customer1.Id);

                var averageValue = orderService.getAverageOrderValue();

                var averageArticle = orderService.getAverageArticlePerOrder();

            }


            Console.WriteLine("Import terminé");
            Console.ReadLine();
        }

        private static void InsertDefaultData()
        {
            using (var context = new ApplicationDbContext())
            {
                var warehouse01 = new Warehouse
                {
                    Name = "Entrepot de Paris",
                    Address = "10 rue du csharp",
                    PostalCode = 75000,
                    CodeAccesMD5 = new List<string>
                                {
                                   "840e998a22948adf5de39bd4f2b35da7" ,
                                   "74b87337454200d4d33f80c4663dc5e5"
                                }
                };

                var warehouse02 = new Warehouse
                {
                    Name = "Entrepot de Nantes",
                    Address = "20 avenue du Dotnet",
                    PostalCode = 75000,
                    CodeAccesMD5 = new List<string>
                                {
                                   "840e998a22948adf5de39bd4f2b35da7" ,
                                   "74b87337454200d4d33f80c4663dc5e5"
                                }
                };

                context.warehouse.Add(warehouse01);
                context.warehouse.Add(warehouse02);
                context.SaveChanges();

                //var articles = new List<Articles>

                //            {
                //                new Articles { Name = "Ordinateur Portable", Description = "Ordinateur portable haute performance", Price = 1200.00m, StockQuantity = 50 },
                //                new Articles { Name = "Smartphone", Description = "Smartphone avec écran AMOLED", Price = 800.00m, StockQuantity = 100 },
                //                new Articles { Name = "Tablette", Description = "Tablette 10 pouces avec stylet", Price = 600.00m, StockQuantity = 30 }
                //            };
                //context.articles.AddRange(articles);
                //context.SaveChanges(); 

                //var orders = new List<Orders>
                //            {
                //                new Orders
                //                {
                //                    WarehouseId = 1,
                //                    CustomerId = 1,
                //                    CustomerName = "John Doe",
                //                    Email = "johndoe@example.com",
                //                    ShippingAddress = "123 Main Street",
                //                    OrderDate = DateTime.Now,
                //                    TotalAmount = 2000.00d,
                //                    OrderStatus = "Processing",
                //                    OrderDetails = new List<OrderDetails>
                //                    {
                //                        new OrderDetails { Article = articles[1], Quantity = 1, UnitPrice = 1200.00m },
                //                        new OrderDetails { Article = articles[1], Quantity = 1, UnitPrice = 800.00m }
                //                    }
                //                },
                //                new Orders
                //                {
                //                    WarehouseId = 2,
                //                    CustomerId = 2,
                //                    CustomerName = "Bill Gate",
                //                    Email = "bill.gate@example.com",
                //                    ShippingAddress = "One Microsoft Way",
                //                    OrderDate = DateTime.Now,
                //                    TotalAmount = 2000.00d,
                //                    OrderStatus = "Processing",
                //                    OrderDetails = new List<OrderDetails>
                //                    {
                //                        new OrderDetails { Article = articles[2], Quantity = 1, UnitPrice = 800.00m }
                //                    }
                //                }
                //            };

                //context.orders.AddRange(orders);
                //context.SaveChanges();

                var customers = new List<Customers> {
                    new Customers {
                        Name = "Bill Gates",
                        Email = "bill.gates@example.com",
                        Address = new Address(
                            street: "One Microsoft Way",
                            city: "Redmond",
                            state: "Washington",
                            country: "USA",
                            postalCode: "98052"
                        )
                    },
                    new Customers {
                        Name = "John Doe",
                        Email = "johndoe@example.com",
                        Address = new Address(
                            street: "123 Main Street",
                            city: "Paris",
                            state: "Île-de-France",
                            country: "France",
                            postalCode: "75001"
                        )
                    },
                    new Customers {
                        Name = "Elon Musk",
                        Email = "elon.musk@example.com",
                        Address = new Address(
                            street: "1 Rocket Road",
                            city: "Hawthorne",
                            state: "California",
                            country: "USA",
                            postalCode: "90250"
                        )
                    },
                    new Customers {
                        Name = "Marie Curie",
                        Email = "marie.curie@example.com",
                        Address = new Address(
                            street: "1 Rue Pierre et Marie Curie",
                            city: "Paris",
                            state: "Île-de-France",
                            country: "France",
                            postalCode: "75005"
                        )
                    },
                    new Customers {
                        Name = "Steve Jobs",
                        Email = "steve.jobs@example.com",
                        Address = new Address(
                            street: "1 Infinite Loop",
                            city: "Cupertino",
                            state: "California",
                            country: "USA",
                            postalCode: "95014"
                        )
                    },
                    new Customers {
                        Name = "Ada Lovelace",
                        Email = "ada.lovelace@example.com",
                        Address = new Address(
                            street: "10 Downing Street",
                            city: "London",
                            state: "England",
                            country: "UK",
                            postalCode: "SW1A 2AA"
                        )
                    },
                    new Customers {
                        Name = "Jeff Bezos",
                        Email = "jeff.bezos@example.com",
                        Address = new Address(
                            street: "410 Terry Avenue North",
                            city: "Seattle",
                            state: "Washington",
                            country: "USA",
                            postalCode: "98109"
                        )
                    },
                    new Customers {
                        Name = "Grace Hopper",
                        Email = "grace.hopper@example.com",
                        Address = new Address(
                            street: "9800 Savage Road",
                            city: "Fort Meade",
                            state: "Maryland",
                            country: "USA",
                            postalCode: "20755"
                        )
                    },
                    new Customers {
                        Name = "Tim Berners-Lee",
                        Email = "tim.bl@example.com",
                        Address = new Address(
                            street: "32 Vassar Street",
                            city: "Cambridge",
                            state: "Massachusetts",
                            country: "USA",
                            postalCode: "02139"
                        )
                    }
                };
                foreach (var item in customers)
                {
                    context.customer.AddRange(item);
                    context.SaveChanges();
                }
            }
        }

        public static void DisplayAllArticles()
        {
            using (var context = new ApplicationDbContext())
            {
                var articles = context.articles.ToList();
                foreach (var article in articles)
                {
                    Console.WriteLine($"ID: {article.Id}, Name: {article.Name}, Price: {article.Price}, NbStock: {article.StockQuantity}");
                }
            }
        }

        public enum Section
        {
            None,
            Articles,
            Orders,
            OrderDetails
        }

        public class DataParser
        {
            public List<Articles> Articles { get; set; } = new();
            public List<Orders> Orders { get; set; } = new();
            public List<OrderDetails> OrderDetails { get; set; } = new();

            public void Parse(string path)
            {
                var lines = File.ReadAllLines(path);
                Section currentSection = Section.None;
                bool skipHeader = false;

                foreach (var line in lines)
                {
                    if (string.IsNullOrWhiteSpace(line))
                        continue;

                    if (line.StartsWith("#"))
                    {
                        currentSection = line switch
                        {
                            "# Articles" => Section.Articles,
                            "# Orders" => Section.Orders,
                            "# OrderDetails" => Section.OrderDetails,
                            _ => Section.None
                        };

                        skipHeader = true;
                        continue;
                    }

                    if (skipHeader)
                    {
                        skipHeader = false;
                        continue;
                    }

                    var parts = line.Split(',');

                    switch (currentSection)
                    {
                        case Section.Articles:
                            Articles.Add(new Articles
                            {
                                Name = parts[1],
                                Description = parts[2],
                                Price = decimal.Parse(parts[3], CultureInfo.InvariantCulture),
                                StockQuantity = int.Parse(parts[4])
                            });
                            break;

                        case Section.Orders:

                            Orders.Add(new Orders
                            {
                                WarehouseId = int.Parse(parts[1]),
                                CustomerId = int.Parse(parts[2]),
                                Email = parts[3],
                                ShippingAddress = parts[4],
                                //City = parts[5],
                                OrderDate = DateTime.Parse(parts[6]),
                                TotalAmount = (double)decimal.Parse(parts[7], CultureInfo.InvariantCulture),
                                OrderStatus = parts[8]
                            });
                            break;

                        case Section.OrderDetails:
                            OrderDetails.Add(new OrderDetails
                            {
                                OrderId = int.Parse(parts[1]),
                                ArticleId = int.Parse(parts[2]),
                                Quantity = int.Parse(parts[3]),
                                UnitPrice = decimal.Parse(parts[4], CultureInfo.InvariantCulture)
                            });
                            break;
                    }
                }
            }
        }

        //public static void Main(string[] args)
        //{
        //    var builder = WebApplication.CreateBuilder(args);

        //    // Add services to the container.
        //    builder.Services.AddRazorPages();

        //    var app = builder.Build();

        //    // Configure the HTTP request pipeline.
        //    if (!app.Environment.IsDevelopment())
        //    {
        //        app.UseExceptionHandler("/Error");
        //        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        //        app.UseHsts();
        //    }

        //    app.UseHttpsRedirection();
        //    app.UseStaticFiles();

        //    app.UseRouting();

        //    app.UseAuthorization();

        //    app.MapRazorPages();

        //    app.Run();
        //}
    }
}
