using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Movements.Documents.Commands;
public class PostShippingResRequest(ShippingResBody body) : IRequest<PostShippingResResponse>
{
    public ShippingResBody Body { get; set; } = body;
}

public class PostShippingResResponse(ShippingResEntity entity)
{
    public ShippingResEntity Entity { get; set; } = entity;
}

public class PostShippingResRequestHandler(IShippingResRepository repository, IMapper mapper) : IRequestHandler<PostShippingResRequest, PostShippingResResponse>
{
    public async Task<PostShippingResResponse> Handle(PostShippingResRequest request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<ShippingResEntity>(request.Body);
        var addedEntity = await repository.Add(entity);
        return new PostShippingResResponse(addedEntity);
    }
}