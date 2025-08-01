using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Balances.Commands;

public class PostBalanceRequest(BalanceBody body) : IRequest<PostBalanceResponse>
{
    public BalanceBody Body { get; set; } = body;
}

public class PostBalanceResponse(BalanceEntity entity)
{
    public BalanceEntity Entity { get; set; } = entity;
}

public class PostBalanceRequestHandler(IBalanceRepository repository, IMapper mapper) : IRequestHandler<PostBalanceRequest, PostBalanceResponse>
{
    public async Task<PostBalanceResponse> Handle(PostBalanceRequest request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<BalanceEntity>(request.Body);
        var addedEntity = await repository.Add(entity);
        return new PostBalanceResponse(addedEntity);
    }
}