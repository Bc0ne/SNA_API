namespace SocialNetworkAnalyser.API
{
    using Autofac;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Http.Features;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using SocialNetworkAnalyser.API.Bootstraper;
    using SocialNetworkAnaylser.Data.Context;

    public class Startup
    {
        private const string AllowedSpecificOrigins = "_myAllowSpecificOrigins";

        public Startup(IConfiguration configuration, IHostingEnvironment hostingEnvironment)
        {
            Configuration = configuration;
            HostingEnvironment = hostingEnvironment;
        }

        public IConfiguration Configuration { get; }
        public IHostingEnvironment HostingEnvironment { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddCors(options =>
            {
                options.AddPolicy(AllowedSpecificOrigins,
                builder =>
                {
                    builder
                    .AllowAnyHeader()
                    .AllowAnyMethod()
                    .AllowAnyOrigin();
                });
            });

            services.Configure<FormOptions>(x => x.ValueLengthLimit = int.MaxValue);

            var connection = Configuration["ConnectionStrings:AnalyserConnectionString"];

            services.AddDbContext<SocialNetworkAnalyserContext>(options => options.UseSqlServer(connection));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            using(var serviceScope = app.ApplicationServices.GetRequiredService<IServiceScopeFactory>().CreateScope())
            {
                var contextService = serviceScope.ServiceProvider.GetService<SocialNetworkAnalyserContext>();
                if (contextService.AllMigrationsApplied())
                {
                    contextService.EnsureMigrated();
                }
            }

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
            app.UseCors(AllowedSpecificOrigins);
            app.UseMvc();
        }

        public void ConfigureContainer(ContainerBuilder builder) =>
            builder.RegisterModule(new DependencyResolver(HostingEnvironment));
    }
}
