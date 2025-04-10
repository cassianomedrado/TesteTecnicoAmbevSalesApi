using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.Application.Customers.Commands.CreateCustomer;
using Ambev.DeveloperEvaluation.WebApi.Features.Customers.CreateCustomer;
using Ambev.DeveloperEvaluation.Application.Customers.Querys.GetAllCustomers;
using Ambev.DeveloperEvaluation.Application.Customers.Querys.GetById;
using Ambev.DeveloperEvaluation.WebApi.Features.Customers.GetById;
using Ambev.DeveloperEvaluation.WebApi.Features.Customers.UpdateCustomer;
using Ambev.DeveloperEvaluation.Application.Customers.Commands.UpdateCustomer;
using Ambev.DeveloperEvaluation.Application.Customers.Commands.DeleteCustomer;
using Ambev.DeveloperEvaluation.WebApi.Features.Customers.GetAllCustomers;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Customers
{
    /// <summary>
    /// Controller responsável por gerenciar os clientes do sistema.
    /// Fornece endpoints para criação, consulta, atualização e exclusão de clientes.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class CustomersController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor da controller de clientes.
        /// </summary>
        /// <param name="mediator">Instância do MediatR para envio de comandos e queries.</param>
        /// <param name="mapper">Instância do AutoMapper para conversão de DTOs.</param>
        public CustomersController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Cria um novo cliente.
        /// </summary>
        /// <param name="request">Dados do cliente a ser criado.</param>
        /// <param name="cancellationToken">Token de cancelamento da requisição.</param>
        /// <returns>Retorna o cliente criado.</returns>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateCustomerResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateCustomerRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateCustomerValidator();
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

            var command = _mapper.Map<CreateCustomerCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CreateCustomerResponse>
            {
                Success = true,
                Message = "Cliente criado com sucesso.",
                Data = _mapper.Map<CreateCustomerResponse>(result)
            });
        }

        /// <summary>
        /// Retorna todos os clientes cadastrados.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento da requisição.</param>
        /// <returns>Lista de clientes.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseWithData<List<GetAllCustomersResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllCustomersQuery(), cancellationToken);
            return Ok(new ApiResponseWithData<List<GetAllCustomersResponse>>
            {
                Success = true,
                Message = "Clientes obtidos com sucesso.",
                Data = result.Select(_mapper.Map<GetAllCustomersResponse>).ToList()
            });
        }

        /// <summary>
        /// Retorna os dados de um cliente pelo seu ID.
        /// </summary>
        /// <param name="id">ID do cliente.</param>
        /// <param name="cancellationToken">Token de cancelamento da requisição.</param>
        /// <returns>Dados do cliente encontrado ou erro se não existir.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetCustomerByIdResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetCustomerByIdRequest(id);
            var validator = new GetCustomerByIdRequestValidator();
            var validationResult = await validator.ValidateAsync(query, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Erro de validação.",
                    Errors = validationResult.Errors.Select(e => (ValidationErrorDetail)e).ToList()
                });

            var result = await _mediator.Send(new GetCustomerByIdQuery(id), cancellationToken);
            if (result == null)
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Cliente não encontrada."
                });

            return Ok(new ApiResponseWithData<GetCustomerByIdResponse>
            {
                Success = true,
                Message = "Cliente encontrado com sucesso.",
                Data = _mapper.Map<GetCustomerByIdResponse>(result)
            });
        }

        /// <summary>
        /// Atualiza os dados de um cliente existente.
        /// </summary>
        /// <param name="id">ID do cliente a ser atualizado.</param>
        /// <param name="request">Dados de atualização.</param>
        /// <param name="cancellationToken">Token de cancelamento da requisição.</param>
        /// <returns>Mensagem de sucesso ou erro.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateCustomerRequest request, CancellationToken cancellationToken)
        {
            var validator = new UpdateCustomerRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Erro de validação.",
                    Errors = validationResult.Errors.Select(e => (ValidationErrorDetail)e).ToList()
                });

            var command = _mapper.Map<UpdateCustomerCommand>(request);
            command.Id = id;

            var result = await _mediator.Send(command, cancellationToken);
            if (!result.Success)
                return NotFound(new ApiResponse { Success = false, Message = result.Message });

            return Ok(new ApiResponse { Success = true, Message = result.Message });
        }

        /// <summary>
        /// Remove um cliente pelo seu ID.
        /// </summary>
        /// <param name="id">ID do cliente a ser removido.</param>
        /// <param name="cancellationToken">Token de cancelamento da requisição.</param>
        /// <returns>Mensagem de sucesso ou erro.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteCustomerCommand(id), cancellationToken);
            if (!result.Success)
                return NotFound(new ApiResponse { Success = false, Message = result.Message });

            return Ok(new ApiResponse { Success = true, Message = result.Message });
        }
    }
}
