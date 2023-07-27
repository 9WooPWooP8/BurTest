using bur_test.Domain.Dto;
using bur_test.Data.Models;


namespace bur_test.Domain.Interfaces;

public interface IWellService {
	public Task<WellDepthProgressDto> GetWellDepthProgress(int wellId, DateTime start, DateTime end);
	public Task<List<WellDepthProgressDto>> GetActiveWellsDepthProgressByCompany(int companyId, DateTime start, DateTime end);
    public Task<List<Well>> DeactivateInactiveWells();
    public Task<List<DetailedWellDto>> GetDetailedActiveWells();
}
