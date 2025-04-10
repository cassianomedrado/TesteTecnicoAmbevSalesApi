using Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSaleItems;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateItems
{
    public class UpdateSaleItemsProfile: Profile
    {
        public UpdateSaleItemsProfile()
        {
            CreateMap<UpdateSaleItemsRequest, UpdateSaleItemsCommand>();
            CreateMap<UpdateSaleItemRequest, UpdateSaleItemCommand>();                
        }
    }
}
