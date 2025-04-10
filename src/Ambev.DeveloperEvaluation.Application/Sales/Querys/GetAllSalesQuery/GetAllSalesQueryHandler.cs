using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Sales.Querys.GetAllSalesQuery
{
    public class GetAllSalesQueryHandler : IRequestHandler<GetAllSalesQuery, List<GetAllSalesResult>>
    {
        private readonly ISaleRepository _saleRepository;
        private readonly IMapper _mapper;

        public GetAllSalesQueryHandler(ISaleRepository saleRepository, IMapper mapper)
        {
            _saleRepository = saleRepository;
            _mapper = mapper;
        }

        public async Task<List<GetAllSalesResult>> Handle(GetAllSalesQuery request, CancellationToken cancellationToken)
        {
            var sales = await _saleRepository.GetAllAsync(cancellationToken);
            return sales.Select(_mapper.Map<GetAllSalesResult>).ToList();
        }
    }
}
