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

public class PostShippingDocRequestHandler(
    IShippingDocRepository repository,
    IUnitsRepository unitsRepository,
    IResourceRepository resourceRepository,
    IClientRepository clientRepository,
    IMapper mapper) : IRequestHandler<CreateShippingDocRequest, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(CreateShippingDocRequest request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        request.Body.Date = request.Body.Date.ToUniversalTime();
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
            var shippingRes = entity.ShippingRes;

            var client = await clientRepository.GetByName(request.Body.Client.Name);
            if (client is null)
            {
                response.Success = false;
                response.Message = "Документ не сохранен";
                response.Errors = ["Клиент в базе не найден"];
                return response;
            }
            entity.Client = client;

            if (shippingRes != null)
            {
                var unit = await unitsRepository.GetByName(shippingRes.UnitOfMeasurement.Name);
                var resource = await resourceRepository.GetByName(shippingRes.Resource.Name);

                if (unit is null || resource is null)
                {
                    response.Success = false;
                    response.Message = "Документ не сохранен";
                    response.Errors = ["Ресурс или единица измерения в базе не найдены"];
                    return response;
                }
                shippingRes.UnitOfMeasurement = unit;
                shippingRes.Resource = resource;
            }

            var addedEntity = await repository.Add(entity);
            response.Success = true;
            response.Message = "Документ создан успешно";
        }

        return response;
    }
}
