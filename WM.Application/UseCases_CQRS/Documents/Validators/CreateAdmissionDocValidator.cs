using FluentValidation;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Mapper_Profiles;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Documents.Validators;

public class CreateAdmissionDocValidator : AbstractValidator<AdmissionDocBody>
{
    public CreateAdmissionDocValidator(IAdmissionDocRepository repository)
    {
        RuleFor(d => d.ResBody)
            .Must((resourceMovmn) =>
            {
                return resourceMovmn.Resource.State.ConvertToState() != State.Archived;
            }).WithMessage("Архивный ресурс невозможно выбрать");


        RuleFor(d => d.ResBody)
           .Must((resourceMovmn) =>
           {
               return resourceMovmn.UnitOfMeasurement.State.ConvertToState() != State.Archived;
           }).WithMessage("Архивную единицу измерения невозможно выбрать");

        RuleFor(d => d.Number)
              .MustAsync(async (number, token) =>
              {
                  return await repository.GetByNumber(number) is null;
              })
            .WithMessage("Документ с таким номером создан ранее.")
             .NotEmpty().WithMessage("Поле номер не должно быть пуcтым")
             .NotNull()
             .MaximumLength(50).WithMessage("Ммаксимальная длина поля номер 50 символов");

        RuleFor(c => c.Date)
            .NotNull().WithMessage("Поле дата не должно быть пуcтым")
            .Must((date) =>
            {
                return date.Date == DateTime.Today.ToUniversalTime().Date;
            } )
            .WithMessage("Дата должна быть текущая.");
    }
}

