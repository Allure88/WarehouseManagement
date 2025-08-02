using AutoMapper;
using MediatR;
using WM.Application.Bodies;
using WM.Application.Contracts;
using WM.Application.Responces;
using WM.Application.UseCases_CQRS.Movements.Resources.Validators;
using WM.Domain.Entities;

namespace WM.Application.UseCases_CQRS.Movements.Resources.Commands;

public class PostAdmissionResRequest(AdmissionResBody body) : IRequest<BaseCommandResponse>
{
    public AdmissionResBody Body { get; set; } = body;
}


public class PostAdmissionResRequestHandler(IAdmissionResRepository repository, IMapper mapper) : IRequestHandler<PostAdmissionResRequest, BaseCommandResponse>
{
    public async Task<BaseCommandResponse> Handle(PostAdmissionResRequest request, CancellationToken cancellationToken)
    {
        var response = new BaseCommandResponse();
        var validator = new PostAdmissionResValidator();
        var validationResult = await validator.ValidateAsync(request.Body, cancellationToken);

        if (validationResult.IsValid == false)
        {
            response.Success = false;
            response.Message = "Баланс не изменен";
            response.Errors = [.. validationResult.Errors.Select(q => q.ErrorMessage)];
        }
        else
        {
            var entity = mapper.Map<AdmissionResEntity>(request.Body);
            var addedEntity = await repository.Add(entity);
            response.Success = true;
            response.Message = "Баланс изменен успешно";
        }

        return response;
    }
}