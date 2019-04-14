using Microsoft.AspNet.OData;
using Microsoft.AspNet.OData.Extensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.OData.Edm;
using Microsoft.OData.UriParser;
using OData.Data;
using Swashbuckle.AspNetCore.Swagger;

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

            services.AddSwaggerGen(s =>
            {
                s.SwaggerDoc("v1", new Info
                {
                    Version = "v1",
                    Title = "OData API",
                    Description = "API Open Data Protocol",
                    TermsOfService = "Nenhum",
                    Contact = new Contact { Name = "Adler Pagliarini", Email = "adlerpagliarini@gmail.com", Url = "localhost" },
                    License = new License { Name = "MIT", Url = "localhost" }
                });
            });

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
                routerBuilder.EnableDependencyInjection(builder =>
                    {
                        var modelConfig = ODataEntitiesConfiguration.GetEdmModel(app.ApplicationServices);
                        builder
                        .AddService(Microsoft.OData.ServiceLifetime.Singleton, typeof(IEdmModel), sp => modelConfig)
                        .AddService(Microsoft.OData.ServiceLifetime.Singleton, typeof(ODataUriResolver), sp => new StringAsEnumResolver())
                        .AddService(Microsoft.OData.ServiceLifetime.Singleton, typeof(ODataUriResolver), sp => new UnqualifiedCallAndEnumPrefixFreeResolver { EnableCaseInsensitive = true });
                    });
                
                // Enables OData to all entities
                //routerBuilder.Select().Filter().OrderBy().Count().Expand();
            });

            app.UseSwagger();
            app.UseSwaggerUI(s =>
            {
                s.SwaggerEndpoint("/swagger/v1/swagger.json", "OData API v1.0");
            });
        }
    }
}
