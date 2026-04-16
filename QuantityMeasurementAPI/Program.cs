
using QuantityMeasurementAppModelLayer.Util;
using QuantityMeasurementAppBusinessLayer.Interface;
using QuantityMeasurementAppRepositoryLayer.Interface;
using QuantityMeasurementAppRepositoryLayer.Repository;
using QuantityMeasurementAppBusinessLayer.Services;
using QuantityMeasurementAppRepositoryLayer.Context;

using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.EntityFrameworkCore;


var builder = WebApplication.CreateBuilder(args);

var jwtSettings=new JwtSettings();
builder.Configuration.GetSection("Jwt").Bind(jwtSettings);
builder.Services.AddSingleton(jwtSettings);

builder.Services.AddScoped<IJwtService, JwtService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<IMeasurementService, MeasurementService>();
builder.Services.AddScoped<IUserService, UserService>();

//OLD REPOSITORY
builder.Services.AddScoped<IMeasurementHistoryRepository, MeasurementHistoryRepository>();

//NEW REPOSITORY
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IMeasurementRepository, MeasurementRepository>();
builder.Services.AddDbContext<AppDbContext>(options =>{

    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"));
});
builder.Services.AddAuthentication(options =>
{
    options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    options.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
    };
});

builder.Services.AddOpenApi();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();



// for ADO.net  
AppConfig.ConnectionString = builder.Configuration.GetConnectionString("DefaultConnection") ;

// for EF


builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.WithOrigins("http://localhost:4200", "http://localhost:4000")
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

var app = builder.Build();

app.UseCors();

using (var scope = app.Services.CreateScope())
{
    try
    {
        var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
        context.Database.Migrate();
    }
    catch (Exception ex)
    {
        Console.WriteLine($"An error occurred while migrating the database: {ex.Message}");
    }
}

// Configure the HTTP request pipeline.
// if (app.Environment.IsDevelopment())
// {
//     app.MapOpenApi();
//     app.UseSwagger();
//     app.UseSwaggerUI();
// }
    app.UseSwagger();
    app.UseSwaggerUI();

app.MapGet("/", () => "Hello World from QuantityMeasurementAPI!");
    


app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

var port = Environment.GetEnvironmentVariable("PORT") ?? "10000";
app.Run($"http://0.0.0.0:{port}");

app.Run(); 