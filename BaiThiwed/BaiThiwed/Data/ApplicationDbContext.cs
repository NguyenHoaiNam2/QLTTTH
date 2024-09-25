using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BaiThiwed.Models;


namespace BaiThiwed.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        public  DbSet<User> Users { get; set; }
        public  DbSet<Teacher> Teachers { get; set; }
    }
}
