using System.ComponentModel.DataAnnotations.Schema;

namespace bur_test.Data.Models;

[Table("t_telemetry")]
public class Telemetry
{
    [Column("id")]
    public int Id { get; set; }
    [Column("date_time")]
    public DateTime DateTime { get; set; }
    [Column("depth")]
    public float Depth { get; set; }

    public Well? Well { get; set; }
}
