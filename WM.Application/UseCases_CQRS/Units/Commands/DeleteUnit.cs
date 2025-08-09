using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Units.Validators;

namespace WM.Application.UseCases_CQRS.Units.Commands;

public class DeleteUnitCommand(UnitBody body) : IRequest<BaseCommandResponse>
{
    public UnitBody Body { get; set; } = body;
}

public class DeleteUnitCommandHandler(IMapper mapper, IUnitsRepository repository) : IRequestHandler<DeleteUnitCommand, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(DeleteUnitCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var entity = await repository.GetByNameWithDependents(command.Body.Name);
        var validator = new DeleteUnitValidator(entity);
        var validationResult = await validator.ValidateAsync(command.Body, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Единица измерения не удалена";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            entity = mapper.Map(command.Body, entity!);
            await repository.Delete(entity);
            response.Success = true;
            response.Message = "Единица измерения удалена успешно";
        }

        return response;
    }
}
