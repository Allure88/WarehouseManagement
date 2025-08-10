using MediatR;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Documents.Validators;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Documents.Commands;

public class RevocateShippingDocCommand(string number) : IRequest<BaseCommandResponse>
{
    public string Number { get; set; } = number;
}



public class RevocateShippingDocCommandHandler(IShippingDocRepository docRepository, IBalanceRepository balanceRepository) : IRequestHandler<RevocateShippingDocCommand, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(RevocateShippingDocCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var entity = await docRepository.GetByNumber(command.Number);
        var validator = new RevocateShippingDocValidator(entity);
        var validationResult = await validator.ValidateAsync(command.Number, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Документ не отозван";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            entity!.Status = DocumentStatus.Revocated;
            var balances = await balanceRepository.GetAllWithDependencies();

            var balance = balances.First(b => b.UnitOfMeasurement.Name == entity.ShippingRes.UnitOfMeasurement.Name &&
            b.Resource.Name == entity.ShippingRes.Resource.Name);

            balance.Quantity += entity.ShippingRes.Quantity;
            await balanceRepository.Update(balance);
            await docRepository.Update(entity);

            response.Success = true;
            response.Message = "Документ отозван успешно";
        }

        return response;
    }
}