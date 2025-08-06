using MediatR;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Documents.Validators;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Documents.Commands;

public class SignShippingDocCommand(string number) : IRequest<BaseCommandResponse>
{
    public string Number { get; set; } = number;
}

public class SignShippingDocCommandHandler(IShippingDocRepository docRepository, IBalanceRepository balanceRepository) : IRequestHandler<SignShippingDocCommand, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(SignShippingDocCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new SignShippingDocValidator(docRepository);
        var validationResult = await validator.ValidateAsync(command.Number, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Документ не подписан";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            var entity = validator.ShippingDocEntity!;
            entity.Status = DocumentStatus.Approved;
            var balances = await balanceRepository.GetAll();

            var balance = balances.First(b => b.UnitOfMeasurement.Name == entity.ShippingRes.UnitOfMeasurement.Name &&
            b.Resource.Name == entity.ShippingRes.Resource.Name);

            balance.Quantity -= entity.ShippingRes.Quantity;
            await balanceRepository.Update(balance);
            await docRepository.Update(entity);

            response.Success = true;
            response.Message = "Документ подписан успешно";
        }

        return response;
    }
}
