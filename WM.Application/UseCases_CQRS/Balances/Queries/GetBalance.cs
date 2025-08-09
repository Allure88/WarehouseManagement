using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;

namespace WM.Application.UseCases_CQRS.Balances.Queries;
public class GetBalanceBodiesListRequest : IRequest<GetBalanceBodiesListResponse> { }

public class GetBalanceBodiesListResponse(List<BalanceBody> Balances)
{
    public List<BalanceBody> Balances { get; } = Balances;
}

public class GetPresetnBodiesListRequestHandler(IBalanceRepository repository, IMapper mapper) : IRequestHandler<GetBalanceBodiesListRequest, GetBalanceBodiesListResponse>
{
    public async Task<GetBalanceBodiesListResponse> Handle(GetBalanceBodiesListRequest request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAllWithDependencies();

        List<BalanceBody> balances = [.. entities.Select(mapper.Map<BalanceBody>)];

        return new GetBalanceBodiesListResponse(balances);
    }
}