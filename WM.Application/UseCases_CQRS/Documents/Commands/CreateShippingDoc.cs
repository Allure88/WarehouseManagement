using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Documents.Validators;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Documents.Commands;

public class CreateShippingDocRequest(ShippingDocBody body) : IRequest<BaseCommandResponse>
{
    public ShippingDocBody Body { get; set; } = body;
}

public class PostShippingDocRequestHandler(IShippingDocRepository repository, IMapper mapper) : IRequestHandler<CreateShippingDocRequest, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(CreateShippingDocRequest request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new CreateShippingDocValidator(repository);
        var validationResult = await validator.ValidateAsync(request.Body, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Документ не создан.";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            var entity = mapper.Map<ShippingDocEntity>(request.Body);
            entity.Status = DocumentStatus.Created;
            var addedEntity = await repository.Add(entity);
            response.Success = true;
            response.Message = "Документ создан успешно";
        }

        return response;
    }
}
