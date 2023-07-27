using System.ComponentModel.DataAnnotations.Schema;

namespace bur_test.Data.Models;

[Table("t_well")]
public class Well
{
    [Column("id")]
    public int Id { get; set; }
    [Column("name")]
    public string? Name { get; set; }
    [Column("active")]
    public int Active { get; set; }

    [Column("id_telemetry")]
    [ForeignKey("Telemetry")]
    public int TelemetryId { get; set; }
    public Telemetry? Telemetry { get; set; }

    [Column("id_company")]
    public int CompanyId { get; set; }
    public Company? Company { get; set; }

    public List<TelemetryHistory> TelemetryHistory { get; set; } = new List<TelemetryHistory>();
}
