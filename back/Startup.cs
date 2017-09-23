using System;
using System.Collections.Generic;
using System.Globalization;
using Neppo.Repositories;
using Neppo.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Localization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Neppo.Contexts;
using Neppo.Repositories.Interfaces;
using Neppo.Services.Interfaces;
using FluentValidation.AspNetCore;

namespace Neppo
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
            services.AddMvc()
                .AddFluentValidation( f => f.RegisterValidatorsFromAssemblyContaining<Startup>());

            services.AddDbContext<Context>(options => options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")));
            services.AddTransient<IInitializeDBService, InitializeDBService>();
            services.AddTransient<IPessoaRepository, PessoaRepository>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env, IServiceProvider serviceProvider)
        {
            if (env.IsDevelopment()) {
                app.UseDeveloperExceptionPage();
            }

            IInitializeDBService dataService = serviceProvider.GetService<IInitializeDBService>();
            dataService.InicializeDB();

            app.UseRequestLocalization(getLocalizationConfig());

            app.UseMvc();
        }

        private RequestLocalizationOptions getLocalizationConfig() 
        {
            var locale = Configuration["SiteLocale"];
            var cultura = new CultureInfo(locale);
            cultura.NumberFormat.CurrencyPositivePattern = 2;
            cultura.NumberFormat.CurrencyNegativePattern = 2;
            cultura.DateTimeFormat.ShortDatePattern = "dd/MM/yyyy";

            RequestLocalizationOptions localizationOptions = new RequestLocalizationOptions {
                SupportedCultures = new List<CultureInfo> { cultura },
                SupportedUICultures = new List<CultureInfo> { cultura },
                DefaultRequestCulture = new RequestCulture(locale)
            };

            return localizationOptions;
        }
    }
}
