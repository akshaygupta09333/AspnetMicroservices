using Xunit;
using Catalog.API.Controllers;
using System.Threading.Tasks;
using Catalog.API.Repositories.Interfaces;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Catalog.API.Entities;
using System.Collections.Generic;
using Microsoft.Extensions.Logging;

namespace Catelog.UnitTest;

public class CatalogControllerTest
{
    private readonly CatalogController _controller;
    private readonly Mock<IProductRepository> _repository;
    private readonly Mock<ILogger<CatalogController>> _logger;
    private string ProductId = "1";
    private string Category = "Smart Phone";
    private string Name = "IPhone 15";

    public CatalogControllerTest()
    {
        _repository = new Mock<IProductRepository>();
        _logger = new Mock<ILogger<CatalogController>>();
        _controller = new CatalogController(_repository.Object, _logger.Object);
    }

    [Fact]
    public async Task GetProducts_Test()
    {
        var result = await _controller.GetProducts();
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetProductById_Test()
    {
        var result = await _controller.GetProductById(ProductId);
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetProductByName_Test()
    {
        var result = await _controller.GetProductByName(Name);
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task GetProductByCategory_Test()
    {
        var result = await _controller.GetProductByCategory(Category);
        Assert.IsType<ActionResult<IEnumerable<Product>>>(result as ActionResult<IEnumerable<Product>>);
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateProduct_Test()
    {
        Product product = new Product()
        {
            Id = "123",
            Name = "IPhone 15",
            Category = "Smart Phone",
            Summary = "Smart Phone IPhone 15",
            Description = "Smart Phone IPhone 15",
            ImageFile = "Image1",
            Price = 1000
        };
        var result = await _controller.CreateProduct(product);
        Assert.IsType<OkObjectResult>(result.Result);
        _repository.Verify();
    }

    [Fact]
    public async Task UpdateProduct_Test()
    {
        Product product = new Product()
        {
            Id = "123",
            Name = "IPhone 15",
            Category = "Smart Phone",
            Summary = "Smart Phone IPhone 15",
            Description = "Smart Phone IPhone 15",
            ImageFile = "Image1",
            Price = 1000
        };
        var result = await _controller.UpdateProduct(product);
        Assert.IsType<OkObjectResult>(result);
        _repository.Verify();
    }

    [Fact]
    public async Task DeleteProductById_Test()
    {
        var result = await _controller.DeleteProductById(ProductId);
        Assert.IsType<OkObjectResult>(result);
        _repository.Verify();
    }
}