using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Units.Validators;
using WM.Domain.Models;

namespace WM.Application.UseCases_CQRS.Units.Commands;

public class ReturnToWorkUnitCommand(UnitBody body) : IRequest<BaseCommandResponse>
{
    public UnitBody Body { get; set; } = body;
}



public class ReturnToWorkUnitRequestHandler(IUnitsRepository repository) : IRequestHandler<ReturnToWorkUnitCommand, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(ReturnToWorkUnitCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var entity = await repository.GetByName(command.Body.Name);
        var validator = new ReturnToWorkUnitValidator(entity);
        var validationResult = validator.Validate(command.Body);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Единица измерения не обновлена";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            entity!.State = State.Active;
            await repository.Update(entity);
            response.Success = true;
            response.Message = "Единица измерения обновлена успешно";
        }
        return response;
    }
}