using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using SurveyApp.Infrastructure.Services;
using oig_assessment.Infrastructure.Data;
using oig_assessment.Infrastructure.Security;
using oig_assessment.Common;
using oig_assessment.Api.Endpoints;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Infrastructure.Persistence;
using oig_assessment.Application.Common;
using MediatR;
using oig_assessment.Application.Common.Interfaces;
using oig_assessment.Application.Interfaces;
using oig_assessment.Infrastructure.Identity;

var builder = WebApplication.CreateBuilder(args);

// db context for reading
builder.Services.AddDbContext<IReadSurveyDbContext, ReadSurveyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbConnString"))
    .UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));

// db context for writing
builder.Services.AddDbContext<IWriteSurveyDbContext, WriteSurveyDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("dbConnString")));

// db context registration
builder.Services.AddScoped<IWriteSurveyDbContext, WriteSurveyDbContext>();
builder.Services.AddScoped<IReadSurveyDbContext, ReadSurveyDbContext>();

builder.Services.AddTransient<DataSeeder>();
builder.Services.AddSingleton<JwtProvider>();
builder.Services.AddScoped<IUserContext, UserContext>();

// Register services
builder.Services.AddScoped<IUserService, UserService>();

builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

builder.Services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(typeof(MappingProfiles).Assembly));


builder.Services.AddHttpContextAccessor();

builder.Services.AddTransient(typeof(IPipelineBehavior<,>), typeof(UnitOfWorkBehavior<,>));



builder.Services.AddAutoMapper(AppDomain.CurrentDomain.GetAssemblies());

builder.Services.AddSwaggerGen(options =>
{
    options.SwaggerDoc("v1", new OpenApiInfo
    {
        Title = "Survey API",
        Version = "v1",
        Description = "API for survey with authorize",
    });

    options.AddSecurityDefinition("Bearer",
        new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        BearerFormat = "JWT",
        In = ParameterLocation.Header,
        Description = "Enter token in format: Bearer {your_token}"
    });

    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});
builder.Services.AddAuthorization();

builder.Services.AddEndpointsApiExplorer();

// Login settings binding
builder.Services.Configure<LocalLoginSettings>(builder.Configuration.GetSection("LocalLoginSettings"));
LocalLoginSettings loginSettings = new LocalLoginSettings();
builder.Configuration.Bind("LocalLoginSettings", loginSettings);
builder.Services.AddSingleton(loginSettings);



var key = builder.Configuration.GetValue<string>("LocalLoginSettings:Key")
    ?? throw new Exception("JwtSettings:Key is missing in configuration"); ;

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(o =>
{   
    o.TokenValidationParameters = new TokenValidationParameters
    {
        ValidIssuer = builder.Configuration["LocalLoginSettings:Issuer"],
        ValidAudience = builder.Configuration["LocalLoginSettings:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(key)),
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = false,
        ValidateIssuerSigningKey = true,
    };
});


builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowBlazorClient",
        policy =>
        {
            policy.WithOrigins("https://localhost:7274") // Allow Blazor WebAssembly
                  .AllowAnyMethod()
                  .AllowAnyHeader()
                  .AllowCredentials();
        });
});


var app = builder.Build();
app.UseHttpsRedirection();
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.SwaggerEndpoint("/swagger/v1/swagger.json", "Survey API v1");
        options.RoutePrefix = string.Empty;
    });
}



app.UseAuthentication();
app.UseAuthorization();
app.UseCors("AllowBlazorClient");

// Auth Endpoints
app.MapAuthEndpoints();

// User Endpoints
app.MapUserEndpoints();

// Survey endpoints
app.MapSurveyEndpoints();

// Answer endpoints
app.MapSurveyAnswersEndpoints();


using (var scope = app.Services.CreateScope())
{
    var services = scope.ServiceProvider;
    var seeder = services.GetRequiredService<DataSeeder>();
    await seeder.SeedUsers();
}

app.Run();
