using Ambev.DeveloperEvaluation.Application.Sales.Transport;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSale;

public class UpdateSaleProfile : Profile
{
    public UpdateSaleProfile()
    {
        CreateMap<SaleTransport, Sale>();
        CreateMap<Sale, SaleTransport>();
        CreateMap<SaleItemTransport, SaleItem>();
        CreateMap<SaleItem, SaleItemTransport>();
    }
}