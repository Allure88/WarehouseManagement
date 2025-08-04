using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Movements.Documents.Validators;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Movements.Documents.Commands;

public class DeleteShippingDocCommand(ShippingDocBody body) : IRequest<BaseCommandResponse>
{
    public ShippingDocBody Body { get; set; } = body;
}


public class DeleteShippingDocCommandHandler(IMapper mapper, IShippingDocRepository repository) : IRequestHandler<DeleteShippingDocCommand, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(DeleteShippingDocCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new DeleteShippingDocValidator(repository);
        var validationResult = await validator.ValidateAsync(command.Body, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Документ списания не удален";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            var entity = mapper.Map<ShippingDocEntity>(command.Body);

            await repository.Delete(entity);
            response.Success = true;
            response.Message = "Документ списания удален успешно";
        }

        return response;
    }
}