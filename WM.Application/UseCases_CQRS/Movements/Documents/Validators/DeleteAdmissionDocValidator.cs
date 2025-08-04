using FluentValidation;
using WM.Application.Bodies;
using WM.Application.Contracts;

namespace WM.Application.UseCases_CQRS.Movements.Documents.Validators;

public class DeleteAdmissionDocValidator : AbstractValidator<AdmissionDocBody>
{
    public DeleteAdmissionDocValidator(IAdmissionDocRepository repository)
    {
        RuleFor(c => c.Number)
            .Custom(async (name, context) =>
            {
                var admissionDoc = await repository.GetByNumber(name);
                if (admissionDoc is null)
                {
                    context.AddFailure(nameof(name), "Документ с таким именем не существует");
                }
                else
                {
                    var resMvmn = admissionDoc.AdmissionRes;
                    if (resMvmn is not null)
                    {
                        var diminishingQuantity = resMvmn.Quantity;
                        var diminishingUnit = resMvmn.UnitOfMeasurement;
                        foreach (var balance in resMvmn.Resource.Balances)
                        {
                            if (balance.UnitOfMeasurement == resMvmn.UnitOfMeasurement)
                            {
                                var currentQuantity = balance.Quantity;
                                if (currentQuantity < diminishingQuantity)
                                {
                                    context.AddFailure(nameof(name), "Недостаточное количество ресурса на балансе");
                                    break;
                                }
                            }
                        }
                    }
                }
            });
    }
}
