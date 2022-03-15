using GerenciadorCursos.DataAcessRepo.Context;
using GerenciadorCursos.DataAcessRepo.Repository;
using GerenciadorCursos.DataAcessRepo.RepositoryInterfaces;
using GerenciadorCursos.DataAcessRepo.UnitOfWork;
using GerenciadorCursos.DomainCore.Models;
using GerenciadorCursos.ServicesExtensions;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using System;

namespace GerenciadorCursos
{
    public class Startup
    {
        private static readonly ILoggerFactory _logger = LoggerFactory.Create(o => o.AddConsole());

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddControllers();
            services.SwaggerService(); //Extension method!
            services.AuthenticationService(Configuration); //Extension method!
            services.AddDbContext<RepositoryContext>(
                opt => opt.UseLoggerFactory(_logger)
                          .EnableSensitiveDataLogging()
                          .UseSqlServer(Configuration.GetConnectionString("Default"),
                          o => o.EnableRetryOnFailure(maxRetryCount: 2, maxRetryDelay: TimeSpan.FromSeconds(5), errorNumbersToAdd: null)
            ));

            services.AddAuthorization(options => {
                options.AddPolicy("Gerencia", policy => policy.RequireRole("Gerencia"));
                options.AddPolicy("Secretaria", policy => policy.RequireRole("Secretaria"));
                options.AddPolicy("Aluno", policy => policy.RequireRole("Aluno"));
            }
            );

            services.AddTransient<IGenericRepositoryBase<CursoModel>, GenericRepositoryBase<CursoModel>>();
            services.AddTransient<IGenericRepositoryBase<UserModel>, GenericRepositoryBase<UserModel>>();
            services.AddTransient<IUnitOfWork, UnitOfWork>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(
                    c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GerenciadorCustos Squadra")
                );
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(
                endpoints => {
                    endpoints.MapControllers();
                }
            );
        }
    }
}