using Dapper;
using SalesManagementSystem.Models;
using Microsoft.VisualBasic.FileIO;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;

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

                        if (fields.Length < 10) continue;

                        string name = fields[2];
                        string description = CleanHTML(fields[3]);
                        string imageUrl = fields[4];
                        string collection = fields[5];
                        string brandString = fields.Length > 52 ? fields[52].Replace(" ", "") : "Other";

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


                        decimal.TryParse(fields[8], NumberStyles.Any, CultureInfo.InvariantCulture, out decimal rawPrice);
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
                            Stock = 20,
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
                connection.Execute("DELETE FROM Products WHERE Id = @Id", new { Id = id });
            }
        }
    }
}
