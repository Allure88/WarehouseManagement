using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Units.Validators;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Units.Commands;

public class ArchiveUnitCommand(UnitBody body) : IRequest<BaseCommandResponse>
{
    public UnitBody Body { get; set; } = body;
}


public class ArchiveUnitRequestHandler(IUnitsRepository repository) : IRequestHandler<ArchiveUnitCommand, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(ArchiveUnitCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var entity = await repository.GetByName(command.Body.Name);
        var validator = new ArchiveUnitValidator(entity);
        var validationResult = validator.Validate(command.Body);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Единица измерения не обновлена";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            entity!.State = State.Archived;
            await repository.Update(entity);
            response.Success = true;
            response.Message = "Клиент обновлен успешно";
        }
        return response;
    }
}

