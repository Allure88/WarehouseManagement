using FluentValidation;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Documents.Validators;

public class SignShippingDocValidator : AbstractValidator<string>
{
    public ShippingDocEntity? ShippingDocEntity { get; private set; }
    public SignShippingDocValidator(IShippingDocRepository repository)
    {
        RuleFor(c => c)
            .Custom(async (number, context) =>
            {
                ShippingDocEntity = await repository.GetByNumber(number);
                if (ShippingDocEntity is null)
                {
                    context.AddFailure("Документ с таким номером не существует");
                }
                else
                {
                    if (ShippingDocEntity.Status == DocumentStatus.Approved)
                    {
                        context.AddFailure("Документ подписан ранее. Его возможно отозвать.");
                    }
                    //с архивным атрибутом
                    if (ShippingDocEntity.ShippingRes.Resource.State == State.Archived)
                    {
                        context.AddFailure("Ресурс в архиве. Подпиь невозможна. Изменения архива ресурса в разделе Ресурсы.");
                    }
                    if (ShippingDocEntity.ShippingRes.UnitOfMeasurement.State == State.Archived)
                    {
                        context.AddFailure("Единица измерения в архиве. Подпиь невозможна. Изменения архива в разделе Единицы измерения.");
                    }
                    if (ShippingDocEntity.Client.State == State.Archived)
                    {
                        context.AddFailure("Клиент в архиве. Подпиь невозможна. Изменения архива в разделе Клиенты.");
                    }

                    var diminishingQuantity = ShippingDocEntity.ShippingRes.Quantity;

                    if (diminishingQuantity > 0)
                    {
                        var diminishingUnit = ShippingDocEntity.ShippingRes.UnitOfMeasurement;
                        foreach (var balance in ShippingDocEntity.ShippingRes.Resource.Balances)
                        {
                            if (balance.UnitOfMeasurement == ShippingDocEntity.ShippingRes.UnitOfMeasurement)
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
            });
    }
}
