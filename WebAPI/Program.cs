using Contracts;

using Contracts.AllRepository.DepartamentsRepository;

using Entities;
using Entities.DTO;
using Entities.Model.AnyClasses;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.FileProviders;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using Repository;
using Repository.AllSqlRepository.DepartamentsSqlRepository;
using Serilog;
using Swashbuckle.AspNetCore.SwaggerGen;
using System.Reflection;
using System.Text;
using TSTUWebAPI.Controllers.FileControllers;

try
{
    var builder = WebApplication.CreateBuilder(args);
    var Configuration = new ConfigurationBuilder()
          .AddJsonFile("appsettings.json")
          .Build();


    #region JWT
    IConfigurationSection appSettingsSection = Configuration.GetSection("AppSettings");
    builder.Services.Configure<AppSettings>(appSettingsSection);
    AppSettings appSettings = appSettingsSection.Get<AppSettings>();
    var secretKey = Encoding.ASCII.GetBytes(appSettings.SecretKey);

    builder.Services.AddAuthentication(x =>
    {
        x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    }).AddJwtBearer(x =>
    {
        x.SaveToken = true;
        x.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(secretKey),
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true
        };
    });
    #endregion

    #region Swagger

    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebAPI", Version = "v1" });


        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Description = "Example enter like this => Bearer 'token'",
            Name = "Authorization",
            In = ParameterLocation.Header,
            Type = SecuritySchemeType.ApiKey,
            Scheme = "Bearer",
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
                {
                    {
                       new OpenApiSecurityScheme
                       {
                           Reference=new OpenApiReference
                           {
                               Type = ReferenceType.SecurityScheme,
                               Id = "Bearer",
                           },
                       },
                       Array.Empty<string>()
                    },
                });
    });
    builder.Services.AddSwaggerGen();
    #endregion

    #region DB
    builder.Services.AddDbContext<RepositoryContext>(options =>
                options.UseNpgsql(Configuration.GetConnectionString("DefaultConnection")
                   ));
    #endregion

    #region RepositorysServices
    builder.Services.AddScoped<IRepositoryManager, RepositoryManager>();

   

    //Departament AND DepartamentTranslation
    builder.Services.AddScoped<IDepartamentRepository, DepartamentSqlRepository>();


    #endregion


    #region AnyServices

    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAllOrigins",
            builder =>
            {
                builder.AllowAnyOrigin()
                       .AllowAnyMethod()
                       .AllowAnyHeader();
            });
    });


    builder.Services.AddScoped<FileUploadRepository>();
    builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());
    builder.Services.AddControllers();
    //builder.Services.AddCors(opt =>
    //{
    //    opt.AddPolicy("CorsPolicy", builder =>
    //    builder.AllowAnyOrigin().AllowAnyHeader());
    //});


    builder.Services.AddControllers();
    builder.Services.AddEndpointsApiExplorer();


    #endregion


    #region Midddlware

    var logger = new LoggerConfiguration()
        .ReadFrom.Configuration(builder.Configuration)
        .Enrich.FromLogContext()
        .Enrich.WithProperty("Application", "WebAPI")
        .CreateLogger();
    builder.Logging.ClearProviders();
    builder.Logging.AddSerilog(logger);
    builder.Services.AddControllers();




    var app = builder.Build();

    if (app.Environment.IsDevelopment())
    {
        app.UseDeveloperExceptionPage();
        app.UseSwagger();
        app.UseSwaggerUI(c =>
        {
            c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebAPI v1");
        }
        );


    }

    app.UseHttpsRedirection();

    app.UseRouting();

    app.UseAuthentication();

    app.UseCors("AllowAllOrigins");
    app.UseAuthorization();

    app.MapControllers();

    var fileUploadsPath = Path.Combine(app.Environment.ContentRootPath, "file-uploads");

    if (!Directory.Exists(fileUploadsPath))
    {
        Directory.CreateDirectory(fileUploadsPath);
    }

    var fileProvider = new PhysicalFileProvider(fileUploadsPath);

    app.UseStaticFiles(new StaticFileOptions
    {
        FileProvider = fileProvider,
        RequestPath = "/file-uploads",
        OnPrepareResponse = ctx =>
        {
            var fileExtension = Path.GetExtension(ctx.File.PhysicalPath);

            if (!SessionClass.allowedExtensions.Contains(fileExtension, StringComparer.OrdinalIgnoreCase))
            {
                ctx.Context.Response.StatusCode = StatusCodes.Status403Forbidden;
                ctx.Context.Response.ContentLength = 0;
                ctx.Context.Response.Body = Stream.Null;
            }
        }
    });

    app.Run();
    #endregion

}
catch (Exception ex)
{
    Log.Fatal(ex.ToString());
    throw;
}