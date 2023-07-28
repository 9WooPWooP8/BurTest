using System.ComponentModel.DataAnnotations.Schema;

namespace BurTest.Data.Models;

[Table("t_company")]
public class Company
{
	[Column("id")]
    public int Id { get; set; }
	[Column("name")]
    public string? Name { get; set; }
}
