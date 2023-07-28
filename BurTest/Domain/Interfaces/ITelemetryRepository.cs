using BurTest.Data.Models;
using BurTest.Domain.Dto;

namespace BurTest.Domain.Interfaces;

public interface ITelemetryRepository {
    public Task<List<Telemetry>> AddTelemetry (List<TelemetryDto> telemetryDtos);
    public Task<List<Telemetry>> GetTelemetry();
}
