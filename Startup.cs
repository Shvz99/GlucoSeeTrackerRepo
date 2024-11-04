namespace GlucoSeeTracker
{
    using Microsoft.Extensions.Hosting;
    using Microsoft.EntityFrameworkCore;
    using GlucoSeeTracker.Models;
    
    //Dependency Injection
    public class Startup
    {
        public Startup(IConfiguration configuration) 
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services) 
        {
            services.AddDbContext<GlucoSeeContext>(options => 
            options.UseSqlServer(Configuration.GetConnectionString("GlucoSeeContext")));
        }
    }
}
