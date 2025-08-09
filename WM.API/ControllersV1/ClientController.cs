using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WM.API.Models;
using WM.Application.Bodies;
using WM.Application.UseCases_CQRS.Clients.Commands;
using WM.Application.UseCases_CQRS.Clients.Queries;

namespace WM.API.ControllersV1;

[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[ApiController]
public class ClientsController(IMediator mediator, ILogger<ClientsController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<BaseResponse> Get()
    {
        try
        {
            GetClientBodiesListResponse clientsList = await _mediator.Send(new GetClientBodiesListRequest());
            BaseResponse response = new(clientsList) { Success = true, Code = HttpStatusCode.OK };
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
    public async Task<BaseResponse> Add([FromBody] ClientBody inputBody)
    //public async Task<BaseResponse> Add(JsonElement inputBody)
    {
        try
        {
            var command = await _mediator.Send(new CreateClientCommand(inputBody));

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
                Errors = [ex.ToString()]
            };
            return baseResponse;
        }
    }

    [Route("update")]
    [HttpPut]
    public async Task<BaseResponse> Update([FromBody] ClientBody inputBody)
    {
        try
        {
            var command = await _mediator.Send(new UpdateClientCommand(inputBody));

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
                Errors = [ex.ToString()]
            };
            return baseResponse;
        }
    }


    [Route("archive")]
    [HttpPut]
    public async Task<BaseResponse> Archive([FromBody] ClientBody inputBody)
    {
        try
        {
            var command = await _mediator.Send(new ArchiveClientCommand(inputBody));

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
                Errors = [ex.ToString()]
            };
            return baseResponse;
        }
    }

    [Route("returntowork")]
    [HttpPut]
    public async Task<BaseResponse> ReturnToWork([FromBody] ClientBody inputBody)
    {
        try
        {
            var command = await _mediator.Send(new ReturnToWorkClientCommand(inputBody));

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
                Errors = [ex.ToString()]
            };
            return baseResponse;
        }
    }


    [Route("delete")]
    [HttpDelete]
    public async Task<BaseResponse> Delete([FromBody] ClientBody inputBody)
    {
        try
        {
            var command = await _mediator.Send(new DeleteClientCommand(inputBody));

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
                Errors = [ex.ToString()]
            };
            return baseResponse;
        }
    }
}
