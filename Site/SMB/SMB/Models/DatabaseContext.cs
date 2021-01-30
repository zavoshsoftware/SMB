using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
//using System.Data.Entity;
using System.Data.Entity.ModelConfiguration.Conventions;

namespace Models
{
   public class DatabaseContext:DbContext
    {
        static DatabaseContext()
        {
            System.Data.Entity.Database.SetInitializer(new MigrateDatabaseToLatestVersion<DatabaseContext, Migrations.Configuration>());
        }

        public DbSet<Role> Roles { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<BlogGroup> BlogGroups { get; set; }
        public DbSet<BlogComment> BlogComments { get; set; }
        public DbSet<Newsletter> Newsletters { get; set; }
        public DbSet<Service> Services { get; set; }
        public DbSet<TextItem> TextItems { get; set; }
        public DbSet<TextItemType> TextItemTypes { get; set; }
        public DbSet<ContactUsForm> ContactUsForms { get; set; } 
        public DbSet<ServiceComment> ServiceComments { get; set; } 
        public DbSet<Slider> Sliders { get; set; } 
        public DbSet<Team> Team { get; set; } 
        public DbSet<Testimonial> Testimonials { get; set; } 
        public DbSet<Client> Clients { get; set; }

        public System.Data.Entity.DbSet<Models.ServiceGroup> ServiceGroups { get; set; }
    }
}
