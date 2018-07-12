using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LanguageLearning.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace LanguageLearning
{
    public static class JwtSecurityKey
    {
        public static SymmetricSecurityKey Create(string secret)
        {
            return new SymmetricSecurityKey(Encoding.ASCII.GetBytes(secret));
        }
    }

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
            services.AddDbContext<WordContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("WordContext")));
            services.AddMvc();
            services.AddAntiforgery(o => o.HeaderName = "XSRF-TOKEN");

            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
              .AddJwtBearer(options => {
                  options.TokenValidationParameters =
                       new TokenValidationParameters
                       {
                           ValidateIssuer = true,
                           ValidateAudience = true,
                           ValidateLifetime = true,
                           ValidateIssuerSigningKey = true,

                           ValidIssuer = "Fiver.Security.Bearer",
                           ValidAudience = "Fiver.Security.Bearer",
                           IssuerSigningKey =
                            JwtSecurityKey.Create("fiversecret ")
                       };
              });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseMvc();
        }
    }
}
