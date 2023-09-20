using Xunit;
using Discount.API.Controllers;
using System.Threading.Tasks;
using Discount.API.Repositories.Interfaces;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Discount.API.Entities;
using System.Collections.Generic;

namespace Discount.UnitTest;

public class DiscountControllerTest
{
    private readonly DiscountController _controller;
    private readonly Mock<IDiscountRepository> _repository;
    private string ProductName = "IPhone 15";

    public DiscountControllerTest()
    {
        _repository = new Mock<IDiscountRepository>();
        _controller = new DiscountController(_repository.Object);
    }

    [Fact]
    public async Task GetDiscount_Test()
    {
        var result = await _controller.GetDiscount(ProductName);
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task CreateDiscount_Test()
    {
        Coupon coupon = new Coupon()
        {
            Id = 10,
            ProductName = "IPhone 15",
            Description = "Mobile phone",
            Amount = 10000
        };
        var result = await _controller.CreateDiscount(coupon);
        _repository.Verify();
    }

    [Fact]
    public async Task UpdateDiscount_Test()
    {
        Coupon coupon = new Coupon()
        {
            Id = 10,
            ProductName = "IPhone 15",
            Description = "Mobile phone",
            Amount = 20000
        };
        var result = await _controller.UpdateDiscount(coupon);
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task DeleteDiscount_Test()
    {
        var result = await _controller.DeleteDiscount(ProductName);
        Assert.IsType<OkObjectResult>(result.Result);
        _repository.Verify();
    }
}