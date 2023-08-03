using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using PierresSweets.Models;

namespace PierresSweets
{
  public class Startup
  {
    public IConfigurationRoot Configuration { get; set; }

    public Startup(IWebHostEnvironment env)
    {
      IConfigurationBuilder builder = new ConfigurationBuilder()
        .SetBasePath(env.ContentRootPath)
        .AddJsonFile("appsettings.json");
      Configuration = builder.Build();
    }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();
      services.AddEntityFrameworkMySql()
        .AddDbContext<PierresSweetsContext>(
          options => options.UseMySql(Configuration["ConnectionStrings:DefaultConnection"],
            ServerVersion.AutoDetect(Configuration["ConnectionStrings:DefaultConnection"])));

      services.AddIdentity<ApplicationUser, IdentityRole>()
        .AddEntityFrameworkStores<PierresSweetsContext>()
        .AddDefaultTokenProviders();
      
      services.Configure<IdentityOptions>(options =>
      {
        options.Password.RequireDigit = false;
        options.Password.RequiredLength = 0;
        options.Password.RequireLowercase = false;
        options.Password.RequireNonAlphanumeric = false;
        options.Password.RequireUppercase = false;
        options.Password.RequiredUniqueChars = 0;
      });
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseDeveloperExceptionPage();
      app.UseAuthentication();
      app.UseRouting();
      app.UseAuthorization();
      app.UseStaticFiles();

      app.UseEndpoints(routes =>
      {
        routes.MapControllerRoute("default", "{controller=Home}/{action=Index}/{id?}");
      });

      app.Run(async (context) =>
      {
        await context.Response.WriteAsync("Hello, I didn't find a route!");
      });
    }
  }
}