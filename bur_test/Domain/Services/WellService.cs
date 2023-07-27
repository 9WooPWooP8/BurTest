using AutoMapper;
using bur_test.Domain.Interfaces;
using bur_test.Domain.Dto;
using bur_test.Data.Models;

namespace bur_test.Domain.Services;


public class WellService : IWellService
{
    private const int DaysWellConsideredInactive = 5;
    private readonly IWellRepository _wellRepository;
    private readonly IMapper _mapper;

    public WellService(IWellRepository wellRepository, IMapper mapper)
    {
        _wellRepository = wellRepository;
        _mapper = mapper;
    }

    public async Task<List<WellDepthProgressDto>> GetActiveWellsDepthProgressByCompany(int companyId, DateTime start, DateTime end)
    {
        var wellDepthProgressDtos = await _wellRepository.GetActiveWellsDepthProgressByCompany(companyId, start, end);

        return wellDepthProgressDtos;
    }

    public async Task<List<DetailedWellDto>> GetDetailedActiveWells()
    {
        var detailedActiveWells = await _wellRepository.GetDetailedActiveWells();

        var detailedActiveWellsDtos = _mapper.Map<List<DetailedWellDto>>(detailedActiveWells);

        return detailedActiveWellsDtos;
    }

    public async Task<WellDepthProgressDto> GetWellDepthProgress(int wellId, DateTime start, DateTime end)
    {
        var depthProgress = await _wellRepository.GetWellDepthProgress(wellId, start, end);

        var wellDepthProgressDto = new WellDepthProgressDto
        {
            WellId = wellId,
            DepthProgress = depthProgress
        };

        return wellDepthProgressDto;
    }

    public async Task<List<Well>> DeactivateInactiveWells()
    {
        var wellsToDeactivate = await _wellRepository.GetWellsWithoutActivity(DaysWellConsideredInactive);

		DeactivateWells(wellsToDeactivate);

        await _wellRepository.UpdateWells(wellsToDeactivate);

        return wellsToDeactivate;
    }

	public List<Well> DeactivateWells(List<Well> wells){
        foreach (var well in wells)
        {
            well.Active = 0;
        }

		return wells;
	}
}
