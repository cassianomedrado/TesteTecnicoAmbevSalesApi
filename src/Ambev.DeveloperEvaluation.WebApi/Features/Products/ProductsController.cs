using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Products.Commands.CreateProduct;
using Ambev.DeveloperEvaluation.Application.Customers.Querys.GetAllCustomers;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetById;
using Ambev.DeveloperEvaluation.Application.Products.Querys.GetById;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.UpdateProduct;
using Ambev.DeveloperEvaluation.Application.Products.Commands.UpdateProduct;
using Ambev.DeveloperEvaluation.Application.Customers.Commands.DeleteCustomer;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.GetAllProducts;
using Ambev.DeveloperEvaluation.Application.Products.Querys.GetAllProducts;
using Ambev.DeveloperEvaluation.Application.Products.Commands.DeleteProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products
{
    /// <summary>
    /// Controller responsável por gerenciar os produtos do sistema.
    /// Fornece endpoints para criação, consulta, atualização e exclusão de produtos.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor da controller de produtos.
        /// </summary>
        /// <param name="mediator">Instância do MediatR para envio de comandos e queries.</param>
        /// <param name="mapper">Instância do AutoMapper para conversão de DTOs.</param>
        public ProductsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Cria um novo produto.
        /// </summary>
        /// <param name="request">Dados do produto a ser criado.</param>
        /// <param name="cancellationToken">Token de cancelamento da requisição.</param>
        /// <returns>Retorna o produto criado.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateProductResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateProductRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Erro de validação.",
                    Errors = validationResult.Errors.Select(e => (ValidationErrorDetail)e).ToList()
                });
            }

            var command = _mapper.Map<CreateProductCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CreateProductResponse>
            {
                Success = true,
                Message = "Produto criado com sucesso.",
                Data = _mapper.Map<CreateProductResponse>(result)
            });
        }

        /// <summary>
        /// Retorna todos os produtos cadastrados.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento da requisição.</param>
        /// <returns>Lista de produtos.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseWithData<List<GetAllProductsResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllProductsQuery(), cancellationToken);
            return Ok(new ApiResponseWithData<List<GetAllProductsResponse>>
            {
                Success = true,
                Message = "Produtos obtidos com sucesso.",
                Data = result.Select(_mapper.Map<GetAllProductsResponse>).ToList()
            });
        }

        /// <summary>
        /// Retorna os dados de um produto pelo seu ID.
        /// </summary>
        /// <param name="id">ID do produto.</param>
        /// <param name="cancellationToken">Token de cancelamento da requisição.</param>
        /// <returns>Dados do produto encontrado ou erro se não existir.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetProductByIdResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetProductByIdRequest(id);
            var validator = new GetProductByIdRequestValidator();
            var validationResult = await validator.ValidateAsync(query, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Erro de validação.",
                    Errors = validationResult.Errors.Select(e => (ValidationErrorDetail)e).ToList()
                });

            var result = await _mediator.Send(new GetProductByIdQuery(id), cancellationToken);
            if (result == null)
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Produto não encontrada."
                });

            return Ok(new ApiResponseWithData<GetProductByIdResponse>
            {
                Success = true,
                Message = "Produto encontrado com sucesso.",
                Data = _mapper.Map<GetProductByIdResponse>(result)
            });
        }

        /// <summary>
        /// Atualiza os dados de um produto existente.
        /// </summary>
        /// <param name="id">ID do produto a ser atualizado.</param>
        /// <param name="request">Dados de atualização do produto.</param>
        /// <param name="cancellationToken">Token de cancelamento da requisição.</param>
        /// <returns>Mensagem de sucesso ou erro.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateProductRequest request, CancellationToken cancellationToken)
        {
            var validator = new UpdateProductRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Erro de validação.",
                    Errors = validationResult.Errors.Select(e => (ValidationErrorDetail)e).ToList()
                });

            var command = _mapper.Map<UpdateProductCommand>(request);
            command.Id = id;

            var result = await _mediator.Send(command, cancellationToken);
            if (!result.Success)
                return NotFound(new ApiResponse { Success = false, Message = result.Message });

            return Ok(new ApiResponse { Success = true, Message = result.Message });
        }

        /// <summary>
        /// Remove um produto pelo seu ID.
        /// </summary>
        /// <param name="id">ID do produto a ser removido.</param>
        /// <param name="cancellationToken">Token de cancelamento da requisição.</param>
        /// <returns>Mensagem de sucesso ou erro.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteProductCommand(id), cancellationToken);
            if (!result.Success)
                return NotFound(new ApiResponse { Success = false, Message = result.Message });

            return Ok(new ApiResponse { Success = true, Message = result.Message });
        }
    }
}
