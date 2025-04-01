using Ambev.DeveloperEvaluation.Application.Sales.Transport;
using Ambev.DeveloperEvaluation.Domain.Entities;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.Queries.GetSale;

public class GetSaleProfile : Profile
{
    public GetSaleProfile()
    {
        CreateMap<SaleTransport, Sale>();
        CreateMap<Sale, SaleTransport>();
        CreateMap<SaleItemTransport, SaleItem>();
        CreateMap<SaleItem, SaleItemTransport>();
    }
}