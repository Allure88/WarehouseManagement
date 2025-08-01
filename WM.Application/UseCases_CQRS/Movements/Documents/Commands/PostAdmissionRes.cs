using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Movements.Documents.Commands;

public class PostAdmissionResRequest(AdmissionResBody body) : IRequest<PostAdmissionResResponse>
{
    public AdmissionResBody Body { get; set; } = body;
}

public class PostAdmissionResResponse(AdmissionResEntity entity)
{
    public AdmissionResEntity Entity { get; set; } = entity;
}

public class PostAdmissionResRequestHandler(IAdmissionResRepository repository, IMapper mapper) : IRequestHandler<PostAdmissionResRequest, PostAdmissionResResponse>
{
    public async Task<PostAdmissionResResponse> Handle(PostAdmissionResRequest request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<AdmissionResEntity>(request.Body);
        var addedEntity = await repository.Add(entity);
        return new PostAdmissionResResponse(addedEntity);
    }
}