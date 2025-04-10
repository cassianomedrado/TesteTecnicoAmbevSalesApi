using Ambev.DeveloperEvaluation.Domain.Entities;
using MediatR;
using Bogus;
using Moq;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData
{
    /// <summary>
    /// Fornece métodos para gerar dados de teste para a entidade Sale e seus itens.
    /// </summary>
    public static class SaleTestData
    {
        private static readonly Faker Faker = new();

        public static Sale GenerateValidSale(IMediator mediator = null)
        {
            mediator ??= new Mock<IMediator>().Object;

            var sale = new Sale(
                saleNumber: $"SALE-{Faker.Random.Int(1000, 9999)}",
                saleDate: DateTime.UtcNow,
                customerId: Guid.NewGuid(),
                branchId: Guid.NewGuid(),
                mediator: mediator
            );

            // Adiciona de 1 a 3 itens válidos
            for (int i = 0; i < Faker.Random.Int(1, 3); i++)
            {
                sale.AddItem(GenerateValidSaleItem());
            }

            return sale;
        }

        public static SaleItem GenerateValidSaleItem()
        {
            return new SaleItem(
                productId: Guid.NewGuid(),
                quantity: Faker.Random.Int(1, 5),
                unitPrice: Faker.Random.Decimal(10m, 500m)
            );
        }

        public static SaleItem GenerateLargeQuantityItem()
        {
            return new SaleItem(
                productId: Guid.NewGuid(),
                quantity: 21, // quebra regra de negócio (>20)
                unitPrice: 100
            );
        }

        public static List<SaleItem> GenerateItemsWithDiscount(int quantity)
        {
            return Enumerable.Range(1, quantity).Select(_ => new SaleItem(
                productId: Guid.NewGuid(),
                quantity: 1,
                unitPrice: 100
            )).ToList();
        }
    }
}
