﻿using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;
using OData.Data;

namespace OData
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
            services.AddDbContext<DatabaseContext>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            // 1. Add o Data service
            services.AddOData();
        }

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
            app.UseMvc(routerBuilder =>
            {
                // 2. Enable Dependency Injection on current routes
                routerBuilder.EnableDependencyInjection(builder =>
                {
                    var modelConfig = ODataEntitiesConfiguration.GetEdmModel(app.ApplicationServices);
                    builder
                    .AddService(Microsoft.OData.ServiceLifetime.Singleton, typeof(IEdmModel), sp => modelConfig)
                    .AddService(Microsoft.OData.ServiceLifetime.Singleton, typeof(ODataUriResolver), sp => new StringAsEnumResolver())
                    .AddService(Microsoft.OData.ServiceLifetime.Singleton, typeof(ODataUriResolver), sp => new UnqualifiedCallAndEnumPrefixFreeResolver { EnableCaseInsensitive = true });
                });

                // 3. Enable OData operations for all entities
                //routerBuilder.Select().Filter().OrderBy().Expand().MaxTop(null);
            });
        }
    }
}
