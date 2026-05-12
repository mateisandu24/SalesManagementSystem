using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using Dapper;
using SalesManagementSystem.Models;

namespace SalesManagementSystem.Repositories
{

    public class OrderRepository
    {
        private readonly string _connectionString;
        public OrderRepository(string connectionString)
        {
            _connectionString = connectionString;
        }

        public bool PlaceOrder(Guid userId, List<Product> items)
        {
            if (items == null || items.Count == 0) return false;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        decimal totalAmount = items.Sum(p => p.Price);

                        string insertOrderSql = @"
                            INSERT INTO Orders (UserId, OrderDate, TotalAmount) 
                            OUTPUT INSERTED.Id 
                            VALUES (@UserId, GETDATE(), @TotalAmount)";

                        int orderId = connection.QuerySingle<int>(
                            insertOrderSql,
                            new { UserId = userId, TotalAmount = totalAmount },
                            transaction);

                        foreach (var item in items)
                        {
                            InsertItem(connection, orderId, item, transaction);

                            UpdateStock(connection,item,transaction);
                        }

                        transaction.Commit();

                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();

                        throw;
                    }
                }
            }
        }

        public void InsertItem(SqlConnection connection, int orderId, Product item, SqlTransaction transaction)
        {
            string insertItemSql = @"
                                INSERT INTO OrderItems (OrderId, ProductId, Price) 
                                VALUES (@OrderId, @ProductId, @Price)";

            connection.Execute(insertItemSql, new
            {
                OrderId = orderId,
                ProductId = item.Id,
                Price = item.Price
            }, transaction);
        }

        public void UpdateStock(SqlConnection connection, Product item, SqlTransaction transaction)
        {
            string updateStockSql = @"
                                UPDATE Products SET Stock = Stock - 1 WHERE Id = @Id AND Stock > 0";

            connection.Execute(updateStockSql, new { Id = item.Id }, transaction);
        }

        public bool PlaceOrderDirect(Guid userId, Product product, int quantity)
        {
            if (product == null || quantity <= 0) return false;

            using (var connection = new SqlConnection(_connectionString))
            {
                connection.Open();

                using (var transaction = connection.BeginTransaction())
                {
                    try
                    {
                        int currentStock = connection.QuerySingle<int>(
                            "SELECT Stock FROM Products WHERE Id = @Id",
                            new { Id = product.Id },
                            transaction);

                        if (currentStock < quantity)
                        {
                            transaction.Rollback();
                            return false;
                        }

                        decimal totalAmount = product.Price * quantity;

                        string insertOrderSql = @"
                            INSERT INTO Orders (UserId, OrderDate, TotalAmount) 
                            OUTPUT INSERTED.Id 
                            VALUES (@UserId, GETDATE(), @TotalAmount)";

                        int orderId = connection.QuerySingle<int>(
                            insertOrderSql,
                            new { UserId = userId, TotalAmount = totalAmount },
                            transaction);

                        // Insert one OrderItem row per unit (or a single row — here we insert one row with the total price)
                        string insertItemSql = @"
                            INSERT INTO OrderItems (OrderId, ProductId, Price) 
                            VALUES (@OrderId, @ProductId, @Price)";

                        connection.Execute(insertItemSql, new
                        {
                            OrderId = orderId,
                            ProductId = product.Id,
                            Price = totalAmount
                        }, transaction);

                        // Decrement stock
                        string updateStockSql = @"
                            UPDATE Products SET Stock = Stock - @Qty WHERE Id = @Id";

                        connection.Execute(updateStockSql, new
                        {
                            Qty = quantity,
                            Id = product.Id
                        }, transaction);

                        transaction.Commit();

                        return true;
                    }
                    catch
                    {
                        transaction.Rollback();

                        throw;
                    }
                }
            }
        }

        public object GetAllOrdersForAdmin()
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = @"
            SELECT 
                o.Id AS [Număr Comandă],
                u.Username AS [Nume Client],
                o.OrderDate AS [Data Comenzii],
                o.TotalAmount AS [Total de Plată (RON)]
            FROM Orders o
            INNER JOIN Users u ON o.UserId = u.Id
            ORDER BY o.OrderDate DESC";

                return connection.Query(sql).ToList();
            }
        }

        public object GetOrdersByUserId(Guid userId)
        {
            using (var connection = new SqlConnection(_connectionString))
            {
                string sql = @"
            SELECT 
                o.Id AS [Număr Comandă],
                o.OrderDate AS [Data Comenzii],
                o.TotalAmount AS [Total de Plată (RON)]
            FROM Orders o
            WHERE o.UserId = @UserId
            ORDER BY o.OrderDate DESC";

                return connection.Query(sql, new { UserId = userId }).ToList();
            }
        }
    }

}
