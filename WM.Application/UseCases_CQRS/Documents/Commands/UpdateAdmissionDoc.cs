using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Documents.Validators;

namespace WM.Application.UseCases_CQRS.Documents.Commands;

public class UpdateAdmissionDocCommand(AdmissionDocBody body) : IRequest<BaseCommandResponse>
{
    public AdmissionDocBody Body { get; set; } = body;
}

public class UpdateAdmissionDocRequestHandler(IAdmissionDocRepository repository, IBalanceRepository balanceRepository, IMapper mapper) : IRequestHandler<UpdateAdmissionDocCommand, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(UpdateAdmissionDocCommand command, CancellationToken cancellationToken)
    {
        command.Body.Date = command.Body.Date.ToUniversalTime();
        float newQuantity = command.Body.ResBody.Quantity;
        var response = new BaseCommandResponse();
        var entity = await repository.GetByNumber(command.Body.Number);
        float oldQuantity = entity?.AdmissionRes?.Quantity ?? 0;

        var validator = new UpdateAdmissionDocValidator(entity);
        var validationResult = await validator.ValidateAsync(command.Body, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Документ не сохранен";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            entity = mapper.Map(command.Body, entity);

            if (entity!.AdmissionRes != null)
            {
                var balances = await balanceRepository.GetAll();

                var balance = balances.FirstOrDefault(b => b.UnitOfMeasurement.Name == entity.AdmissionRes?.UnitOfMeasurement.Name &&
                b.Resource.Name == entity.AdmissionRes.Resource.Name);

                if (balance == null)
                {
                    balance = new()
                    {
                        Resource = entity.AdmissionRes.Resource,
                        UnitOfMeasurement = entity.AdmissionRes.UnitOfMeasurement,
                        Quantity = newQuantity
                    };
                    await balanceRepository.Add(balance);
                }
                else
                {
                    balance.Quantity += (newQuantity - oldQuantity);
                    await balanceRepository.Update(balance);
                }
            }
            await repository.Update(entity);


            response.Success = true;
            response.Message = "Документ сохранен успешно";
        }

        return response;
    }
}

