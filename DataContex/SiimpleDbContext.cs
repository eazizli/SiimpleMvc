using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using SiimpleMvc.Models;

namespace SiimpleMvc.DataContex
{
    public class SiimpleDbContext:IdentityDbContext<AppUser>
    {
        public SiimpleDbContext(DbContextOptions<SiimpleDbContext> opt):base(opt)
        {

        }
        public DbSet<Card> Cards { get; set; }  
    }
}
