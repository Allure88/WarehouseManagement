using FluentValidation;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Mapper_Profiles;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Documents.Validators;

public class UpdateShippingDocValidator : AbstractValidator<ShippingDocBody>
{
    public ShippingDocEntity? ShippingDocEntity { get; private set; }

    public UpdateShippingDocValidator(IShippingDocRepository repository)
    {
        RuleFor(c => c)
            .Custom(async (docBody, context) =>
            {
                ShippingDocEntity = await repository.GetByNumber(docBody.Number);
                if (ShippingDocEntity is null)
                {
                    context.AddFailure(nameof(docBody.Number), "Документ с таким номером не существует");
                }
                else
                {
                    if (docBody.Date > ShippingDocEntity.Date)
                    {
                        context.AddFailure("Нельзя изменить дату документа на предыдущую");
                    }

                    var resMvmn = ShippingDocEntity.ShippingRes;

                    if (resMvmn is not null)
                    {
                        bool restrict = false;
                        //с составом движения ресурса
                        ShippingResBody newAdmissionResource = docBody.ResBody;
                        if (newAdmissionResource.UnitOfMeasurement.Name != resMvmn.UnitOfMeasurement.Name
                        || newAdmissionResource.Resource.Name != resMvmn.Resource.Name)
                        {
                            restrict = true;
                            context.AddFailure("Разрешено изменять только количество. Изменение состава раесурса через удаление.");
                        }
                        if (!restrict)
                        {
                            //с архивным атрибутом
                            if (resMvmn.Resource.State != newAdmissionResource.Resource.State.ConvertToState())
                            {
                                context.AddFailure("Разрешено изменять только количество. Изменения архива ресурса в разделе Ресурсы.");
                            }

                            if (ShippingDocEntity.Client.State != docBody.Client.State.ConvertToState())
                            {
                                context.AddFailure("Разрешено изменять только количество. Изменения архива клиентов в разделе Клиенты.");
                            }
                        }
                    }
                }
            });
    }
}

