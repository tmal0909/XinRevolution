using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using XinRevolution.CloudService.AzureService;
using XinRevolution.CloudService.AzureService.Interface;
using XinRevolution.Database;
using XinRevolution.Manager.Services;
using XinRevolution.Repository;
using XinRevolution.Repository.Interface;

namespace XinRevolution.Manager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            services.AddDbContext<XinRevolutionContext>(options =>
            {
                options.UseSqlServer(Configuration.GetConnectionString("Database"));
            });

            services.AddScoped<DbContext, XinRevolutionContext>();
            services.AddScoped<IUnitOfWork<DbContext>, UnitOfWork<DbContext>>();
            services.AddScoped<IAzureBlobService, AzureBlobService>(x => new AzureBlobService(Configuration.GetConnectionString("Blob")));
            services.AddScoped<UserService>();
            services.AddScoped<IssueService>();
            services.AddScoped<IssueItemService>();
            services.AddScoped<IssueRelativeLinkService>();
            services.AddScoped<TagService>();
        }

        public void Configure(IApplicationBuilder app, XinRevolutionContext dbContext)
        {
            app.UseExceptionHandler("/Home/Error");
            app.UseHsts();
            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();
            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Home}/{action=Login}/{id?}");
            });
            dbContext.Database.EnsureCreated();
            dbContext.Database.Migrate();
        }
    }
}
