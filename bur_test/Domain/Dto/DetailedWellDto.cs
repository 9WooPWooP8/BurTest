namespace bur_test.Domain.Dto;

public class DetailedWellDto
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public int Active { get; set; }
    public float TelemetryDepth {get; set;}
    public DateTime TelemetryDateTime { get; set; }
    public string? CompanyName { get; set; }
}
