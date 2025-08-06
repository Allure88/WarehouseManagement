using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Documents.Validators;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Documents.Commands;
public class CreateAdmissionDocCommand(AdmissionDocBody body) : IRequest<BaseCommandResponse>
{
    public AdmissionDocBody Body { get; set; } = body;
}


public class CreateAdmissionDocCommandHandler(IAdmissionDocRepository repository, IBalanceRepository balanceRepository, IMapper mapper) : IRequestHandler<CreateAdmissionDocCommand, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(CreateAdmissionDocCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new CreateAdmissionDocValidator(repository);
        var validationResult = await validator.ValidateAsync(request.Body, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Документ не сохранен";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            var entity = mapper.Map<AdmissionDocEntity>(request.Body);
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
                        Quantity = entity.AdmissionRes.Quantity
                    };
                    await balanceRepository.Add(balance);
                }
                else
                {
                    balance.Quantity += entity.AdmissionRes.Quantity;
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
