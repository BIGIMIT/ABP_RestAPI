using Microsoft.EntityFrameworkCore;

namespace ABP_RestAPI.Models;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) 
        : base(options)
    {
    }

    public DbSet<ExperimentResult> ExperimentResults { get; set; }
}
