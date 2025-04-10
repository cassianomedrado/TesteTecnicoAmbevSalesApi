using Ambev.DeveloperEvaluation.Application.Sales.Commands.CreateSale;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSale;
using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.Application.Sales.Querys.GetAllSalesQuery;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetAll;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleById;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateItems;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.UpdateSaleItems;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.RemoverItem;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.DeleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.CompleteSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.ProcessSale;
using Ambev.DeveloperEvaluation.Application.Sales.Commands.RefundSale;
using Ambev.DeveloperEvaluation.Application.Sales.Querys.GetSaleByIdQuery;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales
{
    /// <summary>
    /// Controller responsável por gerenciar as vendas do sistema.
    /// Fornece endpoints para criação, consulta, atualização de itens, cancelamento, processamento, conclusão, reembolso e remoção de vendas.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class SalesController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor da controller de vendas.
        /// </summary>
        /// <param name="mediator">Instância do MediatR para envio de comandos e queries.</param>
        /// <param name="mapper">Instância do AutoMapper para conversão de DTOs.</param>
        public SalesController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Cria uma nova venda.
        /// </summary>
        /// <param name="request">Dados da venda.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Venda criada com sucesso.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> CreateSale([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateSaleValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(validationResult.Errors);

            var command = _mapper.Map<CreateSaleCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CreateSaleResponse>
            {
                Success = true,
                Message = "Venda criada com sucesso.",
                Data = _mapper.Map<CreateSaleResponse>(result)
            });
        }

        /// <summary>
        /// Retorna todas as vendas cadastradas.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Lista de vendas.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseWithData<List<GetAllSalesResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var query = _mapper.Map<GetAllSalesQuery>(new GetAll.GetAllSalesRequest());

            var result = await _mediator.Send(query, cancellationToken);

            return Ok(new ApiResponseWithData<List<GetAllSalesResponse>>
            {
                Success = true,
                Message = "Vendas obtidas com sucesso.",
                Data = result.Select(_mapper.Map<GetAllSalesResponse>).ToList()
            });
        }

        /// <summary>
        /// Retorna os detalhes de uma venda pelo ID.
        /// </summary>
        /// <param name="id">ID da venda.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Venda encontrada ou erro se não existir.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetSaleByIdResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var query = new GetSaleByIdRequest(id);
            var validator = new GetSaleByIdRequestValidator();
            var validationResult = await validator.ValidateAsync(query, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Erro de validação.",
                    Errors = validationResult.Errors.Select(e => (ValidationErrorDetail)e).ToList()
                });

            var result = await _mediator.Send(_mapper.Map<GetSaleByIdQuery>(query), cancellationToken);
            if (result == null)
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Venda não encontrada."
                });

            return Ok(new ApiResponseWithData<GetSaleByIdResponse>
            {
                Success = true,
                Message = "Venda encontrada com sucesso.",
                Data = _mapper.Map<GetSaleByIdResponse>(result)
            });
        }

        /// <summary>
        /// Atualiza os itens de uma venda existente.
        /// </summary>
        /// <param name="id">ID da venda.</param>
        /// <param name="request">Itens a serem atualizados.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Mensagem de sucesso ou erro.</returns>
        [HttpPut("{id}/items")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> UpdateItems([FromRoute] Guid id, [FromBody] UpdateSaleItemsRequest request, CancellationToken cancellationToken)
        {
            var validator = new UpdateSaleItemsRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Erro de validação.",
                    Errors = validationResult.Errors.Select(e => (ValidationErrorDetail)e).ToList()
                });

            var command = _mapper.Map<UpdateSaleItemsCommand>(request);
            command.SaleId = id;

            var result = await _mediator.Send(command, cancellationToken);

            if (!result.Success)
            {
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = result.Message
                });
            }

            return Ok(new ApiResponse
            {
                Success = result.Success,
                Message = result.Message
            });
        }

        /// <summary>
        /// Cancela uma venda existente.
        /// </summary>
        /// <param name="id">ID da venda.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Mensagem de sucesso ou erro.</returns>
        [HttpPut("{id}/cancel")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Cancel([FromRoute] Guid id, CancellationToken cancellationToken)
        {
            var command = new CancelSaleCommand(id);
            var result = await _mediator.Send(command, cancellationToken);

            if (!result.Success)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = result.Message
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Message = result.Message
            });
        }

        /// <summary>
        /// Remove um item de uma venda específica.
        /// </summary>
        /// <param name="saleId">ID da venda.</param>
        /// <param name="itemId">ID do item a ser removido.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Mensagem de sucesso ou erro.</returns>
        [HttpDelete("{saleId}/items/{itemId}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> RemoveItem(Guid saleId, Guid itemId, CancellationToken cancellationToken)
        {
            var command = new RemoveItemFromSaleCommand(saleId, itemId);
            var result = await _mediator.Send(command, cancellationToken);

            if (!result.Success)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = result.Message
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Message = result.Message
            });
        }

        /// <summary>
        /// Exclui uma venda existente.
        /// </summary>
        /// <param name="id">ID da venda.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Mensagem de sucesso ou erro.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var command = new DeleteSaleCommand(id);
            var result = await _mediator.Send(command, cancellationToken);

            if (!result.Success)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = result.Message
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Message = result.Message
            });
        }

        /// <summary>
        /// Finaliza uma venda, marcando-a como concluída.
        /// </summary>
        /// <param name="id">ID da venda.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Mensagem de sucesso ou erro.</returns>
        [HttpPut("{id}/complete")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Complete(Guid id, CancellationToken cancellationToken)
        {
            var command = new CompleteSaleCommand(id);
            var result = await _mediator.Send(command, cancellationToken);

            if (!result.Success)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = result.Message
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Message = result.Message
            });
        }

        /// <summary>
        /// Processa uma venda, alterando seu status para "Processando".
        /// </summary>
        /// <param name="id">ID da venda.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Mensagem de sucesso ou erro.</returns>
        [HttpPut("{id}/process")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Process(Guid id, CancellationToken cancellationToken)
        {
            var command = new ProcessSaleCommand(id);
            var result = await _mediator.Send(command, cancellationToken);

            if (!result.Success)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = result.Message
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Message = result.Message
            });
        }

        /// <summary>
        /// Reembolsa uma venda já concluída.
        /// </summary>
        /// <param name="id">ID da venda.</param>
        /// <param name="cancellationToken">Token de cancelamento.</param>
        /// <returns>Mensagem de sucesso ou erro.</returns>
        [HttpPut("{id}/refund")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Refund(Guid id, CancellationToken cancellationToken)
        {
            var command = new RefundSaleCommand(id);
            var result = await _mediator.Send(command, cancellationToken);

            if (!result.Success)
            {
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = result.Message
                });
            }

            return Ok(new ApiResponse
            {
                Success = true,
                Message = result.Message
            });
        }
    }
}
