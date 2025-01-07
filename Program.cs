using Asp.Versioning;
using Asp.Versioning.Routing;
using LojaAPI.Context;
using LojaAPI.DTOs.Mappings;
using LojaAPI.Extensions;
using LojaAPI.Filters;
using LojaAPI.Logging;
using LojaAPI.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;

namespace LojaAPI;

public class Program
{

    public static void Main(string[] args)
    { 


        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers(options =>
        {
            options.Filters.Add(typeof(ApiExceptionFilter));
        })
        .AddJsonOptions(options =>
        {
            options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        }).AddNewtonsoftJson();

        builder.Services.AddApiVersioning(options =>
        {
            options.ReportApiVersions = true;
            options.DefaultApiVersion = new ApiVersion(1, 0);
            options.AssumeDefaultVersionWhenUnspecified = true;
        });

        builder.Services.Configure<RouteOptions>(options =>
        {
            options.ConstraintMap["apiVersion"] = typeof(ApiVersionRouteConstraint);
        });

        builder.Services.AddAuthorization();
        builder.Services.AddAuthentication();

        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        //builder.Services.AddIdentity<ApplicationUser>()
               //.AddEntityFrameworkStores<AppDbContext>();

        

        string SqlConnection = builder.Configuration.GetConnectionString("DefaultConnection");

        builder.Services.AddDbContext<AppDbContext>(options =>
        {
            options.UseSqlServer(SqlConnection,
            sqlOptions => sqlOptions.EnableRetryOnFailure());
        });

       
        builder.Services.AddScoped<ApiLogginFilter>();
        builder.Services.AddScoped<IVendedorRepository, VendedorRepository>();
        builder.Services.AddScoped<IProdutoRepository, ProdutoRepository>();
        builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
        builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
        builder.Services.AddAutoMapper(typeof(ProdutoDtoMappingProfile));

       

        builder.Logging.AddProvider(new CustomLoggerProvider(new CustomLoggerProviderConfiguration
        {
            LogLevel = LogLevel.Information
        }));


        

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
            app.ConfigureExceptionHandler();
        }

        app.UseHttpsRedirection();
        app.UseAuthentication();
        app.UseAuthorization();

        app.Use(async (context, next) =>
        {
            await next(context);
        });

        app.MapControllers();

        app.Run();
    }
}
