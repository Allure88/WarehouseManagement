using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Movements.Documents.Commands;



public class PostShippingDocRequest(ShippingDocBody body) : IRequest<PostShippingDocResponse>
{
    public ShippingDocBody Body { get; set; } = body;
}

public class PostShippingDocResponse(ShippingDocEntity entity)
{
    public ShippingDocEntity Entity { get; set; } = entity;
}

public class PostShippingDocRequestHandler(IShippingDocRepository repository, IMapper mapper) : IRequestHandler<PostShippingDocRequest, PostShippingDocResponse>
{
    public async Task<PostShippingDocResponse> Handle(PostShippingDocRequest request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<ShippingDocEntity>(request.Body);
        var addedEntity = await repository.Add(entity);
        return new PostShippingDocResponse(addedEntity);
    }
}