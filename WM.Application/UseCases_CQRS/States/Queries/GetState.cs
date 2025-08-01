using MediatR;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.States.Queries
{
    public class GetStateBodiesListRequest : IRequest<GetStateBodiesListResponse> { }

    public class GetStateBodiesListResponse(List<StateEntity> States)
    {
        public List<StateEntity> States { get; } = States;
    }

    public class GetStateBodiesListRequestHandler(IStateRepository repository) : IRequestHandler<GetStateBodiesListRequest, GetStateBodiesListResponse>
    {
        public async Task<GetStateBodiesListResponse> Handle(GetStateBodiesListRequest request, CancellationToken cancellationToken)
        {
            var entities = await repository.GetAll();
            return new GetStateBodiesListResponse(entities);
        }
    }
}
