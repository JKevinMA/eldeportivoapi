using ElDeportivoAPI.Repository;
using ElDeportivoAPI.Repository.IRepository;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ElDeportivoAPI
{
    public class Startup
    {
        readonly string MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddScoped<ICategoriaRepository, CategoriaRepository>();
            services.AddScoped<IMaterialRepository, MaterialRepository>();
            services.AddScoped<IOrdenRepository, OrdenRepository>();
            services.AddScoped<IOrdenCompraRepository, OrdenCompraRepository>();
            services.AddScoped<IProveedorRepository, ProveedorRepository>();
            services.AddScoped<ISolicitudCotizacionRepository, SolicitudCotizacionRepository>();
            services.AddScoped<ITrabajadorRepository, TrabajadorRepository>();
            services.AddScoped<IOrdenPedidoRepository, OrdenPedidoRepository>();
            services.AddScoped<ITransportistaRepository, TransportistaRepository>();
            services.AddScoped<IDistritoRepository, DistritoRepository>();
            services.AddScoped<IGuiaRemisionRepository, GuiaRemisionRepository>();
            services.AddScoped<IDespachoRepository, DespachoRepository>();
            services.AddScoped<IOrdenPagoRepository, OrdenPagoRepository>();
            services.AddScoped<IOrdenConfeccionRepository, OrdenConfeccionRepository>();
            services.AddScoped<IFichaESRepository, FichaESRepository>();
            

            services.AddCors(options => {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  builder => {
                                      builder
                                             .AllowAnyOrigin()
                                             .AllowAnyHeader()
                                             .AllowAnyMethod();
                                  }
                    );
            });
            services.AddControllers();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseRouting();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
