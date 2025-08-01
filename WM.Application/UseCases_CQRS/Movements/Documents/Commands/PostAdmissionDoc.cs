using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Movements.Documents.Commands;
public class PostAdmissionDocRequest(AdmissionDocBody body) : IRequest<PostAdmissionDocResponse>
{
    public AdmissionDocBody Body { get; set; } = body;
}

public class PostAdmissionDocResponse(AdmissionDocEntity entity)
{
    public AdmissionDocEntity Entity { get; set; } = entity;
}

public class PostAdmissionDocRequestHandler(IAdmissionDocRepository repository, IMapper mapper) : IRequestHandler<PostAdmissionDocRequest, PostAdmissionDocResponse>
{
    public async Task<PostAdmissionDocResponse> Handle(PostAdmissionDocRequest request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<AdmissionDocEntity>(request.Body);
        var addedEntity = await repository.Add(entity);
        return new PostAdmissionDocResponse(addedEntity);
    }
}