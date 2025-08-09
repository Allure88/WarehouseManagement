using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Documents.Queries;

public class GetAdmissionDocBodiesListRequest : IRequest<GetAdmissionDocBodiesListResponse> { }

public class GetAdmissionDocBodiesListResponse(List<AdmissionDocBody> AdmissionDocs)
{
    public List<AdmissionDocBody> AdmissionDocs { get; } = AdmissionDocs;
}

public class GetPresetnBodiesListRequestHandler(IAdmissionDocRepository repository, IMapper mapper) : IRequestHandler<GetAdmissionDocBodiesListRequest, GetAdmissionDocBodiesListResponse>
{
    public async Task<GetAdmissionDocBodiesListResponse> Handle(GetAdmissionDocBodiesListRequest request, CancellationToken cancellationToken)
    {
        List<AdmissionDocEntity> entities = await repository.GetAllWithDependents();
        entities = [.. entities.Select(e =>
        {
            e.Date = e.Date.ToLocalTime();
            return e;
        })];
        List<AdmissionDocBody> docs = [.. entities.Select(mapper.Map<AdmissionDocBody>)];
        return new GetAdmissionDocBodiesListResponse(docs);
    }
}
