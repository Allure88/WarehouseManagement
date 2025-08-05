using MediatR;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Documents.Queries;

public class GetAdmissionDocBodiesListRequest : IRequest<GetAdmissionDocBodiesListResponse> { }

public class GetAdmissionDocBodiesListResponse(List<AdmissionDocEntity> AdmissionDocs)
{
    public List<AdmissionDocEntity> AdmissionDocs { get; } = AdmissionDocs;
}

public class GetPresetnBodiesListRequestHandler(IAdmissionDocRepository repository) : IRequestHandler<GetAdmissionDocBodiesListRequest, GetAdmissionDocBodiesListResponse>
{
    public async Task<GetAdmissionDocBodiesListResponse> Handle(GetAdmissionDocBodiesListRequest request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAll();
        return new GetAdmissionDocBodiesListResponse(entities);
    }
}
