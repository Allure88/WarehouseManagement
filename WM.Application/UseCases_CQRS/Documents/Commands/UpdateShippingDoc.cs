using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Documents.Validators;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Documents.Commands;

public class UpdateShippingDocCommand(ShippingDocBody body) : IRequest<BaseCommandResponse>
{
    public ShippingDocBody Body { get; set; } = body;
}



public class UpdateShippingDocCommandHandler(IShippingDocRepository repository, IMapper mapper) : IRequestHandler<UpdateShippingDocCommand, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(UpdateShippingDocCommand command, CancellationToken cancellationToken)
    {
        command.Body.Date = command.Body.Date.ToUniversalTime();
        var response = new BaseCommandResponse();
        var entity = await repository.GetByNumber(command.Body.Number);
        var validator = new UpdateShippingDocValidator(entity);
        var validationResult = await validator.ValidateAsync(command.Body, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Баланс без изменений";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            entity = mapper.Map(command.Body, entity);
            await repository.Update(entity!);
            response.Success = true;
            response.Message = "Баланс изменен успешно";
        }

        return response;
    }
}