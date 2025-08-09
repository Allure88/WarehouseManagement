using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Units.Queries;

public class GetUnitBodiesListRequest : IRequest<GetUnitBodiesListResponse> { }

public class GetUnitBodiesListResponse(List<UnitBody> Units)
{
    public List<UnitBody> Units { get; } = Units;
}

public class GetPresetnBodiesListRequestHandler(IUnitsRepository repository,IMapper mapper) : IRequestHandler<GetUnitBodiesListRequest, GetUnitBodiesListResponse>
{
    public async Task<GetUnitBodiesListResponse> Handle(GetUnitBodiesListRequest request, CancellationToken cancellationToken)
    {
        var entities = await repository.GetAll();
        List<UnitBody> units = [.. entities.Select(e => mapper.Map<UnitBody>(e))];
        return new GetUnitBodiesListResponse(units);
    }
}
