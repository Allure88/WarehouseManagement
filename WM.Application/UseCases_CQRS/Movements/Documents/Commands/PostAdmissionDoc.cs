using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Movements.Documents.Commands;
public class PostAdmissionDocRequest(AdmissionDocBody body) : IRequest<BaseCommandResponse>
{
    public AdmissionDocBody Body { get; set; } = body;
}


public class PostAdmissionDocRequestHandler(IAdmissionDocRepository repository, IMapper mapper) : IRequestHandler<PostAdmissionDocRequest, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(PostAdmissionDocRequest request, CancellationToken cancellationToken)
    {
        var entity = mapper.Map<AdmissionDocEntity>(request.Body);
        var addedEntity = await repository.Add(entity);
        return new BaseCommandResponse(addedEntity);
    }
}