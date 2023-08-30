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
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        private readonly IConfiguration _configuration1;
        public OrderRepository(IConfiguration configuration1)
        {
            _configuration1 = configuration1;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            using var connection = new NpgsqlConnection(_configuration1.GetValue<string>("DatabaseSettings:ConnectionString"));

            var orders = await connection.QueryFirstOrDefaultAsync<List<Order>>
                ("SELECT * FROM Coupon WHERE UserName = @UserName", new { UserName = userName });
            return orders;
        }
    }
}
