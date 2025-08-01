using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Units.Commands;

public class PostUnitRequest(UnitBody body) : IRequest<PostUnitResponse>
{
    public UnitBody Body { get; set; } = body;
}

public class PostUnitResponse(UnitEntity entity)
{
    public UnitEntity Entity { get; set; } = entity;
}

public class PostUnitRequestHandler(IUnitsRepository repository, IMapper mapper) : IRequestHandler<PostUnitRequest, PostUnitResponse>
{
    public async Task<PostUnitResponse> Handle(PostUnitRequest request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<UnitEntity>(request.Body);
        var addedEntity = await repository.Add(entity);
        return new PostUnitResponse(addedEntity);
    }
}
