using System.ComponentModel.DataAnnotations.Schema;

namespace BurTest.Data.Models;

[Table("t_telemetry_history")]
public class TelemetryHistory
{
    [Column("id")]
    public int Id { get; set; }

    [Column("date_time")]
    public DateTime DateTime { get; set; }
    [Column("depth")]
    public float Depth { get; set; }

    [Column("id_well")]
    public int WellId { get; set; }
    public Well Well { get; set; } = null!;

    [Column("id_telemetry")]
    public int TelemetryId { get; set; }
    public Telemetry Telemetry { get; set; } = null!;

}
