using crtcprog.api.Models;
using ctrcprog.api.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace ctrcprog.api.Data
{

    public class DataContext : IdentityDbContext<ApplicationUser>
    {

        public virtual DbSet<User>? Users { get; set; }
        public virtual DbSet<ApplicationUser>? ApplicationUsers { get; set; }
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        { }
    }
}
 
