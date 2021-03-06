using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using LanguageLearning.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Routing;
using System;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.Extensions.Options;
using System.Security.Claims;
using LanguageLearning.Models.JWT;
using LanguageLearning.Interfaces;
using LanguageLearning.Models.UserAccount;
using LanguageLearning.Models.Languages;
using LanguageLearning.Models.LanguageQuiz;

namespace LanguageLearning
{
    public static class ServiceExtensions
    {
        public static IServiceCollection RegisterServices(this IServiceCollection services)
        {
            services.AddScoped<ILoginManager, LoginManager>();
            services.AddScoped<IJwtFactory, JwtFactory>();
            services.AddScoped<IJwtValidation, JwtValidation>();            
            services.AddScoped<IHashing, Hashing>();

            services.AddScoped<IWordQuizService, WordQuizService>();
            services.AddScoped<IWordQuizDbManager, WordQuizDbManager>();

            services.AddSingleton<ILanguageOptions, LanguageOptions>();
            return services;
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
            services.AddScoped<IJwtFactory, JwtFactory>();
            services.RegisterServices();
            
            services.AddMvc();                
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseBrowserLink();
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();                
            app.UseMvc();
        }        
    }   
}
