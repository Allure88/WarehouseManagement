using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WM.API.Models;
using WM.Application.Bodies;
using WM.Application.UseCases_CQRS.Resources.Commands;
using WM.Application.UseCases_CQRS.Resources.Queries;

namespace WM.API.ControllersV1;

[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[ApiController]
public class ResourcesController(IMediator mediator, ILogger<ResourcesController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<BaseResponse> Get()
    {
        try
        {
            GetResourceBodiesListResponse ResourcesList = await _mediator.Send(new GetResourceBodiesListRequest());
            BaseResponse response = new(ResourcesList) { Success = true, Code = HttpStatusCode.OK };
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
    public async Task<BaseResponse> Add([FromBody] ResourceBody inputBody)
    {
        try
        {
            var command = await _mediator.Send(new CreateResourceCommand(inputBody));

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
    [HttpPut]
    public async Task<BaseResponse> Update([FromBody] ResourceBody inputBody)
    {
        try
        {
            var command = await _mediator.Send(new UpdateResourceCommand(inputBody));

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

    [Route("archive")]
    [HttpPut]
    public async Task<BaseResponse> Archive([FromBody] ResourceBody inputBody)
    {
        try
        {
            var command = await _mediator.Send(new ArchiveResourceCommand(inputBody));

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

    [Route("returntowork")]
    [HttpPut]
    public async Task<BaseResponse> ReturnToWork([FromBody] ResourceBody inputBody)
    {
        try
        {
            var command = await _mediator.Send(new ReturnToWorkResourceCommand(inputBody));

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
    public async Task<BaseResponse> Delete([FromBody] ResourceBody inputBody)
    {
        try
        {
            var commandResponce = await _mediator.Send(new DeleteResourceCommand(inputBody));

            HttpStatusCode code = commandResponce.Success ? HttpStatusCode.OK : HttpStatusCode.BadRequest;
            BaseResponse response = new(null)
            {
                Success = commandResponce.Success,
                Code = code,
                Errors = commandResponce.Errors
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
