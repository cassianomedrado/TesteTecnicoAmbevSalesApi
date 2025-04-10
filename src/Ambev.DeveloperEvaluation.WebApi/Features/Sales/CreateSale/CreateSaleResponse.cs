using System;
using System.Collections.Generic;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleResponse
    {
        public Guid SaleId { get; set; }
        public string SaleNumber { get; set; }
        public decimal TotalValue { get; set; }
        public DateTime SaleDate { get; set; }
    }
}