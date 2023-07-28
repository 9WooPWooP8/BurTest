using BurTest.Domain.Dto;
using BurTest.Data.Models;


namespace BurTest.Domain.Interfaces;

public interface IWellService {
	public Task<WellDepthProgressDto> GetWellDepthProgress(int wellId, DateTime start, DateTime end);
	public Task<List<WellDepthProgressDto>> GetActiveWellsDepthProgressByCompany(int companyId, DateTime start, DateTime end);
    public Task<List<Well>> DeactivateInactiveWells();
    public Task<List<DetailedWellDto>> GetDetailedActiveWells();
    public Task<List<DetailedWellDto>> GetDetailedWells();
}
