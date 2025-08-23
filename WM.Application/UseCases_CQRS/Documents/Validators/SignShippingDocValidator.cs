using FluentValidation;
using WM.Domain.Entities;
using WM.Domain.Models;

namespace WM.Application.UseCases_CQRS.Documents.Validators;

public class SignShippingDocValidator : AbstractValidator<string>
{
    public SignShippingDocValidator(ShippingDocEntity? shippingDocEntity)
    {
        RuleFor(c => c)
            .Custom((number, context) =>
            {
                if (shippingDocEntity is null)
                {
                    context.AddFailure("Документ с таким номером не существует");
                }
                else
                {
                    if (shippingDocEntity.Status == DocumentStatus.Approved)
                    {
                        context.AddFailure("Документ подписан ранее. Его возможно отозвать.");
                    }
                    //с архивным атрибутом
                    if (shippingDocEntity.ShippingRes.Resource.State == State.Archived)
                    {
                        context.AddFailure("Ресурс в архиве. Подпиь невозможна. Изменения архива ресурса в разделе Ресурсы.");
                    }
                    if (shippingDocEntity.ShippingRes.UnitOfMeasurement.State == State.Archived)
                    {
                        context.AddFailure("Единица измерения в архиве. Подпиь невозможна. Изменения архива в разделе Единицы измерения.");
                    }
                    if (shippingDocEntity.Client.State == State.Archived)
                    {
                        context.AddFailure("Клиент в архиве. Подпиь невозможна. Изменения архива в разделе Клиенты.");
                    }

                    var diminishingQuantity = shippingDocEntity.ShippingRes.Quantity;

                    if (diminishingQuantity > 0)
                    {
                        var diminishingUnit = shippingDocEntity.ShippingRes.UnitOfMeasurement;
                        foreach (var balance in shippingDocEntity.ShippingRes.Resource.Balances)
                        {
                            if (balance.UnitOfMeasurement == shippingDocEntity.ShippingRes.UnitOfMeasurement)
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
