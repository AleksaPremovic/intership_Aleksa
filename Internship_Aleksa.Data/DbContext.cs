using Internship_Aleksa.Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Internship_Aleksa.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<Student> Students { get; set; } = null!;
    }
}
