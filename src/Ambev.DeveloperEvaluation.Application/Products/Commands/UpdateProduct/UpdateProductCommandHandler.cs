using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Commands.UpdateProduct
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, UpdateProductResult>
    {
        private readonly IProductRepository _repository;

        public UpdateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<UpdateProductResult> Handle(UpdateProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _repository.GetByIdAsync(command.Id, cancellationToken);
                if (product == null)
                {
                    return new UpdateProductResult
                    {
                        Success = false,
                        Message = "Produto não encontrado."
                    };
                }

                product.Update(command.Name, command.Description, command.Price);
                await _repository.UpdateAsync(product, cancellationToken);

                return new UpdateProductResult
                {
                    Success = true,
                    Message = "Produto atualizado com sucesso."
                };

            }
            catch (Exception ex)
            {
                return new UpdateProductResult
                {
                    Success = false,
                    Message = ex.Message
                };
            }
        }
    }
}
