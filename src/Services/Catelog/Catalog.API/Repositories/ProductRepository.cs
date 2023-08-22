using System.Dynamic;
using Catalog.API.Entities;
using Catalog.API.Repositories.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Data;
using Npgsql;


namespace Catalog.API.Repositories
{
    public class ProductRepository : IProductRepository
    {
        private readonly string _connectionString;
        private readonly string productTableName = "public.\"Product\"";
        private readonly ILogger<ProductRepository> _logger;
        public ProductRepository(ILogger<ProductRepository> logger)
        {
            _connectionString = "Host=localhost;Username=postgres;Password=123;Database=Amazon";
            _logger = logger;
        }

        public async Task<IEnumerable<Product>> GetProducts()
        {
            List<Product> products = new();
            NpgsqlConnection con = new NpgsqlConnection(_connectionString);
            try
            {
                con.Open();
                var query = "Select * from " + productTableName;
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                var dataReader =await cmd.ExecuteReaderAsync();
                DataTable dt = new DataTable();
                dt.Load(dataReader);
                foreach (DataRow row in dt.Rows)
                {
                    Product product = new();
                    product.Name = Convert.ToString(row["Name"]);
                    product.Id = Convert.ToString(row["Id"]);
                    product.Category = Convert.ToString(row["Category"]);
                    product.Summary = Convert.ToString(row["Summary"]);
                    product.Description = Convert.ToString(row["Description"]);
                    product.ImageFile = Convert.ToString(row["ImageFile"]);
                    product.Price = Convert.ToInt64(row["Price"]);
                    products.Add(product);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return products;
        }

        public async Task<Product> GetProduct(string id)
        {
            Product product = new();
            NpgsqlConnection con = new NpgsqlConnection(_connectionString);
            try
            {
                con.Open();
                var query = "SELECT * FROM " + productTableName + " WHERE \"Id\" =" + id;
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                var dataReader = await cmd.ExecuteReaderAsync();
                DataTable dt = new DataTable();
                dt.Load(dataReader);
                foreach (DataRow row in dt.Rows)
                {
                    product.Name = Convert.ToString(row["Name"]);
                    product.Id = Convert.ToString(row["Id"]);
                    product.Category = Convert.ToString(row["Category"]);
                    product.Summary = Convert.ToString(row["Summary"]);
                    product.Description = Convert.ToString(row["Description"]);
                    product.ImageFile = Convert.ToString(row["ImageFile"]);
                    product.Price = Convert.ToInt64(row["Price"]);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return product;
        }

        public async Task<IEnumerable<Product>> GetProductByName(string name)
        {
            List<Product> products = new();
            NpgsqlConnection con = new NpgsqlConnection(_connectionString);
            try
            {
                con.Open();
                var query = "Select * from " + productTableName + " WHERE \"Name\" ='" + name + "'";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                var dataReader = await cmd.ExecuteReaderAsync();
                DataTable dt = new DataTable();
                dt.Load(dataReader);
                foreach (DataRow row in dt.Rows)
                {
                    Product product = new();
                    product.Name = Convert.ToString(row["Name"]);
                    product.Id = Convert.ToString(row["Id"]);
                    product.Category = Convert.ToString(row["Category"]);
                    product.Summary = Convert.ToString(row["Summary"]);
                    product.Description = Convert.ToString(row["Description"]);
                    product.ImageFile = Convert.ToString(row["ImageFile"]);
                    product.Price = Convert.ToInt64(row["Price"]);
                    products.Add(product);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return products;
        }

        public async Task<IEnumerable<Product>> GetProductByCategory(string categoryName)
        {
            List<Product> products = new();
            NpgsqlConnection con = new NpgsqlConnection(_connectionString);
            try
            {
                con.Open();
                var query = "Select * from " + productTableName + " WHERE \"Category\" ='" + categoryName + "'";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                var dataReader =await cmd.ExecuteReaderAsync();
                DataTable dt = new DataTable();
                dt.Load(dataReader);
                foreach (DataRow row in dt.Rows)
                {
                    Product product = new();
                    product.Name = Convert.ToString(row["Name"]);
                    product.Id = Convert.ToString(row["Id"]);
                    product.Category = Convert.ToString(row["Category"]);
                    product.Summary = Convert.ToString(row["Summary"]);
                    product.Description = Convert.ToString(row["Description"]);
                    product.ImageFile = Convert.ToString(row["ImageFile"]);
                    product.Price = Convert.ToInt64(row["Price"]);
                    products.Add(product);
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            finally
            {
                con.Close();
            }
            return products;
        }


        public async Task CreateProduct(Product product)
        {
            NpgsqlConnection con = new NpgsqlConnection(_connectionString);
            try
            {

                var query = "INSERT INTO " + productTableName + "(\"Id\", \"Name\", \"Summary\", \"Description\", \"ImageFile\", \"Price\", \"Category\") VALUES"
                            +"("+product.Id+",'"+product.Name+"','"+product.Summary+"','"+product.Description+"'"
                            +",'"+product.ImageFile+"','"+product.Price+"','"+product.Category+"')";
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
            }
            finally
            {
                con.Close();
            }
        }

        public async Task<bool> UpdateProduct(Product product)
        {
            NpgsqlConnection con = new NpgsqlConnection(_connectionString);
            try
            {
                var query = "UPDATE " + productTableName + " SET \"Price\" = " + product.Price + " WHERE \"Id\" =" + product.Id;
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }

        public async Task<bool> DeleteProduct(string id)
        {
            NpgsqlConnection con = new NpgsqlConnection(_connectionString);
            try
            {
                var query = "DELETE FROM " + productTableName + " WHERE \"Id\" =" + id;
                NpgsqlCommand cmd = new NpgsqlCommand(query, con);
                con.Open();
                await cmd.ExecuteNonQueryAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message);
                return false;
            }
            finally
            {
                con.Close();
            }
            return true;
        }
    }
}