using AutoMapper;
using bur_test.Domain.Interfaces;
using bur_test.Data.Models;
using bur_test.Domain.Dto;
using Microsoft.EntityFrameworkCore;

namespace bur_test.Repository;

public class TelemetryRepository : ITelemetryRepository
{
    private readonly BurDbContext _context;
    private readonly IMapper _mapper;

    public TelemetryRepository(BurDbContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<Telemetry>> AddTelemetry(List<TelemetryDto> telemetryDtos)
    {
        var telemetryRecords = _mapper.Map<List<Telemetry>>(telemetryDtos);

		foreach(var telemetry in telemetryRecords)
		{
			_context.Entry(telemetry).State = telemetry.Id == 0 ?
				EntityState.Added :
				EntityState.Modified;
			
			await _context.SaveChangesAsync();
		}


        foreach (var telemetryRecord in telemetryRecords)
        {
			var well = telemetryRecord.Well;

			if (well is null)
				continue;

            var telemetryHistory = new TelemetryHistory
            {
                DateTime = telemetryRecord.DateTime,
                TelemetryId = telemetryRecord.Id,
                Depth = telemetryRecord.Depth,
                WellId = well.Id
            };

            _context.Add(telemetryHistory);
        }

        await _context.SaveChangesAsync();

		telemetryRecords = await _context.Telemetry
			.Where(x => telemetryRecords.Any(tr => tr == x))
			.Include(t => t.Well)
			.ThenInclude(x => x.Company)
			.ToListAsync();

        return telemetryRecords;
    }

    public async Task<List<Telemetry>> GetTelemetry()
    {
		var telemetry = await _context.Telemetry
			.Include(t => t.Well)
			.ThenInclude(x => x.Company)
			.ToListAsync();

        return telemetry;
    }
}
