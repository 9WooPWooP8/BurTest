using Microsoft.AspNetCore.Mvc;
using BurTest.Domain.Interfaces;
using BurTest.Domain.Dto;
using AutoMapper;

namespace BurTest.Api.Controllers;

[ApiController]
[Route("[controller]")]
public class WellController : ControllerBase
{
    private readonly IWellRepository _wellRepository;
    private readonly IWellService _wellService;
    private readonly IMapper _mapper;


    public WellController(IWellRepository wellRepository, IMapper mapper, IWellService wellService)
    {
        _wellRepository = wellRepository;
        _mapper = mapper;
		_wellService = wellService;
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> GetWellById([FromRoute] int id)
    {
        var well = await _wellRepository.GetWellById(id);

		if (well == null)
			return NotFound();

        var wellDto = _mapper.Map<WellDto>(well);

        return Ok(wellDto);
    }

    [HttpGet()]
    public async Task<IActionResult> GetWellByCompanyName([FromQuery] string companyName)
    {
        var wells = await _wellRepository.GetWellsByCompanyName(companyName);

        var wellDtos = _mapper.Map<List<WellDto>>(wells);

        return Ok(wellDtos);
    }

    [HttpGet("active/{id:int}")]
    public async Task<IActionResult> GetActiveWellById([FromRoute] int id)
    {
        var well = await _wellRepository.GetActiveWellById(id);

		if (well == null)
			return NotFound();

        var wellDto = _mapper.Map<WellDto>(well);

        return Ok(wellDto);
    }

    [HttpGet("active")]
    public async Task<IActionResult> GetActiveWellByCompanyName([FromQuery] string companyName)
    {
        var wells = await _wellRepository.GetActiveWellsByCompanyName(companyName);

        var wellDtos = _mapper.Map<List<WellDto>>(wells);

        return Ok(wellDtos);
    }

    [HttpGet("detailed/active")]
    public async Task<IActionResult> GetDetailedActiveWells()
    {
        var wells = await _wellService.GetDetailedActiveWells();

        return Ok(wells);
    }

    [HttpGet("detailed")]
    public async Task<IActionResult> GetDetailedWells()
    {
        var wells = await _wellService.GetDetailedWells();

        return Ok(wells);
    }

    [HttpGet("{id:int}/depthProgress")]
    public async Task<IActionResult> GetWellDepthProgress(
        [FromRoute] int id,
        [FromQuery] DateTime start,
        [FromQuery] DateTime end)
    {
        var depthProgress = await _wellService.GetWellDepthProgress(id, start, end);

        return Ok(depthProgress);
    }

    [HttpGet("active/depthProgress")]
    public async Task<IActionResult> GetActiveWellDepthProgressByCompany(
        [FromQuery] int companyId,
        [FromQuery] DateTime start,
        [FromQuery] DateTime end)
    {
        var wellsDepthProgress = await _wellService.GetActiveWellsDepthProgressByCompany(companyId, start, end);

        return Ok(wellsDepthProgress);
    }
}
