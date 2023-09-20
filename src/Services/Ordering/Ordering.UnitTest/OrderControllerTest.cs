using Xunit;
using Moq;
using MediatR;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Ordering.API.Controllers;
using System.Collections.Generic;

namespace Ordering.UnitTest;

public class OrderControllerTest
{
    private readonly Mock<IMediator> _mediator;
    private readonly OrderController _controller;
    private readonly string UserName="Akshay";
    public OrderControllerTest()
    {
        _mediator = new Mock<IMediator>();
        _controller = new OrderController(_mediator.Object);
    }

    [Fact]
    public async Task GetOrdersByUserName_Test()
    {
        var result = await _controller.GetOrdersByUserName(UserName);
        Assert.IsType<OkObjectResult>(result.Result);
    }

    [Fact]
    public async Task DeleteOrder_Test()
    {
        var result = await _controller.DeleteOrder(1);
        Assert.IsType<NoContentResult>(result);
        _mediator.Verify();
    }

}