using BurTest.Domain.Dto;

namespace BurTest.Domain.Interfaces;

public interface ITelemetryService {
    public Task<List<TelemetryDto>> AddTelemetry (List<TelemetryDto> telemetryDtos);
    public Task<List<DetailedTelemetryDto>> GetDetailedTelemetry ();
}
