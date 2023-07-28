namespace BurTest.Domain.Dto;

public class WellDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Active { get; set; }
	
    public int TelemetryId { get; set; }

    public int CompanyId { get; set; }
}
