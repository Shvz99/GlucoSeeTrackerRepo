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
            //Googled
            services.AddControllersWithViews();

            /*// Add session services
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromMinutes(30); // Set session timeout
                options.Cookie.HttpOnly = true; // Makes the cookie inaccessible to JavaScript
                options.Cookie.IsEssential = true; // Ensure the cookie is included in requests
            });*/
        }

        //Googled
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseSession(); // Enable session middleware
            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }

    }
}
