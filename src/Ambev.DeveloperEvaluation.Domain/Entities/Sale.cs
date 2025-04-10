using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Domain.Common;
using Ambev.DeveloperEvaluation.Domain.Enums;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Validation;
using MediatR;

namespace Ambev.DeveloperEvaluation.Domain.Entities
{
    public class Sale : BaseEntity
    {
        private IMediator _mediator;
        public string SaleNumber { get; private set; }
        public DateTime SaleDate { get; private set; }
        public Guid CustomerId { get; private set; }
        public Guid BranchId { get; private set; }
        public decimal TotalValue { get; private set; }
        public SaleStatus Status { get; private set; }
        public Customer Customer { get; private set; }
        public Branch Branch { get; private set; }
        public List<SaleItem> Items { get; private set; } = new();
        public DateTime CreatedAt { get; private set; }

        public DateTime? UpdatedAt { get; private set; }

        public Sale() { }  

        public Sale(string saleNumber, DateTime saleDate, Guid customerId, Guid branchId, IMediator mediator)
        {
            SaleNumber = saleNumber;
            SaleDate = saleDate;
            CustomerId = customerId;
            BranchId = branchId;
            Status = SaleStatus.Pending;
            CreatedAt = DateTime.UtcNow;
            _mediator = mediator;

            _mediator.Publish(new SaleCreatedEvent(SaleNumber, SaleDate));
        }

        public void SetMediator(IMediator mediator)
        {
            _mediator = mediator;
        }

        public void AddItem(SaleItem item)
        {
            if (Status != SaleStatus.Pending)
                throw new InvalidOperationException("Não é possível modificar uma venda que não está pendente.");

            Items.Add(item);
            ApplyDiscounts();
            CalculateTotal();
            UpdatedAt = DateTime.UtcNow;

            _mediator.Publish(new SaleModifiedEvent(SaleNumber, UpdatedAt.Value));
        }

        public void RemoveItem(SaleItem item)
        {
            if (Status != SaleStatus.Pending)
                throw new InvalidOperationException("Não é possível modificar uma venda que não está pendente.");

            Items.Remove(item);
            ApplyDiscounts();
            CalculateTotal();
            UpdatedAt = DateTime.UtcNow;

            _mediator.Publish(new ItemCancelledEvent(SaleNumber, item.ProductId));
        }

        private void ApplyDiscounts()
        {
            int totalQuantity = Items.Sum(i => i.Quantity);
            decimal discount = 0;

            if (totalQuantity >= 4 && totalQuantity < 10)
                discount = 0.10m;
            else if (totalQuantity >= 10 && totalQuantity <= 20)
                discount = 0.20m;
            else if (totalQuantity > 20)
                throw new InvalidOperationException("Não é permitido vender mais de 20 itens.");

            foreach (var item in Items)
                item.ApplyDiscount(discount);
        }

        private void CalculateTotal()
        {
            TotalValue = Items.Sum(i => i.TotalPrice);
        }

        public void Complete()
        {
            if (Status != SaleStatus.Pending)
                throw new InvalidOperationException("A venda precisa estar pendente para ser concluída.");

            Status = SaleStatus.Completed;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Cancel()
        {
            if (Status == SaleStatus.Completed)
                throw new InvalidOperationException("Não é possível cancelar uma venda já concluída.");

            Status = SaleStatus.Canceled;
            UpdatedAt = DateTime.UtcNow;

            _mediator.Publish(new SaleCancelledEvent(SaleNumber));
        }

        public void Process()
        {
            if (Status != SaleStatus.Pending)
                throw new InvalidOperationException("A venda precisa estar pendente para ser processada.");

            Status = SaleStatus.Processing;
            UpdatedAt = DateTime.UtcNow;
        }

        public void Refund()
        {
            if (Status != SaleStatus.Completed)
                throw new InvalidOperationException("A venda só pode ser reembolsada se estiver concluída.");

            Status = SaleStatus.Refunded;
            UpdatedAt = DateTime.UtcNow;
        }

        public void UpdateItem(Guid itemId, int newQuantity, decimal newUnitPrice)
        {
            if (Status != SaleStatus.Pending)
                throw new InvalidOperationException("Não é possível modificar uma venda que não está pendente.");

            var item = Items.FirstOrDefault(i => i.Id == itemId);
            if (item == null)
                throw new InvalidOperationException("Item não encontrado na venda.");

            item.Update(newQuantity, newUnitPrice);

            ApplyDiscounts();
            CalculateTotal();
            UpdatedAt = DateTime.UtcNow;

            _mediator.Publish(new SaleModifiedEvent(SaleNumber, UpdatedAt.Value));
        }

        public ValidationResultDetail Validate()
        {
            var validator = new SaleValidator();
            var result = validator.Validate(this);
            return new ValidationResultDetail
            {
                IsValid = result.IsValid,
                Errors = result.Errors.Select(o => (ValidationErrorDetail)o)
            };
        }
    }
}
