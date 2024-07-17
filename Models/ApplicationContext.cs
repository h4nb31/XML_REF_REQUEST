using Microsoft.EntityFrameworkCore;

namespace REF_XML_REQUEST.Models
{
    public class ApplicationContext : DbContext
    {
        public DbSet<RequestListModel> RequestListModels { get; set; }
        public DbSet<RestConnectionsModel> RestConnectionsModels { get; set; }  

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
        :base(options)
        {
            Database.EnsureCreated();
        }
    }
}
