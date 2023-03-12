using AuthenticationAndAuthorization.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Persistence.Context;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.EntityFrameworkCore;

namespace AuthenticationAndAuthorization
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {
            var connectionString = Configuration["ConnectionStrings:DbConnection"];

            services.AddDbContext<AuthenticationAndAuthorizationContext>(options =>
            {
                options.UseNpgsql(connectionString);
            });

            services.AddScoped<IAuthenticationAndAuthorizationContext>(provider => provider.GetService<AuthenticationAndAuthorizationContext>()!);

            services.AddRazorPages();
            services.AddServerSideBlazor();
            services.AddSingleton<SimulatedDataProviderService>();
            services.AddScoped<WebsiteAuthenticator>();
            services.AddScoped<AuthenticationStateProvider>(sp => sp.GetRequiredService<WebsiteAuthenticator>());
            services.AddAuthorization(config =>
            {
                config.AddPolicy("CanBuyAlcohol", policy =>
                {
                    policy.AddRequirements(new AdultRequirement());
                    policy.RequireClaim("IsPremiumMember", true.ToString());
                });

                config.AddPolicy("OverAge", policy => policy.AddRequirements(new OverAgeRequirement()));
            });
            services.AddSingleton<IAuthorizationHandler, AdultRequirementHandler>();
            services.AddSingleton<IAuthorizationHandler, OverAgeRequirementHandler>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
            }

            app.UseStaticFiles();

            app.UseRouting();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapBlazorHub();
                endpoints.MapFallbackToPage("/_Host");
            });
        }
    }
}
