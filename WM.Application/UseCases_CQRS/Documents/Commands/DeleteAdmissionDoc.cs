using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Movements.Documents.Validators;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Documents.Commands;

public class DeleteAdmissionDocCommand(AdmissionDocBody body) : IRequest<BaseCommandResponse>
{
    public AdmissionDocBody Body { get; set; } = body;
}

public class DeleteAdmissionDocCommandHandler(IMapper mapper, IAdmissionDocRepository repository, IBalanceRepository balanceRepository) : IRequestHandler<DeleteAdmissionDocCommand, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(DeleteAdmissionDocCommand command, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new DeleteAdmissionDocValidator(repository);
        var validationResult = await validator.ValidateAsync(command.Body, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Документ поступления не удален";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            var entity = mapper.Map<AdmissionDocEntity>(command.Body);

            if (entity.AdmissionRes != null)
            {
                var balances = await balanceRepository.GetAll();

                var balance = balances.First(b => b.UnitOfMeasurement.Name == entity.AdmissionRes?.UnitOfMeasurement.Name &&
                b.Resource.Name == entity.AdmissionRes.Resource.Name);
                balance.Quantity -= entity.AdmissionRes.Quantity;
                await balanceRepository.Update(balance);
            }
            await repository.Delete(entity);
            response.Success = true;
            response.Message = "Документ поступления удален успешно";
        }

        return response;
    }
}
