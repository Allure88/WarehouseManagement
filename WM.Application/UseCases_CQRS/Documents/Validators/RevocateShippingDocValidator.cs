using FluentValidation;
using WM.Application.Contracts;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Documents.Validators;

public class RevocateShippingDocValidator : AbstractValidator<string>
{
    public ShippingDocEntity? ShippingDocEntity { get; private set; }
    public RevocateShippingDocValidator(IShippingDocRepository repository)
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
                    if (ShippingDocEntity.Status != DocumentStatus.Approved)
                    {
                        context.AddFailure("Документ не подписан ранее. Его возможно подписать.");
                    }
                    //с архивным атрибутом
                    if (ShippingDocEntity.ShippingRes.Resource.State == State.Archived)
                    {
                        context.AddFailure("Ресурс в архиве. Отзыв невозможен. Изменения архива ресурса в разделе Ресурсы.");
                    }
                    if (ShippingDocEntity.ShippingRes.UnitOfMeasurement.State == State.Archived)
                    {
                        context.AddFailure("Единица измерения  в архиве. Отзыв невозможен. Изменения архива в разделе Единицы измерения.");
                    }
                    if (ShippingDocEntity.Client.State == State.Archived)
                    {
                        context.AddFailure("Клиент в архиве. Отзыв невозможен. Изменения архива в разделе Клиенты.");
                    }
                }
            });
    }
}
