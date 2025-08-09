using Asp.Versioning;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WM.API.Models;
using WM.Application.UseCases_CQRS.Balances.Queries;

namespace WM.API.ControllersV1;

[Route("api/[controller]")]
[Route("api/v{version:apiVersion}/[controller]")]
[ApiVersion("1")]
[ApiController]
public class BalancesController(IMediator mediator, ILogger<BalancesController> logger) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    public async Task<BaseResponse> Get()
    {
        try
        {
            GetBalanceBodiesListResponse BalancesList = await _mediator.Send(new GetBalanceBodiesListRequest());
            BaseResponse response = new(BalancesList) { Success = true, Code = HttpStatusCode.OK };
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
