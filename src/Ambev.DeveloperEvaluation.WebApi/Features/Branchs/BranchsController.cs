using AutoMapper;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.Common.Validation;
using Ambev.DeveloperEvaluation.WebApi.Features.Branchs.CreateBranch;
using Ambev.DeveloperEvaluation.Application.Branchs.Commands.CreateBranch;
using Ambev.DeveloperEvaluation.WebApi.Features.Branchs.GetAllBranchs;
using Ambev.DeveloperEvaluation.Application.Branchs.Querys.GetAllBranchs;
using Ambev.DeveloperEvaluation.WebApi.Features.Branchs.GetById;
using Ambev.DeveloperEvaluation.Application.Branchs.Querys.GetById;
using Ambev.DeveloperEvaluation.WebApi.Features.Branchs.UpdateBranch;
using Ambev.DeveloperEvaluation.Application.Branchs.Commands.UpdateBranch;
using Ambev.DeveloperEvaluation.Application.Branchs.Commands.DeleteBranch;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Branchs
{
    /// <summary>
    /// Controller responsável por gerenciar as filiais (branches) do sistema.
    /// Fornece endpoints para criação, listagem, consulta, atualização e exclusão de filiais.
    /// </summary>
    [ApiController]
    [Route("api/[controller]")]
    public class BranchsController : ControllerBase
    {
        private readonly IMediator _mediator;
        private readonly IMapper _mapper;

        /// <summary>
        /// Construtor da controller de filiais.
        /// </summary>
        /// <param name="mediator">Instância do MediatR para envio de comandos e queries.</param>
        /// <param name="mapper">Instância do AutoMapper para conversão de DTOs.</param>
        public BranchsController(IMediator mediator, IMapper mapper)
        {
            _mediator = mediator;
            _mapper = mapper;
        }

        /// <summary>
        /// Cria uma nova filial.
        /// </summary>
        /// <param name="request">Dados da filial a ser criada.</param>
        /// <param name="cancellationToken">Token de cancelamento da requisição.</param>
        [HttpPost]
        [ProducesResponseType(typeof(ApiResponseWithData<CreateBranchResponse>), StatusCodes.Status201Created)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Create([FromBody] CreateBranchRequest request, CancellationToken cancellationToken)
        {
            var validator = new CreateBranchRequestValidator();
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

            var command = _mapper.Map<CreateBranchCommand>(request);
            var result = await _mediator.Send(command, cancellationToken);

            return Created(string.Empty, new ApiResponseWithData<CreateBranchResponse>
            {
                Success = true,
                Message = "Filial criada com sucesso.",
                Data = _mapper.Map<CreateBranchResponse>(result)
            });
        }

        /// <summary>
        /// Retorna a lista de todas as filiais cadastradas.
        /// </summary>
        /// <param name="cancellationToken">Token de cancelamento da requisição.</param>
        /// <returns>Lista de filiais.</returns>
        [HttpGet]
        [ProducesResponseType(typeof(ApiResponseWithData<List<GetAllBranchsResponse>>), StatusCodes.Status200OK)]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new GetAllBranchesQuery(), cancellationToken);
            return Ok(new ApiResponseWithData<List<GetAllBranchsResponse>>
            {
                Success = true,
                Message = "Filiais obtidas com sucesso.",
                Data = result.Select(_mapper.Map<GetAllBranchsResponse>).ToList()
            });
        }

        /// <summary>
        /// Busca uma filial pelo ID informado.
        /// </summary>
        /// <param name="id">ID da filial.</param>
        /// <param name="cancellationToken">Token de cancelamento da requisição.</param>
        /// <returns>Retorna a filial encontrada ou erro caso não exista.</returns>
        [HttpGet("{id}")]
        [ProducesResponseType(typeof(ApiResponseWithData<GetBranchByIdResponse>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken)
        {
            var query = new GetBranchByIdRequest(id);
            var validator = new GetBranchByIdRequestValidator();
            var validationResult = await validator.ValidateAsync(query, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Erro de validação.",
                    Errors = validationResult.Errors.Select(e => (ValidationErrorDetail)e).ToList()
                });

            var result = await _mediator.Send(new GetBranchByIdQuery(id), cancellationToken);
            if (result == null)
                return NotFound(new ApiResponse
                {
                    Success = false,
                    Message = "Filial não encontrada."
                });

            return Ok(new ApiResponseWithData<GetBranchByIdResponse>
            {
                Success = true,
                Message = "Filial encontrada com sucesso.",
                Data = _mapper.Map<GetBranchByIdResponse>(result)
            });
        }

        /// <summary>
        /// Atualiza os dados de uma filial existente.
        /// </summary>
        /// <param name="id">ID da filial.</param>
        /// <param name="request">Campos a serem atualizados.</param>
        /// <param name="cancellationToken">Token de cancelamento da requisição.</param>
        /// <returns>Mensagem de sucesso ou erro.</returns>
        [HttpPut("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(Guid id, [FromBody] UpdateBranchRequest request, CancellationToken cancellationToken)
        {
            var validator = new UpdateBranchRequestValidator();
            var validationResult = await validator.ValidateAsync(request, cancellationToken);

            if (!validationResult.IsValid)
                return BadRequest(new ApiResponse
                {
                    Success = false,
                    Message = "Erro de validação.",
                    Errors = validationResult.Errors.Select(e => (ValidationErrorDetail)e).ToList()
                });

            var command = _mapper.Map<UpdateBranchCommand>(request);
            command.Id = id;

            var result = await _mediator.Send(command, cancellationToken);
            if (!result.Success)
                return NotFound(new ApiResponse { Success = false, Message = result.Message });

            return Ok(new ApiResponse { Success = true, Message = result.Message });
        }

        /// <summary>
        /// Remove uma filial existente pelo ID informado.
        /// </summary>
        /// <param name="id">ID da filial.</param>
        /// <param name="cancellationToken">Token de cancelamento da requisição.</param>
        /// <returns>Mensagem de sucesso ou erro.</returns>
        [HttpDelete("{id}")]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken)
        {
            var result = await _mediator.Send(new DeleteBranchCommand(id), cancellationToken);
            if (!result.Success)
                return NotFound(new ApiResponse { Success = false, Message = result.Message });

            return Ok(new ApiResponse { Success = true, Message = result.Message });
        }
    }
}
