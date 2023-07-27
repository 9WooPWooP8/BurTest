namespace bur_test.Domain.Interfaces;

using bur_test.Data.Models;
using bur_test.Domain.Dto;

public interface IWellRepository {
	public Task<Well> GetWellById(int id);
	public Task<List<Well>> GetWellsByCompanyName(string companyName);
	public Task<Well> GetActiveWellById(int id);
	public Task<List<Well>> GetActiveWellsByCompanyName(string companyName);
    public Task<float> GetWellDepthProgress(int id, DateTime start, DateTime end);
    public Task<List<Well>> GetWellsWithoutActivity(int daysInactive);
    public Task<List<Well>> DeactivateWells(List<Well> wells);
	public Task<List<WellDepthProgressDto>> GetActiveWellsDepthProgressByCompany(int companyId, DateTime start, DateTime end);
	public Task<Well> GetWellByTelemetryId(int telemetryId);
	public Task<Well> UpdateWell(Well well);
	public Task<List<Well>> UpdateWells(List<Well> wells);
	public Task<List<Well>> GetDetailedActiveWells();
}
