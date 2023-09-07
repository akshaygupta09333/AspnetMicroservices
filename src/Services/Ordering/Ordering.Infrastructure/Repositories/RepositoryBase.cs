using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;
using Ordering.Domain.Entities;
using System.Data;

namespace Ordering.Infrastructure.Repositories
{
    public class RepositoryBase<T> : IAsyncRepository<T> where T : EntityBase
    {
        private readonly string ConnectionString = "Host=localhost;Username=postgres;Password=123;Database=Amazon;";
        public async Task<List<Order>> GetAllAsync()
        {
           return await GetAllOrders();
        }

        public async Task<List<Order>> GetAsync(Expression<Func<T, bool>> predicate)
        {
           return await GetAllOrders();
        }

        public async Task<List<Order>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, string includeString = null, bool disableTracking = true)
        {
           return await GetAllOrders();
        }

        public async Task<List<Order>> GetAsync(Expression<Func<T, bool>> predicate = null, Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null, List<Expression<Func<T, object>>> includes = null, bool disableTracking = true)
        {
           return await GetAllOrders();
        }

        public virtual async Task<Order> GetByIdAsync(int id)
        {
            using var connection = new NpgsqlConnection(ConnectionString);

            var order = await connection.QueryFirstOrDefaultAsync<Order>
                ("SELECT * FROM Orders WHERE Id = @Id", new { Id = id });

            if (order == null)
                return new Order();
            return order;
        }

        public async Task<Order> AddAsync(Order order)
        {
            using var connection = new NpgsqlConnection(ConnectionString);

            var affected =
                await connection.ExecuteAsync
                    ("INSERT INTO Orders (UserName, CVV, CardName, CardNumber, Country) VALUES (@UserName, @CVV, @CardName, @CardNumber, @Country)",
                            new { UserName = order.UserName, CVV = order.CVV, CardName = order.CardName, CardNumber = order.CardNumber, Country = order.Country });

            return order;
        }

        public async Task UpdateAsync(Order order)
        {
            using var connection = new NpgsqlConnection(ConnectionString);

            var affected = await connection.ExecuteAsync
                    ("UPDATE Orders SET CVV=@CVV, CardName = @CardName, CardNumber = @CardNumber WHERE UserName = @UserName",
                            new { CVV = order.CVV, CardName = order.CardName, CardNumber = order.CardNumber, UserName = order.UserName });

        }

        public async Task DeleteAsync(Order order)
        {
            using var connection = new NpgsqlConnection(ConnectionString);

            var affected = await connection.ExecuteAsync("DELETE FROM Orders WHERE UserName = @UserName",
                new { UserName = order.UserName });
        }

        
        private async Task<List<Order>> GetAllOrders()
        {
             List<Order> orders = new();
            NpgsqlConnection con = new NpgsqlConnection(ConnectionString);
            try
            {
                con.Open();
                var query = "Select * from Orders";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                var dataReader =await cmd.ExecuteReaderAsync();
                DataTable dt = new DataTable();
                dt.Load(dataReader);
                foreach (DataRow row in dt.Rows)
                {
                    Order order = new();
                    order.UserName = Convert.ToString(row["UserName"]);
                    order.CVV = Convert.ToString(row["CVV"]);
                    order.CardName = Convert.ToString(row["CardName"]);
                    order.CardNumber = Convert.ToString(row["CardNumber"]);
                    order.Country = Convert.ToString(row["Country"]);
                    orders.Add(order);
                }
            }
            catch (Exception ex)
            {
            }
            finally
            {
                con.Close();
            }
            return orders;
        }
    }
}
