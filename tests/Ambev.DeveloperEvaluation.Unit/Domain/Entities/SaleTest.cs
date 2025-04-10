using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Unit.Domain.Entities.TestData;
using MediatR;
using Moq;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Domain.Entities;

public class SaleTests
{
    private readonly Mock<IMediator> _mediatorMock;

    public SaleTests()
    {
        _mediatorMock = new Mock<IMediator>();
    }

    [Fact(DisplayName = "Deve criar uma venda válida com itens e status Pendente")]
    public void Given_ValidSale_When_Created_Then_StatusShouldBePending()
    {
        var sale = SaleTestData.GenerateValidSale(_mediatorMock.Object);

        Assert.Equal(SaleStatus.Pending, sale.Status);
        Assert.NotEmpty(sale.Items);
        Assert.True(sale.TotalValue > 0);
    }

    [Fact(DisplayName = "Deve aplicar 10% de desconto para 4 a 9 itens")]
    public void Given_SaleWith5Items_When_Added_Then_DiscountShouldBe10Percent()
    {
        var sale = SaleTestData.GenerateValidSale(_mediatorMock.Object);

        sale.Items.Clear();
        SaleTestData.GenerateItemsWithDiscount(5)
            .ForEach(i => sale.AddItem(i));

        var totalSemDesconto = 5 * 100m;
        var esperado = totalSemDesconto * 0.90m;

        Assert.Equal(esperado, sale.TotalValue);
    }

    [Fact(DisplayName = "Deve aplicar 20% de desconto para 10 a 20 itens")]
    public void Given_SaleWith10Items_When_Added_Then_DiscountShouldBe20Percent()
    {
        var sale = SaleTestData.GenerateValidSale(_mediatorMock.Object);

        sale.Items.Clear();
        SaleTestData.GenerateItemsWithDiscount(10)
            .ForEach(i => sale.AddItem(i));

        var esperado = 10 * 100m * 0.80m;
        Assert.Equal(esperado, sale.TotalValue);
    }

    [Fact(DisplayName = "Deve lançar exceção ao adicionar mais de 20 itens")]
    public void Given_MoreThan20Items_When_Added_Then_ShouldThrowException()
    {
        var sale = SaleTestData.GenerateValidSale(_mediatorMock.Object);

        sale.Items.Clear();

        var exception = Assert.Throws<InvalidOperationException>(() =>
        {
            SaleTestData.GenerateItemsWithDiscount(21)
                .ForEach(i => sale.AddItem(i));
        });

        Assert.Equal("Não é permitido vender mais de 20 itens.", exception.Message);
    }

    [Fact(DisplayName = "Deve atualizar o item corretamente")]
    public void Given_ValidItem_When_Updated_Then_ShouldApplyChanges()
    {
        var sale = SaleTestData.GenerateValidSale(_mediatorMock.Object);
        var item = sale.Items.First();

        sale.UpdateItem(item.Id, 5, 200m);

        Assert.Equal(5, item.Quantity);
        Assert.Equal(200m, item.UnitPrice);
        Assert.True(item.TotalPrice > 0);
    }

    [Fact(DisplayName = "Deve mudar status para Concluída")]
    public void Given_Sale_When_Complete_Then_StatusShouldBeCompleted()
    {
        var sale = SaleTestData.GenerateValidSale(_mediatorMock.Object);

        sale.Complete();

        Assert.Equal(SaleStatus.Completed, sale.Status);
        Assert.NotNull(sale.UpdatedAt);
    }

    [Fact(DisplayName = "Deve cancelar a venda")]
    public void Given_Sale_When_Cancel_Then_StatusShouldBeCanceled()
    {
        var sale = SaleTestData.GenerateValidSale(_mediatorMock.Object);

        sale.Cancel();

        Assert.Equal(SaleStatus.Canceled, sale.Status);
        Assert.NotNull(sale.UpdatedAt);
    }

    [Fact(DisplayName = "Não deve cancelar uma venda já concluída")]
    public void Given_CompletedSale_When_Cancel_Then_ShouldThrowException()
    {
        var sale = SaleTestData.GenerateValidSale(_mediatorMock.Object);
        sale.Complete();

        var ex = Assert.Throws<InvalidOperationException>(() => sale.Cancel());
        Assert.Equal("Não é possível cancelar uma venda já concluída.", ex.Message);
    }

    [Fact(DisplayName = "Deve processar uma venda pendente")]
    public void Given_PendingSale_When_Process_Then_StatusShouldBeProcessing()
    {
        var sale = SaleTestData.GenerateValidSale(_mediatorMock.Object);

        sale.Process();

        Assert.Equal(SaleStatus.Processing, sale.Status);
        Assert.NotNull(sale.UpdatedAt);
    }

    [Fact(DisplayName = "Deve reembolsar uma venda concluída")]
    public void Given_CompletedSale_When_Refund_Then_StatusShouldBeRefunded()
    {
        var sale = SaleTestData.GenerateValidSale(_mediatorMock.Object);
        sale.Complete();

        sale.Refund();

        Assert.Equal(SaleStatus.Refunded, sale.Status);
    }

    [Fact(DisplayName = "Validação deve retornar válida para venda correta")]
    public void Given_ValidSale_When_Validate_Then_ShouldReturnValid()
    {
        var sale = SaleTestData.GenerateValidSale(_mediatorMock.Object);
        var result = sale.Validate();

        Assert.True(result.IsValid);
    }
}
