using MediatR;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Movements.Documents.Queries;

public class GetAdmissionResBodiesListRequest : IRequest<GetAdmissionResBodiesListResponse> { }

public class GetAdmissionResBodiesListResponse(List<AdmissionResEntity> AdmissionRess)
{
    public List<AdmissionResEntity> AdmissionRess { get; } = AdmissionRess;
}

public class GetAdmissionResBodiesListRequestHandler(IAdmissionResRepository repository) : IRequestHandler<GetAdmissionResBodiesListRequest, GetAdmissionResBodiesListResponse>
{
    public async Task<GetAdmissionResBodiesListResponse> Handle(GetAdmissionResBodiesListRequest request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAll();
        return new GetAdmissionResBodiesListResponse(entities);
    }
}
