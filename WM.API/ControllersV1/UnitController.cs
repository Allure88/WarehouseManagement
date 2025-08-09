using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WM.API.Models;
using WM.Application.Bodies;
using WM.Application.UseCases_CQRS.Units.Commands;
using WM.Application.UseCases_CQRS.Units.Queries;

namespace WM.API.ControllersV1;

[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[ApiController]
public class UnitsController(IMediator mediator, ILogger<UnitsController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<BaseResponse> Get()
    {
        try
        {
            GetUnitBodiesListResponse UnitsList = await _mediator.Send(new GetUnitBodiesListRequest());
            BaseResponse response = new(UnitsList) { Success = true, Code = System.Net.HttpStatusCode.OK };
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

    [Route("add")]
    [HttpPost]
    public async Task<BaseResponse> Add([FromBody] UnitBody inputBody)
    {
        try
        {
            var command = await _mediator.Send(new CreateUnitCommand(inputBody));

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
    public async Task<BaseResponse> Update([FromBody] UnitBody inputBody)
    {
        try
        {
            var command = await _mediator.Send(new UpdateUnitCommand(inputBody));

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
    public async Task<BaseResponse> ReturnToWork([FromBody] UnitBody inputBody)
    {
        try
        {
            var command = await _mediator.Send(new ReturnToWorkUnitCommand(inputBody));

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
                Errors =  [ex.ToString()]
            };
            return baseResponse;
        }
    }

    [Route("archive")]
    [HttpPost]
    public async Task<BaseResponse> Archive([FromBody] UnitBody inputBody)
    {
        try
        {
            var command = await _mediator.Send(new ArchiveUnitCommand(inputBody));

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
    public async Task<BaseResponse> Delete([FromBody] UnitBody inputBody)
    {
        try
        {
            var commandResponce = await _mediator.Send(new DeleteUnitCommand(inputBody));

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
                Errors = [ex.ToString()]
            };
            return baseResponse;
        }
    }
}
