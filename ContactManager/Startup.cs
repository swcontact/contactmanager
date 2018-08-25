using ContactManager.Data;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace ContactManager
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            // CORS policy
            string corsOrigin = Configuration.GetValue<string>("CorsOrigin");
            if (corsOrigin == "*")
            {
                services.AddCors(options =>
                {
                    options.AddPolicy("AllowSpecificOrigin",
                        builder => builder.AllowAnyOrigin().AllowAnyHeader().AllowAnyMethod());
                });
            }
            else
            {
                services.AddCors(options =>
                {
                options.AddPolicy("AllowSpecificOrigin",
                    builder => builder.WithOrigins(corsOrigin).AllowAnyHeader().AllowAnyMethod());
                });

            }

            // Dependency Injection
            string dbConnection = Configuration.GetValue<string>("Database");
            services.AddDbContext<ContactContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString(dbConnection)));

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseCors("AllowSpecificOrigin");
            app.UseMvc();
        }
    }
}
