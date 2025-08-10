using FluentValidation;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Documents.Validators;

public class RevocateShippingDocValidator : AbstractValidator<string>
{
    public RevocateShippingDocValidator(ShippingDocEntity? shippingDocEntity)
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
                    if (shippingDocEntity.Status != DocumentStatus.Approved)
                    {
                        context.AddFailure("Документ не подписан ранее. Его возможно подписать.");
                    }

                    //с архивным атрибутом
                    if (shippingDocEntity.ShippingRes.Resource.State == State.Archived)
                    {
                        context.AddFailure("Ресурс в архиве. Отзыв невозможен. Изменения архива ресурса в разделе Ресурсы.");
                    }
                    if (shippingDocEntity.ShippingRes.UnitOfMeasurement.State == State.Archived)
                    {
                        context.AddFailure("Единица измерения  в архиве. Отзыв невозможен. Изменения архива в разделе Единицы измерения.");
                    }
                    if (shippingDocEntity.Client.State == State.Archived)
                    {
                        context.AddFailure("Клиент в архиве. Отзыв невозможен. Изменения архива в разделе Клиенты.");
                    }
                }
            });
    }
}
