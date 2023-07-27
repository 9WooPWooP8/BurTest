using bur_test.Data.Models;
using bur_test.Domain.Dto;

namespace bur_test.Domain.Interfaces;

public interface ITelemetryRepository {
    public Task<List<Telemetry>> AddTelemetry (List<TelemetryDto> telemetryDtos);
    public Task<List<Telemetry>> GetTelemetry();
}
