using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Dapper;
using Microsoft.VisualBasic.FileIO;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Repositories
{
    public class ProductRepository
    {
        private readonly string _connectionString;

        public ProductRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        private string CleanHTML(string input)
        {
            if (string.IsNullOrEmpty(input))
            {
                return input;
            }

            return Regex.Replace(input, "<.*?>", string.Empty).Trim();
        }

        public void ImportFromCSV(string filePath)
        {
            using (TextFieldParser parser = new TextFieldParser(filePath))
            {
                parser.TextFieldType = FieldType.Delimited;
                parser.SetDelimiters(",");
                parser.HasFieldsEnclosedInQuotes = true;

                if (!parser.EndOfData)
                {
                    parser.ReadFields();
                }

                using (var connection = new SqlConnection(_connectionString))
                {
                    connection.Open();

                    while (!parser.EndOfData)
                    {
                        string[] fields = parser.ReadFields();

                        if (fields.Length < 11) continue;

                        string name = fields[0];
                        string baseDescription = CleanHTML(fields[1]);
                        string imageUrl = fields[2];
                        string collection = fields[3];

                        string modUtilizare = CleanHTML(fields[6]);
                        string ingrediente = CleanHTML(fields[7]);
                        string precautii = CleanHTML(fields[8]);
                        string producator = CleanHTML(fields[9]);

                        string combinedDescription = baseDescription;
                        if (!string.IsNullOrWhiteSpace(modUtilizare))
                            combinedDescription += $"\r\n\r\nMod de utilizare:\r\n{modUtilizare}";
                        if (!string.IsNullOrWhiteSpace(ingrediente))
                            combinedDescription += $"\r\n\r\nIngrediente:\r\n{ingrediente}";
                        if (!string.IsNullOrWhiteSpace(precautii))
                            combinedDescription += $"\r\n\r\nPrecauții:\r\n{precautii}";
                        if (!string.IsNullOrWhiteSpace(producator))
                            combinedDescription += $"\r\n\r\nProducător:\r\n{producator}";

                        string description = combinedDescription;
                        string brandString = fields[10].Replace(" ", "");

                        if (!Enum.TryParse(brandString, true, out Brand brandEnum))
                        {
                            brandEnum = Brand.Other;
                        }

                        MainCategory mainCat = MainCategory.Other;

                        if (collection.IndexOf("Picioare", StringComparison.OrdinalIgnoreCase) >= 0)
                            mainCat = MainCategory.FootCare;

                        else if (collection.IndexOf("Corp", StringComparison.OrdinalIgnoreCase) >= 0
                            || collection.IndexOf("Body", StringComparison.OrdinalIgnoreCase) >= 0)
                            mainCat = MainCategory.BodyCare;

                        else if (collection.IndexOf("Baie", StringComparison.OrdinalIgnoreCase) >= 0)
                            mainCat = MainCategory.Bath;


                        SubCategory subCat = SubCategory.Other;

                        if (name.IndexOf("Balsam", StringComparison.OrdinalIgnoreCase) >= 0
                            || name.IndexOf("Loțiune", StringComparison.OrdinalIgnoreCase) >= 0)
                            subCat = SubCategory.Lotion;

                        else if (name.IndexOf("Exfoliant", StringComparison.OrdinalIgnoreCase) >= 0)
                            subCat = SubCategory.Scrub;

                        else if (name.IndexOf("Gel de duș", StringComparison.OrdinalIgnoreCase) >= 0)
                            subCat = SubCategory.ShowerGel;

                        else if (name.IndexOf("Săpun", StringComparison.OrdinalIgnoreCase) >= 0
                            || name.IndexOf("Soaps", StringComparison.OrdinalIgnoreCase) >= 0)
                            subCat = SubCategory.Soap;


                        decimal.TryParse(fields[5], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal rawPrice);
                        decimal finalPrice = Math.Max(10m, Math.Min(200m, rawPrice));


                        string sql = @"
                    INSERT INTO Products (Name, Description, ImageUrl, Price, Stock, Vat, MainCategory, SubCategory, Brand) 
                    VALUES (@Name, @Description, @ImageUrl, @Price, @Stock, @Vat, @MainCategory, @SubCategory, @Brand)";


                        connection.Execute(sql, new
                        {
                            Name = name,
                            Description = description,
                            ImageUrl = imageUrl,
                            Price = finalPrice,
                            Stock = 50,
                            Vat = 0.21m,
                            MainCategory = (int)mainCat,
                            SubCategory = (int)subCat,
                            Brand = (int)brandEnum
                        });
                    }
                }
            }
        }

        public List<Product> GetAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.Query<Product>("SELECT Id, Name, Description, ImageUrl, Price, Stock, Vat, MainCategory, SubCategory, Brand FROM Products").ToList();
            }
        }

        public Product GetById(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                return connection.QueryFirstOrDefault<Product>(
                    "SELECT Id, Name, Description, ImageUrl, Price, Stock, Vat, MainCategory, SubCategory, Brand FROM Products WHERE Id = @Id",
                    new { Id = id });
            }
        }

        public void UpdateStock(Guid productId, int newStock)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("UPDATE Products SET Stock = @Stock WHERE Id = @Id",
                    new { Stock = newStock, Id = productId });
            }
        }

        public void Add(Product p)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = @"INSERT INTO Products (Name, Description, ImageUrl, Price, Stock, Vat, MainCategory, SubCategory, Brand) 
                               VALUES (@Name, @Description, @ImageUrl, @Price, @Stock, @Vat, @MainCategory, @SubCategory, @Brand)";

                connection.Execute(sql, new
                {
                    p.Name,
                    p.Description,
                    p.ImageUrl,
                    p.Price,
                    p.Stock,
                    p.Vat,
                    MainCategory = (int)p.MainCategory,
                    SubCategory = (int)p.SubCategory,
                    Brand = (int)p.Brand
                });
            }
        }

        public void Delete(Guid id)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("DELETE FROM OrderItems WHERE ProductId = @Id", new { Id = id });
                connection.Execute("DELETE FROM Products WHERE Id = @Id", new { Id = id });
            }
        }

        public void DeleteAll()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Execute("DELETE FROM OrderItems");
                connection.Execute("DELETE FROM Products");
            }
        }

        public void Update(Product p)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = @"UPDATE Products 
                               SET Name = @Name, 
                                   Description = @Description, 
                                   ImageUrl = @ImageUrl, 
                                   Price = @Price, 
                                   Stock = @Stock, 
                                   Vat = @Vat, 
                                   MainCategory = @MainCategory, 
                                   SubCategory = @SubCategory, 
                                   Brand = @Brand 
                               WHERE Id = @Id";

                connection.Execute(sql, new
                {
                    p.Id,
                    p.Name,
                    p.Description,
                    p.ImageUrl,
                    p.Price,
                    p.Stock,
                    p.Vat,
                    MainCategory = (int)p.MainCategory,
                    SubCategory = (int)p.SubCategory,
                    Brand = (int)p.Brand
                });
            }
        }
    }
}
