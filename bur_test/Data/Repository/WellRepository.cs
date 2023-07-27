using bur_test.Domain.Interfaces;
using Microsoft.EntityFrameworkCore;
using bur_test.Data.Models;
using bur_test.Domain.Dto;

namespace bur_test.Repository;

public class WellRepository : IWellRepository
{
    private readonly BurDbContext _context;

    public WellRepository(BurDbContext context)
    {
        _context = context;
    }

    public async Task<List<Well>> GetActiveWellsByCompanyName(string companyName)
    {
        var wells = await _context.Wells
            .Where(w => w.Active == 1)
            .Where(w => w.Company != null && w.Company.Name.Contains(companyName))
            .ToListAsync();

        return wells;
    }

    public async Task<Well> GetActiveWellById(int id)
    {
        var well = await _context.Wells
            .Where(w => w.Active == 1)
            .Where(w => w.Id == id)
            .FirstOrDefaultAsync();

        return well;
    }

    public async Task<List<Well>> GetWellsByCompanyName(string companyName)
    {
        var wells = await _context.Wells
            .Where(w => w.Company != null && w.Company.Name.Contains(companyName))
            .ToListAsync();

        return wells;
    }

    public async Task<Well> GetWellById(int id)
    {
        var well = await _context.Wells
            .Where(w => w.Id == id)
            .FirstOrDefaultAsync();

        return well;
    }

    public async Task<float> GetWellDepthProgress(int id, DateTime start, DateTime end)
    {
        var well = await _context.Wells
            .Where(w => w.Id == id)
            .Include(w => w.TelemetryHistory)
            .FirstOrDefaultAsync();

        if (well == null)
            return 0;

        var wellHistory = well.TelemetryHistory
            .Where(th => th.DateTime > start && th.DateTime < end)
            .OrderBy(th => th.DateTime);

        var wellHistoryCount = wellHistory.Count();

        if (wellHistoryCount < 2)
            return 0;

        var wellDepthProgress = wellHistory.Last().Depth - wellHistory.First().Depth;

        return wellDepthProgress;
    }

    public async Task<List<Well>> GetWellsWithoutActivity(int daysToBeConsideredInactive)
    {
        var now = DateTime.UtcNow;
        var activeWells = await _context.Wells
            .Where(w => w.Active == 1)
            .Include(w => w.TelemetryHistory)
			.Where(w => w.TelemetryHistory.Count > 0)
            .ToListAsync();

        var inactveWells = activeWells
            .Where(x => (now - x.TelemetryHistory.Max(x => x.DateTime)).Days > daysToBeConsideredInactive)
            .ToList();

        return inactveWells;
    }

    public async Task<List<Well>> DeactivateWells(List<Well> wells)
    {
        foreach (var well in wells)
        {
            well.Active = 0;
        }

        _context.UpdateRange(wells);
        await _context.SaveChangesAsync();

        return wells;
    }

    public async Task<Well> GetWellByTelemetryId(int telemetryId)
    {
        var well = await _context.Wells
			.Where(w => w.TelemetryId == telemetryId)
			.FirstOrDefaultAsync();

        return well;
    }

    public async Task<Well> UpdateWell(Well well)
    {
        _context.Update(well);

        await _context.SaveChangesAsync();

        return well;
    }

    public async Task<List<Well>> UpdateWells(List<Well> wells)
    {
        _context.UpdateRange(wells);

        await _context.SaveChangesAsync();

        return wells;
    }

    public async Task<List<WellDepthProgressDto>> GetActiveWellsDepthProgressByCompany(int companyId, DateTime start, DateTime end)
    {
        var activeCompanyWells = _context.Wells
            .Where(w => w.Active == 1)
            .Where(w => w.CompanyId == companyId);

        var wellsDepthProgress = new List<WellDepthProgressDto>();

        foreach (var well in activeCompanyWells)
        {
            var wellDepthProgress = await GetWellDepthProgress(well.Id, start, end);

            var wellDepthProgressDto = new WellDepthProgressDto
            {
                WellId = well.Id,
                DepthProgress = wellDepthProgress
            };

            wellsDepthProgress.Add(wellDepthProgressDto);
        }

        return wellsDepthProgress;
    }

    public async Task<List<Well>> GetDetailedActiveWells()
    {
        var wells = await _context.Wells
            .Include(x => x.Company)
            .Include(x => x.Telemetry)
            .ToListAsync();

        return wells;
    }
}
