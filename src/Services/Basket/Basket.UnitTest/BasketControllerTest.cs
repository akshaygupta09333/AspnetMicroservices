using Xunit;
using Basket.API.Controllers;
using System.Threading.Tasks;
using Basket.API.Repositories.Interfaces;
using Moq;
using Microsoft.AspNetCore.Mvc;
using Basket.API.Entities;
using System.Collections.Generic;

namespace Basket.UnitTest;

public class BasketControllerTest
{
    private readonly BasketController _controller;
    private readonly Mock<IBasketRepository> _repository;
    private string UserName = "Akshay";

    public BasketControllerTest()
    {
        _repository = new Mock<IBasketRepository>();
        _controller = new BasketController(_repository.Object);
    }

    [Fact]
    public async Task GetBasket_Test()
    {
        var result = await _controller.GetBasket(UserName);
        Assert.IsType<ActionResult<ShoppingCart>>(result as ActionResult<ShoppingCart>);
    }

    [Fact]
    public async Task UpdateBasket_Test()
    {
        ShoppingCart shoppingCart = new ShoppingCart();
        shoppingCart.UserName = UserName;
        ShoppingCartItem shoppingCartItem = new ShoppingCartItem();
        shoppingCartItem.Quantity = 1;
        shoppingCartItem.Color = "Green";
        shoppingCartItem.Price = 1000;
        shoppingCartItem.ProductId = "165TIUJ18";
        shoppingCartItem.ProductName = "Iphone 15";
        shoppingCart.Items.Add(shoppingCartItem);

        var result = await _controller.UpdateBasket(shoppingCart);
        Assert.IsType<ActionResult<ShoppingCart>>(result as ActionResult<ShoppingCart>);
    }

    [Fact]
    public async Task DeleteBasket_Test()
    {
        var result = await _controller.DeleteBasket(UserName);
        _repository.Verify();
    }
}