using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WM.API.Models;
using WM.Application.Bodies;
using WM.Application.UseCases_CQRS.Documents.Commands;
using WM.Application.UseCases_CQRS.Documents.Queries;

namespace WM.API.ControllersV1;

[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[ApiController]
public class AdmissionDocsController(IMediator mediator, ILogger<AdmissionDocsController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<BaseResponse> Get()
    {
        try
        {
            GetAdmissionDocBodiesListResponse AdmissionDocsList = await _mediator.Send(new GetAdmissionDocBodiesListRequest());
            BaseResponse response = new(AdmissionDocsList) { Success = true, Code = System.Net.HttpStatusCode.OK };
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            BaseResponse baseResponse = new(null)
            {
                Code = HttpStatusCode.InternalServerError,
                Success = false,
                Errors = [ex.Message]
            };
            return baseResponse;
        }
    }

    [Route("add")]
    [HttpPost]
    public async Task<BaseResponse> Add([FromBody] AdmissionDocBody inputBody)
    {
        try
        {
            var command = await _mediator.Send(new CreateAdmissionDocCommand(inputBody));

            HttpStatusCode code = command.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
            BaseResponse response = new(null)
            {
                Success = command.Success,
                Code = code,
                Errors = command.Errors
            };
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            BaseResponse baseResponse = new(null)
            {
                Code = HttpStatusCode.InternalServerError,
                Success = false,
                Errors = [ex.Message]
            };
            return baseResponse;
        }
    }

    [Route("update")]
    [HttpPost]
    public async Task<BaseResponse> Update([FromBody] AdmissionDocBody inputBody)
    {
        try
        {
            var command = await _mediator.Send(new UpdateAdmissionDocCommand(inputBody));

            HttpStatusCode code = command.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
            BaseResponse response = new(null)
            {
                Success = command.Success,
                Code = code,
                Errors = command.Errors
            };
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            BaseResponse baseResponse = new(null)
            {
                Code = HttpStatusCode.InternalServerError,
                Success = false,
                Errors = [ex.Message]
            };
            return baseResponse;
        }
    }

    [Route("delete")]
    [HttpDelete]
    public async Task<BaseResponse> Delete([FromBody] AdmissionDocBody inputBody)
    {
        try
        {
            var command = await _mediator.Send(new DeleteAdmissionDocCommand(inputBody));

            HttpStatusCode code = command.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
            BaseResponse response = new(null)
            {
                Success = command.Success,
                Code = code,
                Errors = command.Errors
            };
            return response;
        }
        catch (Exception ex)
        {
            logger.LogError(ex.ToString());
            BaseResponse baseResponse = new(null)
            {
                Code = HttpStatusCode.InternalServerError,
                Success = false,
                Errors = [ex.Message]
            };
            return baseResponse;
        }   
    }
}
