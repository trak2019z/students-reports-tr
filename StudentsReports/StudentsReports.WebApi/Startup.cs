using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using StudentsReports.Domain.IRepositories;
using StudentsReports.Domain.Models;
using StudentsReports.Domain.Models.Identity;
using StudentsReports.Domain.Repositories;
using StudentsReports.WebApi.Mappings;

namespace StudentsReports.WebApi
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
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            services.AddDbContext<IdentityStudentsReportsContext>(options =>
              options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"))
           );

            services.AddDbContext<IdentityStudentsReportsContext>(options =>
             options.UseSqlServer(
                Configuration.GetConnectionString("DefaultConnection"))
                );

            services.AddIdentity<Users, IdentityRole>()
                .AddEntityFrameworkStores<IdentityStudentsReportsContext>()
                .AddDefaultTokenProviders();

            services.AddMvc();
            
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = true,
                        ValidateAudience = true,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = Configuration["Jwt:Issuer"],
                        ValidAudience = Configuration["Jwt:Issuer"],
                        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["Jwt:Key"]))
                    };
                });

            var config = new AutoMapper.MapperConfiguration(c =>
            {
                c.AddProfile(new MappingProfile());
            });

            var mapper = config.CreateMapper();
            services.AddSingleton(mapper);

            var context = new StudentsReportsContext();
            services.AddSingleton(context);

            #region Repositories
            services.AddTransient<IUsersRepository, UsersRepository>();
            services.AddTransient<IRolesRepository, RolesRepository>();
            services.AddTransient<ICoursesRepository, CoursesRepository>();
            #endregion
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseStatusCodePages();
            app.UseDeveloperExceptionPage();
            app.UseStaticFiles();
            app.UseAuthentication();
            app.UseMvcWithDefaultRoute();

            using (var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var roleManager = serviceScope.ServiceProvider.GetService<RoleManager<IdentityRole>>();
                var userManager = serviceScope.ServiceProvider.GetService<UserManager<Users>>();
           
                DbInitializer.Seed(userManager, roleManager).GetAwaiter().GetResult();
            }
        }
    }
}
