using Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Notifications.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Transport;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using AutoFixture;
using FluentAssertions;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class CreateSaleHandlerTests
{
    private readonly Mock<ILogger<CreateSaleHandler>> _loggerMock;
    private readonly Mock<ISaleRepository> _saleRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly CreateSaleHandler _handler;

    public CreateSaleHandlerTests()
    {
        _loggerMock = new Mock<ILogger<CreateSaleHandler>>();
        _saleRepositoryMock = new Mock<ISaleRepository>();
        _mapperMock = new Mock<IMapper>();
        _mediatorMock = new Mock<IMediator>();
        _handler = new CreateSaleHandler(_loggerMock.Object, _saleRepositoryMock.Object, _mapperMock.Object, _mediatorMock.Object);
    }

    [Fact]
    public async Task Handle_CreatesSaleSuccessfully()
    {
        // Arrange
        var data = new Fixture()
            .Build<SaleTransport>()
            .With(s => s.Date, DateTime.Now)
            .Create();
        var command = new Fixture()
            .Build<CreateSaleCommand>()
            .With(s => s.Data, data)
            .Create();
        var sale = new Fixture()
            .Build<Sale>()
            .With(s => s.Date, DateTime.Now)
            .Create();
        
        _mapperMock.Setup(s => s.Map<Sale>(command)).Returns(sale);
        _saleRepositoryMock.Setup(s => s.CreateAsync(It.IsAny<Sale>(), It.IsAny<CancellationToken>())).ReturnsAsync(sale);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        _mapperMock.Verify(s => s.Map<Sale>(command), Times.Once);
        _saleRepositoryMock.Verify(s => s.CreateAsync(sale, It.IsAny<CancellationToken>()), Times.Once);
    }
    
    [Fact]
    public async Task Handle_ThrowsValidationException_WhenSaleIsInvalid()
    {
        // Arrange
        var data = new Fixture()
            .Build<SaleTransport>()
            .Without(s => s.Date)
            .Create();
        var command = new Fixture()
            .Build<CreateSaleCommand>()
            .Without(s => s.Data)
            .Create();
        var sale = new Fixture()
            .Build<Sale>()
            .Without(s => s.Date)
            .Create();
        
        _mapperMock.Setup(s => s.Map<Sale>(command)).Returns(sale);
        _saleRepositoryMock.Setup(s => s.CreateAsync(It.IsAny<Sale>(), It.IsAny<CancellationToken>())).ReturnsAsync(sale);
        
        // Act
        await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));

        // Assert
        _saleRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Sale>(), It.IsAny<CancellationToken>()), Times.Never);
        _mediatorMock.Verify(m => m.Publish(It.IsAny<CreateSaleNotification>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}