using Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Notifications.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.Transport;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentValidation;
using FluentValidation.Results;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;
using System.Threading;
using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;
using AutoFixture;
using FluentAssertions;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

public class UpdateSaleHandlerTests
{
    private readonly Mock<ILogger<UpdateSaleHandler>> _loggerMock;
    private readonly Mock<ISaleRepository> _saleRepositoryMock;
    private readonly Mock<IMapper> _mapperMock;
    private readonly Mock<IMediator> _mediatorMock;
    private readonly UpdateSaleHandler _handler;

    public UpdateSaleHandlerTests()
    {
        _loggerMock = new Mock<ILogger<UpdateSaleHandler>>();
        _saleRepositoryMock = new Mock<ISaleRepository>();
        _mapperMock = new Mock<IMapper>();
        _mediatorMock = new Mock<IMediator>();
        _handler = new UpdateSaleHandler(_loggerMock.Object, _saleRepositoryMock.Object, _mapperMock.Object, _mediatorMock.Object);
    }

    // TODO: Verify if notification is sent
    [Fact]
    public async Task Handle_UpdatesSaleSuccessfully()
    {
        // Arrange
        var data = new Fixture()
            .Build<SaleTransport>()
            .With(s => s.Date, DateTime.Now)
            .Create();
        var command = new Fixture()
            .Build<UpdateSaleCommand>()
            .With(s => s.Data, data)
            .Create();
        var sale = new Fixture()
            .Build<Sale>()
            .With(s => s.Date, DateTime.Now)
            .Create();
        
        _mapperMock.Setup(s => s.Map<Sale>(command)).Returns(sale);
        _saleRepositoryMock.Setup(s => s.UpdateAsync(It.IsAny<Sale>(), It.IsAny<CancellationToken>())).ReturnsAsync(sale);
        
        // Act
        var result = await _handler.Handle(command, CancellationToken.None);
        
        // Assert
        result.Should().NotBeNull();
        _mapperMock.Verify(s => s.Map<Sale>(command), Times.Once);
        _saleRepositoryMock.Verify(s => s.UpdateAsync(sale, It.IsAny<CancellationToken>()), Times.Once);
    }
    
    // TODO: Verify if logging is correct
    [Fact]
    public async Task Handle_ThrowsValidationException_WhenSaleIsInvalid()
    {
        // Arrange
        var data = new Fixture()
            .Build<SaleTransport>()
            .Without(s => s.Date)
            .Create();
        var command = new Fixture()
            .Build<UpdateSaleCommand>()
            .Without(s => s.Data)
            .Create();
        var sale = new Fixture()
            .Build<Sale>()
            .Without(s => s.Date)
            .Create();
        
        _mapperMock.Setup(s => s.Map<Sale>(command)).Returns(sale);
        _saleRepositoryMock.Setup(s => s.UpdateAsync(It.IsAny<Sale>(), It.IsAny<CancellationToken>())).ReturnsAsync(sale);
        
        // Act
        await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));

        // Assert
        _saleRepositoryMock.Verify(r => r.CreateAsync(It.IsAny<Sale>(), It.IsAny<CancellationToken>()), Times.Never);
        _mediatorMock.Verify(m => m.Publish(It.IsAny<UpdateSaleNotification>(), It.IsAny<CancellationToken>()), Times.Never);
    }
}