using AutoMapper;
using bur_test.Data.Models;
using bur_test.Domain.Dto;


public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<Well, WellDto>().ReverseMap();
        CreateMap<Telemetry, TelemetryDto>().ReverseMap();
        CreateMap<Telemetry, DetailedTelemetryDto>()
			.ReverseMap();
        CreateMap<Well, DetailedWellDto>().ReverseMap();
        CreateMap<Company, CompanyDto>().ReverseMap();
    }
}
