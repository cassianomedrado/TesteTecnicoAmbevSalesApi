using System.ComponentModel.DataAnnotations;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale
{
    public class CreateSaleRequest
    {
        [Required]
        public string SaleNumber { get; set; }

        [Required]
        public DateTime SaleDate { get; set; }

        [Required]
        public Guid CustomerId { get; set; }

        [Required]
        public Guid BranchId { get; set; }

        public List<CreateSaleItemRequest> Items { get; set; } = new();
    }

    public class CreateSaleItemRequest
    {
        [Required]
        public Guid ProductId { get; set; }

        [Required]
        public int Quantity { get; set; }

        [Required]
        public decimal UnitPrice { get; set; }
    }

}
