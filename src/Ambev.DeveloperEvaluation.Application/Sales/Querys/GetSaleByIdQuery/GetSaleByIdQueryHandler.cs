using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Querys.GetSaleByIdQuery
{
    public class GetSaleByIdQueryHandler : IRequestHandler<GetSaleByIdQuery, GetSaleByIdResult>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public GetSaleByIdQueryHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<GetSaleByIdResult> Handle(GetSaleByIdQuery request, CancellationToken cancellationToken)
        {
            var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
            return _mapper.Map<GetSaleByIdResult>(sale);
        }
    }
}