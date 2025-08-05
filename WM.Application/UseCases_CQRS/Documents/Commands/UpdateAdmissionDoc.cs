using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Documents.Validators;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Documents.Commands;

public class UpdateAdmissionDocCommand(AdmissionDocBody body) : IRequest<BaseCommandResponse>
{
    public AdmissionDocBody Body { get; set; } = body;
}

public class UpdateAdmissionDocRequestHandler(IAdmissionDocRepository repository, IBalanceRepository balanceRepository, IMapper mapper) : IRequestHandler<UpdateAdmissionDocCommand, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(UpdateAdmissionDocCommand command, CancellationToken cancellationToken)
    {
        var newQuantity = command.Body.ResBody.Quantity;
        var response = new BaseCommandResponse();
        var validator = new UpdateAdmissionDocValidator(repository, newQuantity);
        var validationResult = await validator.ValidateAsync(command.Body, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Документ не сохранен";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            var entity = mapper.Map<AdmissionDocEntity>(command.Body);
            if (entity.AdmissionRes != null)
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
                    balance.Quantity -= (entity.AdmissionRes.Quantity - newQuantity);
                    await balanceRepository.Update(balance);
                }
            }
            var addedEntity = await repository.Add(entity);


            response.Success = true;
            response.Message = "Документ сохранен успешно";
        }

        return response;
    }
}