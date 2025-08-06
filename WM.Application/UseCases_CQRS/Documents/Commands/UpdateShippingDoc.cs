using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Documents.Validators;

namespace WM.Application.UseCases_CQRS.Documents.Commands;

public class UpdateShippingDocCommand(ShippingDocBody body) : IRequest<BaseCommandResponse>
{
    public ShippingDocBody Body { get; set; } = body;
}



public class UpdateShippingDocCommandHandler(IShippingDocRepository repository, IMapper mapper) : IRequestHandler<UpdateShippingDocCommand, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(UpdateShippingDocCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new UpdateShippingDocValidator(repository);
        var validationResult = await validator.ValidateAsync(request.Body, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Баланс без изменений";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            var entity = validator.ShippingDocEntity!;
            entity = mapper.Map(request.Body, entity);
            await repository.Update(entity);
            response.Success = true;
            response.Message = "Баланс изменен успешно";
        }

        return response;
    }
}