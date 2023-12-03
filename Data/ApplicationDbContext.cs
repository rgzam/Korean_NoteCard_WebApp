using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Korean_NoteCard_WebApp.Models;

namespace Korean_NoteCard_WebApp.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public DbSet<Korean_NoteCard_WebApp.Models.Korean> Korean { get; set; } = default!;
    }
}
