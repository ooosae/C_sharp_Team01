using Microsoft.OpenApi.Models;
using rush01.WeatherClient;
using Swashbuckle.AspNetCore.Swagger;

namespace rush01;

public class Startup
{
    public IConfiguration Configuration { get; }

    public Startup(IConfiguration configuration)
    {
        Configuration = configuration;
    }

    public void ConfigureServices(IServiceCollection services)
    {
        services.AddAuthorization();
        services.AddHttpClient();
        services.Configure<ServiceSettings>(Configuration.GetSection("ServiceSettings"));
        services.AddScoped<WeatherClient.WeatherClient>();
        services.AddMemoryCache();

        services.AddSwaggerGen(c =>
        {
            c.SwaggerDoc("v1", new OpenApiInfo { Title = "Your API", Version = "v1" });
        });

        services.AddOptions();
        services.Configure<SwaggerOptions>(Configuration.GetSection("Swagger"));
        services.AddControllers();
    }

    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
        if (env.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();
        app.UseAuthorization();

        app.UseRouting();

        app.UseEndpoints(endpoints =>
        {
            endpoints.MapControllers();
        });
    }
}