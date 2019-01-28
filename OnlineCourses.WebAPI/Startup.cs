using System.Collections.Generic;
using System.Reflection;
using Courses.Application.Courses.Queries;
using Courses.Infrastructure;
using Courses.Persistence;
using Courses.WebAPI.SwaggerFilters;
using IdentityServer4.AccessTokenValidation;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Courses.WebAPI
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
            services.AddAuthentication(IdentityServerAuthenticationDefaults.AuthenticationScheme)
                .AddIdentityServerAuthentication(options =>
                {
                    options.Authority = "http://localhost:5000"; // auth server base endpoint (will use to search for disco doc)
                    options.ApiName = "demo_api"; // required audience of access tokens
                    options.RequireHttpsMetadata = false; // dev only!
                });

            services.AddScoped<IFileService, FileService>();

            services.AddMediatR(typeof(GetCoursePreviewQueryHandler).GetTypeInfo().Assembly);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info
                {
                    Title = "Courses API",
                    Version = "v1"
                });

                c.AddSecurityDefinition("oauth2", new OAuth2Scheme
                {
                    Flow = "implicit", // just get token via browser (suitable for swagger SPA)
                    AuthorizationUrl = "http://localhost:5000/connect/authorize",
                    Scopes = new Dictionary<string, string> { { "demo_api", "Demo API - full access" } }
                });

                c.OperationFilter<FileUploadOperation>();
            });

            services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("CoursesDatabase")));

            services.AddMvc()
                .SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
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
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
            app.UseSwaggerUI(c =>
            {
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "My API V1");

                c.OAuthClientId("demo_api_swagger");
                c.OAuthAppName("Demo API - Swagger"); 
            });

            app.UseStaticFiles(); // For the wwwroot folder
        }
    }
}
