using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using PhoneBook.Api.DataContext;
using PhoneBook.Api.Repositories;
using PhoneBook.Api.Repositories.Interfaces;
using PhoneBook.Api.Services;
using PhoneBook.Api.UnitOfWork.Interfaces;

namespace PhoneBook.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostEnvironment env)
        {
            Configuration = configuration;
            Environment = env;
        }

        public IConfiguration Configuration { get; }

        public IHostEnvironment Environment { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddCors(options =>
            {
                options.AddDefaultPolicy(builder =>
                {
                    builder
                        .WithOrigins("http://localhost:4200")
                        .AllowAnyMethod()
                        .AllowAnyHeader();
                });
            });


            services.AddControllers();

            if (Environment.IsDevelopment())
            {
                services.AddDbContext<PhoneBookDbContext>(option =>
                {
                    option.UseSqlite(Configuration["Connections:Sqlite"], b => b.MigrationsAssembly("api"));
                });
            }
            else
            {
                services.AddDbContext<PhoneBookDbContext>(option =>
                {
                    option.UseSqlServer(Configuration["Connections:SqlServer"], b => b.MigrationsAssembly("api"));
                });
            }

            services.AddAutoMapper(typeof(Startup));
            services.AddScoped<IPhoneBookRepository, PhoneBookRepository>();
            services.AddScoped<PhoneBookService>();
            services.AddScoped<IPhoneBookEntryRepository, PhoneBookEntryRepository>();
            services.AddScoped<PhoneBookEntryService>();
            services.AddScoped<IUnitOfWork, UnitOfWork.UnitOfWork>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "PhoneBook API", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "api v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseCors();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
