using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using KiwiTracker.API.Data;
using KiwiTracker.API.Services;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// DATABASE CONNECTION - SSL Mode = Prefer
Console.WriteLine("🔧 Database setup...");
string connectionString = "Host=shuttle.proxy.rlwy.net;Port=59015;Database=railway;Username=postgres;Password=SDcVqugRuVEDlJUtzsMPFpHgXwlaUBYn;SSL Mode=Prefer;Trust Server Certificate=true;Pooling=true";
Console.WriteLine("✅ Using public URL with SSL Prefer");

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
    
    Console.WriteLine(" Creating database schema...");
    Console.WriteLine(" Applying migrations...");
    context.Database.Migrate();
    Console.WriteLine(" Database ready!");
    Console.WriteLine(" Database tables created!");
}


// SWAGGER
app.UseSwagger(c =>
{
    c.RouteTemplate = "swagger/{documentName}/swagger.json";
});

app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "KiwiTracker API V1");
    c.RoutePrefix = string.Empty;
});

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();
app.MapControllers();

app.Run();