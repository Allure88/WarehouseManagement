using FluentValidation;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Mapper_Profiles;
using WM.Domain.Models;

namespace WM.Application.UseCases_CQRS.Documents.Validators;

public class CreateShippingDocValidator:AbstractValidator<ShippingDocBody>
{
	public CreateShippingDocValidator(IShippingDocRepository repository)
	{
        RuleFor(d => d.ResBody)
            .NotNull()
            .Must((resourceMovmn) =>
            {
                return resourceMovmn.Resource.State.ConvertToState() != State.Archived;
            }).WithMessage("Архивный ресурс невозможно выбрать");

        RuleFor(d => d.ResBody)
         .Must((resourceMovmn) =>
         {
             return resourceMovmn.UnitOfMeasurement.State.ConvertToState() != State.Archived;
         }).WithMessage("Архивную единицу измерения невозможно выбрать");

        RuleFor(c => c.Number)
             .MustAsync(async (number, token) =>
             {
                 return await repository.GetByNumber(number) is null;
             })
            .WithMessage("Документ с таким номером создан ранее.")
         .NotEmpty().WithMessage("Поле номаре не должно быть пустым")
         .NotNull()
         .MaximumLength(50).WithMessage("Максимальная длина поля номер 50 символов");

        RuleFor(c => c.Client)
            .NotEmpty().WithMessage("Поле клиент не должно быть пустым")
            .NotNull()
            .Must((client) =>
              {
                  return client.State.ConvertToState() != State.Archived;
              }).WithMessage("Клиента из архива невозможно выбрать");

        RuleFor(c => c.Date)
            .NotNull().WithMessage("Поле дата не должно быть пустым")
             .Must((date) =>
             {
                 return date.Date == DateTime.Today.ToUniversalTime().Date;
             })
            .WithMessage("Дата должна быть текущая."); 

    }
}

