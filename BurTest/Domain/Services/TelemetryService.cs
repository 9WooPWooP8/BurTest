using AutoMapper;
using BurTest.Domain.Interfaces;
using BurTest.Domain.Dto;
using Microsoft.AspNetCore.SignalR;

namespace BurTest.Domain.Services;


public class TelemetryService : ITelemetryService
{
    private readonly ITelemetryRepository _telemetryRepository;
    private readonly IWellRepository _wellRepository;
    private readonly IMapper _mapper;
    private readonly IHubContext<TelemetryUpdateHub> _telemetryUpdateHubContext;

    public TelemetryService(
        ITelemetryRepository telemetryRepository,
        IMapper mapper,
        IWellRepository wellRepository,
		IHubContext<TelemetryUpdateHub> telemetryUpdateHubContext)    
	{
        _telemetryRepository = telemetryRepository;
        _mapper = mapper;
        _wellRepository = wellRepository;
        _telemetryUpdateHubContext = telemetryUpdateHubContext;
    }

    public async Task<List<TelemetryDto>> AddTelemetry(List<TelemetryDto> telemetryDtos)
    {
        foreach (var telemetryDto in telemetryDtos)
        {
            var well = await _wellRepository.GetWellByTelemetryId(telemetryDto.Id);

			if (well is null)
				continue;

            well.Active = 1;
            await _wellRepository.UpdateWell(well);
        }

        var telemetry = await _telemetryRepository.AddTelemetry(telemetryDtos);

        /* var telemetryDetailedDtos = _mapper.Map<List<DetailedTelemetryDto>>(telemetry); */
        /* await _telemetryUpdateHubContext.Clients.All.SendAsync("SendTelemetryUpdate", telemetryDetailedDtos); */

        telemetryDtos = _mapper.Map<List<TelemetryDto>>(telemetry);

        return telemetryDtos;
    }

    public async Task<List<DetailedTelemetryDto>> GetDetailedTelemetry()
    {
		var telemetry = await _telemetryRepository.GetTelemetry();

        var detailedTelemetryDtos = _mapper.Map<List<DetailedTelemetryDto>>(telemetry);

		return detailedTelemetryDtos;
    }
}
