using Microsoft.EntityFrameworkCore;

namespace BurTest.Data.Models;

public class BurDbContext : DbContext
{
    public DbSet<Well> Wells { get; set; }
    public DbSet<Company> Companies { get; set; }
    public DbSet<Telemetry> Telemetry { get; set; }
    public DbSet<TelemetryHistory> TelemetryHistory { get; set; }

    public string DbPath { get; }

    public BurDbContext()
    {
        var folder = Environment.SpecialFolder.Startup;
        var path = Environment.GetFolderPath(folder);
        DbPath = System.IO.Path.Join(path, "bur.db");
    }

	public BurDbContext(DbContextOptions<BurDbContext> options)
    : base(options)
	{

	}

    /* protected override void OnConfiguring(DbContextOptionsBuilder options) */
    /*     => options.UseSqlite($"Data Source={DbPath}"); */
}

