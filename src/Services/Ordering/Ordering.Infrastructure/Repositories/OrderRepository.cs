using Ordering.Application.Contracts.Persistence;
using Ordering.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Configuration;
using Npgsql;
using Dapper;
using Microsoft.Extensions.Logging;
using System.Data;

namespace Ordering.Infrastructure.Repositories
{
    public class OrderRepository : RepositoryBase<Order>, IOrderRepository
    {
        private readonly ILogger<OrderRepository> _logger;
        private readonly IConfiguration _configuration;
        public OrderRepository(IConfiguration configuration, ILogger<OrderRepository> logger)
        {
            _configuration = configuration;
            _logger = logger;
        }

        public async Task<IEnumerable<Order>> GetOrdersByUserName(string userName)
        {
            List<Order> orders = new();
            NpgsqlConnection con = new NpgsqlConnection(_configuration.GetValue<string>("DatabaseSettings:ConnectionString"));
            try
            {
                con.Open();
                var query = "Select * from Orders WHERE UserName = '" + userName + "'";
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
