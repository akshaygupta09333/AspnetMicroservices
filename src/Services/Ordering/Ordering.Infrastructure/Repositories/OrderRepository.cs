using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository
    {
        private readonly IConfiguration _configuration;
        public OrderRepository(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            using var connection = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));

            var orders = await connection.QueryFirstOrDefaultAsync<List<Order>>
                ("SELECT * FROM Coupon WHERE UserName = @UserName", new { UserName = userName });
            return orders;
        }
    }
}
