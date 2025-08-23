using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Documents.Validators;
using WM.Domain.Entities;
using WM.Domain.Models;

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
            AdmissionDocBody body = request.Body;
            AdmissionRes? admissionRes = mapper.Map<AdmissionResBody, AdmissionRes>(request.Body.ResBody);
            var (admissionDoc, errors) = AdmissionDoc.GetInstance(admissionRes, body.Number, CheckIfNumberExists, body.Date, GetUnitFromStorageByName, GetResourceFromStorageByName);

            if (admissionDoc is null)
            {
                response.Success = false;
                response.Message = "Документ не сохранен";
                response.Errors = errors;
                return response;
            }



            var docEntity = mapper.Map<AdmissionDocEntity>(admissionDoc);
            admissionRes = admissionDoc.AdmissionRes;
            if (admissionRes is not null)
            {
                UnitEntity unit = (await unitsRepository.GetByName(admissionRes.UnitOfMeasurement.Name))!;
                ResourceEntity resource = (await resourceRepository.GetByName(admissionRes.Resource.Name))!;
                docEntity.AdmissionRes!.UnitOfMeasurement = unit;
                docEntity.AdmissionRes.Resource = resource;


                var balanceEntity = await balanceRepository.GetByPair(docEntity.AdmissionRes.UnitOfMeasurement.Name, docEntity.AdmissionRes.Resource.Name);


                Balance balance;


                if (balanceEntity == null)
                {
                    balance = new()
                    {
                        Resource = admissionRes.Resource,
                        UnitOfMeasurement = admissionRes.UnitOfMeasurement,
                        Quantity = 0
                    };


                    var (applied, balanceErrors) = balance.Apply(admissionDoc);
                    if (applied)
                    {
                        balanceEntity = mapper.Map<BalanceEntity>(balance);
                        balanceEntity.UnitOfMeasurement = unit;
                        balanceEntity.Resource = resource;
                        await balanceRepository.Add(balanceEntity);
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Документ не сохранен";
                        response.Errors = balanceErrors;
                        return response;
                    }
                }
                else
                {
                    balance = mapper.Map<Balance>(balanceEntity);
                    var (applied, balanceErrors) = balance.Apply(admissionDoc);
                    if (applied)
                    {
                        balanceEntity = mapper.Map<BalanceEntity>(balance);
                        balanceEntity.UnitOfMeasurement = unit;
                        balanceEntity.Resource = resource;
                        await balanceRepository.Update(balanceEntity);
                    }
                    else
                    {
                        response.Success = false;
                        response.Message = "Документ не сохранен";
                        response.Errors = balanceErrors;
                        return response;
                    }
                    //balance.Quantity += admissionRes.Quantity;
                    //await balanceRepository.Update(balance);
                }

            }



            await repository.Add(docEntity);
            response.Success = true;
            response.Message = "Документ сохранен успешно";
        }

        return response;
    }

    public Domain.Models.Unit GetUnitFromStorageByName(string name)
    {
        return mapper.Map<Domain.Models.Unit>(unitsRepository.GetByName(name));
    }

    public bool CheckIfNumberExists(string number)
    {
        return repository.GetByNumber(number) is not null;
    }


    public Resource GetResourceFromStorageByName(string name)
    {
        return mapper.Map<Resource>(resourceRepository.GetByName(name));
    }
}
