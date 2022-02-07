using GringottBank.DataAccess.EF;
using GringottBank.DataAccess.Service.Abstractions;
using GringottBank.DataAccess.Service.Concrete;
using GringottsBank.BusinessLogic.Service;
using GringottsBank.Service.JWT;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.OpenApi.Models;
using Prometheus;
using Serilog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace GringottsBank.Service
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
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "GringottsBank.Service", Version = "v1" });
                c.AddSecurityDefinition("Bearer", new Microsoft.OpenApi.Models.OpenApiSecurityScheme
                {
                    Name = "Authorization",
                    Type = Microsoft.OpenApi.Models.SecuritySchemeType.Http,
                    Scheme = "Bearer",
                    BearerFormat = "JWT",
                    In = Microsoft.OpenApi.Models.ParameterLocation.Header,
                    Description = "JWT Authorization header using the Bearer scheme."
                });
                c.AddSecurityRequirement(new Microsoft.OpenApi.Models.OpenApiSecurityRequirement {
                {
                    new Microsoft.OpenApi.Models.OpenApiSecurityScheme {
                    Reference = new Microsoft.OpenApi.Models.OpenApiReference {
                        Type = Microsoft.OpenApi.Models.ReferenceType.SecurityScheme,
                            Id = "Bearer"
                    }
                },
                    new string[] {}
                }
                });
            });
            services.AddDbContext<BankDBContext>(options =>
            {
                //for configuring runtime
                options.UseSqlServer(Configuration["GringottsBankDB"], sqlOptions =>
                {
                    sqlOptions.MigrationsAssembly("GringottBank.DataAccess.EF");
                });

                //for generation of migrations
                //options.UseSqlServer(Configuration.GetConnectionString("GringottsBankDB"), sqlOptions =>
                //{
                //    sqlOptions.MigrationsAssembly("GringottBank.DataAccess.EF");
                //});

            });

            //Inject Repository
            services.AddScoped<IGringottBankUnitOfWork, GringottBankUnitOfWork>();

            //Inject Services
            services.AddTransient<ITransactionService, TransactionService>();
            services.AddTransient<IAccountService,AccountService>();
            services.AddTransient<ICustomerService,CustomerService>();

            //Inject AutoMapper
            services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

            //Inject JwtToken Service
            services.AddJWTTokenServices(Configuration);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {

            app.UseSerilogRequestLogging();
            app.UseMetricServer();
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            app.UseSwagger();
            app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "GringottsBank.Service v1"));

            app.UseHttpsRedirection();

            app.UseRouting();
            app.UseHttpMetrics();

            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });

            

            ApplyDBMigrations(app);
        }

        private void ApplyDBMigrations(IApplicationBuilder app)
        {
            using var scope=app.ApplicationServices.CreateScope();
            var services=scope.ServiceProvider;

            var dbContext=services.GetRequiredService<BankDBContext>();
            if (dbContext.Database.GetPendingMigrations().Any())
                dbContext.Database.Migrate();
        }
    }
}
