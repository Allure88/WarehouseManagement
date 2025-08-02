using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Movements.Documents.Commands;



public class PostShippingDocRequest(ShippingDocBody body) : IRequest<BaseCommandResponse>
{
    public ShippingDocBody Body { get; set; } = body;
}


public class PostShippingDocRequestHandler(IShippingDocRepository repository, IMapper mapper) : IRequestHandler<PostShippingDocRequest, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(PostShippingDocRequest request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<ShippingDocEntity>(request.Body);
        var addedEntity = await repository.Add(entity);
        return new BaseCommandResponse(addedEntity);
    }
}