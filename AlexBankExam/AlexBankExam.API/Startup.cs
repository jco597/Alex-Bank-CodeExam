using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authentication.Cookies;
using AlexBankExam.Persistence;
using AlexBankExam.API.Services;
using AlexBankExam.API.Services.DataTxn;
using MediatR;

namespace AlexBankExam.API
{
    public class Startup
    {
        public readonly IConfiguration _config;

        public Startup(IConfiguration config)
        {
            _config = config;
        }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "AlexBankExam.API", Version = "v1" });
            });

            services.AddDbContext<DataContext>(option => { option.UseSqlServer(_config.GetConnectionString("DefaultDbConnection")); });

            services.ConfigureCors(_config);

            services.AddMediatR(typeof(DataList.Handler));
            services.AddMediatR(typeof(DataFind.Handler));
            services.AddMediatR(typeof(DataCreate.Handler));

            services.AddScoped<IDataAccessService, DataAccessService>();
            services.ConfigureAuthenticationJwtBearer(_config);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "AlexBankExam.API v1"));
            }

            app.UseRouting();
            app.UseCors("ApiCORSPolicy");
            app.UseAuthentication();
            app.UseAuthorization();

            app.UseEndpoints(endpoints => {
                endpoints.MapControllers();
            });
        }
    }
}
