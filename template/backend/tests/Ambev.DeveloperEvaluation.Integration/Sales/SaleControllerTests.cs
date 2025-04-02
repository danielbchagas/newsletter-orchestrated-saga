using Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.Transport;
using Ambev.DeveloperEvaluation.Application.Sales.Validators;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales;
using AutoFixture;
using FluentValidation.Results;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Integration.Sales;

public class SaleControllerTests
{
    private readonly Mock<IMediator> _mediatorMock;
    private readonly SaleController _controller;

    public SaleControllerTests()
    {
        _mediatorMock = new Mock<IMediator>();
        _controller = new SaleController(_mediatorMock.Object);
    }

    [Fact]
    public async Task Get_ReturnsOkResult_WithSale()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var query = new GetSaleQuery(saleId);
        var saleTransport = new Fixture()
            .Build<SaleTransport>()
            .With(s => s.Id, saleId)
            .Create();
        var sale = new Fixture()
            .Build<GetSaleResult>()
            .With(s => s.Transport, saleTransport)
            .Create();
        
        _mediatorMock.Setup(m => m.Send(query, It.IsAny<CancellationToken>())).ReturnsAsync(sale);

        // Act
        var result = await _controller.Get(saleId, CancellationToken.None) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task Post_ReturnsOkResult_WithCreatedSale()
    {
        // Arrange
        var saleTransport = new Fixture()
            .Build<SaleTransport>()
            .With(s => s.Id, Guid.NewGuid())
            .Create();
        var command = new CreateSaleCommand(saleTransport);
        var createdSale = new Fixture()
            .Build<CreateSaleResult>()
            .With(s => s.Id, saleTransport.Id)
            .Create();
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(createdSale);

        // Act
        var result = await _controller.Post(saleTransport, CancellationToken.None) as OkObjectResult;

        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }

    [Fact]
    public async Task Post_ReturnsBadRequest_WhenValidationFails()
    {
        // Arrange
        var saleTransport = new Fixture()
            .Build<SaleTransport>()
            .With(s => s.Id, Guid.NewGuid())
            .Create();
        var validationResult = new SaleTransportValidator().Validate(saleTransport);
        validationResult.Errors.Add(new ValidationFailure("Property", "Error"));
    
        // Act
        var result = await _controller.Post(saleTransport, CancellationToken.None) as BadRequestObjectResult;
    
        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
    }
    
    [Fact]
    public async Task Put_ReturnsOkResult_WithUpdatedSale()
    {
        // Arrange
        var saleTransport = new Fixture()
            .Build<SaleTransport>()
            .With(s => s.Id, Guid.NewGuid())
            .Create();
        var command = new UpdateSaleCommand(saleTransport);
        var updatedSaleResult = new Fixture()
            .Build<UpdateSaleResult>()
            .With(s => s.Id, Guid.NewGuid())
            .Create();
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(updatedSaleResult);
    
        // Act
        var result = await _controller.Put(saleTransport.Id, saleTransport, CancellationToken.None) as OkObjectResult;
    
        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }
    
    [Fact]
    public async Task Put_ReturnsBadRequest_WhenIdMismatch()
    {
        // Arrange
        var saleTransport = new Fixture()
            .Build<SaleTransport>()
            .With(s => s.Id, Guid.NewGuid())
            .Create();
    
        // Act
        var result = await _controller.Put(Guid.NewGuid(), saleTransport, CancellationToken.None) as BadRequestObjectResult;
    
        // Assert
        Assert.NotNull(result);
        Assert.Equal(400, result.StatusCode);
    }
    
    [Fact]
    public async Task Delete_ReturnsOkResult_WithDeletedSale()
    {
        // Arrange
        var saleId = Guid.NewGuid();
        var command = new DeleteSaleCommand(saleId);
        var deletedSaleResult = new Fixture()
            .Build<DeleteSaleResult>()
            .With(s => s.Id, saleId)
            .Create();
        _mediatorMock.Setup(m => m.Send(command, It.IsAny<CancellationToken>())).ReturnsAsync(deletedSaleResult);
    
        // Act
        var result = await _controller.Delete(saleId, CancellationToken.None) as OkObjectResult;
    
        // Assert
        Assert.NotNull(result);
        Assert.Equal(200, result.StatusCode);
    }
}