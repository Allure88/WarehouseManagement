using FluentValidation;
using WM.Application.Bodies;
using WM.Application.Mapper_Profiles;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Documents.Validators;

public class UpdateAdmissionDocValidator : AbstractValidator<AdmissionDocBody>
{
    public UpdateAdmissionDocValidator(AdmissionDocEntity? AdmissionDocEntity)
    {
        RuleFor(c => c)
            .Custom((docBody, context) =>
            {
                if (AdmissionDocEntity is null)
                {
                    context.AddFailure(nameof(docBody.Number), "Документ с таким номером не существует");
                }
                else
                {
                    if (docBody.Date.Date > AdmissionDocEntity.Date.Date)
                    {
                        context.AddFailure("Нельзя изменить дату документа на предыдущую");
                    }

                    var resMvmn = AdmissionDocEntity.AdmissionRes;

                    if (resMvmn is not null)
                    {
                        bool restrict = false;
                        //с составом движения ресурса
                        AdmissionResBody newAdmissionResource = docBody.ResBody;
                        if (newAdmissionResource.UnitOfMeasurement.Name != resMvmn.UnitOfMeasurement.Name
                        || newAdmissionResource.Resource.Name != resMvmn.Resource.Name)
                        {
                            restrict = true;
                            context.AddFailure("Разрешено изменять только количество. Изменение состава ресурса через удаление.");
                        }
                        if (!restrict)
                        {
                            //с архивным атрибутом
                            if (resMvmn.Resource.State != newAdmissionResource.Resource.State.ConvertToState())
                            {
                                context.AddFailure("Разрешено изменять только количество. Изменения архива ресурса в разделе Ресурсы.");
                            }


                            //с балансом
                            var diminishingQuantity = resMvmn.Quantity - newAdmissionResource.Quantity;

                            if (diminishingQuantity > 0)
                            {
                                var diminishingUnit = resMvmn.UnitOfMeasurement;
                                foreach (var balance in resMvmn.Resource.Balances)
                                {
                                    if (balance.UnitOfMeasurement == resMvmn.UnitOfMeasurement)
                                    {
                                        var currentQuantity = balance.Quantity;
                                        if (currentQuantity < diminishingQuantity)
                                        {
                                            context.AddFailure("Недостаточное количество ресурса на балансе");
                                            break;
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
            });
    }
}
