using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Bakery.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Pomelo.EntityFrameworkCore.MySql.Infrastructure;
using Microsoft.EntityFrameworkCore;

namespace Bakery {
  public class Startup {
    public Startup (Microsoft.Extensions.Hosting.IHostEnvironment env) {
      var builder = new ConfigurationBuilder ()
        .SetBasePath (env.ContentRootPath)
        .AddJsonFile ("appsettings.json", optional : true, reloadOnChange : true)
        .AddJsonFile ($"appsettings.{env.EnvironmentName}.json", optional : true)
        .AddEnvironmentVariables ();
      Configuration = builder.Build ();
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices (IServiceCollection services) {

      var connectionString = Configuration.GetConnectionString ("DefaultConnection");

      services.AddDbContext<BakeryContext> (options =>
        options.UseMySql (connectionString));
      services.AddRazorPages ();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure (IApplicationBuilder app, IWebHostEnvironment env) {
      if (env.IsDevelopment ()) {
        app.UseDeveloperExceptionPage ();
      } else {
        app.UseExceptionHandler ("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts ();
      }

      //app.UseHttpsRedirection();
      app.UseStaticFiles ();

      app.UseRouting ();

      app.UseAuthorization ();

      app.UseEndpoints (endpoints => {
        endpoints.MapRazorPages ();
      });
    }
  }
}