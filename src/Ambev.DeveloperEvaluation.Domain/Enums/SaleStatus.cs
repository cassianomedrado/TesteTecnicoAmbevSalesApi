namespace Ambev.DeveloperEvaluation.Domain.Enums;

/// <summary>
/// Define os possíveis status de uma venda.
/// </summary>
public enum SaleStatus
{
    Pending = 0,     // Venda criada, aguardando pagamento ou confirmação.
    Completed = 1,   // Venda finalizada com sucesso.
    Canceled = 2,    // Venda cancelada antes da conclusão.
    Refunded = 3,    // Venda reembolsada após ser concluída.
    Processing = 4   // Venda em processo de pagamento ou envio.
}
