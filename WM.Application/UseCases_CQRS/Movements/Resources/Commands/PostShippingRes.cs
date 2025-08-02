using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Movements.Resources.Validators;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Movements.Resources.Commands;
public class PostShippingResRequest(ShippingResBody body) : IRequest<BaseCommandResponse>
{
    public ShippingResBody Body { get; set; } = body;
}

public class PostShippingResRequestHandler(IShippingResRepository repository, IMapper mapper) : IRequestHandler<PostShippingResRequest, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(PostShippingResRequest request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new PostShippingResValidator();
        var validationResult = await validator.ValidateAsync(request.Body, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Баланс без изменений";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            var entity = mapper.Map<ShippingResEntity>(request.Body);
            var addedEntity = await repository.Add(entity);
            response.Success = true;
            response.Message = "Баланс изменен успешно";
        }

        return response;
    }
}