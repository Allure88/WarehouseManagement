using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Units.Validators;
using WM.Domain.Entities;

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
        var validator = new DeleteUnitValidator(repository);
        var validationResult = await validator.ValidateAsync(command.Body, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Единица измерения не удалена";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            var entity = mapper.Map<UnitEntity>(command.Body);
            await repository.Delete(entity);
            response.Success = true;
            response.Message = "Единица измерения удалена успешно";
        }

        return response;
    }
}
