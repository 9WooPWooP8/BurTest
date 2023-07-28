namespace BurTest.Domain.Dto;

public class DetailedTelemetryDto
{
    public int Id { get; set; }
    public string? WellCompanyName { get; set; }
    public string? WellName { get; set; }
    public int WellId { get; set; }
    public DateTime DateTime { get; set; }
    public float Depth { get; set; }
}
