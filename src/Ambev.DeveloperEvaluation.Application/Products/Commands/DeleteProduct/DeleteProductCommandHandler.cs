using Ambev.DeveloperEvaluation.Domain.Repositories;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.Commands.DeleteProduct
{
    public class DeleteProductCommandHandler : IRequestHandler<DeleteProductCommand, DeleteProductResult>
    {
        private readonly IProductRepository _repository;

        public DeleteProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<DeleteProductResult> Handle(DeleteProductCommand command, CancellationToken cancellationToken)
        {
            try
            {
                var deleted = await _repository.DeleteAsync(command.Id, cancellationToken);

                return new DeleteProductResult
                {
                    Success = deleted,
                    Message = deleted ? "Produto excluído com sucesso." : "Produto não encontrado."
                };
            }
            catch (Exception ex)
            {
                throw new ArgumentException("Erro ao deletar produto: " + ex.Message);
            }
        }
    }
}
