using Microsoft.EntityFrameworkCore;

namespace NoteTakerWebApp.Models
{
    public class ApplicationDbContext : DbContext 
    {
        //Connects to the DB
       public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
       {
            
       }
       
       //Creates the user and Notes tables 
       public DbSet<User> Users { get; set; }
       public DbSet<Notes> Notes { get; set; }  
        
    }
}
