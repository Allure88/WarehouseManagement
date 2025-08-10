using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Documents.Validators;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Documents.Commands;

public class DeleteShippingDocCommand(ShippingDocBody body) : IRequest<BaseCommandResponse>
{
    public ShippingDocBody Body { get; set; } = body;
}


public class DeleteShippingDocCommandHandler(IShippingDocRepository repository) : IRequestHandler<DeleteShippingDocCommand, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(DeleteShippingDocCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var entity = await repository.GetByNumber(command.Body.Number);
        var validator = new DeleteShippingDocValidator(entity);
        var validationResult = await validator.ValidateAsync(command.Body, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Документ списания не удален";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            await repository.Delete(entity!);
            response.Success = true;
            response.Message = "Документ списания удален успешно";
        }

        return response;
    }
}