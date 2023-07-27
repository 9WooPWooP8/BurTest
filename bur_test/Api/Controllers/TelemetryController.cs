using Microsoft.AspNetCore.Mvc;
using bur_test.Domain.Interfaces;
using bur_test.Domain.Dto;
using AutoMapper;

namespace bur_test.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class TelemetryController : ControllerBase
{
    private readonly ITelemetryService _telemetryService;
    private readonly IMapper _mapper;

    public TelemetryController(ITelemetryRepository telemetryRepository, IMapper mapper, ITelemetryService telemetryService)
    {
        _mapper = mapper;
        _telemetryService = telemetryService;
    }

    [HttpPost]
    public async Task<IActionResult> AddTelemetry([FromBody]List<TelemetryDto> telemetryDtos)
    {
        var updatedTelemetryDtos = await _telemetryService.AddTelemetry(telemetryDtos);

        return Ok(updatedTelemetryDtos);
    }

    [HttpGet]
    public async Task<IActionResult> GetTelemetry()
    {
        var detailedTelemetry = await _telemetryService.GetDetailedTelemetry();

        return Ok(detailedTelemetry);
    }
}
