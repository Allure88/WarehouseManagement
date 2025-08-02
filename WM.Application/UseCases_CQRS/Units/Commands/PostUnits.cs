using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Units.Validators;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Units.Commands;

public class PostUnitRequest(UnitBody body) : IRequest<BaseCommandResponse>
{
    public UnitBody Body { get; set; } = body;
}

public class PostUnitRequestHandler(IUnitsRepository repository, IMapper mapper) : IRequestHandler<PostUnitRequest, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(PostUnitRequest request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new PostUnitValidator();
        var validationResult = await validator.ValidateAsync(request.Body, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Единицп измерения не добавлена";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            var entity = mapper.Map<UnitEntity>(request.Body);
            var addedEntity = await repository.Add(entity);
            response.Success = true;
            response.Message = "Единица измерения добавлена успешно";
        }

        return response;
    }
}
