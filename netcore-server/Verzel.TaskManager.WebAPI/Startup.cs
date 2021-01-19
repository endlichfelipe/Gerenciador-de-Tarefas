using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Verzel.TaskManager.WebAPI.Authentication;
using Verzel.TaskManager.WebAPI.Database;
using Verzel.TaskManager.WebAPI.DTO;

namespace Verzel.TaskManager.WebAPI
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
            services.AddControllers();
            services.AddCors();

            // Strongly typed settings object
            services.Configure<JwtSettings>(Configuration.GetSection("Authentication:JwtBearer"));

            // Configure mapper
            services.RegisterMapper();

            services.AddScoped<ITokenService, TokenService>();

            ConfigureDatabase(services);
            ConfigureAuthentication(services);
        }

        public virtual void ConfigureDatabase(IServiceCollection services)
        {
            // Configure database
            services.AddDbContext<ApiContext>(opt => opt.UseInMemoryDatabase("apiDB"))
                .SeedDatabase();
        }

        public virtual void ConfigureAuthentication(IServiceCollection services)
        {
            var signInKey = Configuration.GetValue<string>("Authentication:JwtBearer:SignInKey");
            var issuer = Configuration.GetValue<string>("Authentication:JwtBearer:Issuer");
            var audience = Configuration.GetValue<string>("Authentication:JwtBearer:Audience");

            var key = Encoding.ASCII.GetBytes(signInKey);
            
            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters()
                {
                    ValidateActor = true,
                    ValidateLifetime = true,
                    ClockSkew = TimeSpan.Zero,

                    ValidIssuer = issuer,

                    ValidateAudience = true,
                    ValidAudience = audience,

                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(key)
                };
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {   
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseCors(opt => 
            {
                opt.AllowAnyOrigin();
                opt.AllowAnyMethod();
                opt.AllowAnyHeader();
            });

            //app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
