using bur_test.Domain.Dto;

namespace bur_test.Domain.Interfaces;

public interface ITelemetryService {
    public Task<List<TelemetryDto>> AddTelemetry (List<TelemetryDto> telemetryDtos);
    public Task<List<DetailedTelemetryDto>> GetDetailedTelemetry ();
}
