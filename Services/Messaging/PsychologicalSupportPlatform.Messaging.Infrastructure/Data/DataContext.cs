using Microsoft.EntityFrameworkCore;

namespace PsychologicalSupportPlatform.Messaging.Infrastructure.Data;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {
    }
}
