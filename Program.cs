using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using KiwiTracker.API.Data;
using KiwiTracker.API.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// DATABASE CONNECTION
var databaseUrl = Environment.GetEnvironmentVariable("DATABASE_URL");
string connectionString;

// FORCE PUBLIC URL (temporary fix)
Console.WriteLine(" Using HARDCODED public database URL");
connectionString = "Host=shuttle.proxy.rlwy.net;Port=59015;Database=railway;Username=postgres;Password=SDcVqugRuVEDlJUtzsMPpHgXnlaUBYn;SSL Mode=Require;Trust Server Certificate=true";

Console.WriteLine($" Connection: shuttle.proxy.rlwy.net:59015");

if (string.IsNullOrEmpty(connectionString))
{
    throw new InvalidOperationException(" No database connection found!");
}


builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(connectionString));

// SERVICES
builder.Services.AddScoped<AuthService>();
builder.Services.AddScoped<IGoalService, GoalService>();

// JWT
var jwtKey = Environment.GetEnvironmentVariable("Jwt__Key") ?? builder.Configuration["Jwt:Key"];
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = "KiwiTrackerAPI",
            ValidAudience = "KiwiTrackerClient",
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(jwtKey))
        };
    });

builder.Services.AddAuthorization();

// SWAGGER
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        Type = SecuritySchemeType.Http,
        Scheme = "Bearer",
        In = ParameterLocation.Header
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme { Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Bearer" } },
            new string[] {}
        }
    });
});

var app = builder.Build();

// MIGRATIONS
using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    context.Database.Migrate();
}

// MIDDLEWARE
app.UseSwagger();
app.UseSwaggerUI(c => c.RoutePrefix = string.Empty);
app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();
