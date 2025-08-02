using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Balances.Validators;
using WM.Application.UseCases_CQRS.Clients.Validators;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Balances.Commands;

public class PostBalanceRequest(BalanceBody body) : IRequest<BaseCommandResponse>
{
    public BalanceBody Body { get; set; } = body;
}

public class PostBalanceRequestHandler(IBalanceRepository repository, IMapper mapper) : IRequestHandler<PostBalanceRequest, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(PostBalanceRequest request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new PostBalanceValidator();
        var validationResult = await validator.ValidateAsync(request.Body, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Баланс без изменений";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            var entity = mapper.Map<BalanceEntity>(request.Body);
            var addedEntity = await repository.Add(entity);
            response.Success = true;
            response.Message = "Баланс изменен успешно";
        }

        return response;
    }
}