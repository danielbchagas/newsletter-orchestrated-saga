using Ambev.DeveloperEvaluation.Application.Sales.Transport;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;

public class CreateSaleProfile : Profile
{
    public CreateSaleProfile()
    {
        CreateMap<SaleTransport, Sale>();
        CreateMap<Sale, SaleTransport>();
        CreateMap<SaleItemTransport, SaleItem>();
        CreateMap<SaleItem, SaleItemTransport>();
    }
}