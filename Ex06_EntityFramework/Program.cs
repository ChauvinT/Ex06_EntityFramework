using BO;
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
            //InsertDefaultData();

            DisplayAllArticles();
            //Console.ReadLine();

            var parser = new DataParser();
            parser.Parse(@"C:\Users\thomas.chauvin\Downloads\export.csv");

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

                var articles = new List<Articles>

                            {
                                new Articles { Name = "Ordinateur Portable", Description = "Ordinateur portable haute performance", Price = 1200.00m, StockQuantity = 50 },
                                new Articles { Name = "Smartphone", Description = "Smartphone avec écran AMOLED", Price = 800.00m, StockQuantity = 100 },
                                new Articles { Name = "Tablette", Description = "Tablette 10 pouces avec stylet", Price = 600.00m, StockQuantity = 30 }
                            };
                context.articles.AddRange(articles);
                context.SaveChanges(); 

                var orders = new List<Orders>
                            {
                                new Orders
                                {
                                    WarehouseId = 1,
                                    CustomerId = 1,
                                    CustomerName = "John Doe",
                                    Email = "johndoe@example.com",
                                    ShippingAddress = "123 Main Street",
                                    OrderDate = DateTime.Now,
                                    TotalAmount = 2000.00d,
                                    OrderStatus = "Processing",
                                    OrderDetails = new List<OrderDetails>
                                    {
                                        new OrderDetails { Article = articles[1], Quantity = 1, UnitPrice = 1200.00m },
                                        new OrderDetails { Article = articles[1], Quantity = 1, UnitPrice = 800.00m }
                                    }
                                },
                                new Orders
                                {
                                    WarehouseId = 2,
                                    CustomerId = 2,
                                    CustomerName = "Bill Gate",
                                    Email = "bill.gate@example.com",
                                    ShippingAddress = "One Microsoft Way",
                                    OrderDate = DateTime.Now,
                                    TotalAmount = 2000.00d,
                                    OrderStatus = "Processing",
                                    OrderDetails = new List<OrderDetails>
                                    {
                                        new OrderDetails { Article = articles[2], Quantity = 1, UnitPrice = 800.00m }
                                    }
                                }
                            };

                context.orders.AddRange(orders);
                context.SaveChanges();
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
                                //Id = int.Parse(parts[0]),
                                Name = parts[1],
                                Description = parts[2],
                                Price = decimal.Parse(parts[3], CultureInfo.InvariantCulture),
                                StockQuantity = int.Parse(parts[4])
                            });
                            break;

                        case Section.Orders:

                            Orders.Add(new Orders
                            {
                                //Id = int.Parse(parts[0]),
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
                                //Id = int.Parse(parts[0]),
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
