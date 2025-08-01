using MediatR;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Balances.Queries;
public class GetBalanceBodiesListRequest : IRequest<GetBalanceBodiesListResponse> { }

public class GetBalanceBodiesListResponse(List<BalanceEntity> Balances)
{
    public List<BalanceEntity> Balances { get; } = Balances;
}

public class GetPresetnBodiesListRequestHandler(IBalanceRepository repository) : IRequestHandler<GetBalanceBodiesListRequest, GetBalanceBodiesListResponse>
{
    public async Task<GetBalanceBodiesListResponse> Handle(GetBalanceBodiesListRequest request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAll();
        return new GetBalanceBodiesListResponse(entities);
    }
}