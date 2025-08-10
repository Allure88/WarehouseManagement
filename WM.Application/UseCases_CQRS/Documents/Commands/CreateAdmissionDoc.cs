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


public class CreateAdmissionDocCommandHandler(
    IAdmissionDocRepository repository,
    IBalanceRepository balanceRepository,
    IUnitsRepository unitsRepository,
    IResourceRepository resourceRepository,
    IMapper mapper) : IRequestHandler<CreateAdmissionDocCommand, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(CreateAdmissionDocCommand request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        request.Body.Date = request.Body.Date.ToUniversalTime();
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
            var admissionRes = entity.AdmissionRes;

            if (admissionRes != null)
            {
                var unit = await unitsRepository.GetByName(admissionRes.UnitOfMeasurement.Name);
                var resource = await resourceRepository.GetByName(admissionRes.Resource.Name);

                if (unit is null || resource is null)   
                {
                    response.Success = false;
                    response.Message = "Документ не сохранен";
                    response.Errors = ["Ресурс или единица измерения в базе не найдены"];
                    return response;
                }
                admissionRes.UnitOfMeasurement = unit;
                admissionRes.Resource = resource;


                var balances = await balanceRepository.GetAllWithDependencies();

                var balance = balances.FirstOrDefault(b => b.UnitOfMeasurement.Name == entity.AdmissionRes?.UnitOfMeasurement.Name &&
                b.Resource.Name == entity.AdmissionRes.Resource.Name);

                if (balance == null)
                {
                    balance = new()
                    {
                        Resource = resource,
                        UnitOfMeasurement = unit,
                        Quantity = admissionRes.Quantity
                    };
                    await balanceRepository.Add(balance);
                }
                else
                {
                    balance.Quantity += admissionRes.Quantity;
                    await balanceRepository.Update(balance);
                }
            }


            await repository.Add(entity);


            response.Success = true;
            response.Message = "Документ сохранен успешно";
        }

        return response;
    }
}
